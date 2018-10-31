var adminKey = "";

//AJAX
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
            
            if(result["result"])   
            {
                adminKey = result["data"];
                alert("登入成功");
                document.getElementById("login").style.display = "none";
                document.getElementById("ctrlPlane").style.display = "block";
                loadData();
            }
            else
            {
                alert("帳號或密碼錯誤");
            }
        }
    )
}

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

//------------------------------------
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

function onBtnLogin() {
    toSLogin();
}

function loadData() {
    toSGetShareMsg();
    toSGetAutoClearDay();
    toSGetRunStartTime();
    toSGetRunResetTime();
    toSGetBoxType();
}
//------------------------------------
window.onload
{

}