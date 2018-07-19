using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// response 的摘要描述
/// </summary>
public class response
{
    public string active { get; set; }
    public bool result { get; set; }
    public string msg { get; set; }
    public object data { get; set; }

    public response()
    {
        active = "unknow";
        result = false;
        msg = "";
        data = null;
    }
}