function ozMobileConfig($stateProvider) {

    $stateProvider
        .state('ozViewer', {
            url: "/ozviewer?completeNo&reportPath&companyName&customParams&reloadPage",
            component: 'ozViewer',
            resolve: {
                loadPlugin: function ($ocLazyLoad) {
                    return $ocLazyLoad.load('ozTotoFramework', {
                        insertBefore: '#loadBefore',
                        timeout: 300000
                    });
                },

                ozActions: function() {
                    // all possible actions:
                    // + Back
                    // + Navigation
                    // + Save
                    // + SubmitAll
                    // + SubmitSingle
                    // + Register
                    // + SubmitQRScan
                    // + SubmitFormEmail

                    return ["Back","SubmitNoEmail"];
                },

                ozParams: function(ozReportService) {
                    return ozReportService.getDefaultTotoViewerParams();
                },
                ozCustomParams: function($stateParams, ozReportService) {
                    return ozReportService.loadCustomParams($stateParams);
                },
                ozEvents: function() {
                    return null;
                },

                mailTitle: function() { return ""; },
                images: function() {
                    // return [
                    //     "OZLogo.jpg",
                    //     "Tablet-Branch-Banking.jpg"
                    // ];
                    return [
                        "OZ-SDS-Edited.jpg"
                    ];
                },
                // emailTemplate: function() { return ""; },
                successMessage: function() { return "You have successfully submitted."; }
            }
        })
        .state('ozRegisterViewer', {
            url: "/ozregister?completeNo&reportPath&customParams&reloadPage",
            component: 'ozViewer',
            resolve: {
                loadPlugin: function ($ocLazyLoad) {
                    return $ocLazyLoad.load('ozTotoFramework', {
                        insertBefore: '#loadBefore',
                        timeout: 300000
                    });
                },

                ozActions: function() {
                    return ["Back","SubmitFormEmail"];
                },

                ozParams: function(ozReportService) {
                    var params = ozReportService.getDefaultTotoViewerParams();
                    var additionParams = {
                        "viewer.usetoolbar" : "false",
                        "eform.imagepicker_camera_show_choose_button": "true",
                        "eform.imagepicker_camera_facingmode": "front"
                    };

                    // merging with additionParams
                    for (var attrname in additionParams) { params[attrname] = additionParams[attrname]; }

                    return params;
                },
                ozCustomParams: function($stateParams, ozReportService) {
                    return ozReportService.loadCustomParams($stateParams);
                },

                ozEvents: function($log) {
                    return function($scope, ozviewer) {
                        // event name - OZ<value...>
                        $log.debug("Register OZ User Event");
                        ozviewer.addEventListener("OZUserEvent", function(event) {
                            //event.param1
                            //event.param2
                            //event.param3

                            $log.debug("Return the value");
                            ozviewer.document.triggerExternalEvent("attendanceInfo",
                                                                    JSON.stringify({
                                                                        "id":192,
                                                                        "campaignName":"FORCS FinTech Conference 2017",
                                                                        "memberType":"Contact",
                                                                        "memberStatus":"Registered",
                                                                        "firstName": event.param2.includes("1") ? "Q" : "Quyen",
                                                                        "lastName":"Phan",
                                                                        "title":"Developers",
                                                                        "company":"FORCS Singapore Pte Ltd",
                                                                        "phone":"",
                                                                        "email": event.param2.includes("1") ? "quyen.it1423@gmail.com" : "pvmquyen@forcs.com",
                                                                        "attendanceFlag":"Y"
                                                                    }),
                                                                    "param3",
                                                                    "param4");
                        });
                    }
                },

                mailTitle: function() { return "The Digital Revolution in Financial Services"; },
                images: function() {
                    return [
                        "forcs.png",
                        "OZLogo.jpg",
                        "facebook.jpg",
                        "twitter.jpg",
                        "linkedin.jpg",
                        "Agenda.jpg"
                    ];
                },
                emailTemplate: function() { return "email_register_confirmation_template.vm"; },
                successMessage: function() { return "Thank You for Your Registration!"; }
            }
        })
        .state('ozQRcodeScanViewer', {
            url: "/QRcodescan?completeNo&reportPath&companyName&customParams&reloadPage",
            component: 'ozViewer',
            resolve: {
                loadPlugin: function ($ocLazyLoad) {
                    return $ocLazyLoad.load('ozTotoFramework', {
                        insertBefore: '#loadBefore',
                        timeout: 300000
                    });
                },
                ozActions: function() {
                    return ["SubmitQRScan"];
                },

                ozParams:  function(ozReportService) {
                    var params = ozReportService.getDefaultTotoViewerParams();
                    var additionParams = {
                        "viewer.useprogressbar": "false",
                        "viewer.bgcolor": "FFFFFF",
                        "viewer.useinborder": "false",
                        "viewer.pagedisplay": "singlepagecontinuous",
                        "viewer.closetree_at_autohide": "true",
                        "viewer.external_functions_path": ["ozp://js/rws.js", "ozp://js/eform.js"]
                    };

                    // merging with additionParams
                    for (var attrname in additionParams) { params[attrname] = additionParams[attrname]; }

                    return params;
                },
                ozCustomParams: function($stateParams, ozReportService) {
                    return ozReportService.loadCustomParams($stateParams);
                },

                ozEvents: function() {
                    return null;
                },

                mailTitle: function() { return ""; },
                images: function() {
                    return [
                        "OZLogo.jpg",
                        "Tablet-Branch-Banking.jpg"
                    ];
                },
                // emailTemplate: function() { return ""; },
                successMessage: function() { return "You have successfully submitted e-Form data."; }
            }
        })
}

angular.module('ozViewer')
    .config(ozMobileConfig);