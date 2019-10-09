'use strict';

var serverPath      = location.origin + location.pathname.substring(0, location.pathname.lastIndexOf("/")) + "/"; // it might be load from .html
var parentScope     = window.parent.angular.element(window.frameElement).scope();
// retrieve value from father layer
var ozParams         = parentScope.ozParams;
var ozClientParams   = parentScope.ozClientParams;
var ozEvents         = parentScope.ozEvents;
var reportPath       = parentScope.ozReportPath;

var clientNo              = parentScope.clientNo    ? parentScope.clientNo    : DEFAULT_REMOTE_CLIENT_NO;
var tripathKey            = parentScope.tripathKey  ? parentScope.tripathKey  : DEFAULT_TRIPATH_KEY;
var tripathEdgeServerPort = parentScope.tripathPort ? parentScope.tripathPort : DEFAULT_TRIPATH_PORT;
// var tripathEdgeServerPath = location.protocol + "//" + location.hostname + ":" + tripathEdgeServerPort;
var tripathEdgeServerPath = "https://osd.ozsaas.com:14127";

var check_disable = 0; // 0 :init, 1 : disable, -1 : enable

var OZExportMemoryStreamCallBack_OZReportViewer = parentScope.exportFunction;

var call_prefix = "";//"call_shim_";
// create tripath service manager
var saManager = new ServiceManager(tripathEdgeServerPath);

// listen response message from other client to this client in same transaction route
// params: ['from_clientindex', 'actioncommand', 'formid', 'value', 'arg_4']
saManager.responseCallBack = function(param_){
    var ozv = OZReportViewer; // this ozviewer

    if(param_[0] !== clientNo){ // if from_clientindex in the same transaction route is not this client "2"
        if(param_[1] === "enableozviewer"){
            if(ozv) {
                if(check_disable > 0){
                    ozv.script("enable_input_all");
                }
                check_disable = -1;
            }
        } else {
            ozv.Document_TriggerExternalEvent(call_prefix+param_[1], param_[2], param_[3], param_[4]);
        }
    }
};

// request client (customer: "2") authentication
// param: userName
// to be in the same transaction route, then need userName of clients (agent: "1", customer: "2") have to be same
saManager.requestAuthCustomer(tripathKey);

// listen input event of ozviewer
function OZEFormInputEventCommand_OZReportViewer(docindex, formid, eventname, mainscreen) {
    var ozv = OZReportViewer; // running ozviewer

    if(eventname === "OnValueChanged") {
        // send message from client "2" to other client in same transaction route
        // params: ['from_clientindex', 'actioncommand', 'formid', 'value']
        saManager.sendMessage([clientNo, "setvalue", formid, ozv.Document_TriggerExternalEvent(call_prefix+"getvalue", formid)]); // set value for 'formid' of other ozviewer client in same transaction route
    }else if(eventname === "OnFocus" && mainscreen === "false") {
        // send message from client "2" to other client in same transaction route
        // params: ['from_clientindex', 'actioncommand', 'formid']
        saManager.sendMessage([clientNo, "focus", formid]);
    }else if(eventname === "OnKillFocus" && mainscreen === "false") {
        // send message from client "2" to other client in same transaction route
        // params: ['from_clientindex', 'actioncommand', 'formid']
        saManager.sendMessage([clientNo, "killfocus", formid]);
    }
}

// listen onProgress command of OZViewer
function OZProgressCommand_OZReportViewer(step, state, reportname) {
    //console.log("OZProgressCommand_OZViewer step=" + step + ", state=" + state);
    if(step == 4 && state == 2) { // if this viewer complete binding
        // send message from client "2" to other client in same transaction route
        // params: ['from_clientindex', 'actioncommand']

        //saManager.sendMessage(["2", "enableozviewer"]);
        //TODO: Do not use timeout
        //saManager.sendMessage(["2", "enableozviewer"]);
        //console.log("before sendMessage(2, enableozviewer)");
        setTimeout(function(){ saManager.sendMessage([clientNo, "enableozviewer"]);  }, 3000);
        // enable ozviewer in other client in same transaction route
        if(check_disable >= 0){
            var ozv = OZReportViewer;

            if(ozv) {
                ozv.script("disable_input_all");
                check_disable = 1;
            }
        }
    }
}

function OZUserActionCommand_OZReportViewer(type, attr) {
    var ozv = OZReportViewer;
    if(type === "CommentDraw") {
        var MyObj = eval('(' + attr + ')');
        saManager.sendMessage([clientNo, "setcomment", MyObj.pageindex, ozv.Document_TriggerExternalEvent("getcomment", MyObj.pageindex)]);
    }else if(type === "CommentErase") {
        var MyObj = eval('(' + attr + ')');
        saManager.sendMessage([clientNo, "setcomment", MyObj.pageindex, ozv.Document_TriggerExternalEvent("getcomment", MyObj.pageindex)]);
    }else if(type === "CommentClear") {
        var MyObj = eval('(' + attr + ')');
        if(MyObj.pageindex == "0"){
            saManager.sendMessage([clientNo, "clearcomment"]);
        }else{
            saManager.sendMessage([clientNo, "setcomment", MyObj.pageindex, ozv.Document_TriggerExternalEvent("getcomment", MyObj.pageindex)]);
        }
    }else if(type === "MovePage") {
        var MyObj = eval('(' + attr + ')');
        saManager.sendMessage([clientNo,"movepage", MyObj.reportname, MyObj.index]);
    }
}


function ZTInstallEndCommand_ZTransferX(param1,param2) {
    Create_Div();
    Initialize_OZViewer("OZReportViewer", "CLSID:0DEF32F8-170F-46f8-B1FF-4BF7443F5F25", "100%", "95%", "application/OZRViewer");
    if (ozParams) {
        Insert_OZViewer_Param("connection.servlet", serverPath + "server");
        Insert_OZViewer_Param("connection.reportname", reportPath);
        Object.keys(ozParams).forEach(function (key) {
            // key: the name of the object key
            // index: the ordinal position of the key within the object
            var value = ozParams[key];
            if (Array.isArray(value) && value.length > 0) {
                value.forEach(function (item) {
                    Insert_OZViewer_Param(key, item);
                });
            } else {
                Insert_OZViewer_Param(key, value);
            }
        });
        Insert_OZViewer_Param("windowless", "true");
    }

    // Insert_OZViewer_Param("connection.servlet", "https://osd.ozsaas.com/server");
    // Insert_OZViewer_Param("connection.reportname", "/Komercijalnabanka/KB_v1.2.ozr");
    // Insert_OZViewer_Param("viewer.external_functions_path", "ozp://js/eform.js");
    // Insert_OZViewer_Param("odi.odinames", "kb,DataService,ExtendedData");
    // Insert_OZViewer_Param("odi.kb.pcount", "1");
    // Insert_OZViewer_Param("odi.kb.args1", "customer_no=0");
    // Insert_OZViewer_Param("eform.signpad_type", "dialog");
    // //Insert_OZViewer_Param("information.writelogfile", "true");
    // Insert_OZViewer_Param("comment.triggercommandinterval", "100");
    // Insert_OZViewer_Param("comment.all", "true");
    // Insert_OZViewer_Param("comment.selectedpen", "fillbackground");
    // Insert_OZViewer_Param("comment.fillbackground", "true");
    // //Insert_OZViewer_Param("comment.drawborder", "true");
    // //Insert_OZViewer_Param("eform.highlightpen_guide", "reorder");
    // Insert_OZViewer_Param("multiscreen.highlightpen_guide", "false");
    //
    // Insert_OZViewer_Param("comment.selectedpen", "highlightpen");
    // Insert_OZViewer_Param("viewer.isframe", "false");
    // Insert_OZViewer_Param("viewer.namespace", "tripathmirroring\\ozviewer");
    // Insert_OZViewer_Param("viewer.viewmode", "fittowidth");
    // Insert_OZViewer_Param("information.debug", "true");
    // Insert_OZViewer_Param("toolbar.all", "true");
    // //Insert_OZViewer_Param("connection.pcount", "3");
    // //Insert_OZViewer_Param("connection.args1", "role=user");
    // //Insert_OZViewer_Param("connection.args2", "newForm=true");
    // //Insert_OZViewer_Param("connection.args3", "processorData=" + processorData);
    // //Insert_OZViewer_Param("connection.args3", "processorData={'userName': 'CMS', 'cselNo': '12345'}");
    //
    //
    // // tripath mirroring ozparams >>
    // Insert_OZViewer_Param("eform.inputeventcommand", "true");
    // Insert_OZViewer_Param("eform.usemultiscreen", "true");
    // Insert_OZViewer_Param("multiscreen.screentype", "device4monitor");
    // Insert_OZViewer_Param("viewer.useractioncommand", "true");
    // Insert_OZViewer_Param("viewer.progresscommand", "true");
    // Insert_OZViewer_Param("etcmenu.multiscreenguide", "true");
    // //Insert_OZViewer_Param("viewer.externaleventshim", code);
    // Insert_OZViewer_Param("eform.show_prev_next_input", "true");

    // << tripath mirroring ozparams
    check_disable = 0;

    Start_OZViewer();

    var params = "";
    var delimiter = "$oz@";

    if (ozClientParams) {
        params += delimiter + "connection.servlet=" + serverPath + "server";
        params += delimiter + "connection.reportname=" + reportPath;
        Object.keys(ozClientParams).forEach(function (key) {
            // key: the name of the object key
            // index: the ordinal position of the key within the object
            var value = ozClientParams[key];
            if (Array.isArray(value) && value.length > 0) {
                value.forEach(function (item) {
                    params += delimiter + key + "=" + item;
                });
            } else {
                params += delimiter + key + "=" + value;
            }
        });
    }

    // params += delimiter + "connection.servlet=https://osd.ozsaas.com/server";
    // params += delimiter + "connection.reportname=/Komercijalnabanka/KB_v1.2.ozr";
    // params += delimiter + "viewer.external_functions_path=ozp://js/eform.js";
    // params += delimiter + "odi.odinames=kb,DataService,ExtendedData";
    // params += delimiter + "odi.kb.pcount=1";
    // params += delimiter + "odi.kb.args1=customer_no=0";
    // params += delimiter + "eform.signpad_type=dialog";
    // //params += delimiter + "eform.signpad_type=zoom";
    // params += delimiter + "viewer.isframe=false";
    // params += delimiter + "viewer.viewmode=fittowidth";
    // //params += delimiter + "toolbar.all=true";
    // params += delimiter + "toolbar.all=false";
    // //params += delimiter + "connection.pcount=3";
    // //params += delimiter + "connection.args1=role=user";
    // //params += delimiter + "connection.args2=newForm=true";
    // params += delimiter + "viewer.exportcommand=true";
    // //params += delimiter + "connection.args3=processorData=" + processorData;
    // //params += delimiter + "connection.args3=processorData={'userName': 'CMS', 'cselNo': '12345'}";
    // //params += delimiter + "comment.all=true";
    // //params += delimiter + "comment.selectedpen=fillbackground";
    // //params += delimiter + "comment.fillbackground =true";
    // //params += delimiter + "comment.drawborder=true";
    // params += delimiter + "multiscreen.highlightpen_guide=false";
    // params += delimiter + "eform.show_prev_next_input=true";
    //
    // // tripath mirroring ozparams >>
    // params += delimiter + "viewer.progresscommand=true";
    // params += delimiter + "viewer.reportchangecommand=true";
    // params += delimiter + "viewer.useractioncommand=true";
    // params += delimiter + "eform.inputeventcommand=true";
    // params += delimiter + "eform.usemultiscreen=true";
    // params += delimiter + "multiscreen.screentype=subscreen";
    // //params += delimiter + "viewer.externaleventshim="+code;
    // // << tripath mirroring ozparams

    // TODO: do not use settimeout
    //console.log("before sendMessage(2, initozviewer)");
    setTimeout(function(){ saManager.sendMessage([clientNo, "initozviewer", params, delimiter]);  }, 2000);
    //saManager.sendMessage(["2", "initozviewer", params, delimiter]);
}
