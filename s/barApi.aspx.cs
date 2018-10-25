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
            //Mobile
            case "createSharePage":
                {
                    var uKey = Request["guid"];
                    checkShare(uKey);
                    rep.result = true;
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

    private bool checkShare(string uKey)
    {
        bool result = true;
        if (!File.Exists(Server.MapPath("~/s/barShareImg/" + uKey + ".jpg")))
        {
            createImage(uKey);
            createSharePage(uKey);
        }
        return result;
    }

    private void createImage(string uKey)
    {

        Bitmap bg = (Bitmap)System.Drawing.Image.FromFile(Server.MapPath("~/s/assets/bar_ShareBG.png"));
        Bitmap cup = (Bitmap)System.Drawing.Image.FromFile(Server.MapPath("~/s/liquor/" + uKey + ".png"));
        Font font = new Font("Noto Sans CJK TC Regular", 31, FontStyle.Regular);
        
        SolidBrush brush = new SolidBrush(Color.White);
        RectangleF rect = new RectangleF(412, 123, 714, 332);
        string shareStr = "正經歷創業維艱的你，天生獨立自主又有著甜蜜濃郁的Life Style。你的白日夢冒險將會在美國西岸展開，享受加州陽光與葡萄酒，看幾部西部冒險電影，還會與好萊塢明星來場豔遇。";
        using (Graphics gfx = Graphics.FromImage(bg))
        {

            gfx.Flush();
            gfx.DrawString(shareStr, font, brush, rect);
            gfx.DrawImage(cup, -8, 38, 413, 566);
            
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
        title = "";
        desc = "";
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
}