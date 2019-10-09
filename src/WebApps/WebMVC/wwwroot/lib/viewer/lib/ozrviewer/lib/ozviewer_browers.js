var OZViewerObject;
var strOZViewerObject = "";
function Create_Div() {
    var objDiv = document.createElement("div");
    objDiv.id = "RunOZViewer";
    document.body.appendChild(objDiv);
}

function Initialize_OZViewer(ObjectID, ClassID, Width, Height, Type) {
    document.getElementById('RunOZViewer').style['width'] = Width;
    document.getElementById('RunOZViewer').style['height'] = Height;
    if(navigator.appName == "Microsoft Internet Explorer" || (navigator.userAgent.indexOf("Trident") > -1)) {
        if(navigator.appVersion.indexOf("MSIE 6") > -1) {
            OZViewerObject = document.createElement('<object id = "' + ObjectID + '" classid = "' + ClassID + '" style = "width:100%;height:100%"></object>');
        } else {
            strOZViewerObject = '<object id = "' + ObjectID + '" classid = "' + ClassID + '" style = "width:100%;height:100%">';
        }
    }
}

function Insert_OZViewer_Param(ParameterName, ParameterValue) {
    if(navigator.appName == "Microsoft Internet Explorer" || (navigator.userAgent.indexOf("Trident") > -1)) {
        if(navigator.appVersion.indexOf("MSIE 6") > -1) {
            var OZViewerParam = document.createElement('<param name = "' + ParameterName + '" value = "' + ParameterValue + '">');
            OZViewerObject.appendChild(OZViewerParam);
        } else {
            strOZViewerObject += '<param name = "' + ParameterName + '" value = "' + ParameterValue + '"/>';
        }
    }
}

function Start_OZViewer() {
    if((navigator.appName == "Microsoft Internet Explorer"  || (navigator.userAgent.indexOf("Trident") > -1))&& navigator.appVersion.indexOf("MSIE 6") == -1) {
        strOZViewerObject += '</object>';
        document.getElementById('RunOZViewer').innerHTML = strOZViewerObject;
        return;
    }
    document.getElementById('RunOZViewer').appendChild(OZViewerObject);
}
