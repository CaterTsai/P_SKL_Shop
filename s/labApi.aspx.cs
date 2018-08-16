using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public partial class labApi : System.Web.UI.Page
{
    private dbMgrLab _dbMgr = new dbMgrLab();

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
            //admin
            case "getShareMsg":
                {
                    var config = _dbMgr.getConfigData(configData.ConfigMap["MobileMsg"]);

                    if (config != null)
                    {
                        rep.result = true;
                        rep.data = config.value_3;
                    }
                    else
                    {
                        rep.result = false;
                        rep.msg = "Can't found config data";
                    }
                    break;
                }
            case "updateShareMsg":
                {
                    configData config = new configData();
                    config.id = configData.ConfigMap["MobileMsg"];
                    config.value_3 = Request["msg"];

                    _dbMgr.updateConfigData(config);
                    rep.result = true;
                    break;
                }
            case "getAutoClearDay":
                {
                    var config = _dbMgr.getConfigData(configData.ConfigMap["AutoClearDay"]);
                    if (config != null)
                    {
                        rep.result = true;
                        rep.data = config.value_1;
                    }
                    else
                    {
                        rep.result = false;
                        rep.msg = "Can't found config data";
                    }
                    break;
                }
            case "updateAutoClearDay":
                {
                    configData config = new configData();
                    config.id = configData.ConfigMap["AutoClearDay"];
                    config.value_1 = Convert.ToInt32(Request["day"]);

                    _dbMgr.updateConfigData(config);
                    rep.result = true;
                    break;
                }
            case "clearRun":
                {
                    _dbMgr.clearRun();
                    rep.result = true;
                    break;
                }
            case "clearCity":
                {
                    _dbMgr.clearCity();
                    rep.result = true;
                    break;
                }
            case "getLabRunRestTime":
                {
                    var config = _dbMgr.getConfigData(configData.ConfigMap["RunResetT"]);
                    if (config != null)
                    {
                        rep.result = true;
                        rep.data = config.value_1;
                    }
                    else
                    {
                        rep.result = false;
                        rep.msg = "Can't found config data";
                    }
                    break;
                }
            case "updateRunRestTime":
                {
                    configData config = new configData();
                    config.id = configData.ConfigMap["RunResetT"];
                    config.value_1 = Convert.ToInt32(Request["RunResetT"]);

                    _dbMgr.updateConfigData(config);
                    rep.result = true;
                    break;
                }
            //Lab Run
            case "getRunRank":
                {
                    getRunRank(ref rep);

                    break;
                }
            case "updateRunData":
                {
                    if (Request["runData"] == null)
                    {
                        break;
                    }
                    var runData = JsonConvert.DeserializeObject<runData>(Request["runData"]);
                    _dbMgr.updateRunData(Request["guid"], runData);
                    rep.result = true;
                    break;
                }
            case "createRunData":
                {
                    string guid = getShortGUID();
                    _dbMgr.addRunData(guid);
                    rep.result = true;
                    rep.data = guid;
                    break;
                }
            case "getUserRank":
                {
                    int rank = -1;
                    int score = 0;
                    _dbMgr.getUserRank(Request["guid"], ref rank, ref score);

                    if (rank != -1)
                    {
                        rep.result = true;
                        rep.data = rank;
                    }
                    break;
                }
            case "getRunSetting":
                {
                    configData runResetT = _dbMgr.getConfigData(configData.ConfigMap["RunResetT"]);
                    configData boxType = _dbMgr.getConfigData(configData.ConfigMap["RunBoxType"]);
                    runSetting setting = new runSetting();
                    setting.resetSecond = runResetT.value_1;
                    setting.boxType = boxType.value_1;

                    rep.result = true;
                    rep.data = setting;
                    break;
                }
            //LabCity
            case "getCityData":
                {
                    getCityData(ref rep);
                    break;
                }
            case "getNewCityUser":
                {
                    cityDisplayData cityData = new cityDisplayData();
                    try
                    {
                        _dbMgr.getNewCityUser(ref cityData);
                        rep.result = true;
                        rep.data = cityData;
                    }
                    catch (Exception ex)
                    {
                        rep.result = false;
                        rep.msg = "DB Error";
                        throw ex.GetBaseException();
                    }
                    break;
                }
            //Mobile
            case "addMobileData":
                {
                    var ukey = Request["guid"];
                    if (ukey == null)
                    {
                        rep.result = false;
                    }
                    else
                    {
                        if (_dbMgr.addMobileData(ukey))
                        {
                            rep.result = true;
                        }
                        else
                        {
                            rep.msg = "Wrong guid";
                            rep.result = false;
                        }
                    }
                    break;
                }
            case "addCityData":
                {
                    var ukey = Request["guid"];
                    var nickName = Request["nick"];


                    if (ukey == null || nickName == null)
                    {
                        rep.result = false;
                    }
                    else
                    {
                        int rank = -1;
                        int score = 0;
                        _dbMgr.getUserRank(Request["guid"], ref rank, ref score);

                        cityDisplayData cityData = _dbMgr.addCityData(ukey, nickName, rank);
                        if (cityData == null)
                        {
                            rep.result = false;
                            rep.msg = "GUID Dupicate";
                        }
                        else
                        {
                            rep.data = cityData;
                            rep.result = true;
                        }
                    }
                    break;
                }
            case "updateMobileData":
                {
                    if (Request["mobileData"] == null || Request["guid"] == null)
                    {
                        break;
                    }
                    var mobileData = JsonConvert.DeserializeObject<mobileData>(Request["mobileData"]);
                    var guid = Request["guid"];
                    _dbMgr.updateMobileData(guid, mobileData);
                    rep.result = true;
                    break;
                }
            case "getShareUrl":
                {
                    var ukey = Request["guid"];
                    var imgData = Request["img"];
                    var shareTo = Request["share"];
                    if (!checkShare(ukey, ref imgData))
                    {
                        rep.result = false;
                        rep.msg = "DB error";
                    }
                    else
                    {
                        rep.result = true;
                        if (shareTo == "line")
                        {
                            _dbMgr.updateShare(ukey, 0, 1);
                        }
                        else
                        {
                            _dbMgr.updateShare(ukey, 1, 0);
                        }
                        rep.data = parameter._serverUrl + "s/share/" + ukey + ".html";
                    }
                    break;
                }
            //Debug
            case "getConfig":
                {
                    var config = Request["config"];
                    configData data = _dbMgr.getConfigData(Convert.ToInt32(config));
                    rep.result = true;
                    rep.data = data;
                    break;
                }
            case "addConfig":
                {
                    configData data = JsonConvert.DeserializeObject<configData>(Request["config"]);
                    _dbMgr.addConfigData(data);
                    rep.result = true;
                    break;
                }
            case "listShare":
                {
                    List<string> shareList = new List<string>();
                    listShare(ref shareList);
                    rep.data = shareList;
                    rep.result = true;
                    break;
                }
            case "fixShare":
                {
                    List<string> shareList = new List<string>();
                    listShare(ref shareList);

                    foreach (var name in shareList)
                    {
                        _dbMgr.updateShare(name, 1, 0);
                    }
                    rep.data = shareList;
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

    private void getRunRank(ref response rep)
    {
        List<runRankData> rank = new List<runRankData>();
        _dbMgr.getRunRank(ref rank);
        rep.result = true;
        rep.data = rank;
    }

    private void getCityData(ref response rep)
    {
        List<cityDisplayData> city = new List<cityDisplayData>();
        _dbMgr.getCityDisplay(ref city);
        rep.result = true;
        rep.data = city;
    }

    private void sendToCity(cityDisplayData cityData)
    {
        udpSender sender = new udpSender("127.0.0.1", 2233);
        string msg = JsonConvert.SerializeObject(cityData);
        sender.send(msg);
    }

    private bool checkShare(string uKey, ref string imgData)
    {
        bool result = true;
        if (!File.Exists(Server.MapPath("~/s/shareImg/" + uKey + ".jpg")))
        {
            runData data = _dbMgr.getUserRunData(uKey);
            if (data != null)
            {
                createImage(uKey, ref data, ref imgData);
                createSharePage(uKey, data.runTime, data.carType);
            }
            else
            {
                result = false;
            }
        }
        return result;
    }

    private void createImage(string uKey, ref runData data, ref string imgData)
    {
        byte[] bytes = Convert.FromBase64String(imgData);
        System.IO.MemoryStream m = new System.IO.MemoryStream(bytes);
        m.Position = 0;

        Bitmap bg = (Bitmap)System.Drawing.Image.FromFile(Server.MapPath("~/s/assets/mobile_image_fb.png"));
        Bitmap c = (Bitmap)Bitmap.FromStream(m);

        Font font1 = new Font("華康儷粗黑", 44, FontStyle.Bold);
        Font font2 = new Font("華康儷粗黑", 33, FontStyle.Regular);
        SolidBrush brush = new SolidBrush(Color.White);
        using (Graphics gfx = Graphics.FromImage(bg))
        {

            gfx.Flush();
            gfx.DrawString(data.score.ToString("0000"), font1, brush, new PointF(93, 169));
            gfx.DrawString(data.specialItem.ToString("0000"), font2, brush, new PointF(137, 261));
            gfx.DrawString(data.dist.ToString("0000"), font2, brush, new PointF(137, 335));
            gfx.DrawString(data.umbrella.ToString("0000"), font2, brush, new PointF(137, 406));
            gfx.DrawString(data.coin.ToString("0000"), font2, brush, new PointF(137, 480));

            gfx.DrawImage(c, 498, 77, 204, 553);
        }

        var path = Server.MapPath("~/s/shareImg/" + uKey + ".jpg");
        bg.Save(Server.MapPath("~/s/shareImg/" + uKey + ".jpg"), ImageFormat.Jpeg);
        bg.Dispose();
        c.Dispose();
        m.Close();
        m = null;
        bytes = null;
    }

    private void createSharePage(string uKey, float second, int carType)
    {
        string imgUrl, title, desc, shareUrl;
        imgUrl = parameter._serverUrl + "s/shareImg/" + uKey + ".jpg";
        shareUrl = parameter._videoUrl + carType.ToString();
        title = "";
        desc = "";
        for (int i = 0; i < parameter._shareLevel.Count(); i++)
        {
            if (second <= parameter._shareLevel[i])
            {
                title = parameter._shareTitle[i];
                desc = parameter._shareDesc;
                break;
            }
        }
        string sharePage = String.Format(parameter._shareUrlT, imgUrl, title, desc, shareUrl);

        string path = Server.MapPath("~/s/share/" + uKey + ".html");

        if (!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(sharePage);
            }
        }
    }

    private void listShare(ref List<string> shareList)
    {

        var dir = new DirectoryInfo(Server.MapPath("~/s/shareImg"));

        foreach (var file in dir.GetFiles())
        {
            shareList.Add(Path.GetFileNameWithoutExtension(file.Name));
        }

    }
}