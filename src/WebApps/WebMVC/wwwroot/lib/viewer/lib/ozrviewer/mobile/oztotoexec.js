/**
 * @author PHAM
 * @param $http
 * @param $log
 * @param $window
 * @param cfpLoadingBar
 * @param toaster
 * @returns {___anonymous127_5153}
 */
function oztotoService($http, $log, $window, cfpLoadingBar) {
	
	var timeout = 30000;
	var methods = {
		initOZTotoViewer: function($scope, reloadPage) {
			$log.debug("oztotoService - initOZTotoViewer");

			var submitReport = function(mode, formdata, ozviewer) {
                var req = {
                    method: 'POST',
                    url: './api/v1/reports', // actionType can be 'submit' or 'mail'
                    params: {
                        "mode": mode
                    },
                    dataType: 'json',
                    transformRequest: angular.identity,
                    headers: {
                        'Content-Type': undefined	// case attachment
                    },
                    data: formdata
                };

                $http(req).then(function (response, status, headers, config) {
                    $log.info("Submit report successfully");

                    alert($scope.successMessage);

                    ozviewer.dispose();
                    cfpLoadingBar.complete();	// end progress bar
                    // stop OZ indicator
                    OZTotoFramework.indicator.stop(100);

                    // remove OZ listener event
                    ozviewer.removeEventListener("OZProgressCommand");
                    ozviewer.removeEventListener("OZExportMemoryStreamCallBack");

                    if($scope.osdResultHandler) {
						$scope.osdResultHandler(response.data.return_filename);
					} else if(reloadPage) {
						$window.location.reload();
					} else {
						$window.history.back();
					}


                }, function (data, status, headers, config) {
                    $log.error("Cannot submit report");
                    cfpLoadingBar.complete();	// end progress bar
                    // stop OZ indicator
                    OZTotoFramework.indicator.stop(100);
                }).catch(function (data, status) {
                    // catch error
                    $log.debug("error", status, data);
                });

			};

			var ozviewer = new OZTotoFramework.OZViewer();

			ozviewer.addEventListener("OZProgressCommand", function(event){
				if(event.step === '4' && event.state === '2') { // Progress is completed
                    ozviewer.updateLayout();
                }
			});
			
			ozviewer.addEventListener("OZExportMemoryStreamCallBack", function(event){
				$log.debug("oztotoService - OZExportMemoryStreamCallBack");
				cfpLoadingBar.start();	// Loading Bar Start
				
				if(event.outputdata === "{}" ) {
					// toaster.pop({
					// 	type: 'error',
					// 	title: 'Export',
					// 	body: 'Fail to Export Memory Stream',
					// 	showCloseButton: true,
					// 	timeout: timeout
					// });
                    cfpLoadingBar.complete();	// end progress bar

                    // stop OZ indicator
                    OZTotoFramework.indicator.stop();
				} else {

					var obj = eval('(' + event.outputdata + ')');
					var value = null;
					var formdata = new FormData();
					var index = 1;

					for(var key in obj){
                        value = obj[key];
                        formdata.append("formName", $scope.fileName);
                        formdata.append("email", $scope.cmail);
						formdata.append("attachments", $scope.attachments ? $scope.attachments : "");
						formdata.append("completeNo", $scope.completeNo);
                        formdata.append("paramKeys", $scope.paramKeys);
						formdata.append("paramValues", $scope.paramValues);

						if($scope.additionInfo) {
                            Object.keys($scope.additionInfo).forEach(function(key) {
                                // key: the name of the object key
                                // index: the ordinal position of the key within the object
								formdata.append(key, $scope.additionInfo[key]);
                            });
						}

						formdata.append("file_name_"+index, key.replace("/sdcard/",""));
						formdata.append("file_stream_"+index, value);
						index++;
					}

                    // alert("Build request completely: " + $scope.report);

					if($scope.report !== null && typeof $scope.report !== "undefined") { // submit form of sitesurvey module
						if($scope.actionType) {
                            var mode = 'mail';
							switch($scope.actionType) {
                                case "register":
                                    mode = 'submit';
                                    break;
                                case "mail":
                                    mode = 'mail';
                                    break;
								case "submit":
									mode = 'submit';
									break;
                            }
                            submitReport(mode, formdata, ozviewer);
                        }
					}
				}
			});
		
			return ozviewer;
		},
		runOZViewer: function($scope, ozviewer, ozparam_splitter) {
			$log.debug("oztotoService - runOZViewer");
			
			var reportparam = $scope.servletconnection_url;
			
			$.each($scope.ozparams, function(key, value){
				reportparam = reportparam + ozparam_splitter + value;
			});

			//alert(reportparam);
			$log.debug("oztotoService - runOZViewer >> " + reportparam);
			ozviewer.createViewer($scope.ozviewer_div, reportparam, ozparam_splitter);
			ozviewer.setVisible(true);
		},
		createReport: function($scope, ozviewer, ozparam_splitter) {
			$log.debug("oztotoService - createReport");
			
			var reportparam = $scope.servletconnection_url;
			
			$.each($scope.ozparams, function(key, value){
				reportparam = reportparam + ozparam_splitter + value;
			});
			
			//$log.debug("oztotoService - runOZViewer >> " + reportparam);
			ozviewer.createReport(reportparam, ozparam_splitter);
		}
	};
	
	return methods;
}
angular
    .module('ozViewer')
    .service('oztotoService', oztotoService);