
function html5ViewerCtrl($scope, $log, $http, $window, $stateParams, $uibModal, cfpLoadingBar, toastr) {
    var timeout = 3000; // for toastr



    $scope.mode = 'mail';
    $scope.displayReport = true;

    if (!String.prototype.includes) {
        String.prototype.includes = function(search, start) {
            if (typeof start !== 'number') {
                start = 0;
            }

            if (start + search.length > this.length) {
                return false;
            } else {
                return this.indexOf(search, start) !== -1;
            }
        };
    }

    if(this.ozActions) {
        this.ozActions.forEach(function(action) {
            if(action.includes('-')) {
                var actionParts = action.split('-');
                $scope["show" + actionParts[0]] = true;
                $scope["text" + actionParts[0]] = actionParts[1];
            } else {
                $scope["show" + action] = true;
            }
        });
    }

    $scope.ozEvents       = this.ozEvents;
    $scope.optional       = this.ozOpt;
    $scope.ozParams       = this.ozParams;
    $scope.ozClientParams = this.ozClientParams;
    $scope.ozPopupOptions = this.ozPopupOptions;
    $scope.ozPopupDefault = this.ozPopupDefault;

    $scope.tripathPort    = $stateParams.tripathPort;
    $scope.clientNo       = $stateParams.clientNo;
    $scope.tripathKey     = $stateParams.tripathKey;

    if(this.ozCustomParams) {
        angular.extend($scope.ozParams, this.ozCustomParams);
        if(this.ozClientParams) {
            angular.extend($scope.ozClientParams, this.ozCustomParams);
        }
    }

    $scope.additionInfo = {};
    if(this.ozCompletedReport) {
        var completeReport = this.ozCompletedReport.data;
        if(completeReport) {
            $scope.additionInfo.completeNo = completeReport.completeNo;
            $scope.additionInfo.email      = completeReport.completeEmail;
            $scope.additionInfo.formName   = completeReport.completeFilename.substr(0, completeReport.completeFilename.lastIndexOf("."));

            $scope.ozReportPath = $scope.additionInfo.formName + ".ozd";

            $scope.viewTitle = completeReport.completeCompanyName;
        } else {
            return;
        }
    } else {
        $scope.ozReportPath = $stateParams.reportPath;
        var start = $scope.ozReportPath.lastIndexOf("/") + 1;

        $scope.additionInfo.completeNo   = "0";
        $scope.additionInfo.formName     = $scope.ozReportPath.substr(start, $scope.ozReportPath.lastIndexOf(".") - start);
        $scope.additionInfo.confirmState = this.confirmState;

        $scope.shareLink  = location.origin + location.pathname.substring(0, location.pathname.lastIndexOf("/")) + "/#/ozpreview?reportPath=" + $scope.ozReportPath;
    }

    $scope.additionInfo.mailTitle      = this.mailTitle;
    $scope.additionInfo.images         = this.images ? this.images.join(",") : undefined;
    $scope.additionInfo.emailTemplate  = this.emailTemplate;

    $scope.reportLink = location.origin + location.pathname.substring(0, location.pathname.lastIndexOf("/")) + "/" +
                        (this.viewerPage ? this.viewerPage : "reportPage.html") + "#/" +
                        (this.viewerState ? this.viewerState : "ozhtml5");

    $scope.exportFunction = function(outputdata) {
        if(outputdata === "{}" ){
            $log.debug("Fail to Export Memory Stream ");
        } else {
            if ($scope.afterExportAction) {
                $scope.afterExportAction(function() {
                    uploadToServer(outputdata);
                });
            } else
                uploadToServer(outputdata);
        }
    };

    $scope.uploadReportForm = function() {
        document.getElementById("iFOZViewer").contentWindow.angular.element("#OZViewer").scope().uploadFileFunc();
    };

    this.copyClipboard = function() {
        toastr.info("Copied to clipboard!", "Done", {
            timeout: timeout,
            closeButton: true
        });
    };

    this.submitWithFormEmail = function() {
        var json = document.getElementById("iFOZViewer").contentWindow.OZViewer.GetInformation("INPUT_JSON_ALL");
        var obj = JSON.parse(json);
        $scope.additionInfo.email = obj.email;
        $scope.mode = 'mail';

        $scope.uploadReportForm();
    };

    // when loading OZD and using the email from previous steps
    this.submitWithExistingEmail = function() {
        $scope.mode = 'submit';

        $scope.uploadReportForm();
    };

    this.sendEmail = function () {
	
        $scope.afterExportAction = function(continueAction) {
            executeWithEmail(function() {return function(mail) {
                $scope.mode = 'mail';

                if(mail.all) {
                    $scope.additionInfo.email = '';
                    continueAction();
                } else if (mail.address && mail.address.trim()) {
                    $scope.additionInfo.email = mail.address;
                    continueAction();
                }
            }});
        };
        $scope.uploadReportForm();
    };

    this.submitReport = function (showMailDialog) {

        $scope.afterExportAction = function(continueAction) {
            executeWithEmail(function() {return function(mail) {
                $scope.mode = 'submit';

                if (mail.all) {
                    $scope.additionInfo.email = '';
                } else if (mail.address && mail.address.trim()) {
                    $scope.additionInfo.email = mail.address;
                } else {
                    $scope.additionInfo.email = 'undefined';
                }

                continueAction();
            }});
        };
        $scope.uploadReportForm();
    };
	 this.submitReportDocName = function (showMailDialog) {

        $scope.afterExportAction = function(continueAction) {
            executeWithDocName(function() {return function(inputDocName) {
                $scope.mode = 'submit';

                if (inputDocName.all) {
                    $scope.additionInfo.email = '';
                } else if (inputDocName.address && inputDocName.address.trim()) {
                    $scope.additionInfo.email = mail.address;
                } else {
                    $scope.additionInfo.email = 'undefined';
                }
				$scope.additionInfo.formName = /*$scope.additionInfo.formName + "."+*/ inputDocName; //this is doc name
                continueAction();
            }});
        };
        $scope.uploadReportForm();
    };

	this.submitReportSuccess = function (showMailDialog) {
	 $scope.afterExportAction = function(continueAction) {
                $scope.mode = 'submit';
				inputDocName = '';
                if (inputDocName.all) {
                    $scope.additionInfo.email = '';
                } else if (inputDocName.address && inputDocName.address.trim()) {
                    $scope.additionInfo.email = mail.address;
                } else {
                    $scope.additionInfo.email = 'undefined';
                }
				$scope.additionInfo.formName = $scope.additionInfo.formName + "."+ inputDocName; //this is doc name
                continueAction();
        };
        $scope.uploadReportForm();
    };
	
    this.submitAll = function () {
        $scope.mode = 'submit';

        $scope.additionInfo.email = '';
        $scope.uploadReportForm();
    };

    this.sendMultiEmailInputType = function (mode) {
        $scope.afterExportAction = function(continueAction) {
            var json = document.getElementById("iFOZViewer").contentWindow.OZViewer.GetInformation("INPUT_JSON_ALL");
            var obj = JSON.parse(json);
            executeWithEmailMultiTypeInput(continueAction, mode, obj.email, $scope.ozPopupOptions, $scope.ozPopupDefault);
        };
        $scope.uploadReportForm();
    };
	this.startMultiScreen = function(){
		var companyName = $window.localStorage.getItem('SESSION_USER_COMPANY');
		var userEmail = $window.localStorage.getItem('SESSION_USER_EMAIL');
		var url = location.origin + location.pathname.substring(0, location.pathname.lastIndexOf("/")) + "/wacomtest.html?name=" + $stateParams.reportPath+"&companyname="+companyName+"&useremail="+userEmail;
		//var myshell = new ActiveXObject("Shell.Application");
		//myshell.ShellExecute("iexplore.exe", "-noframemerging " + url, "", "open", 1);
		window.open(url);

	}	

	
    var executeWithEmailMultiTypeInput = function(continueAction, mode, defaultEmail, options, defaultOption) {
        var uibModalInstance = $uibModal.open({
            component: 'multiEmailInputType',
            resolve: {
                options: function() {
                    return options;
                },
                defaultOption: function() {
                    return defaultOption;
                },
                email: function() {
                    return defaultEmail;
                }
            }
        });
        uibModalInstance.result.then(function(input){
            $scope.mode = mode;

            switch(input.type) {
                case 'formEmail':
                    var json = document.getElementById("iFOZViewer").contentWindow.OZViewer.GetInformation("INPUT_JSON_ALL");
                    var obj = JSON.parse(json);
                    $scope.additionInfo.email = obj.email;
                    break;
                case 'customEmail':
                    $scope.additionInfo.email = input.email;
                    break;
                case 'SMS':
                default:
                    toastr.warn("Do not support this type yet", "Warning", {
                        timeout: timeout,
                        closeButton: true
                    });
                    break;
            }
            continueAction();
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };

    var executeWithEmail = function(handler) {
        $scope.displayReport = false;
        var uibModalInstance = $uibModal.open({
            templateUrl: 'custom_components/oz-viewer/web/oz-viewer.popup.html',
            controller: PopupCont,
            resolve: {
                allOption: function() {
                    return $scope.showSubmitAll;
                }
            }
        });
        uibModalInstance.result.then(function(result) {
            $scope.displayReport = true;
            handler()(result);
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
            $scope.displayReport = true;
        });
    };

	var executeWithDocName = function(handler) {
        $scope.displayReport = false;
        var uibModalInstance = $uibModal.open({
            templateUrl: 'custom_components/oz-viewer/web/oz-viewer.popup.docName.html',
            controller: PopupDocNameCont,
            resolve: {
                allOption: function() {
                    return $scope.showSubmitAll;
                }
            }
        });
        uibModalInstance.result.then(function(result) {
            $scope.displayReport = true;
            handler()(result);
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
            $scope.displayReport = true;
        });
    };
	
	
    var uploadToServer = function(outputdata) {
        cfpLoadingBar.start();

        $log.debug("Success to Export Memory Stream ");
        $log.debug("outputdata :: " + outputdata);
		
        var obj = eval('(' + outputdata + ')');
        var formdata = new FormData();
        var index = 1;
		
        for(var key in obj) {
            formdata.append("file_name_" + index, key.replace("/sdcard/",""));
            formdata.append("file_stream_" + index, obj[key]);
            Object.keys($scope.additionInfo).forEach(function(key) {
                // key: the name of the object key
                // index: the ordinal position of the key within the object
                formdata.append(key, $scope.additionInfo[key]);
            });

            index ++;
        }

        $http.post(
            './api/v1/reports',
            formdata, {
                transformRequest: angular.identity,
                headers: {'Content-Type': undefined},
                params: {"mode": $scope.mode }
            }).success(function (data, status, headers, config) {

            toastr.info("Submit completed.", "Update", {
                showCloseButton: true,
                timeout: timeout
            });

            // reload the page
            //$window.location.reload();
			window.location.href = "/OZSaleDemoSystem/#/completeReports/list";
        }).error(function (data, status, headers, config) {
            toastr.error("Cannot submit the report", "Error", {
                showCloseButton: true,
                timeout: timeout
            });
        }).then(function () {
            cfpLoadingBar.complete();	// Loading Bar End
        });
    };

	function getDateTime(){
		
		var currentdate = new Date(); 
		var datetime =  "" + currentdate.getDate() + ""
                + (currentdate.getMonth()+1)  + "" 
                + currentdate.getFullYear() + ""  
                + currentdate.getHours() + ""  
                + currentdate.getMinutes() + "" 
                + currentdate.getSeconds();
		return datetime;
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
	
	
	function deleteReport(){
		$http.delete(
                './api/v1/completed/reports/'+$scope.options).success(function(data, status, headers, config)
            {
                toastr.info("Delete completed report successfully", "Success", {
                    showCloseButton: true,
                    timeout: timeout
                });
            }).error(function(data, status, headers, config) {
                $log.error("Error occurs: " + data);
                toastr.error("Cannot delete completed report", "Error", {
                    showCloseButton: true,
                    timeout: timeout
                });
            }).finally(function() {
                methods.getCompletedList($scope);
                cfpLoadingBar.complete();	// Loading Bar End
            });
	}
	
}

function PopupCont($scope, $uibModalInstance, $log, $http, $window, toastr, allOption) {
    $scope.mail = {};
    $scope.companyName = $window.localStorage.getItem('SESSION_USER_COMPANY');
    $scope.customers = [];

    // enable this option to display checkbox for all users
    $scope.all = allOption;

    $scope.ok = function () {
		
        $uibModalInstance.close($scope.mail);
        $log.debug("Scope.data ---------" + $scope.mail);
    };
    $scope.close = function () {
        $uibModalInstance.dismiss('cancel');
    };

    if(allOption) {
        let req = {
            method: 'GET',
            url: './api/customer/v1/customers',
            dataType: 'json',
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        };

        $http(req).success(function (response, status, headers, config) {
            $scope.customers = response.data;
        }).error(function (data, status, headers, config) {
            toastr.error("Cannot get customer list", "Error");
            $log.error("Error details");
            $log.error(data);
        }).finally(function (data) {
        });
    }
}

function PopupDocNameCont($scope, $uibModalInstance, $log, $http, $window, toastr, allOption) {
    $scope.mail = {};
    $scope.companyName = $window.localStorage.getItem('SESSION_USER_COMPANY');
    $scope.customers = [];

    // enable this option to display checkbox for all users
    $scope.all = allOption;

    $scope.ok = function () {
		//$scope.additionInfo.formName = $scope.docName;
        $uibModalInstance.close($scope.docName);
        $log.debug("Scope.data ---------" + $scope.docName);
    };
    $scope.close = function () {
        $uibModalInstance.dismiss('cancel');
    };

    if(allOption) {
        let req = {
            method: 'GET',
            url: './api/customer/v1/customers',
            dataType: 'json',
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        };

        $http(req).success(function (response, status, headers, config) {
            $scope.customers = response.data;
        }).error(function (data, status, headers, config) {
            toastr.error("Cannot get customer list", "Error");
            $log.error("Error details");
            $log.error(data);
        }).finally(function (data) {
        });
    }
}


angular
    .module('ozViewer')
    .controller('webWrapperCtrl', ['$scope', '$log', '$http', '$window', '$stateParams', '$uibModal', 'cfpLoadingBar', 'toastr', html5ViewerCtrl])
    .component('html5Viewer', {
        templateUrl: 'custom_components/oz-viewer/web/oz-viewer.wrapper.template.html',
        controller: 'webWrapperCtrl',
        bindings: {
            ozActions     : '<',
            ozPopupOptions: '<?',
            ozPopupDefault: '<?',

            ozEvents         : '<?',
            ozParams         : '<',
            ozCustomParams   : '<?',
            ozOpt            : '<?',
            ozCompletedReport: '<?',

            mailTitle    : '<?',
            images       : '<?',
            emailTemplate: '<?',
            confirmState : '<?',

            viewTitle: '<?'
        }
    })
    .component('blankHtml5Viewer', {
        templateUrl: 'custom_components/oz-viewer/web/oz-viewer.wrapper.blank.html',
        controller: 'webWrapperCtrl',
        bindings: {
            ozActions: '<',
            ozPopupOptions: '<?',
            ozPopupDefault: '<?',

            ozEvents         : '<?',
            ozParams         : '<',
            ozCustomParams   : '<?',
            ozOpt            : '<?',
            ozCompletedReport: '<?',

            mailTitle    : '<?',
            images       : '<?',
            emailTemplate: '<?',
            confirmState : '<?',

            viewTitle    : '<?'
        }
    });