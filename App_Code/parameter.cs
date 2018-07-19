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
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }
    public static string _serverUrl = "http://192.168.1.141/";
    //public static string _serverUrl = "https://skllifelab.skl.com.tw/";
    public static string _websiteUrl = "https://lifelab.skl.com.tw/";

    public static List<float> _shareLevel = new List<float>(new float[] {
        30.0f,
        40.0f,
        60.0f,
        90.0f,
        999.0f,
        }
    );

    public static List<string> _shareTitle = new List<string>(new string[] {
        "我不想人生還沒開始就結束！",
        "生無可戀？好險人生可以砍掉重練！",
        "普通的分數、普通的人生、普通的我。",
        "遊戲有終點，人生卻只能拼命為鐘點！",
        "人生沒有滿分，最適合的人生要自己生！"
        }
    );

    public static string _shareDesc = "超乎想像的人生設計所，讓人生好好玩！快進來看看吧~";

    //{0}:image url
    //{1}:title
    //{2}:description
    //{3}:video url
    public static string _shareUrlT = "<!DOCTYPE html><html><head><title></title><meta property=\"og:image\" content=\"{0}\" /><meta property=\"og:title\" content=\"{1}\" /><meta property=\"og:description\" content=\"{2}\" /></head><body><script>window.location = \"{3}\";</script></body></html>";
    
}