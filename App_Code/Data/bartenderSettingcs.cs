using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// bartenderSettingcs 的摘要描述
/// </summary>
public class bartenderSettingcs
{
    public int qrDisplaySecond { get; set; }
    public int liquorDisplaySecoud { get; set; }
    public bartenderSettingcs()
    {
        qrDisplaySecond = 10;
        liquorDisplaySecoud = 10;
    }
}