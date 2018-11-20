using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// configData 的摘要描述
/// </summary>
public class configData
{
    public int id { get; set; }
    public int value_1 { get; set; }
    public float value_2 { get; set; }
    public string value_3  { get; set; }

    public configData()
    {
        value_1 = 0;
        value_2 = 0.0f;
        value_3 = null;
    }

    public static Dictionary<string, int> ConfigMap = new Dictionary<string, int>()
    {
        { "MobileMsg", 1},
        { "AutoClearDay", 2},
        { "RunResetT", 3},
        { "RunBoxType", 4},
        { "RunStartT", 5},
        { "BarQRShowT", 6},
        { "BarMobileMsg", 7},
        { "BarPopoutMsg", 8},
        { "BarDataMsg", 9}

    };
}

