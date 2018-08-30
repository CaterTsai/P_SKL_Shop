var _gGuid = "";
var _nickName = "";
var _gTypeNumber = { "mh": 16, "wh": 18, "ml": 8, "wl": 9 };
var _gHeadList = [];
var _gLegList = [];
var _gBodyPrefix, _gHeadPrefix, _gLegPrefix;
var _gBodyIdx, _gHeadIdx, _gLegIdx;
var _gNickNameMaxLength = 5;
var _gIsMobile = false;
//---------------------------------
//AJAX
function toSCreateMobileData() {
    if (_gGuid != "") {
        $.post(
        "s/labApi.aspx",
        {
            active: "addMobileData"
            , guid: _gGuid
        },
        'json'
        ).done(
            function (data) {
                console.log(data);
            }
        )
    }
}

function toSCreateCityData() {
    _nickName = $('#nickInput').val();

    $.post(
        "s/labApi.aspx",
        {
            active: "addCityData"
            , guid: _gGuid
            , nick: _nickName
        },
        'json'
    ).done(
        function (data) {
            console.log(data);
        }
    )
}

function toSUpdateUserData() {

    var gender = $("#genderSelect").val();

    var mobileData = { "nick": _nickName, "gender": gender, "careerType": _gBodyIdx + 1, "headType": _gHeadIdx + 1, "footType": _gLegIdx + 1 };
    var jsonData = JSON.stringify(mobileData);
    console.log(jsonData);
    $.post(
        "s/labApi.aspx",
        {
            active: "updateMobileData"
            , guid: _gGuid
            , mobileData: jsonData
        },
        'json'
    ).done(
        function (data) {
            console.log(data);
        }
    )
}

function toSGetShareMsg() {
    $.post(
        "s/labApi.aspx",
        {
            active: "getShareMsg"
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);

            if (result["result"]) {
                $("#shareMsg").text(result["data"]);
            }
        }
    )
}

function toSShare(shareTo, callback) {
    var canvas = $("#charCanvas")[0];
    var imgData = canvas.toDataURL();
    imgData = imgData.split(',')[1];

    $.post(
        "s/labApi.aspx",
        {
            active: "getShareUrl"
            , guid: _gGuid
            , img: imgData
            , share : shareTo
        },
        'json'
    ).done(
        function (data) {
            callback(data);
        }
    )
}



//---------------------------------
//Button Events
function onBtnHeadLeft() {
    $("#" + _gHeadPrefix + FormatNumberLength(_gHeadIdx + 1, 2)).toggleClass("slide");
    _gHeadIdx--;
    if (_gHeadIdx < 0) {
        _gHeadIdx = _gTypeNumber[_gHeadPrefix] - 1;
    }
    $("#" + _gHeadPrefix + FormatNumberLength(_gHeadIdx + 1, 2)).toggleClass("slide");
}

function onBtnHeadRight() {
    $("#" + _gHeadPrefix + FormatNumberLength(_gHeadIdx + 1, 2)).toggleClass("slide");
    _gHeadIdx++;
    if (_gHeadIdx >= _gTypeNumber[_gHeadPrefix]) {
        _gHeadIdx = 0;
    }
    $("#" + _gHeadPrefix + FormatNumberLength(_gHeadIdx + 1, 2)).toggleClass("slide");
}

function onBtnLegLeft() {
    $("#" + _gLegPrefix + FormatNumberLength(_gLegIdx + 1, 2)).toggleClass("slide");
    _gLegIdx--;
    if (_gLegIdx < 0) {
        _gLegIdx = _gTypeNumber[_gLegPrefix] - 1;
    }
    $("#" + _gLegPrefix + FormatNumberLength(_gLegIdx + 1, 2)).toggleClass("slide");
}

function onBtnLegRight() {
    $("#" + _gLegPrefix + FormatNumberLength(_gLegIdx + 1, 2)).toggleClass("slide");
    _gLegIdx++;
    if (_gLegIdx >= _gTypeNumber[_gLegPrefix]) {
        _gLegIdx = 0;
    }
    $("#" + _gLegPrefix + FormatNumberLength(_gLegIdx + 1, 2)).toggleClass("slide");
}

function onBtnGONick() {

    toSCreateCityData();
    $("#nickPage").hide();
    $("#inputMsg").show();
}

function onBtnGOInfo() {
    $("#inputMsg").hide();
    var career = $("#careerSelect").val();
    var gender = $("#genderSelect").val();
    setCharatoer(gender, career);
    $("#charactorPage").show();
}

function onBtnGOChar() {

    toSUpdateUserData();
    toSGetShareMsg();
    $("#charactorPage").hide();
    drawOnCanvas();
    $("#sharePage").show();
}

function onBtnFB() {
    toSShare(
        "fb",
        function (data) {
            var result = JSON.parse(data);
            var url = encodeURIComponent(result['data']);

            var hashTag = encodeURIComponent("#LIFELab人生設計所");
            var reurl = encodeURIComponent("https://lifelab.skl.com.tw/");
            var share_url = "https://www.facebook.com/dialog/share?"
				+ "app_id=2160056277354327"
				+ "&href=" + url
				+ "&hashtag=" + hashTag
				+ "&redirect_uri=" + reurl;
            window.location = share_url;
        }
    );
}

function onBtnLine() {
    toSShare(
        "line",
        function (data) {
            var result = JSON.parse(data);
            var url = "";
            if (_gIsMobile)
            {
                url = "line://msg/text/" + encodeURIComponent(result['data']);
            }
            else
            {
                url = "https://lineit.line.me/share/ui?url=" + encodeURIComponent(result['data']);
            }
            window.location = url;
        }
    );
}

//---------------------------------
//Charactor
function setCharatoer(gender, type) {
    var bodyFile;
    if (gender == 1) {
        _gBodyPrefix = "mb";
        bodyFile = "mb" + FormatNumberLength(type, 2) + ".png";
    }
    else {
        _gBodyPrefix = "wb";
        bodyFile = "wb" + FormatNumberLength(type, 2) + ".png";
    }
    _gBodyIdx = type - 1;

    _gHeadList.length = 0;
    _gLegList.length = 0;
    initHeadAndLegList(gender);
    addCharacterPart();

    var bodySrc = "assets/charactor/body/" + bodyFile;
    $("#charactorDiv").prepend(
        $('<img>',
        { id: _gBodyPrefix + FormatNumberLength(_gBodyIdx + 1, 2), src: bodySrc, class: "character body" }
        )
    );

    $("#" + _gHeadPrefix + FormatNumberLength(1, 2)).toggleClass("slide");
    $("#" + _gLegPrefix + FormatNumberLength(1, 2)).toggleClass("slide");
    _gHeadIdx = _gLegIdx = 0;
}

function addCharacterPart() {
    _gHeadList.forEach(
        function (element) {
            var headSrc = "assets/charactor/head/" + element + '.png';
            $("#headDiv").prepend($('<img>', { id: element, src: headSrc, class: "character head slide" }));
        }
    );

    _gLegList.forEach(
        function (element) {
            var legSrc = "assets/charactor/leg/" + element + '.png';
            $("#legDiv").prepend($('<img>', { id: element, src: legSrc, class: "character leg slide" }));
        }
    );
}

function initHeadAndLegList(gender) {
    var headFile, legFile;
    var headNum, legNum;
    if (gender == 1) {
        _gHeadPrefix = "mh";
        _gLegPrefix = "ml";
    }
    else {
        _gHeadPrefix = "wh";
        _gLegPrefix = "wl";
    }

    var headNum = _gTypeNumber[_gHeadPrefix];
    var legNum = _gTypeNumber[_gLegPrefix];

    for (var i = 0; i < headNum; i++) {
        _gHeadList.push(_gHeadPrefix + FormatNumberLength(i + 1, 2));
    }
    for (var i = 0; i < legNum; i++) {
        _gLegList.push(_gLegPrefix + FormatNumberLength(i + 1, 2));
    }
}

//---------------------------------
//Share
function drawOnCanvas() {
    var canvas = $("#charCanvas")[0];
    var ctx = canvas.getContext("2d");

    var body = $("#" + _gBodyPrefix + FormatNumberLength(_gBodyIdx + 1, 2))[0];
    var head = $("#" + _gHeadPrefix + FormatNumberLength(_gHeadIdx + 1, 2))[0];
    var leg = $("#" + _gLegPrefix + FormatNumberLength(_gLegIdx + 1, 2))[0];

    if (ctx == null) {
        return;
    }

    ctx.drawImage(head, 0, 0, 238, 630);
    ctx.drawImage(leg, 0, 0, 238, 630);
    ctx.drawImage(body, 0, 0, 238, 630);
}

//---------------------------------
//Global
function FormatNumberLength(num, length) {
    var r = "" + num;
    while (r.length < length) {
        r = "0" + r;
    }
    return r;
}

function getUrlParameter() {
    var url = new URL(window.location.href);
    _gGuid = get("guid");
    console.log(_gGuid);
}

function get(name) {
    if (name = (new RegExp('[?&]' + encodeURIComponent(name) + '=([^&]*)')).exec(location.search))
        return decodeURIComponent(name[1]);
}

//---------------------------------
//
function checkNickInput() {
    var nickInput = $("#nickInput");
    nickInput.change(function (e) {
        if (nickInput.val().length > _gNickNameMaxLength) {
            nickInput.val(nickInput.val().substr(0, 5));
        }

        if (nickInput.val().length > 0) {
            $("#btnGoNick").show();
        }
        else {
            $("#btnGoNick").hide();
        }
    });
}

function mobilecheck(){
    var check = false;
    (function (a) { if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino/i.test(a) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(a.substr(0, 4))) check = true; })(navigator.userAgent || navigator.vendor || window.opera);
    return check;
};

window.onload
{
    _gIsMobile = mobilecheck();

    getUrlParameter();
    toSCreateMobileData();

    checkNickInput();
}