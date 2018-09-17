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
                    
                    //TODO
                    //Update DB State?
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
}