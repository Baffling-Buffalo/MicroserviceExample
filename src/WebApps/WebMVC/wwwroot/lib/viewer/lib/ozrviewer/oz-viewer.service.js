function reportService($http, toastr) {
    var timeout = 3000;
    var webPath = location.origin + location.pathname.substring(0, location.pathname.lastIndexOf("/"));
    return {
        loadCustomParams : function ($stateParams) {
            if($stateParams.customParams) {
                var params = JSON.parse(window.atob($stateParams.customParams));
                var count = 1;

                var keys = Object.keys(params);
                var result = { "connection.pcount": keys.length };
                keys.forEach(function(key) {
                    result["connection.args" + count++] = key + "=" + params[key];
                });

                return result;
            }
            return {};
        },
        getDefaultOZHTML5ViewerParams: function() {
            return {
                "global.use_preview_progressbar": "false",
                //"viewer.external_functions_path"      : "ozp://js/eform.js",
                "viewer.usetoolbar"    : "true",
                "viewer.useprogressbar": "false",
                "viewer.usestatusbar"  : "false",

                "viewer.viewmode"      : "normal",
                "viewer.zoom"          : "100",
                "viewer.maxzoom"       : "300",

                "viewer.bgcolor"       : "edf5fa",
                "viewer.useoutborder"  : "false",
                "viewer.useinborder"   : "false",
                "viewer.pagedisplay"   : "singlepagecontinuous",

                "eform.signpad_type"   : "dialog",
                "eform.signpad_zoom"                  : "1000",
                "eform.signpad_highlightcolor"        : "FF0000",
                "eform.signpad_iconposition"          : "sign_bottom",
                "eform.signpad_prev_next_iconposition": "true",
                "eform.show_prev_next_input"          : "true",
                "eform.textbox_use_highlight"         : "true",
                "eform.functionbutton_previewonly"    : "true",
                "eform.resourceparam": "window_bgcolor=CC000000,title_bgcolor=CC000000,title_fontcolor=ffffff,title_fontsize=18,"
                                         + "complete_url=" + webPath + "/custom_components/oz-viewer/ozhtml5/theme/btn-done-text.png,"
                                         + "reset_url="+ webPath + "/custom_components/oz-viewer/ozhtml5/theme/btn-clear-text.png"
            };
        },
        getDefaultOZRemoteViewerParams: function() {
            var param = this.getDefaultOZHTML5ViewerParams();
            return angular.merge(param, {
                "viewer.usetoolbar"    : "true",
                "viewer.isframe"       : "false",

                "eform.signpad_type": "dialog",
                "comment.triggercommandinterval": "100",
                "comment.all": "true",
                "comment.fillbackground": "true",
                "comment.selectedpen": "highlightpen",
                "multiscreen.highlightpen_guide": "false",
                "viewer.namespace": "tripathmirroring\\ozviewer",
                "viewer.viewmode": "fittowidth",
                "information.debug": "true",

                // tripath parameters
                "eform.inputeventcommand"    : "true",
                "eform.usemultiscreen"       : "true",
                "multiscreen.screentype"     : "device4monitor",
                "viewer.useractioncommand"   : "true",
                "viewer.progresscommand"     : "true",
                "etcmenu.multiscreenguide"   : "true",
                "eform.show_prev_next_input" : "true"
            });
        },
        getDefaultOpt: function() {
            return {
                "signpad_dialog_fullscreen" : "viewer"
            };
        },
        getDefaultTotoViewerParams: function() {
            return {
                //"viewer.external_functions_path"      : "ozp://js/eform.js",
                "viewer.viewmode"                     : "fittowidth",
                "viewer.usetoolbar"                   : "true",
                "viewer.progresscommand"              : "true",

                "eform.signpad_type"   : "dialog",
                "eform.signpad_zoom"                  : "1000",
                "eform.signpad_highlightcolor"        : "FF0000",
                "eform.signpad_iconposition"          : "sign_bottom",
                "eform.signpad_prev_next_iconposition": "true",
                "eform.show_prev_next_input"          : "true",
                "eform.textbox_use_highlight"         : "true",
                "eform.functionbutton_previewonly"    : "true",

                "eform.imagepicker_camera_show_choose_button": "true",
                "eform.imagepicker_camera_facingmode"        : "back",
                "eform.resourceparam"   : "window_bgcolor=CC000000,title_bgcolor=CC000000,title_fontcolor=ffffff,title_fontsize=18,"
                                        + "complete_url=" + webPath + "/custom_components/oz-viewer/mobile/theme/btn-tick.png,"
                                        + "reset_url="+ webPath + "/custom_components/oz-viewer/mobile/theme/btn-trash.png",

                "font.fontnames" : "font1",
                "font.font1.name": "Arial",
                "font.font1.url" : webPath + "/resources/fonts/arial.ttf",
            }
        },
        getDefaultOZClientViewerParams: function() {
            var param = this.getDefaultTotoViewerParams();
            return angular.extend(param, {
                "viewer.usetoolbar"    : "false",

                "toolbar.all"          : "false",
                "eform.isframe"        : "false",
                "eform.show_prev_next_input": "true",
                "multiscreen.hightlightpen_guide": "false",
                "viewer.exportcommand" : "true",
                "viewer.viewmode"      : "fittowidth",

                // tripath parameters
                "viewer.progresscommand"    : "true",
                "viewer.reportchangecommand": "true",
                "viewer.useractioncommand"  : "true",
                "eform.inputeventcommand"   : "true",
                "eform.usemultiscreen"      : "true",
                "multiscreen.screentype"    : "subscreen"
            });
        }
    }

}

angular.module('ozViewer')
    .service('ozReportService', reportService);