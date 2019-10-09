function ozWebConfig($stateProvider) {
    $stateProvider

        .state('web.forms.ozWeb', {
            url: "/ozweb?completeNo&reportPath&customParams",
            component: "html5Viewer",
            resolve: {
                ozActions: function($stateParams) {
                    // all possible actions:
                    // + Preview
                    // + SendEmail
                    // + SubmitFormEmail
                    // + SubmitAll
                    // + SubmitSingle
                    // + RegisterCustomer
                    // + SubmitQRScan
                    // + SubmitWithExistingEmail
                    // + SendMultiEmailInputType
                    return $stateParams.completeNo && '0' !== $stateParams.completeNo ? ["SubmitWithExistingEmail"] : ["Preview" ,"SendEmail", "SubmitSingle"];
                },

                ozCompletedReport: function($stateParams, $log, completedFactory) {
                    // optional
                    if($stateParams.completeNo && '0' !== $stateParams.completeNo) {
                        return completedFactory.getCompleted($stateParams.completeNo);
                    } else if ($stateParams.completeNo) {
                        $log.error("Invalid completeNo");
                    }
                    return null;
                },

                ozParams: function(ozReportService) {
                    return ozReportService.getDefaultOZHTML5ViewerParams();
                },
                ozCustomParams: function($stateParams, ozReportService) {
                    // optional
                    return ozReportService.loadCustomParams($stateParams);
                },

                ozOpt: function(ozReportService) {
                    return ozReportService.getDefaultOpt()
                },

                images: function($stateParams) {
                    // return [
                    //     "OZLogo.jpg",
                    //     "Tablet-Branch-Banking.jpg"
                    // ];
                    return $stateParams.completeNo && '0' !== $stateParams.completeNo ? ["email.jpg"] : [ "OZ-SDS-Edited.jpg", "email.jpg"];
                },

                mailTitle    : function($stateParams) {
                    // optional
                    return $stateParams.completeNo && '0' !== $stateParams.completeNo ? "[OZ Sales Demo] Thank you for using OZ Sale Demo System" : "";
                }
                // emailTemplate: function($stateParams) {
                //     // optional
                //     return $stateParams.completeNo && '0' !== $stateParams.completeNo ? "fms.user.confirmation.email.template" : "";
                // }
            }
        })
		
        .state('ozConfirm', {
            url: "/ozconfirm?completeNo",
            component: "blankHtml5Viewer",
            resolve: {
                ozActions: function() {
                    return ["SubmitWithExistingEmail"];
                },
                ozCompletedReport: function($stateParams, $log, completedFactory) {
                    // optional
                    if('0' !== $stateParams.completeNo) {
                        return completedFactory.getCompleted($stateParams.completeNo);
                    } else {
                        $log.error("Invalid completeNo");
                        return null;
                    }
                },

                ozParams: function(ozReportService) {
                    var params = ozReportService.getDefaultOZHTML5ViewerParams();
                    var additionParams = {
                        "viewer.usetoolbar" : "false"
                    };
                    additionParams["viewer.usetoolbar"] = "false";
                    if(navigator.userAgent.match("iPhone|iPod|iPad|Android")) {
                        additionParams["viewer.viewmode"] = "fittowidth";
                    }

                    // merging with additionParams
                    angular.extend(params, additionParams);

                    return params;
                },
                ozCustomParams: function($stateParams, ozReportService) {
                    // optional
                    return ozReportService.loadCustomParams($stateParams);
                },
                ozOpt: function(ozReportService) {
                    return ozReportService.getDefaultOpt()
                },

                images: function() {
                    // return [
                    //     "OZLogo.jpg",
                    //     "Tablet-Branch-Banking.jpg"
                    // ];
                    return [
                        "email.jpg"
                    ];
                },
                mailTitle    : function() {
                    // optional
                    return "[OZ Sales Demo] Thank you for using OZ Sale Demo System";
                }
                // emailTemplate: function() {
                //     // optional
                //     return "fms.user.confirmation.email.template";
                // }

            }
        })
        .state('ozPreview', {
            url: "/ozpreview?reportPath&customParams",
            component: "blankHtml5Viewer",
            resolve: {
                ozActions: function () {
                    return [];
                },

                ozParams: function (ozReportService) {
                    var params = ozReportService.getDefaultOZHTML5ViewerParams();
                    var additionParams = {};
                    if (navigator.userAgent.match("iPhone|iPod|iPad|Android")) {
                        additionParams = {
                            "viewer.viewmode": "fittowidth"
                        };
                    }

                    // merging with additionParams
                    angular.extend(params, additionParams);

                    return params;
                },
                ozCustomParams: function ($stateParams, ozReportService) {
                    // optional
                    return ozReportService.loadCustomParams($stateParams);
                },
                ozOpt: function(ozReportService) {
                    return ozReportService.getDefaultOpt()
                }

            }
        })
        .state('web.forms.ozRegister', {
            url: "/ozregister?completeNo&reportPath",
            component: 'html5Viewer',
            resolve: {
                ozActions: function() {
                    return ["SubmitFormEmail"];
                },
                ozParams: function(ozReportService) {
                    return ozReportService.getDefaultOZHTML5ViewerParams();
                },
                ozOpt: function(ozReportService) {
                    return ozReportService.getDefaultOpt()
                },
                ozEvents: function () {
                    return function ($scope, OZViewer) {
                        return function (paramName, paramValue, param3) {

                            // $resource('./api/v1/customer', {}, {
                            //     query: {
                            //         method: 'GET',
                            //         params: {email: '@email'},
                            //         isArray: false
                            //     }
                            // }).get({email: paramValue}, function (customer) {
                            //     // event name - value...
                            //     $scope.paramKeys = "fullName";
                            //     $scope.paramValues = customer.firstName + " " + customer.lastName;
                            //
                            //     $scope.pdfPrefix = "Hello_" + customer.firstName + "_" + customer.lastName;
                            //     OZViewer.Document_TriggerExternalEvent("attendanceInfo", JSON.stringify(customer), "", "");
                            // });

                            OZViewer.Document_TriggerExternalEvent("attendanceInfo",
                                JSON.stringify({
                                    "id": 192,
                                    "campaignName": "FORCS FinTech Conference 2017",
                                    "memberType": "Contact",
                                    "memberStatus": "Registered",
                                    "firstName": paramValue.includes("1") ? "Q" : "Quyen",
                                    "lastName": "Phan",
                                    "title": "Developers",
                                    "company": "FORCS Singapore Pte Ltd",
                                    "phone": "",
                                    "email": paramValue.includes("1") ? "quyen.it1423@gmail.com" : "pvmquyen@forcs.com",
                                    "attendanceFlag": "Y"
                                }), "", "");
                        }
                    }
                },

                mailTitle    : function() {
                    // optional
                    return "Welcome to Forcs' booth";
                }
            }
        })
        .state('web.forms.ozMirror', {
            url: "/ozmirror?reportPath&customParams&clientNo&tripathPort&tripathKey",
            component: "mirrorViewer",
            resolve: {
                ozActions: function() {
                    // all possible actions:
                    // + Preview
                    // + SendEmail
                    // + SubmitFormEmail
                    // + SubmitAll
                    // + SubmitSingle
                    // + RegisterCustomer
                    // + SubmitQRScan
                    // + SubmitWithExistingEmail
                    // + SendMultiEmailInputType
                    return ["Preview" ,"SendEmail", "SubmitSingle"];
                },

                ozParams: function(ozReportService) {
                    return ozReportService.getDefaultOZRemoteViewerParams();
                },
                ozClientParams: function(ozReportService) {
                    return ozReportService.getDefaultOZClientViewerParams();
                },
                ozCustomParams: function($stateParams, ozReportService) {
                    // optional
                    return ozReportService.loadCustomParams($stateParams);
                },

                ozOpt: function(ozReportService) {
                    return ozReportService.getDefaultOpt()
                },

                images: function($stateParams) {
                    // return [
                    //     "OZLogo.jpg",
                    //     "Tablet-Branch-Banking.jpg"
                    // ];
                    return ["OZ-SDS-Edited.jpg", "email.jpg"];
                },

                // viewerPage: function() {
                //     return "mirrorReportPage.html";
                // },
                viewerState: function() {
                    return "mirror";
                    // return "temp";
                }
            }
        })
}
ozWebConfig.$inject = ['$stateProvider'];

angular.module('ozViewer')
    .config(ozWebConfig);