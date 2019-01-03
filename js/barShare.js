var _gGuid = "";
var _gFrom = "";
var _gIsSlickInit = false;
var _gIsEnterLottery = false;

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

//------------------------------
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
function toSGetResultMsg()
{
    $.post(
        "s/barApi.aspx",
        {
            active: "getResultMsg"
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);

            if (result["result"]) {
                $("#resultText").html(result["data"]);
            }
        }
    )
}

//------------------------------
function toSGetPopoutMsg() {
    $.post(
        "s/barApi.aspx",
        {
            active: "getPopoutMsg"
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);

            if (result["result"]) {
                popoutHtml(result["data"]);
            }
        }
    )
}

//------------------------------
function toSGetDataMsg() {
    $.post(
        "s/barApi.aspx",
        {
            active: "getDataMsg"
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);

            if (result["result"]) {
                $("#dataMsg").text(result["data"]);
            }
        }
    )
}

//------------------------------
function toSGetInfoState()
{
    $.post(
        "s/barApi.aspx",
        {
            active: "getInfoState"
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);

            if (result["result"]) {
                var state = result["data"];
                for(var i = 0; i < state.length; i++)
                {
                    if(state.charAt(i) == '0')
                    {
                        $("#shareInfoDiv" + (i + 1)).hide();
                    }
                }
            }
        }
    )
}

//------------------------------
//Button Event
function onBtnSend()
{
    var nickName = $("#nickName").val();
    if (nickName.length > 0)
    {
        toSSetLiquorNickname(nickName);
    }
    else
    {
        popout("請輸入暱名");
    }
}

//------------------------------
function onBtnShare()
{   
    fbShare(_gGuid);

    setTimeout(function () {
        initSharePage();
    }, 2000);
}

//------------------------------
function onBtnLottery()
{
    var userName = $("#nameData").val();
    var mobile = $("#mobileData").val();
    var term = $("#datacheck").is(':checked');
    if (userName.length == 0)
    {   
        popout("請填入姓名");
    }
    else if(mobile.length == 0)
    {
        popout("請填入電話");
    }
    else if (!$("#mobileData")[0].checkValidity()) {
        popout("請填入正確的電話格式");
    }
    else if(!term)
    {
        popout("請勾選同意個人資料告知事項");
    }
    else
    {
        toSAddBarMobileData(userName, mobile);
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

//------------------------------
function onBtnGift()
{
    $("#photoDiv").show();
    initSlick();
}
//------------------------------
function onBtnPhotoClose()
{
    $("#photoDiv").hide();
}
//------------------------------
function onBtnPopoutClose()
{
    $("#popoutDiv").hide();

    if (_gIsEnterLottery)
    {
        setTimeout(function () {
            $("#presentDiv").addClass("enter");
        }, 1000);
        _gIsEnterLottery = false;
    }
}


//----------------------------------
//FB
function fbShare(guid)
{
    var hashTag = encodeURIComponent("#LIFELab人生設計所");
    var reurl = encodeURIComponent("https://skllifelab.skl.com.tw/barShare.html?from=afterShare&guid=" + guid);
    var url = encodeURIComponent("https://skllifelab.skl.com.tw/s/barShare/" + guid + ".html");
    var share_url = "https://www.facebook.com/dialog/share?"
        + "app_id=2160056277354327"
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
        initSharePage();
    }
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

//----------------------------------------
function initSlick()
{
    if (!_gIsSlickInit)
    {
        $("#photoSliderDiv").slick();
        _gIsSlickInit = true;
    }
}

//----------------------------------------
function popout(msg)
{
    $("#popoutDiv").show();
    $("#popoutMsg").text(msg);

}

//----------------------------------------
function popoutHtml(msg) {
    $("#popoutDiv").show();
    $("#popoutMsg").html(msg);

}

//----------------------------------------
function initSharePage()
{
    $("#lotteryDiv").show();
    toSGetResultMsg();
    toSGetPopoutMsg();
    toSGetDataMsg();
    toSGetInfoState();
    _gIsEnterLottery = true;
}


var viewW;
var viewH;
window.load
{
    getUrlParameter();
    loadTerms();

    viewW = Math.max(document.documentElement.clientWidth, window.innerWidth || 0);
    viewH = Math.max(document.documentElement.clientHeight, window.innerHeight || 0);
}

window.onresize = function(event) {
    var viewport = $('meta[name="viewport"]');
    viewport.attr("content", "width="+ viewW + ",height=" + viewH +", initial-scale=1.0");
}