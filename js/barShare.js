//------------------------------
//Button Event
function onBtnSend()
{
    var nickName = $("#nickName").val();
    if (nickName.length > 0)
    {
        $("#sendDiv").fadeOut();
        $("#shareDiv").fadeIn();
    }
    else
    {
        alert("請輸入暱名");
    }
}

//------------------------------
function onBtnShare()
{   
    fbShare();

}

//------------------------------
function onBtnLottery()
{
    $("#dataDiv").fadeOut();
    $("#resultDiv").fadeIn();
}

//------------------------------
function onBtnTest()
{
    $("#barDiv").fadeOut();
    $("#lotteryDiv").fadeIn();
}

//----------------------------------
//FB
function fbShare()
{
    var hashTag = encodeURIComponent("#LIFELab人生設計所");
    var reurl = encodeURIComponent("http://artgital.com/skl_barTest/barShare.html");
    var url = encodeURIComponent("http://artgital.com/skl_barTest/share.html");
    var share_url = "https://www.facebook.com/dialog/share?"
        + "app_id=304914879841201"
        + "&href=" + url
        + "&hashtag=" + hashTag
        + "&redirect_uri=" + reurl;
    window.location = share_url;
}

window.load
{

}