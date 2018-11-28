var adminKey = 53764716;
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

function onBtnUpdateShareMsg() {
    if (adminKey != "") {
        toSUpdateShareMsg();
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
    if (adminKey != "") {
        toSUpdateBoxType();
    }
    else {
        alert("請先登入");
    }
}

//Main
function loadData() {
    toSGetShareMsg();
    toSGetRunStartTime();
    toSGetRunResetTime();
    toSGetBoxType();
}

//------------------------------------
window.onload
{
    loadData();
}