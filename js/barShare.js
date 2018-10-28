var _gGuid = "";
var _gFrom = "";
//---------------------------------
//AJAX
function toSSetLiquorNickname(nick) {
    
    if (_gGuid != "") {
        $.post(
        "s/barApi.aspx",
        {
            active: "setLiquorNickname"
            , guid: _gGuid
            , nickName: nick
        },
        'json'
        ).done(
            function (data) {
                $("#sendDiv").fadeOut();
                $("#shareDiv").fadeIn();
            }
        )
    }
}

function toSAddBarMobileData(name, phone) {

    if (_gGuid != "") {
        $.post(
        "s/barApi.aspx",
        {
            active: "addBarMobileData"
            , guid: _gGuid
            , userName: name
            , mobile : phone
        },
        'json'
        ).done(
            function (data) {
                $("#dataDiv").fadeOut();
                $("#resultDiv").fadeIn();
            }
        )
    }
}

//------------------------------
//Button Event
function onBtnSend()
{
    var nickName = $("#nickName").val();
    if (nickName.length > 0)
    {
        //toSSetLiquorNickname(nickName);
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
    fbShare(_gGuid);
    $("barDiv").fadeOut();
    $("lotteryDiv").fadeIn();
}

//------------------------------
function onBtnLottery()
{
    var userName = $("#nameData").val();
    var mobile = $("#mobileData").val();
    var term = $("#datacheck").is(':checked');
    if (userName.length == 0 || mobile.length == 0)
    {   
        alert("請先填入個人資料");
    }
    else if(!term)
    {
        alert("請勾選同意個人資料告知事項");
    }
    else
    {
        //toSAddBarMobileData(userName, mobile);
        $("#dataDiv").fadeOut();
        $("#resultDiv").fadeIn();
    }
    
}

//------------------------------
function onBtnTerm()
{
    $("#termsDiv").show();
}

//------------------------------
function onBtnClose()
{
    $("#termsDiv").hide();
}


//----------------------------------
//FB
function fbShare(guid)
{
    var hashTag = encodeURIComponent("#LIFELab人生設計所");
    var reurl = encodeURIComponent("http://artgital.com/barShare.html?from=afterShare&guid=" + guid + "");
    var url = encodeURIComponent("http://artgital.com/testShare.html");
    var share_url = "https://www.facebook.com/dialog/share?"
        + "app_id=304914879841201"
        + "&href=" + url
        + "&hashtag=" + hashTag
        + "&redirect_uri=" + reurl;
    window.location = share_url;
}

//-----------------------------
//Method
function get(name) {
    if (name = (new RegExp('[?&]' + encodeURIComponent(name) + '=([^&]*)')).exec(location.search))
        return decodeURIComponent(name[1]);
}

function getUrlParameter() {
    var url = new URL(window.location.href);
    _gGuid = get("guid");
    _gFrom = get("from");
    
    if(_gFrom == 'qrcode')
    {
        setLiquor(_gGuid);
        $("#barDiv").show();
    }
    else if(_gFrom == 'afterShare')
    {
        $("#lotteryDiv").show();
    }
    console.log(_gFrom);
}

//----------------------------------------
function setLiquor() {
    if (_gGuid != "")
    {
        $("#liquor").attr('src', "s/liquor/" + _gGuid + ".png");
    }
}

//----------------------------------------
function loadTerms() {

    $.get(
		"assets/barPage/offerTerms.txt",
		function (data) {
		    $("#termText").text(data);
		},
		"text"
	);
}

window.load
{
    getUrlParameter();
    loadTerms();
}