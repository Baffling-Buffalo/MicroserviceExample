function ozhtml5iFrameViewerCtrl($scope, $log, $window) {
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

    var serverPath = location.origin + location.pathname.substring(0, location.pathname.lastIndexOf("/")) + "/"; // it might be load from .html

    var parentScope = $window.parent.angular.element($window.frameElement).scope();
    if(parentScope) {
        // retrieve value from father layers
        var ozParams = parentScope.ozParams;
        var ozClientParams   = parentScope.ozClientParams;
        var ozEvents = parentScope.ozEvents;
        //var reportPath = parentScope.ozReportPath;
var reportPath ="/FILIJALA/Proba.ozr";
        var reportType = reportPath.substr(reportPath.lastIndexOf(".") + 1, reportPath.length);
        var reportName = reportPath.substr(reportPath.lastIndexOf("/") + 1, reportPath.length);

        OZExportMemoryStreamCallBack_OZViewer = parentScope.exportFunction;

        SetOZParamters_OZViewer = function () {
            var oz = document.getElementById("OZViewer");

            if (serverPath.endsWith("html")) {
                serverPath += "/../";
            }
            if (reportType === "ozd") {
                oz.sendToActionScript("connection.openfile", serverPath + "upload/" + reportName);
            } else {
                oz.sendToActionScript("connection.servlet", serverPath + "server");
                oz.sendToActionScript("connection.reportname", reportPath);
            }

            oz.sendToActionScript("information.debug", "true");

            if (ozParams) {
                Object.keys(ozParams).forEach(function (key) {
                    // key: the name of the object key
                    // index: the ordinal position of the key within the object
                    var value = ozParams[key];
                    if (Array.isArray(value) && value.length > 0) {
                        value.forEach(function (item) {
                            oz.sendToActionScript(key, item);
                        });
                    } else {
                        oz.sendToActionScript(key, value);
                    }
                });
            }

            if(ozClientParams) {
                // parameter for client
                var params = "";
                var delimiter = "$oz@";
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

                setTimeout(function () {
                    saManager.sendMessage([clientNo, "initozviewer", params, delimiter])
                }, 2000);
            }

        };

        // for mirroring only
        if(ozClientParams) {
            var clientNo              = parentScope.clientNo    ? parentScope.clientNo    : DEFAULT_REMOTE_CLIENT_NO;
            var tripathKey            = parentScope.tripathKey  ? parentScope.tripathKey  : DEFAULT_TRIPATH_KEY;
            var tripathEdgeServerPort = parentScope.tripathPort ? parentScope.tripathPort : DEFAULT_TRIPATH_PORT;
            // var tripathEdgeServerPath = location.protocol + "//" + location.hostname + ":" + tripathEdgeServerPort;
            var tripathEdgeServerPath = "https://osd.ozsaas.com:14127";

            var check_disable = 0;

            var call_prefix = "";//"call_shim_";
            var saManager = new ServiceManager(tripathEdgeServerPath);
            // listen response message from other client to this client in same transaction route
            // params: ['from_clientindex', 'actioncommand', 'formid', 'value', 'arg_4']
            saManager.responseCallBack = function(param_){
                var ozv = OZViewer; // this ozviewer

                if(ozv && param_[0] !== clientNo) { // if from_clientindex in the same transaction route is not this client "2"
                    if (param_[1] === "enableozviewer") {
                        if (check_disable > 0) {
                            ozv.script("enable_input_all");
                        }
                        check_disable = -1;
                    } else {
                        ozv.Document_TriggerExternalEvent(call_prefix + param_[1], param_[2], param_[3], param_[4]);
                    }
                }
            };

            // request client (customer: "2") authentication
            // param: userName
            // to be in the same transaction route, then need userName of clients (agent: "1", customer: "2") have to be same
            saManager.requestAuthCustomer(tripathKey);

            // listen input event of ozviewer
            OZEFormInputEventCommand_OZViewer = function(docindex, formid, eventname, mainscreen) {
                var ozv = OZViewer; // running ozviewer

                if(eventname === "OnValueChanged") {
                    // send message from client "2" to other client in same transaction route
                    // params: ['from_clientindex', 'actioncommand', 'formid', 'value']
                    saManager.sendMessage([clientNo, "setvalue", formid, ozv.Document_TriggerExternalEvent(call_prefix+"getvalue", formid)]); // set value for 'formid' of other ozviewer client in same transaction route
                }else if(eventname === "OnFocus") { // there is no mainscreen option in HTML5
                    // send message from client "2" to other client in same transaction route
                    // params: ['from_clientindex', 'actioncommand', 'formid']
                    saManager.sendMessage([clientNo, "focus", formid]);
                }else if(eventname === "OnKillFocus") { // there is no mainscreen option in HTML5
                    // send message from client "2" to other client in same transaction route
                    // params: ['from_clientindex', 'actioncommand', 'formid']
                    saManager.sendMessage([clientNo, "killfocus", formid]);
                }
            };

            // listen onProgress command of OZViewer
            OZProgressCommand_OZViewer = function(step, state, reportname) {
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
                        var ozv = OZViewer;

                        if(ozv) {
                            ozv.script("disable_input_all");
                            check_disable = 1;
                        }
                    }
                }
            };

            OZUserActionCommand_OZViewer = function(type, attr) {
                var ozv = OZViewer;
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

        }

        if (ozEvents) {
            $log.info("Using addition event...");
            OZUserEvent_OZViewer = ozEvents(parentScope, OZViewer);
        }

        var uploadFile = function () {
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
            uploadFile();
        };

        var opt = [];
        /*Object.keys(parentScope.optional).forEach(function (key) {
            opt[key] = parentScope.optional[key];
        });*/

        start_ozjs("OZViewer", "custom_components/oz-viewer/ozhtml5/", opt);
    } else {
        $log.error("Missing report details");
    }
}


angular
    .module('ozViewer')
    .component('iFrameViewer', {
        templateUrl: 'custom_components/oz-viewer/web/oz-viewer.template.html',
        controller: ['$scope', '$log', '$window', ozhtml5iFrameViewerCtrl]
    });