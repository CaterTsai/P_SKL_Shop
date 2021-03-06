﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public partial class wallApi : System.Web.UI.Page
{
    private dbMgrWall _dbMgr = new dbMgrWall();
    protected void Page_Load(object sender, EventArgs e)
    {
        _dbMgr.connDB();
        handleActive();
    }

    private void handleActive()
    {
        var active = Request["active"];
        var store = Int32.Parse(Request["store"]);
        response rep = new response();
        rep.active = active;

        if (active == "addWallLog")
        {
            _dbMgr.addWallLog(Convert.ToInt32(Request["type"]), store);
            rep.result = true;
        }
        else if(active == "addWallWebLog")
        {
            _dbMgr.addWallWebLog(Convert.ToInt32(Request["type"]), store);
            rep.result = true;
        }
        else
        {
            rep.result = false;
            rep.msg = "Unknow active";
        }
        var repJson = JsonConvert.SerializeObject(rep);
        Response.Write(repJson);
    }
}