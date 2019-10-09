/*	var ozParams = {
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
                "multiscreen.screentype"    : "subscreen",
				
				"eform.signpad_type"   : "dialog",
                "eform.signpad_zoom"                  : "1000",
                "eform.signpad_highlightcolor"        : "FF0000",
                "eform.signpad_iconposition"          : "sign_bottom",
                "eform.signpad_prev_next_iconposition": "true",
                "eform.show_prev_next_input"          : "true",
                "eform.textbox_use_highlight"         : "true",
                "eform.functionbutton_previewonly"    : "true"
				
			
            };*/
			
	var ozParams = {
		                "global.use_preview_progressbar": "false",
                //"viewer.external_functions_path"      : "ozp://js/eform.js",
                "viewer.usetoolbar"    : "true",
                "viewer.useprogressbar": "true",
                "viewer.usestatusbar"  : "true",

                "viewer.viewmode"      : "normal",
                "viewer.zoom"          : "100",
                "viewer.maxzoom"       : "300",

                "viewer.bgcolor"       : "edf5fa",
                "viewer.useoutborder"  : "false",
                "viewer.useinborder"   : "false",
                "viewer.pagedisplay"   : "singlepagecontinuous",
"eform.inputeventcommand" : "true",
                "eform.signpad_type"   : "dialog",
                "eform.signpad_zoom"                  : "1000",
                "eform.signpad_highlightcolor"        : "FF0000",
                "eform.signpad_iconposition"          : "sign_bottom",
                "eform.signpad_prev_next_iconposition": "true",
                "eform.show_prev_next_input"          : "true",
                "eform.textbox_use_highlight"         : "true",
                "eform.functionbutton_previewonly"    : "true",
                "eform.resourceparam": "window_bgcolor=CC000000,title_bgcolor=CC000000,title_fontcolor=ffffff,title_fontsize=18,"
                                         + "complete_url=" + "172.31.10.74:8080/OZSaleDemoSystem" + "/custom_components/oz-viewer/ozhtml5/theme/btn-done-text.png,"
                                         + "reset_url="+ "172.31.10.74:8080/OZSaleDemoSystem" + "/custom_components/oz-viewer/ozhtml5/theme/btn-clear-text.png"
	}
	/*function SetOZParamters_OZViewer(){
		var oz;
		oz = document.getElementById("OZViewer");
		oz.sendToActionScript("connection.servlet","http://172.31.10.74:8080/oz7/server");
		oz.sendToActionScript("connection.reportname","AIK/test.ozr");
		
		
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
			
		return true;
	}*/
	   function SetOZParamters_OZViewer(){

       var oz;

       oz = document.getElementById("OZViewer");

       oz.sendToActionScript("connection.servlet","http://172.31.10.74:8080/oz7/server");

       oz.sendToActionScript("connection.reportname","AIK/test.ozr");

       oz.sendToActionScript("eform.inputeventcommand","true");

       return true;

   }

	start_ozjs("OZViewer","http://172.31.10.74:8080/oz7/viewer/lib/ozviewer/", true);
	
	function OZExportMemoryStreamCallBack_OZViewer(outputdata){
		
			if(outputdata === "{}") {
				alert("Export failed");
			} else {
				// upload process - outputdata is binary string
				uploadToServer(outputdata);
			}	
	}

	function uploadDocument(){

		

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
	}

	function uploadToServer(outputdata) {
      //  cfpLoadingBar.start();
		
		var additionInfo = {completeNo: 0, formName:'SimsicFrm', images: 'OZ-SDS-Edited.jpg,email.jpg', email: 'undefined'};
    
        var obj = eval('(' + outputdata + ')');
        var formdata = new FormData();
        var index = 1;
		
        for(var key in obj) {
            formdata.append("file_name_" + index, key.replace("/sdcard/",""));
            formdata.append("file_stream_" + index, obj[key]);
            Object.keys(additionInfo).forEach(function(key) {
                // key: the name of the object key
                // index: the ordinal position of the key within the object
                formdata.append(key, additionInfo[key]);
            });

            index ++;
        }

		$.ajax({
			url: 'http://172.31.10.74:8080/OZSaleDemoSystem/api/v1/reports?mode=submit',
			type: 'POST',
			data: formdata,
			processData: false,
			contentType: false,
			success: function (response) {
				alert("Upload Success!");
			},
			error: function () {
				alert("There was an error");
			}
		}); 

    };