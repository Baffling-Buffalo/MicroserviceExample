'use strict';

function activeXiFrameViewerCtrl($scope, $log, $window) {

    var serverPath      = location.origin + location.pathname.substring(0, location.pathname.lastIndexOf("/")) + "/"; // it might be load from .html

    $scope.uploadFileFunc = function () {
        $log.debug("viewerService - uploadFile");

        var params = "";
        params += "\n" + "export.mode=silent";
        params += "\n" + "export.confirmsave=false";
        params += "\n" + "export.path=/sdcard";
        params += "\n" + "ozd.filename=export_test.ozd";
        params += "\n" + "export.format=ozd";
        params += "\n" + "export.saveonefile=true";

        //html5 viewer submit
        // TODO : call validation function
        var _totalPage = OZReportViewer.GetInformation("TOTAL_PAGE");
        //alert(_totalPage);
        var _isvalid = true;
        for (var i = 1; i <= _totalPage && _isvalid; i++) {
            _isvalid = OZReportViewer.GetInformation("INPUT_CHECK_VALIDITY_PAGE_AT=" + i) === "valid";
        }
        if (!_isvalid) {
            return;
        } else {
            // OZViewer.ScriptEx("save_memorystream", params, "\n");	//html5
            setTimeout(function () {
                if (_isvalid) {
                    OZReportViewer.ScriptEx("save_memorystream", params, "\n");	//html5
                }
            }, 1000);
        }
    };

    if(IsNeedSha1_ZT())
        Initialize_ZT("ZTransferX", "CLSID:C7C7225A-9476-47AC-B0B0-FF3B79D55E67", "0", "0", serverPath + "custom_components/oz-viewer/activeX/ZTransferX_2,2,5,3_SHA1.cab#version=2,2,5,3", "application/OZTransferX_1027");
    else
        Initialize_ZT("ZTransferX", "CLSID:C7C7225A-9476-47AC-B0B0-FF3B79D55E67", "0", "0", serverPath + "custom_components/oz-viewer/activeX/ZTransferX_2,2,5,3.cab#version=2,2,5,3", "application/OZTransferX_1027");
    Insert_ZT_Param("download.server", serverPath + "custom_components/oz-viewer/activeX/");
    Insert_ZT_Param("download.port", location.port);
    Insert_ZT_Param("download.instruction", "ozrviewer.idf");
    Insert_ZT_Param("install.base", "<PROGRAMS>/Forcs");
    Insert_ZT_Param("install.namespace", "tripathmirroring");
    Start_ZT();
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