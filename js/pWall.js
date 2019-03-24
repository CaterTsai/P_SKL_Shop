var gContent;

function toSAddLog(id)
{
    $.post(
        "s/wallApi.aspx",
        {
            active: "addWallWebLog"
            , type: id
        },
        'json'
    ).done(
        function (data) {
            console.log(data);
        }
    )
}

function onContent(id) {
    setContent(gContent[id]);
    $(".popout").show();
    toSAddLog(id);
}

function onClosePopout() {
    $(".popout").hide();
}

function setContent(data) {
    $(".popoutImg").attr("src", "assets/wall/" + data.img + ".png");
    $("#popoutTitle").text(data.title);
    $("#popoutSubtitle").text(data.subtitle);
    $(".popoutContent").text(data.context);
    $("#ref").text(data.linkText);
    $("#ref").attr("href", data.linkUrl);
    
}

function loadContent() {
    $.get(
    "assets/wall/data.json",
    function (data) {
        gContent = data;
    },
    "json"
);
}

window.onload = function () {
    loadContent();
}