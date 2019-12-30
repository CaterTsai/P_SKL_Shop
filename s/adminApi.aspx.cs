using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

public partial class s_adminApi : System.Web.UI.Page
{
    private dbMgrAdmin _dbMgr = new dbMgrAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {
        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        _dbMgr.connDB();
        handleActive();
    }

    private void handleActive()
    {
        var active = Request["active"];
        var store = Int32.Parse(Request["store"]);
        response rep = new response();

        rep.active = active;
        rep.store = store;
        switch (active)
        {
            #region Admin
            //admin
            case "login":
                {
                    var ID = Request["id"];
                    var PW = Request["pw"];

                    rep.result = sklLogin("https://ws.skl.com.tw/ws/Auth/Login", ID, PW);
                    rep.data = System.Web.Configuration.WebConfigurationManager.AppSettings["adminKey"];
                    break;
                }
            case "addAdmin":
                {
                    var adminId = Request["adminID"];
                    _dbMgr.addAdmin(adminId);
                    rep.result = true;
                    break;
                }
            case "checkAdmin":
                {
                    var adminId = Request["adminID"];
                    bool result = _dbMgr.checkAdmin(adminId);
                    if (!result)
                    {
                        rep.msg = "This id was not in database";
                    }
                    rep.result = result;
                    break;
                }
            case "addQuestion":
                {
                    var qData = JsonConvert.DeserializeObject<barQuestion>(Request["question"]);
                    if (qData != null)
                    {
                        _dbMgr.addBarQuestion(ref qData);
                        rep.result = true;
                    }
                    else
                    {
                        rep.result = false;
                    }

                    break;
                }

            #endregion

            #region Store Data
            case "addStoreData":
                {
                    var storeData = JsonConvert.DeserializeObject<storeData>(Request["storeData"]);
                    if (storeData != null)
                    {
                        _dbMgr.addStoreData(storeData);
                        rep.result = true;
                    }
                    else
                    {
                        rep.result = false;
                    }
                    break;
                }
            case "updateStoreData":
                {
                    var storeData = JsonConvert.DeserializeObject<storeData>(Request["storeData"]);
                    if (storeData != null)
                    {
                        _dbMgr.updateStoreData(storeData);
                        rep.result = true;
                    }
                    else
                    {
                        rep.result = false;
                    }
                    break;
                }
            case "getStoreData":
                {
                    var data = _dbMgr.getStoreData();
                    if (data != null)
                    {
                        rep.data = data;
                        rep.result = true;
                    }
                    else
                    {
                        rep.result = false;
                    }
                    break;
                }
            #endregion

            #region Lab
            //Lab
            case "getShareMsg":
                {
                    var config = _dbMgr.getConfigData(configData.ConfigMap["MobileMsg"], store);

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
                    if (checkAdminKey(Request["key"]))
                    {
                        configData config = new configData();
                        config.id = configData.ConfigMap["MobileMsg"];
                        config.value_3 = Request["msg"];

                        _dbMgr.updateConfigData(config, store);
                        rep.result = true;
                    }
                    else
                    {
                        rep.result = false;
                    }

                    break;
                }
            case "getAutoClearDay":
                {
                    var config = _dbMgr.getConfigData(configData.ConfigMap["AutoClearDay"], store);
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
                    if (checkAdminKey(Request["key"]))
                    {
                        configData config = new configData();
                        config.id = configData.ConfigMap["AutoClearDay"];
                        config.value_1 = Convert.ToInt32(Request["day"]);

                        _dbMgr.updateConfigData(config, store);
                        rep.result = true;
                    }
                    else
                    {
                        rep.result = false;
                    }
                    break;
                }
            case "clearRun":
                {
                    if (checkAdminKey(Request["key"]))
                    {
                        _dbMgr.clearRun(store);
                        rep.result = true;
                    }
                    else
                    {
                        rep.result = false;
                    }

                    break;
                }
            case "clearCity":
                {
                    if (checkAdminKey(Request["key"]))
                    {
                        _dbMgr.clearCity(store);
                        rep.result = true;
                    }
                    else
                    {
                        rep.result = false;
                    }
                    break;
                }
            case "getLabRunStartTime":
                {
                    var config = _dbMgr.getConfigData(configData.ConfigMap["RunStartT"], store);
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
            case "updateRunStartTime":
                {
                    if (checkAdminKey(Request["key"]))
                    {
                        configData config = new configData();
                        config.id = configData.ConfigMap["RunStartT"];
                        config.value_1 = Convert.ToInt32(Request["RunStartT"]);

                        _dbMgr.updateConfigData(config, store);
                        rep.result = true;
                    }
                    else
                    {
                        rep.result = false;
                    }
                    break;
                }
            case "getLabRunRestTime":
                {
                    var config = _dbMgr.getConfigData(configData.ConfigMap["RunResetT"], store);
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
                    if (checkAdminKey(Request["key"]))
                    {
                        configData config = new configData();
                        config.id = configData.ConfigMap["RunResetT"];
                        config.value_1 = Convert.ToInt32(Request["RunResetT"]);

                        _dbMgr.updateConfigData(config, store);
                        rep.result = true;
                    }
                    else
                    {
                        rep.result = false;
                    }
                    break;
                }
            case "getBoxType":
                {
                    var config = _dbMgr.getConfigData(configData.ConfigMap["RunBoxType"], store);
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
            case "updateBoxType":
                {
                    if (checkAdminKey(Request["key"]))
                    {
                        configData config = new configData();
                        config.id = configData.ConfigMap["RunBoxType"];
                        config.value_1 = Convert.ToInt32(Request["BoxType"]);

                        _dbMgr.updateConfigData(config, store);
                        rep.result = true;
                        break;
                    }
                    else
                    {
                        rep.result = false;
                    }
                    break;
                }
            #endregion

            #region Bar
            //Bar
            case "clearBar":
                {
                    if (checkAdminKey(Request["key"]))
                    {
                        _dbMgr.clearBar(store);
                        rep.result = true;
                    }
                    else
                    {
                        rep.result = false;
                    }
                    break;
                }
            case "getLiquorDisplayT":
                {
                    var config = _dbMgr.getConfigData(configData.ConfigMap["BarLiquorDisplayT"], store);
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
            case "updateLiquorDisplayT":
                {
                    if (checkAdminKey(Request["key"]))
                    {
                        configData config = new configData();
                        config.id = configData.ConfigMap["BarLiquorDisplayT"];
                        config.value_1 = Convert.ToInt32(Request["liquorDisplayT"]);

                        _dbMgr.updateConfigData(config, store);
                        rep.result = true;
                    }
                    else
                    {
                        rep.result = false;
                    }
                    break;
                }
            case "getBartenderRestTime":
                {
                    var config = _dbMgr.getConfigData(configData.ConfigMap["BarQRShowT"], store);
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
            case "updateBartenderRestTime":
                {
                    if (checkAdminKey(Request["key"]))
                    {
                        configData config = new configData();
                        config.id = configData.ConfigMap["BarQRShowT"];
                        config.value_1 = Convert.ToInt32(Request["BarQRShowT"]);

                        _dbMgr.updateConfigData(config, store);
                        rep.result = true;
                    }
                    else
                    {
                        rep.result = false;
                    }
                    break;
                }
            case "getBarShareMsg":
                {
                    var config = _dbMgr.getConfigData(configData.ConfigMap["BarMobileMsg"], store);

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
            case "updateBarShareMsg":
                {
                    if (checkAdminKey(Request["key"]))
                    {
                        configData config = new configData();
                        config.id = configData.ConfigMap["BarMobileMsg"];
                        config.value_3 = Regex.Replace(Request["msg"], @"\r|\n|\r\n", "<br>");

                        _dbMgr.updateConfigData(config, store);
                        rep.result = true;
                    }
                    else
                    {
                        rep.result = false;
                    }

                    break;
                }
            case "getBarPopoutMsg":
                {
                    var config = _dbMgr.getConfigData(configData.ConfigMap["BarPopoutMsg"], store);

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
            case "updateBarPopoutMsg":
                {
                    if (checkAdminKey(Request["key"]))
                    {
                        configData config = new configData();
                        config.id = configData.ConfigMap["BarPopoutMsg"];
                        config.value_3 = Regex.Replace(Request["msg"], @"\r|\n|\r\n", "<br>");

                        _dbMgr.updateConfigData(config, store);
                        rep.result = true;
                    }
                    else
                    {
                        rep.result = false;
                    }
                    break;
                }
            case "getBarDataMsg":
                {
                    var config = _dbMgr.getConfigData(configData.ConfigMap["BarDataMsg"], store);

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
            case "updateBarDataMsg":
                {
                    if (checkAdminKey(Request["key"]))
                    {
                        configData config = new configData();
                        config.id = configData.ConfigMap["BarDataMsg"];
                        config.value_3 = Request["msg"];

                        _dbMgr.updateConfigData(config, store);
                        rep.result = true;
                    }
                    else
                    {
                        rep.result = false;
                    }
                    break;
                }
            case "getBarQuestion":
                {
                    barQuestion qData = _dbMgr.getBarQuestion();
                    if (qData != null)
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
            case "updateBarQuestion":
                {
                    if (checkAdminKey(Request["key"]))
                    {
                        if (Request["question"] != null)
                        {
                            var qData = JsonConvert.DeserializeObject<barQuestion>(Request["question"]);
                            _dbMgr.updateQuestion(ref qData);
                            rep.result = true;
                        }
                        else
                        {
                            rep.result = false;
                        }
                    }
                    else
                    {
                        rep.result = false;
                    }
                    break;
                }
            case "getInfoState":
                {
                    var config = _dbMgr.getConfigData(configData.ConfigMap["BarInfoState"], store);
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
            case "updateInfoState":
                {
                    if (checkAdminKey(Request["key"]))
                    {
                        configData config = new configData();
                        config.id = configData.ConfigMap["BarInfoState"];
                        config.value_3 = Request["infoState"];

                        _dbMgr.updateConfigData(config, store);
                        rep.result = true;
                    }
                    else
                    {
                        rep.result = false;
                    }
                    break;
                }
            case "updateInfoImg":
                {
                    if (checkAdminKey(Request["key"]))
                    {
                        try
                        {
                            foreach (string fileName in Request.Files)
                            {
                                var file = Request.Files[fileName];
                                var path = Path.Combine(Server.MapPath("~/s/barShareInfo/"), fileName + ".jpg");
                                file.SaveAs(path);
                            }
                        }
                        catch (Exception e)
                        {
                            rep.msg = e.Message;
                            rep.result = false;
                        }
                    }
                    else
                    {
                        rep.result = false;
                    }
                    break;
                }
            #endregion

            #region Config
            //Config
            case "getConfig":
                {
                    var config = Request["config"];
                    configData data = _dbMgr.getConfigData(Convert.ToInt32(config), store);
                    rep.result = true;
                    rep.data = data;
                    break;
                }
            case "addConfig":
                {
                    configData data = JsonConvert.DeserializeObject<configData>(Request["config"]);
                    _dbMgr.addConfigData(data, store);
                    rep.result = true;
                    break;
                }
            case "updateConfig":
                {
                    configData data = JsonConvert.DeserializeObject<configData>(Request["config"]);
                    _dbMgr.updateConfigData(data, store);
                    rep.result = true;
                    break;
                }

            #endregion

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

    private bool checkAdminKey(string key)
    {
        var syskey = System.Web.Configuration.WebConfigurationManager.AppSettings["adminKey"];
        return key == syskey;
    }

    private bool checkUserID(string id, string pw)
    {
        skl_AuthServiceRef.wsSKL_AuthenticationSoapClient ws = new skl_AuthServiceRef.wsSKL_AuthenticationSoapClient();

        var result = ws.IsAuthenticated(id, pw, "").Element("Result");
        var data = result.Element("ResultCode").Value;
        if (data == "1")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool sklLogin(string url, string account, string pw)
    {
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.ContentType = "application/json";
        request.Method = "POST";

        var postData = new sklLoginData();
        postData.Account = account;
        postData.Pass = pw;
        postData.AuthType = "A";
       
        string postJson = JsonConvert.SerializeObject(postData);
        using (StreamWriter reqStream = new StreamWriter(request.GetRequestStream()))
        {
            reqStream.Write(postJson);
        }


        string responseStr = "";
        using (WebResponse response = request.GetResponse())
        {
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                responseStr = reader.ReadToEnd();
            }

        }
        dynamic o = JObject.Parse(responseStr);
        return (bool)o.isAuth;
    }
}