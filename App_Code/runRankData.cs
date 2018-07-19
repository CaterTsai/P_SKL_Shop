using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// runRank 的摘要描述
/// </summary>
public class runRankData
{
    public int rank { get; set; }
    public string guid { get; set; }
    public int score { get; set; }
    public int gender { get; set; }
    public int group { get; set; }
    public int carType { get; set; }

    public runRankData()
    {
        rank = score = 0;
        guid = "";
        gender = group = carType = 0;

    }
}