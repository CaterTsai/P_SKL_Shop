using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// parameter 的摘要描述
/// </summary>
public class parameter
{
    public parameter()
    {
    }
    public static string _serverUrl = "http://192.168.1.120/";
    //public static string _serverUrl = "https://skllifelab.skl.com.tw/";
    public static string _websiteUrl = "https://lifelab.skl.com.tw/";
    public static string _videoUrl = _serverUrl + "video.html?vType=";

    public static List<float> _shareLevel = new List<float>(new float[] {
        30.0f,
        40.0f,
        60.0f,
        90.0f,
        999.0f,
        }
    );

    public static List<string> _shareTitle = new List<string>(new string[] {
        "一開始就結束也沒關係，來吧我們安慰你！",
        "努力過後我才發現，好險可以砍掉重練！",
        "為什麼我總是感覺自己特別普通？可能因為你確實比較普通吧",
        "遊戲結束可以重來，人生呢？讓專業的來！",
        "跑得高分有實力，滿分人生靠設計"
        }
    );

    public static string _shareDesc = "超乎想像的人生設計所，讓人生好好玩！快進來看看吧~";

    //{0}:image url
    //{1}:title
    //{2}:description
    //{3}:video url
    public static string _shareUrlT = "<!DOCTYPE html><html><head><title></title><meta property=\"og:image\" content=\"{0}\" /><meta property=\"og:title\" content=\"{1}\" /><meta property=\"og:description\" content=\"{2}\" /></head><body><script>window.location = \"{3}\";</script></body></html>";

    //{0}:image url
    //{1}:title
    //{2}:description
    public static string _barShareUrlT = "<!DOCTYPE html><html><head><title></title><meta property=\"og:image\" content=\"{0}\" /><meta property=\"og:title\" content=\"{1}\" /><meta property=\"og:description\" content=\"{2}\" /></head><body><script>window.location = \"{3}\";</script></body></html>";

}