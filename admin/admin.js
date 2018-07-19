//AJAX
function toSGetShareMsg() {
    $.post(
        "../s/backstage.aspx",
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
        "../s/backstage.aspx",
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
        "../s/backstage.aspx",
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
        "../s/backstage.aspx",
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
    "../s/backstage.aspx",
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
    "../s/backstage.aspx",
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

function toSGetRunResetTime() {
    $.post(
    "../s/backstage.aspx",
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
        "../s/backstage.aspx",
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

function onBtnUpdateLabRunReset() {
    toSUpdateRunResetTime();
}

//------------------------------------
window.onload
{
    toSGetShareMsg();
    toSGetAutoClearDay();
    toSGetRunResetTime();
}