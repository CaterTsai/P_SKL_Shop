//AJAX
function toSGetShareMsg() {
    $.post(
        "../s/labApi.aspx",
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
        "../s/labApi.aspx",
        {
            active: "updateShareMsg",
            msg: newMsg
        },
        'json'
    ).done(
        function (data) {
            alert("更新成功");

        }
    )
}

function toSGetAutoClearDay() {
    $.post(
        "../s/labApi.aspx",
        {
            active: "getAutoClearDay"
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            console.log(result["data"]);
            $("#autoClearDay").val(result["data"]);
        }
    )
}

function toSUpdateAutoClearDay() {
    var newDay = $("#autoClearDay").val();

    $.post(
        "../s/labApi.aspx",
        {
            active: "updateAutoClearDay",
            day: newDay
        },
        'json'
    ).done(
        function (data) {
            alert("更新成功");
        }
    )
}

function toSClearRun() {
    $.post(
    "../s/labApi.aspx",
    {
        active: "clearRun"
    },
    'json'
    ).done(
        function (data) {
            alert("清除Run資料成功");
        }
    )
}

function toSClearCity() {
    $.post(
    "../s/labApi.aspx",
    {
        active: "clearCity"
    },
    'json'
    ).done(
        function (data) {
            alert("清除City資料成功");
        }
    )
}

function toSGetRunStartTime() {
    $.post(
        "../s/labApi.aspx",
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
        "../s/labApi.aspx",
        {
            active: "updateRunStartTime",
            RunStartT: newTime
        },
        'json'
    ).done(
        function (data) {
            alert("更新成功");
        }
    )
}

function toSGetRunResetTime() {
    $.post(
        "../s/labApi.aspx",
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
        "../s/labApi.aspx",
        {
            active: "updateRunRestTime",
            RunResetT: newTime
        },
        'json'
    ).done(
        function (data) {
            alert("更新成功");
        }
    )
}

function toSGetBoxType() {
    $.post(
        "../s/labApi.aspx",
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
        "../s/labApi.aspx",
        {
            active: "updateBoxType",
            BoxType: boxType
        },
        'json'
    ).done(
        function (data) {
            alert("更新成功");
        }
    )
}

//------------------------------------
function onBtnClearRun() {
    toSClearRun();
}

function onBtnClearCity() {
    toSClearCity();
}

function onBtnUpdateShareMsg() {
    toSUpdateShareMsg();
}

function onBtnUpdateAutoClearDay() {
    toSUpdateAutoClearDay();
}

function onBtnUpdateLabRunStart() {
    toSUpdateRunStartTime();
}

function onBtnUpdateLabRunReset() {
    toSUpdateRunResetTime();
}

function onBtnUpdateLabBoxType() {
    toSUpdateBoxType();

}
//------------------------------------
window.onload
{
    toSGetShareMsg();
    toSGetAutoClearDay();
    toSGetRunStartTime();
    toSGetRunResetTime();
    toSGetBoxType();
}