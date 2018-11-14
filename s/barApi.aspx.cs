using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


public partial class barApi : System.Web.UI.Page
{

    private dbMgrBar _dbMgr = new dbMgrBar();

    protected void Page_Load(object sender, EventArgs e)
    {
        
        _dbMgr.connDB();
        handleActive();
    }

    private void handleActive()
    {
        var active = Request["active"];
        response rep = new response();
        rep.active = active;
        switch (active)
        {
            //Bartender
            case "createBarData":
                {
                    string guid = getShortGUID();
                    _dbMgr.addBarData(guid);
                    rep.result = true;
                    rep.data = guid;
                    break;
                }
            case "updateBarData":
                {
                    var barData = JsonConvert.DeserializeObject<barData>(Request["barData"]);
                    _dbMgr.updateBarData(Request["guid"], barData);
                    rep.result = true;
                    break;
                }
            case "uploadBarLiquor":
                {
                    string uKey = Request["guid"];
                    try
                    {
                        var fileSet = Request.Files;
                        var file = fileSet["liquor"];
                        var path = Path.Combine(Server.MapPath("~/s/liquor"), uKey + ".png");
                        file.SaveAs(path);
                        rep.result = true;
                    }
                    catch (Exception e)
                    {
                        rep.msg = e.Message;
                        rep.result = false;
                    }
                    break;
                }
            case "getBarQuestion":
                {
                    barQuestion qData = _dbMgr.getBarQuestion();
                    if(qData != null)
                    {
                        rep.result = true;
                        rep.data = qData;
                    }
                    else
                    {
                        rep.result = false;
                        rep.msg = "data is null";
                    }                    
                    break;
                }
            case "getBarSetting":
                {
                    configData barQRT = _dbMgr.getConfigData(configData.ConfigMap["BarQRShowT"]);
                    bartenderSettingcs setting = new bartenderSettingcs();
                    setting.qrDisplaySecond = barQRT.value_1;
                    rep.result = true;
                    rep.data = setting;                    
                    break;
                }

            //Bar
            case "getBarLiquorDisplay":
                {
                    List<barData> barList = new List<barData>();
                    _dbMgr.getBarLiquorDisplay(ref barList);
                    if(barList.Count < 16)
                    {
                        int addNum = (16 - barList.Count);
                        parameter.loadDefaultLiquor(Server.MapPath("~/s/defaultLiquor.json"));
                        for(int i = 0; i < addNum; i++)
                        {
                            barList.Add(parameter._barDefault[i]);
                        }
                    }

                    rep.result = true;
                    rep.data = barList;
                    break;
                }
            case "getNewBarLiquor":
                {
                    barData data = new barData();
                    _dbMgr.getNewBarLiquor(ref data);


                    rep.result = true;
                    rep.data = data;
                    break;
                }
            case "addLike":
                {
                    string uKey = Request["guid"];
                    int likeCount = _dbMgr.setBarLiquorLike(uKey);

                    rep.result = true;
                    rep.data = likeCount;
                    break;
                }
            //Mobile
            case "setLiquorNickname":
                {
                    var uKey = Request["guid"];
                    var nickName = Request["nickname"];
                    _dbMgr.setBarLiquorNickname(uKey, nickName);
                    checkShare(uKey, nickName);
                    rep.result = true;
                    
                    break;
                }
            case "addBarMobileData":
                {
                    var uKey = Request["guid"];
                    var name = Request["userName"];
                    var phone = Request["mobile"];

                    _dbMgr.addBarMobile(uKey, name, phone);
                    rep.result = true;
                    break;
                }
            case "getResultMsg":
                {
                    var msg = _dbMgr.getConfigData(configData.ConfigMap["BarMobileMsg"]);
                    rep.result = true;
                    rep.data = msg.value_3;
                    break;
                }
            default:
                {
                    rep.result = false;
                    rep.msg = "Unknow active";
                    break;
                }
        }

        var repJson = JsonConvert.SerializeObject(rep);
        Response.Write(repJson);
    }

    private string getShortGUID()
    {
        string guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        guid = guid.Replace("/", "_").Replace("+", "-").Substring(0, 22);
        return guid;
    }

    private bool checkShare(string uKey, string nickame)
    {
        bool result = true;
        if (!File.Exists(Server.MapPath("~/s/barShareImg/" + uKey + ".jpg")))
        {
            barData data = _dbMgr.getBarLiquorData(uKey);
            if(data != null)
            {
                string shareStr = getShareMsg(ref data);
                createImage(uKey, shareStr, nickame);
                createSharePage(uKey);
            }
            
        }
        return result;
    }

    private void createImage(string uKey, string shareStr, string nickname)
    {
        Bitmap bg = (Bitmap)System.Drawing.Image.FromFile(Server.MapPath("~/s/assets/bar_ShareBG.png"));
        Bitmap cup = (Bitmap)System.Drawing.Image.FromFile(Server.MapPath("~/s/liquor/" + uKey + ".png"));
        Font font = new Font("Noto Sans CJK TC Regular", 31, FontStyle.Regular);
        Font font2 = new Font("Noto Sans CJK TC Regular", 12, FontStyle.Regular);

        SolidBrush brush = new SolidBrush(Color.White);
        RectangleF rect = new RectangleF(412, 123, 714, 332);
        
        using (Graphics gfx = Graphics.FromImage(bg))
        {
            gfx.Flush();
            gfx.DrawString(shareStr, font, brush, rect);
            gfx.DrawImage(cup, -8, 38, 413, 566);

            gfx.TranslateTransform(bg.Width * 0.5f, bg.Height * 0.5f);
            gfx.RotateTransform(-5);
            SizeF textSize = gfx.MeasureString(nickname, font2);
            gfx.DrawString(nickname, font2, brush, -408 - (textSize.Width * 0.5f), 13 - (textSize.Height * 0.5f));
            
        }

        var path = Server.MapPath("~/s/barShareImg/" + uKey + ".jpg");
        bg.Save(Server.MapPath("~/s/barShareImg/" + uKey + ".jpg"), ImageFormat.Jpeg);
        bg.Dispose();
        cup.Dispose();
    }

    private void createSharePage(string uKey)
    {
        string imgUrl, title, desc, shareUrl;
        imgUrl = parameter._serverUrl + "s/barShareImg/" + uKey + ".jpg";
        title = parameter._barShareTitle;
        desc = parameter._barShareDesc;
        shareUrl = parameter._websiteUrl;
        string sharePage = String.Format(parameter._shareUrlT, imgUrl, title, desc, shareUrl);

        string path = Server.MapPath("~/s/barShare/" + uKey + ".html");

        if (!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(sharePage);
            }
        }
    }

    private string getShareMsg(ref barData data)
    {
        string shareMsg = "";
        if (data.ans2 == 0 || data.ans3 == 0 || data.ans4 == 0 || data.ans5 == 0)
        {
            string lifeNumberText = parameter._barAns1[0];
            string foodText = parameter._barAns2[0];
            string lifeStateText = parameter._barAns3[0];
            string retireText = parameter._barAns5[0];
            shareMsg = String.Format(parameter._barShareMsgT, lifeStateText, lifeNumberText, foodText, retireText);
        }
        else
        {
            int lifeNumber = getLifeNum(data.ans1);
            string lifeNumberText = parameter._barAns1[lifeNumber - 1];
            string foodText = parameter._barAns2[data.ans2 - 1];
            string lifeStateText = parameter._barAns3[data.ans3 - 1];
            string retireText = parameter._barAns5[data.ans5 - 1];
            shareMsg = String.Format(parameter._barShareMsgT, lifeStateText, lifeNumberText, foodText, retireText);
        }
        return shareMsg;
    }

    private int getLifeNum(string birth)
    {
        DateTime date = DateTime.Parse(birth);
        
        int SpiritNumber = (date.Year / 1000) + ((date.Year / 100) % 10) + ((date.Year / 10) % 10) + (date.Year % 10) + (date.Month / 10) + (date.Month % 10) + (date.Day / 10) + (date.Day % 10);
        while (SpiritNumber >= 10)
        {
            SpiritNumber = (SpiritNumber / 10) + (SpiritNumber % 10);
        }
        return SpiritNumber;
    }
}