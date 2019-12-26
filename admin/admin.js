var adminKey;
var infoImgFlag = [false, false, false, false];
var storeId = 1;
//------------------------------------
//AJAX
//Lab Run & City
function toSGetShareMsg() {
    $.post(
        "../s/adminApi.aspx",
        {
            active: "getShareMsg"
            ,store:storeId
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
            store: storeId,
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

function toSGetAutoClearDay() {
    $.post(
        "../s/adminApi.aspx",
        {
            active: "getAutoClearDay",
            store: storeId
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
            store: storeId,
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
        store: storeId,
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
        store: storeId,
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
            active: "getLabRunStartTime",
            store: storeId,

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
            store: storeId,
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
            active: "getLabRunRestTime",
            store: storeId,
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
            store: storeId,
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
            active: "getBoxType",
            store: storeId,
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
            store: storeId,
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
function toSClearBar() {
    $.post(
    "../s/adminApi.aspx",
    {
        active: "clearBar",
        store: storeId,
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

function toSGetBartenderResetTime() {
    $.post(
    "../s/adminApi.aspx",
    {
        active: "getBartenderRestTime",
        store: storeId
    },
    'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            $("#bartenderResetT").val(result["data"]);
        }
    )
}

function toSUpdateBartenderResetTime() {
    var newTime = $("#bartenderResetT").val();

    $.post(
        "../s/adminApi.aspx",
        {
            active: "updateBartenderRestTime",
            store: storeId,
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

function toSGetLiquorDisplayTime() {
    $.post(
        "../s/adminApi.aspx",
        {
            active: "getLiquorDisplayT",
            store: storeId
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            $("#LiquorDisplayT").val(result["data"]);
        }
    )
}

function toSUpdateLiquorDisplayTime() {
    var newTime = $("#LiquorDisplayT").val();

    $.post(
        "../s/adminApi.aspx",
        {
            active: "updateLiquorDisplayT",
            store: storeId,
            key: adminKey,
            liquorDisplayT: newTime
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

function toSGetBarShareMsg() {
    $.post(
    "../s/adminApi.aspx",
    {
        active: "getBarShareMsg",
        store: storeId
    },
    'json'
    ).done(
        function (data) {
            var result = JSON.parse(data)["data"];
            result = result.replace('<br>', '\n');
            $("#barShareMsg").val(result);

        }
    )
}

function toSUpdateBarShareMsg() {
    var newMsg = $("#barShareMsg").val();
    $.post(
        "../s/adminApi.aspx",
        {
            active: "updateBarShareMsg",
            store: storeId,
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

function toSGetBarPopOutMsg() {
    $.post(
    "../s/adminApi.aspx",
    {
        active: "getBarPopoutMsg",
        store: storeId
    },
    'json'
    ).done(
        function (data) {
            var result = JSON.parse(data)["data"];
            result = result.replace('<br>', '\n');
            $("#barPopoutMsg").val(result);
        }
    )
}

function toSUpdateBarPopOutMsg() {
    var newMsg = $("#barPopoutMsg").val();
    console.log(newMsg);
    $.post(
        "../s/adminApi.aspx",
        {
            active: "updateBarPopoutMsg",
            store: storeId,
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

function toSGetBarDataMsg() {
    $.post(
    "../s/adminApi.aspx",
    {
        active: "getBarDataMsg",
        store: storeId
    },
    'json'
    ).done(
        function (data) {
            var result = JSON.parse(data)["data"];
            $("#barDataMsg").val(result);
        }
    )
}

function toSUpdateBarDataMsg() {
    var newMsg = $("#barDataMsg").val();
    console.log(newMsg);
    $.post(
        "../s/adminApi.aspx",
        {
            active: "updateBarDataMsg",
            store: storeId,
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

function toSGetBarQuestion() {
    $.post(
    "../s/adminApi.aspx",
    {
        active: "getBarQuestion",
        store: storeId
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

function toSUpdateBarQuestion() {
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
            store: storeId,
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

function toSUpdateInfoImg(id) {
    
    var fData = new FormData();
    fData.append("active", "updateInfoImg");
    fData.append("key", adminKey);
    fData.append("infoState", getInfoState());
    var idName = "#infoImg" + (id + 1).toString();
    if (infoImgFlag[id] && $(idName).prop('files').length > 0) {
        var file = $(idName).prop('files')[0];
        fData.append("p" + (id + 1).toString(), file);

        $.ajax({
            url: "../s/adminApi.aspx",
            store: storeId,
            type: "POST",
            data: fData,
            processData: false,
            contentType: false,
            success: function (data) {
                var result = JSON.parse(data);
                if (result["result"]) {
                    alert("更新成功");
                }
                else {
                    alert("更新失敗");
                }
            }
        })
    }
}

function toSGetInfoState()
{
    $.post(
    "../s/adminApi.aspx",
    {
        active: "getInfoState",
        store: storeId
    },
    'json'
    ).done(
        function (data) {
            var result = JSON.parse(data)["data"];
            setInfoState(result);
        }
    )
}

function toSUpdateInfoState()
{
    var state = getInfoState()
    $.post(
    "../s/adminApi.aspx",
    {
        active: "updateInfoState",
        store: storeId,
        key: adminKey,
        infoState: state
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
function toSGetStoreList() {
    $.post(
        "../s/adminApi.aspx",
        {
            active: "getStoreData",
            store: -1
        },
        'json'
    ).done(
        function (data) {
            var result = JSON.parse(data);
            var storeList = result.data;
            storeList.forEach(function (store) {
                var optName = store.storeName.toString() + "(" + store.storeNo.toString() + ")";
                $("#storeChoose").append("<option value=" + store.storeId.toString() + ">" + optName + "</option>");
            });
        }
    )
}

function toSLogin() {
    var adminID = $("#ID").val();
    var adminPW = $("#PW").val();
    $.post(
        "../s/adminApi.aspx",
        {
            active: "login",
            id: adminID,
            pw: adminPW,
            store: storeId
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
    if (adminKey != "") {
        toSUpdateBoxType();
    }
    else {
        alert("請先登入");
    }
}

//Life Bar & Bartender
function onBtnResetBar() {
    if (adminKey != "") {
        toSClearBar();
    }
    else {
        alert("請先登入");
    }
}

function onBtnUpdateBartenderReset() {
    if (adminKey != "") {
        toSUpdateBartenderResetTime();
    }
    else {
        alert("請先登入");
    }
}

function onBtnUpdateLiquorDisplayT() {
    if (adminKey != "") {
        toSUpdateBarPopOutMsg();
    }
    else {
        alert("請先登入");
    }
}

function onBtnUpdateBarShareMsg() {
    if (adminKey != "") {
        toSUpdateBarShareMsg();
    }
    else {
        alert("請先登入");
    }
}

function onBtnUpdatePopOutMsg() {
    if (adminKey != "") {
        toSUpdateBarPopOutMsg();
    }
    else {
        alert("請先登入");
    }
}

function onBtnUpdateBarQuestion() {
    if (adminKey != "") {
        toSUpdateBarQuestion();
    }
    else {
        alert("請先登入");
    }
}

function onBtnUpdateDataMsg() {
    if (adminKey != "") {
        toSUpdateBarDataMsg();
    }
    else {
        alert("請先登入");
    }
}

function onBtnUpdateInfoImage(id)
{
    if (adminKey != "") {
        toSUpdateInfoImg(id);
    }
    else {
        alert("請先登入");
    }
}

function onBtnUpdateInfo()
{
    if (adminKey != "") {
        toSUpdateInfoState();
    }
    else {
        alert("請先登入");
    }
}

function setInfoState(state)
{
    for(var i = 0; i < state.length; i++)
    {
        var s = state.charAt(i);

        if(s == '1')
        {
            $("#info" + (i + 1)).prop('checked', true);
        }
        else
        {
            $("#info" + (i + 1)).prop('checked', false);
        }
    }
}

function getInfoState()
{
    var state = "";
    for(var i = 1; i <= 4; i++)
    {
        if ($("#info" + i).prop('checked'))
        {
            state += '1';
        }
        else
        {
            state += '0';
        }
    }
    return state;
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
    //storeId = parseInt($('#storeChoose').val());
    toSLogin();
}

function loadData() {
    
    toSGetShareMsg();
    toSGetAutoClearDay();
    toSGetRunStartTime();
    toSGetRunResetTime();
    toSGetBoxType();

    toSGetBarShareMsg();
    toSGetBarPopOutMsg();
    toSGetBartenderResetTime();
    toSGetLiquorDisplayTime();
    toSGetBarQuestion();
    toSGetBarDataMsg();
    toSGetInfoState();
}

function readUrl(idx, id, input) {
    if(input.files && input.files[0])
    {
        var reader = new FileReader();
        reader.onload = function (e) {
            $("#" + id).attr('src', e.target.result)
        };

        reader.readAsDataURL(input.files[0]);
        infoImgFlag[idx] = true;

        $("#upload" + (idx + 1).toString()).prop("disabled", false);
    }
}

//------------------------------------
window.onload
{
    //loadData();
    toSGetStoreList();
}