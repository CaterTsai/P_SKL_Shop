﻿var adminKey;

//------------------------------------
//AJAX
//Lab Run & City
function toSGetShareMsg() {
    $.post(
        "../s/adminApi.aspx",
        {
            active: "getShareMsg"
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            $("#shareMsg").val(result["data"]);

        }
    )
}

function toSUpdateShareMsg() {
    var newMsg = $("#shareMsg").val();
    $.post(
        "../s/adminApi.aspx",
        {
            active: "updateShareMsg",
            key:adminKey,
            msg: newMsg
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            if (result["result"]) {
                alert("更新成功");
            }
            else {
                alert("更新失敗");
            }
        }
    )
}

function toSGetAutoClearDay() {
    $.post(
        "../s/adminApi.aspx",
        {
            active: "getAutoClearDay"
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            $("#autoClearDay").val(result["data"]);
        }
    )
}

function toSUpdateAutoClearDay() {
    var newDay = $("#autoClearDay").val();

    $.post(
        "../s/adminApi.aspx",
        {
            active: "updateAutoClearDay",
            key: adminKey,
            day: newDay
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            if (result["result"]) {
                alert("更新成功");
            }
            else {
                alert("更新失敗");
            }
        }
    )
}

function toSClearRun() {
    $.post(
    "../s/adminApi.aspx",
    {
        active: "clearRun",
        key: adminKey
    },
    'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            if (result["result"]) {
                alert("清除Run資料成功");
            }
            else {
                alert("清除Run資料失敗");
            }
        }
    )
}

function toSClearCity() {
    $.post(
    "../s/adminApi.aspx",
    {
        active: "clearCity",
        key: adminKey
    },
    'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            if (result["result"]) {
                alert("清除City資料成功");
            }
            else {
                alert("清除City資料失敗");
            }
        }
    )
}

function toSGetRunStartTime() {
    $.post(
        "../s/adminApi.aspx",
        {
            active: "getLabRunStartTime"
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            $("#labRunStartT").val(result["data"]);
        }
    )
}

function toSUpdateRunStartTime() {
    var newTime = $("#labRunStartT").val();

    $.post(
        "../s/adminApi.aspx",
        {
            active: "updateRunStartTime",
            key: adminKey,
            RunStartT: newTime
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            if (result["result"]) {
                alert("更新成功");
            }
            else {
                alert("更新失敗");
            }
        }
    )
}

function toSGetRunResetTime() {
    $.post(
        "../s/adminApi.aspx",
        {
            active: "getLabRunRestTime"
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            $("#labRunResetT").val(result["data"]);
        }
    )
}

function toSUpdateRunResetTime() {
    var newTime = $("#labRunResetT").val();

    $.post(
        "../s/adminApi.aspx",
        {
            active: "updateRunRestTime",
            key: adminKey,
            RunResetT: newTime
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            if (result["result"]) {
                alert("更新成功");
            }
            else {
                alert("更新失敗");
            }
        }
    )
}

function toSGetBoxType() {
    $.post(
        "../s/adminApi.aspx",
        {
            active: "getBoxType"
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            $("#labRunBoxType")[0].selectedIndex = result["data"];
        }
    )
}

function toSUpdateBoxType() {
    var boxType = $("#labRunBoxType").val();

    $.post(
        "../s/adminApi.aspx",
        {
            active: "updateBoxType",
            key: adminKey,
            BoxType: boxType
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            if (result["result"]) {
                alert("更新成功");
            }
            else {
                alert("更新失敗");
            }
        }
    )
}

//Life Bar & Bartender
function toSClearBar()
{
    $.post(
    "../s/adminApi.aspx",
    {
        active: "clearBar",
        key: adminKey
    },
    'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            if (result["result"]) {
                alert("清除Bar資料成功");
            }
            else {
                alert("清除Bar資料失敗");
            }
        }
    )
}

function toSGetBartenderResetTime()
{
    $.post(
    "../s/adminApi.aspx",
    {
        active: "getBartenderRestTime"
    },
    'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            $("#bartenderResetT").val(result["data"]);
        }
    )
}

function toSUpdateBartenderResetTime()
{
    var newTime = $("#bartenderResetT").val();

    $.post(
        "../s/adminApi.aspx",
        {
            active: "updateBartenderRestTime",
            key: adminKey,
            BarQRShowT: newTime
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            if (result["result"]) {
                alert("更新成功");
            }
            else {
                alert("更新失敗");
            }
        }
    )
}

function toSGetBarShareMsg()
{
    $.post(
    "../s/adminApi.aspx",
    {
        active: "getBarShareMsg"
    },
    'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            $("#barShareMsg").val(result["data"]);

        }
    )
}

function toSUpdateBarShareMsg()
{
    var newMsg = $("#barShareMsg").val();
    $.post(
        "../s/adminApi.aspx",
        {
            active: "updateBarShareMsg",
            key: adminKey,
            msg: newMsg
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            if (result["result"]) {
                alert("更新成功");
            }
            else {
                alert("更新失敗");
            }
        }
    )
}

function toSGetBarQuestion()
{
    $.post(
    "../s/adminApi.aspx",
    {
        active: "getBarQuestion"
    },
    'json'
    ).done(
        function (data) {
            var result = JSON.parse(data)["data"];
            $("#barQuestion").val(result.question);
            $("#barOpt1").val(result.opt1);
            $("#barOpt2").val(result.opt2);
            $("#barOpt3").val(result.opt3);
            $("#barOpt4").val(result.opt4);
            $("#barOpt5").val(result.opt5);
            $("#barOpt6").val(result.opt6);
        }
    )
}

function toSUpdateBarQuestion()
{
    var qData = {};
    qData["question"] = $("#barQuestion").val();
    qData["opt1"] = $("#barOpt1").val();
    qData["opt2"] = $("#barOpt2").val();
    qData["opt3"] = $("#barOpt3").val();
    qData["opt4"] = $("#barOpt4").val();
    qData["opt5"] = $("#barOpt5").val();
    qData["opt6"] = $("#barOpt6").val();


    $.post(
        "../s/adminApi.aspx",
        {
            active: "updateBarQuestion",
            key: adminKey,
            question: JSON.stringify(qData)
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            if (result["result"]) {
                alert("更新成功");
            }
            else {
                alert("更新失敗");
            }
        }
    )
}
//Main
function toSLogin() {
    var adminID = $("#ID").val();
    var adminPW = $("#PW").val();
    $.post(
        "../s/adminApi.aspx",
        {
            active: "login",
            id: adminID,
            pw: adminPW
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);

            if (result["result"]) {
                adminKey = result["data"];
                alert("登入成功");
                document.getElementById("login").style.display = "none";
                document.getElementById("ctrlDiv").style.display = "block";
                loadData();
            }
            else {
                if (result["msg"] == "SKLAuthFailed") {
                    alert("員工資料認證錯誤");
                }
                else {
                    alert("帳號或密碼錯誤");
                }

            }
        }
    )
}
//------------------------------------
//Button Event
//Lab Run & City
function onBtnClearRun() {
    if (adminKey != "") {
        toSClearRun();
    }
    else {
        alert("請先登入");
    }
}

function onBtnClearCity() {
    if (adminKey != "") {
        toSClearCity();
    }
    else {
        alert("請先登入");
    }
}

function onBtnUpdateShareMsg() {
    if (adminKey != "") {
        toSUpdateShareMsg();
    }
    else {
        alert("請先登入");
    }
}

function onBtnUpdateAutoClearDay() {
    if (adminKey != "") {
        toSUpdateAutoClearDay();
    }
    else {
        alert("請先登入");
    }
}

function onBtnUpdateLabRunStart() {
    if (adminKey != "") {
        toSUpdateRunStartTime();
    }
    else {
        alert("請先登入");
    }
}

function onBtnUpdateLabRunReset() {
    if (adminKey != "") {
        toSUpdateRunResetTime();
    }
    else {
        alert("請先登入");
    }    
}

function onBtnUpdateLabBoxType() {
    if (adminKey != "")
    {
        toSUpdateBoxType();
    }
    else
    {
        alert("請先登入");
    }
}

//Life Bar & Bartender
function onBtnResetBar()
{
    if (adminKey != "") {
        toSClearBar();
    }
    else {
        alert("請先登入");
    }
}

function onBtnUpdateBartenderReset()
{
    if (adminKey != "") {
        toSUpdateBartenderResetTime();
    }
    else {
        alert("請先登入");
    }
}

function onBtnUpdateBarShareMsg()
{
    if (adminKey != "") {
        toSUpdateBarShareMsg();
    }
    else {
        alert("請先登入");
    }
}

function onBtnUpdateBarQuestion()
{
    if (adminKey != "") {
        toSUpdateBarQuestion();
    }
    else {
        alert("請先登入");
    }
}

//Main
function onBtnRunCtrl() {
    $("#runCtrlPlane").show();
    $("#barCtrlPlane").hide();
}

function onBtnBarCtrl() {
    $("#runCtrlPlane").hide();
    $("#barCtrlPlane").show();
}

function onBtnLogin() {
    toSLogin();
}

function loadData() {
    toSGetShareMsg();
    toSGetAutoClearDay();
    toSGetRunStartTime();
    toSGetRunResetTime();
    toSGetBoxType();

    toSGetBarShareMsg();
    toSGetBartenderResetTime();
    toSGetBarQuestion();
}
//------------------------------------
window.onload
{
    loadData();
}