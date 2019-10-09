/**
 * @author PHAM
 *
 * Site Survey Detail Ctrl
 * @param $scope
 * @param $log
 * @param $window
 * @param $location
 * @param $stateParams
 * @param $http
 * @param toastr
 * @param oztotoService
 * @param authService
 *
 */
function ozViewerCtrl($scope, $log, $window, $location, $stateParams, $http, toastr, oztotoService, authService) {
    $log.debug("ozViewerCtrl");

    var timeout = 3000; // for toastr

    if(this.ozActions) {
        this.ozActions.forEach(function(action){
            $scope["show" + action] = true;
        });
    }

    $scope.smallDevice = $window.innerWidth < 700;

    $scope.osdResultHandler = this.osdResultHandler;

    $scope.additionInfo     = {};
    $scope.additionInfo.mailTitle     = this.mailTitle;
    $scope.additionInfo.images        = this.images.join(",");
    $scope.additionInfo.emailTemplate = this.emailTemplate;

    $scope.successMessage   = this.successMessage;

    // get id from $stateParams and put into scope
    $scope.report = {};

    $scope.report.reportPath = $stateParams.reportPath;
    $scope.completeNo        = $stateParams.completeNo ? $stateParams.completeNo : "0";
    $scope.companyName       = $stateParams.companyName;
    $scope.cmail = "";

    var reportType = $scope.report.reportPath.substr($scope.report.reportPath.lastIndexOf(".") + 1, $scope.report.reportPath.length);
    var reportName = $scope.report.reportPath.substr($scope.report.reportPath.lastIndexOf("/") + 1, $scope.report.reportPath.length);

    $log.debug('Report path:: ' + $scope.report.reportPath);
    $log.debug('Report type:: ' + reportType);
    $log.debug('Report name:: ' + reportName);

    // parameters for submitting form
    $scope.fileName = reportName.split(".")[0];

    // init OZ Viewer
    var ozviewer = oztotoService.initOZTotoViewer($scope, $stateParams.reloadPage);
    var ozParams       = this.ozParams;
    var ozCustomParams = this.ozCustomParams;

    $scope.ozviewer_div = 'bottom';

    // OZ parameters
    $scope.ozparams = {};

    if ('ozd' === reportType.toLowerCase()) { // If working status then need to open ozd form

        $scope.servletconnection_url = 'connection.openfile=' + location.origin + location.pathname.substring(0, location.pathname.lastIndexOf("/")) + '/upload/' + reportName + '.ozd';
        $scope.ozparams.allowreplaceformparam = 'ozd.allowreplaceformparam=true'; // allow to update form param data when open ozd file

        $scope.cmail = 'undefined';
    } else {
        $scope.servletconnection_url = 'connection.servlet=' + location.origin + location.pathname.substring(0, location.pathname.lastIndexOf("/")) + '/server';
        $scope.ozparams.reportname = 'connection.reportname=' + $scope.report.reportPath;

        $scope.additionInfo.confirmState = this.confirmState;
    }

    if(ozParams) {
        var count = 1;
        Object.keys(ozParams).forEach(function(key) {
             // key: the name of the object key
             // index: the ordinal position of the key within the object
            var value = ozParams[key];
            if(Array.isArray(value) && value.length > 0) {
                value.forEach(function(item) {
                    $scope.ozparams[count++] = key + "=" + item;
                    $log.debug("Param:: " + key + "=" + item);
                });
            } else {
                $scope.ozparams[count++] = key + "=" + value;
                $log.debug("Param:: " + key + "=" + value);
            }
        });
    }

    if(ozCustomParams) {
        Object.keys(ozCustomParams).forEach(function(key) {
            // key: the name of the object key
            // index: the ordinal position of the key within the object
            var value = ozCustomParams[key];
            $scope.ozparams[count++] = key + "=" + value;
            $log.debug("Param:: " + key + "=" + value);
        });
    }

    // alert(JSON.stringify($scope.ozparams));

    if(this.ozEvents) {
        $log.debug("Register Events");
        this.ozEvents($scope, ozviewer);
    }

    // OZ parameter splitter
    var ozparam_splitter = "&";

    if($scope.showRegister) {
        $scope.attachments   = "Agenda.jpg"
    }
    oztotoService.runOZViewer($scope, ozviewer, ozparam_splitter);

    // OZ toto navigation event listener: physical button
    OZTotoFramework.navigator.addEventListener("action", function(event){
        if (ozviewer.isVisible()) {
            switch(event.button) {
                case "home":
                    break;
                case "back":
                    break;
                case "forward":
                    break;
                case "refresh":
                    break;
            }

            event.handled = true;
        }
    });

    // go back
    $scope.goBack = function() {
        $log.debug("ozViewerCtrl - goBack");

        ozviewer.dispose(); // dispose ozviewer

        $window.history.back(); // back to previous page
    };

    // go previous form pages of ozviewer
    $scope.goPrevious = function() {
        ozviewer.getInformation("INPUT_CHECK_VALIDITY_CURRENT_PAGE", function(result) {
            if (result !== null && result === "valid") {
                ozviewer.getInformation("CURRENT_PAGE", function(currentReportPage) {
                    if ("1" === currentReportPage) {
                        ozviewer.script("prev_report");
                    } else {
                        ozviewer.script("prev");
                    }
                });
            }
        });
    };

    // go next form pages of ozviewer
    $scope.goNext = function() {
        var currentReportTotalPage;
        ozviewer.getInformation("INPUT_CHECK_VALIDITY_CURRENT_PAGE", function(result) {
            if (result !== null && result === "valid") {
                ozviewer.getInformation("CURRENT_REPORT_INDEX", function(currentReportIdx) {
                    ozviewer.getInformation("TOTAL_PAGE_AT=" + currentReportIdx, function(totalPage) {
                        currentReportTotalPage = totalPage;
                        ozviewer.getInformation("CURRENT_PAGE", function(currentPage) {
                            if (currentReportTotalPage === currentPage) {
                                ozviewer.script("next_report");
                            } else {
                                ozviewer.script("next");
                            }
                        });
                    });
                });
            }
        });
    };

    // do submit
    $scope.doSubmit = function() {
        $log.debug("ozViewerCtrl - doSubmit");

        OZTotoFramework.indicator.start(); // start OZ indicator

        if(!$scope.actionType) {
            $scope.actionType = "mail"; // add action type is 'submit' to scope
        }
        ozviewer.getInformation("INPUT_CHECK_VALIDITY", function(result) {
            if ('valid' === result) {
                var exportparams = "viewer.exportcommand=true";
                exportparams += ozparam_splitter + "export.mode=silent";
                exportparams += ozparam_splitter + "export.confirmsave=false";
                exportparams += ozparam_splitter + "export.path=/sdcard";
                exportparams += ozparam_splitter + "ozd.filename=export_temp.ozd";
                exportparams += ozparam_splitter + "export.format=ozd";

                if ($scope.allAttendees) {
                    // var availableList = $scope.customers.filter(customer => customer.attendanceFlag === 'Y')
                    //                                      .map(customer => {return customer.email;})
                    //                                      .slice(0, 5).join("\n");
                    var availableList = [];
                    var count = 0;
                    $scope.customers.forEach(function(customer) {
                        if(customer.attendanceFlag === 'Y' && 'Forcs' === customer.company) {
                            if(count < 5) {
                                availableList.push(customer.email);
                            }
                            count += 1;
                        }
                    });
                    if(count > 5) {
                        availableList.push("and " + (count - 5) + " more emails");
                    }
                    if (confirm("Do you want to send email to :\n" + availableList.join("\n") + "?")) {
                        // save memory stream
                        ozviewer.scriptEx("save_memorystream", exportparams, ozparam_splitter, function (res) {
                        });

                    } else {
                        OZTotoFramework.indicator.stop(100);
                    }
                } else {

                    // if($scope.cmail && 'undefined' !== $scope.cmail) {
                    //     exportparams += ozparam_splitter + "ozd.password=" + $scope.cmail;
                    // } else {
                    // exportparams += ozparam_splitter + "ozd.password=1111";
                    // }

                    // save memory stream
                    ozviewer.scriptEx("save_memorystream", exportparams, ozparam_splitter, function (res) {
                    });
                }
            } else {
                OZTotoFramework.indicator.stop(100);
            }
        });
    };

    $scope.submitNoEmail = function() {
        $log.debug("Do Submit without sending email");
        $scope.actionType = "submit";
        $scope.cmail = 'undefined';
        $scope.doSubmit();
    };

    $scope.submitSingle = function() {

        $log.debug("ozViewerCtrl - doSubmitSingle");

        if(authService) {
            authService.applySession($scope);
        }
        $scope.cmail = prompt("Please enter your email:", $scope.userEmail ? $scope.userEmail : "");
        if($scope.cmail === null) {
            // click cancel
        } else {
            if ($scope.cmail.length === 0) {
                $scope.cmail = 'undefined';
            }
            $scope.doSubmit();
        }
    };

    $scope.submitEmail = function() {
        $log.debug("Submit form with email and finished");
        $scope.actionType = "submit";
        $scope.submitSingle();
    };

    $scope.submitQRScan = function() {
        $scope.cmail = 'undefined';
        var mail = ozviewer.document.getGlobal("cmail", 0);
        if(mail) {
            $scope.cmail = mail;
            $scope.doSubmit();
        }
    };

    // alert("Testing");
    // extract the email from input with formID is 'email'
    $scope.submitWithFormEmail = function() {
        $log.debug("ozViewerCtrl - submitWithFormEmail");
        $scope.actionType = 'submit';
        $scope.sendWithFormEmail();
    };

    $scope.sendWithFormEmail = function() {
        $log.debug("ozViewerCtrl - submitWithFormEmail");

        ozviewer.getInformation("INPUT_CHECK_VALIDITY", function(result){
            if('valid' === result) {
                ozviewer.getInformation("INPUT_JSON_ALL", function (json) {
                    var obj = JSON.parse(json);
                    $scope.cmail = obj.email;
                    $scope.doSubmit();
                });
            }
        });
    };

    $scope.registerCampaign = function() {

        $scope.actionType = "register";

        OZTotoFramework.indicator.start(); // start OZ indicator

        ozviewer.getInformation("INPUT_CHECK_VALIDITY", function(result){
            if('valid' === result) {
                ozviewer.getInformation("INPUT_JSON_ALL", function(json) {

                    var obj = JSON.parse(json);
                    $scope.cmail = obj.email;

                    var body = {
                        campaignName: 'FORCS FinTech Conference 2017',
                        memberType: obj.memberType,
                        memberStatus: obj.memberStatus,
                        firstName: obj.firstName,
                        lastName: obj.lastName,
                        title: obj.title,
                        company: obj.company,
                        phone: obj.phone,
                        email: $scope.cmail
                    };

                    var req = {
                        method: 'PUT',
                        url: './api/customer/v1/customers/' + obj.customerId,
                        dataType: 'json',
                        headers: {
                            'Content-Type': 'application/json; charset=utf-8'
                        },
                        data: JSON.stringify(body)
                    };

                    $http(req).success(function(data, status, headers, config)
                    {
                    }).error(function(data, status, headers, config) {
                        toastr.error(data.msg, data.code, {
                            showCloseButton: true,
                            timeout: timeout
                        });
                    }).then(function(data) {
                        cfpLoadingBar.complete();	// Loading Bar End
                    });

                    var exportparams = "viewer.exportcommand=true";
                    exportparams += ozparam_splitter + "export.mode=silent";
                    exportparams += ozparam_splitter + "export.confirmsave=false";
                    exportparams += ozparam_splitter + "export.path=/sdcard";
                    exportparams += ozparam_splitter + "ozd.filename=export_temp.ozd";
                    exportparams += ozparam_splitter + "export.format=ozd";

                    // save memory stream
                    ozviewer.scriptEx("save_memorystream", exportparams, ozparam_splitter, function(res){
                    });
                });
            }
        });
    };

    // do save
    $scope.doSave = function() {
        $log.debug("ozViewerCtrl - doSave");

        OZTotoFramework.indicator.start(); // start OZ indicator

        $scope.actionType = "save"; // add action type is 'save' to scope

        ozviewer.getInformation("INPUT_CHECK_VALIDITY", function(result){
            if('valid' === result) {
                var exportparams = "viewer.exportcommand=true";
                exportparams += ozparam_splitter + "export.mode=silent";
                exportparams += ozparam_splitter + "export.confirmsave=false";
                exportparams += ozparam_splitter + "export.path=/sdcard";
                exportparams += ozparam_splitter + "ozd.filename=export_temp.ozd";
                exportparams += ozparam_splitter + "export.format=ozd";

                // save memory stream
                ozviewer.scriptEx("save_memorystream", exportparams, ozparam_splitter, function(res){
                    /* if(res == 0){
                     append("pdfExport", "success.");
                     }else {
                     append("pdfExport", "fail.");
                     } */
                });
            }

            OZTotoFramework.indicator.stop();
        });
    };

    var loadCustomerList = function () {
        var req = {
            method: 'GET',
            url: './api/customer/v1/customers',
            dataType: 'json',
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        };

        $http(req).success(function (data, status, headers, config) {
            $scope.customers = data;
        }).error(function (data, status, headers, config) {

        }).then(function (data) {
        });
    };

    if ($scope.showSubmitAll && $scope.showSubmitSingle) {
        loadCustomerList();
    }

    function clean(obj) {
        var propNames = Object.getOwnPropertyNames(obj);
        for (var i = 0; i < propNames.length; i++) {
            var propName = propNames[i];
            if (obj[propName] === null || obj[propName] === undefined) {
                delete obj[propName];
            }
        }
    }

    clean($scope.additionInfo);

}

angular.module('ozViewer')
    .controller('ozViewerMobileCtrl', ['$scope', '$log', '$window', '$location', '$stateParams', '$http', 'toastr', 'oztotoService', 'authService', ozViewerCtrl])
    .component('ozViewer', {
        templateUrl: 'custom_components/oz-viewer/mobile/oz-viewer.template.html',
        controller: 'ozViewerMobileCtrl',
        bindings: {
            ozActions: '<',

            ozParams: '<',
            ozCustomParams: '<',
            ozEvents: '<',

            osdResultHandler: '<?',

            // for mail detail
            mailTitle: '<',
            images: '<',
            emailTemplate: '<?',

            successMessage: '<'
        }
    });