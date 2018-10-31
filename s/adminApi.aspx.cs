using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class s_adminApi : System.Web.UI.Page
{
    private dbMgrAdmin _dbMgr = new dbMgrAdmin();
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
            case "login":
                {
                    var sysID = System.Web.Configuration.WebConfigurationManager.AppSettings["adminID"];
                    var sysPW = System.Web.Configuration.WebConfigurationManager.AppSettings["adminPW"];
                    var ID = Request["id"];
                    var PW = Request["pw"];

                    if (ID == sysID && PW == sysPW)
                    {
                        rep.result = true;
                        rep.data = System.Web.Configuration.WebConfigurationManager.AppSettings["adminKey"];
                    }
                    else
                    {
                        rep.result = false;
                        rep.data = sysID;
                    }
                    break;
                }
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
                    if (checkAdminKey(Request["key"]))
                    {
                        configData config = new configData();
                        config.id = configData.ConfigMap["MobileMsg"];
                        config.value_3 = Request["msg"];

                        _dbMgr.updateConfigData(config);
                        rep.result = true;
                    }
                    else
                    {
                        rep.result = true;
                    }

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
                    if (checkAdminKey(Request["key"]))
                    {
                        configData config = new configData();
                        config.id = configData.ConfigMap["AutoClearDay"];
                        config.value_1 = Convert.ToInt32(Request["day"]);

                        _dbMgr.updateConfigData(config);
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
                        _dbMgr.clearRun();
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
                        _dbMgr.clearCity();
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
                    var config = _dbMgr.getConfigData(configData.ConfigMap["RunStartT"]);
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

                        _dbMgr.updateConfigData(config);
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
                    if (checkAdminKey(Request["key"]))
                    {
                        configData config = new configData();
                        config.id = configData.ConfigMap["RunResetT"];
                        config.value_1 = Convert.ToInt32(Request["RunResetT"]);

                        _dbMgr.updateConfigData(config);
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
                    var config = _dbMgr.getConfigData(configData.ConfigMap["RunBoxType"]);
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

                        _dbMgr.updateConfigData(config);
                        rep.result = true;
                        break;
                    }
                    else
                    {
                        rep.result = false;
                    }
                    break;
                }
            //Config
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
            case "updateConfig":
                {
                    configData data = JsonConvert.DeserializeObject<configData>(Request["config"]);
                    _dbMgr.updateConfigData(data);
                    rep.result = true;
                    break;
                }
            //Admin
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
                    if(!result)
                    {
                        rep.msg = "This id was not in database";
                    }
                    rep.result = result;
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

    private bool checkAdminKey(string key)
    {
        var syskey = System.Web.Configuration.WebConfigurationManager.AppSettings["adminKey"];
        return key == syskey;
    }
}