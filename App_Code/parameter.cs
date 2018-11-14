using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// parameter 的摘要描述
/// </summary>
public class parameter
{
    public static void loadDefaultLiquor(string path)
    {
        if(_barDefault.Count == 0)
        {
            JObject obj = JObject.Parse(System.IO.File.ReadAllText(path));
            var barData = obj["barData"];
            for(int i = 0; i < 16; i++)
            {
                _barDefault.Add(barData[i].ToObject<barData>());
            }
        }
        
        
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
    public static string _barShareTitle = "Dr. Robot為我打造的人生特調";
    public static string _barShareDesc = "超乎想像的人生設計所，讓人生好好玩！快進來看看吧~";


    //{0}:image url
    //{1}:title
    //{2}:description
    //{3}:video url
    public static string _shareUrlT = "<!DOCTYPE html><html><head><title></title><meta property=\"og:image\" content=\"{0}\" /><meta property=\"og:title\" content=\"{1}\" /><meta property=\"og:description\" content=\"{2}\" /></head><body><script>window.location = \"{3}\";</script></body></html>";

    public static string _barShareMsgT = "正經歷{0}的你，天生{1}又有著{2}的Life Style。你的白日夢冒險將會在{3}";

    public static List<string> _barAns1 = new List<string>(new string[] {
        "獨立自主靠自己",
        "是個社交達人",
        "樂觀又創意無限",
        "心臟大顆沉著冷靜",
        "崇尚自由熱愛挑戰",
        "溫柔體貼樂於助人",
        "善於分析與邏輯思考",
        "是個人生勝利組",
        "單純善良無心機"
        }
    );

    public static List<string> _barAns2 = new List<string>(new string[] {
        "甜蜜放閃",
        "辛酸魯蛇",
        "高潮迭起",
        "熱血少年",
        "佛系青年",
        "人中之龍",
        "生不如死",
        "單身狗"
        }
    );

    public static List<string> _barAns3 = new List<string>(new string[] {
        "初入職場",
        "步入婚姻",
        "小孩出生",
        "創業維艱",
        "樂活退休"
        }
    );

    public static List<string> _barAns5 = new List<string>(new string[] {
        "台灣展開，日月潭騎著腳踏車環湖，與三五好友共乘纜車，珍惜這小確幸的幸福",
        "法國展開，巴黎鐵塔下的野餐搭配偶然的邂逅，寫下一篇法式浪漫love story",
        "荷蘭展開，羊角村腳踏木鞋，悠閒漫步在鬱金花田中",
        "韓國展開，東大門穿著韓服與閨蜜們大肆血拼、路邊小酌暢談往事",
        "日本展開，感受古意盎然的京都歷史風韻，淺嚐黑門市場海產與神戶牛排，見證歷史美食共融的美好",
        "泰國展開，品嚐酸辣的泰式滋味，享受舒壓的精油按摩，晚上再到街頭暢飲冰涼的啤酒",
        "澳洲展開，來一場刺激無比的黃金海岸飆速衝浪，再到布里斯本優雅喝口香醇咖啡",
        "美國西岸展開，享受加州陽光與葡萄酒，看幾部西部冒險電影，可能還會與好萊塢明星來場豔遇",
        "埃及展開，走進吉薩金字塔開始古老而神秘的探險，乘著郵輪享受尼羅河的紅海風情",
        "墨西哥展開，戴上草帽、畫上亡靈節的彩妝，吃一口辣味十足的墨西哥料理，與傳統瑪利亞奇樂隊一起跳舞"
        }
    );

    public static List<barData> _barDefault = new List<barData>();


}