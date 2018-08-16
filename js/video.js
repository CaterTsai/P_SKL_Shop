
_gVType = 1;
_gPath = "assets/videoPage/"

function get(name) {
    if (name = (new RegExp('[?&]' + encodeURIComponent(name) + '=([^&]*)')).exec(location.search))
        return decodeURIComponent(name[1]);
}

function getUrlParameter() {
    var url = new URL(window.location.href);
    _gVType = get("vType");
}


window.onload
{
    getUrlParameter();
    vPath = _gPath + _gVType + ".mp4";
    $("#videoSrc").attr("src", vPath);

    $("#videoPlayer").load();
}