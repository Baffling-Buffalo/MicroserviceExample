'use strict';

function activeXiFrameViewerCtrl($scope, $log, $window) {

    // in case endsWith is missing, like Safari
    if (!String.prototype.endsWith) {
        String.prototype.endsWith = function(searchString, position) {
            var subjectString = this.toString();
            if (typeof position !== 'number' || !isFinite(position) || Math.floor(position) !== position || position > subjectString.length) {
                position = subjectString.length;
            }
            position -= searchString.length;
            var lastIndex = subjectString.indexOf(searchString, position);
            return lastIndex !== -1 && lastIndex === position;
        };
    }

    var serverPath           = location.origin + location.pathname.substring(0, location.pathname.lastIndexOf("/")) + "/"; // it might be load from .html

    var parentScope = $window.parent.angular.element($window.frameElement).scope();
    if(parentScope) {
        // retrieve value from father layer
        var ozParams   = parentScope.ozParams;
        var ozEvents   = parentScope.ozEvents;
        var reportPath = parentScope.ozReportPath;

        var clientNo              = parentScope.clientNo;
        var tripathEdgeServerPort = parentScope.tripathPort;
        // var tripathEdgeServerPath = location.protocol + "//" + location.hostname + ":" + tripathEdgeServerPort;
        var tripathEdgeServerPath = "https://osd.ozsaas.com:14127";

        var check_disable = 0; // 0 :init, 1 : disable, -1 : enable

        var OZExportMemoryStreamCallBack_OZViewer = parentScope.exportFunction;

        if (ozEvents) {
            $log.info("Using addition event...");
            // OZUserEvent_OZViewer = ozEvents(parentScope, OZViewer);
        }

        // listen onProgress command of ozViewer
        var OZProgressCommand_OZViewer        = function(step, state, reportname) {
            //console.log("OZProgressCommand_OZReportViewer step=" + step + ", state=" + state);
            if(step === 4 && state === 2) { // if this viewer complete binding
                // send message from client "2" to other client in same transaction route
                // params: ['from_clientindex', 'actioncommand']

                //saManager.sendMessage(["2", "enableozviewer"]);
                //TODO: Do not use timeout
                //saManager.sendMessage(["2", "enableozviewer"]);
                //console.log("before sendMessage(2, enableozviewer)");
                setTimeout(function(){ saManager.sendMessage([clientNo, "enableozviewer"]);  }, 3000);
                // enable ozviewer in other client in same transaction route
                if(check_disable >= 0){
                    var ozv = OZViewer;

                    if(ozv) {
                        ozv.script("disable_input_all");
                        check_disable = 1;
                    }
                }
            }
        };

        var OZUserActionCommand_OZViewer      = function(type, attr) {
            var MyObj = eval('(' + attr + ')');
            switch(type) {
                case "CommentClear":
                    if(MyObj.pageindex == "0") {
                        saManager.sendMessage(["2","clearcomment"]);
                        break;
                    }
                case "CommentDraw":
                case "CommentErase":
                    saManager.sendMessage([clientNo,"setcomment",MyObj.pageindex, OZViewer.Document_TriggerExternalEvent("getcomment", MyObj.pageindex)]);
                    break;
                case "MovePage":
                    saManager.sendMessage(["2","movepage",MyObj.reportname, MyObj.index]);
                    break;
            }
        };

        var OZEFormInputEventCommand_OZViewer = function(docindex, formid, eventname, mainscreen) {
            var ozv = OZReportViewer; // running ozviewer
            switch(eventname) {
                case "OnValueChanged":
                    // send message from client "2" to other client in same transaction route
                    // params: ['from_clientindex', 'actioncommand', 'formid', 'value']
                    saManager.sendMessage([clientNo, "setvalue", formid, ozv.Document_TriggerExternalEvent(call_prefix+"getvalue", formid)]); // set value for 'formid' of other ozviewer client in same transaction route
                    break;
                case "OnFocus":
                    if(mainscreen == "false") {
                        // send message from client "2" to other client in same transaction route
                        // params: ['from_clientindex', 'actioncommand', 'formid']
                        saManager.sendMessage([clientNo, "focus", formid]);
                    }
                    break;
                case "OnKillFocus":
                    if(mainscreen == "false") {
                        // send message from client "2" to other client in same transaction route
                        // params: ['from_clientindex', 'actioncommand', 'formid']
                        saManager.sendMessage([clientNo, "killfocus", formid]);
                    }
                    break;
            }

        };

        var ZTInstallEndCommand_ZTransferX    = function(param1,param2) {

            Create_Div();
            Initialize_OZViewer("OZViewer", "CLSID:0DEF32F8-170F-46f8-B1FF-4BF7443F5F25", "100%", "98%", "application/OZRViewer");
            Insert_OZViewer_Param("connection.servlet", serverPath + "server");
            Insert_OZViewer_Param("connection.reportname", reportPath);
            // Insert_OZViewer_Param("viewer.external_functions_path", "ozp://js/eform.js");
            // Insert_OZViewer_Param("odi.odinames", "kb,DataService,ExtendedData");
            // Insert_OZViewer_Param("odi.kb.pcount", "1");
            // Insert_OZViewer_Param("odi.kb.args1", "customer_no=0");
            $scope.ozParams = ozParams;
            if (ozParams) {
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
            }

            // Insert_OZViewer_Param("eform.signpad_type", "dialog");
            // Insert_OZViewer_Param("comment.triggercommandinterval", "100");
            // Insert_OZViewer_Param("comment.all", "true");
            // Insert_OZViewer_Param("comment.selectedpen", "fillbackground");
            // Insert_OZViewer_Param("comment.fillbackground", "true");
            // Insert_OZViewer_Param("multiscreen.highlightpen_guide", "false");
            // Insert_OZViewer_Param("comment.selectedpen", "highlightpen");
            // Insert_OZViewer_Param("viewer.isframe", "false");
            // Insert_OZViewer_Param("viewer.viewmode", "fittowidth");
            // Insert_OZViewer_Param("information.debug", "true");
            // Insert_OZViewer_Param("toolbar.all", "true");

            // tripath mirroring mandatory ozparams >>
            Insert_OZViewer_Param("eform.inputeventcommand",    "true");
            Insert_OZViewer_Param("eform.usemultiscreen",       "true");
            Insert_OZViewer_Param("multiscreen.screentype",     "device4monitor");
            Insert_OZViewer_Param("viewer.useractioncommand",   "true");
            Insert_OZViewer_Param("viewer.progresscommand",     "true");
            Insert_OZViewer_Param("viewer.namespace",           "tripathmirroring\\ozviewer");
            Insert_OZViewer_Param("etcmenu.multiscreenguide",   "true");
            Insert_OZViewer_Param("eform.show_prev_next_input", "true");
            // << tripath mirroring mandatory ozparams

            check_disable = 0;

            Start_OZViewer();

            var params = "";
            var delimiter = "$oz@";

            params += delimiter + "connection.servlet=" + serverPath + "server";
            params += delimiter + "connection.reportname=" + reportPath;
            Object.keys(ozParams).forEach(function (key) {
                // key: the name of the object key
                // index: the ordinal position of the key within the object
                var value = ozParams[key];
                if (Array.isArray(value) && value.length > 0) {
                    value.forEach(function (item) {
                        params += delimiter + key + "=" + item;
                    });
                } else {
                    params += delimiter + key + "=" + value;
                }
            });

            // params += delimiter + "viewer.external_functions_path=ozp://js/eform.js";
            // params += delimiter + "odi.odinames=kb,DataService,ExtendedData";
            // params += delimiter + "odi.kb.pcount=1";
            // params += delimiter + "odi.kb.args1=customer_no=0";
            // params += delimiter + "eform.signpad_type=dialog";
            // params += delimiter + "viewer.isframe=false";
            // params += delimiter + "viewer.viewmode=fittowidth";
            // params += delimiter + "toolbar.all=false";
            // params += delimiter + "viewer.exportcommand=true";
            // params += delimiter + "multiscreen.highlightpen_guide=false";
            // params += delimiter + "eform.show_prev_next_input=true";

            // tripath mirroring ozparams >>
            params += delimiter + "eform.inputeventcommand=true";
            params += delimiter + "eform.usemultiscreen=true";
            params += delimiter + "multiscreen.screentype=subscreen";
            params += delimiter + "viewer.useractioncommand=true";
            params += delimiter + "viewer.progresscommand=true";
            params += delimiter + "viewer.reportchangecommand=true";
            // << tripath mirroring ozparams

            // TODO: do not use settimeout
            setTimeout(function(){ saManager.sendMessage([clientNo, "initozviewer", params, delimiter]);  }, 2000);
        };

        var exportReport = function () {
            $log.debug("viewerService - uploadFile");

            var params = "";
            params += "\n" + "export.mode=silent";
            params += "\n" + "export.confirmsave=false";
            params += "\n" + "export.path=/sdcard";
            params += "\n" + "ozd.filename=export_test.ozd";
            params += "\n" + "export.format=ozd";
            params += "\n" + "export.saveonefile=true";
            // params += "\n" + "ozd.password=1111";

            //html5 viewer submit
            // TODO : call validation function
            var _totalPage = OZViewer.GetInformation("TOTAL_PAGE");
            //alert(_totalPage);
            var _isvalid = true;
            for (var i = 1; i <= _totalPage && _isvalid; i++) {
                _isvalid = OZViewer.GetInformation("INPUT_CHECK_VALIDITY_PAGE_AT=" + i) === "valid";
            }
            if (!_isvalid) {
                return;
            } else {
                // OZViewer.ScriptEx("save_memorystream", params, "\n");	//html5
                setTimeout(function () {
                    if (_isvalid) {
                        OZViewer.ScriptEx("save_memorystream", params, "\n");	//html5
                    }
                }, 1000);
            }
        };

        $scope.uploadFileFunc = function () {
            exportReport();
        };

        var saManager = new ServiceManager(tripathEdgeServerPath);

        // listen response message from other client to this client in same transaction route
        // params: ['from_clientindex', 'actioncommand', 'formid', 'value', 'arg_4']
        saManager.responseCallBack = function(param_) {
            var ozv = OZViewer; // this ozviewer

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
        saManager.requestAuthCustomer("9b3");

        // if(IsNeedSha1_ZT())
        //     Initialize_ZT("ZTransferX", "CLSID:C7C7225A-9476-47AC-B0B0-FF3B79D55E67", "0", "0", serverPath + "custom_components/oz-viewer/activeX/ZTransferX_2,2,5,3_SHA1.cab#version=2,2,5,3", "application/OZTransferX_1027");
        // else
        //     Initialize_ZT("ZTransferX", "CLSID:C7C7225A-9476-47AC-B0B0-FF3B79D55E67", "0", "0", serverPath + "custom_components/oz-viewer/activeX/ZTransferX_2,2,5,3.cab#version=2,2,5,3", "application/OZTransferX_1027");
        // Insert_ZT_Param("download.server", serverPath + "custom_components/oz-viewer/activeX/");
        // Insert_ZT_Param("download.port", location.port);
        // Insert_ZT_Param("download.instruction", "ozrviewer.idf");
        // Insert_ZT_Param("install.base", "<PROGRAMS>/Forcs");
        // Insert_ZT_Param("install.namespace", "tripathmirroring");
        // Start_ZT();

    } else {
        $log.error("Missing report details");
    }
}

angular
    .module('ozViewer')
    // only use this one in IE - ActiveX Viewer
    // Support for OZR only, do not support open OZD file
    .component('iFrameMirrorViewer', {
        templateUrl: 'custom_components/oz-viewer/web/oz-viewer.mirror.template.html',
        controller: ['$scope', '$log', '$window', activeXiFrameViewerCtrl]
    });
    // .component('iFrameMirrorViewer', {
    //     templateUrl: 'custom_components/oz-viewer/web/oz-viewer.mirror.template.2.html',
    //     controller: function() {}
    // });