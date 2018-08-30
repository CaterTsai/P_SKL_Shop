using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// runSetting 的摘要描述
/// </summary>
public class runSetting
{
    public int resetSecond { get; set; }
    public int boxType { get; set; }
    public int startSecond { get; set; }
    public runSetting()
    {
        resetSecond = 20;
        boxType = 1;
        startSecond = 5;
    }
}