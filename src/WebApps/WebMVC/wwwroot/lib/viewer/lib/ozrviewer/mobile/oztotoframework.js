var OZTotoFramework=null;
OZTotoFramework=function(){
OZTotoFramework=function(){return OZTotoFramework;};
var $runtime = 0; var simul = false;
var _res = null;
var _args = null;
var $exec = function (opcode, args) {
	if (/iPhone|iPad|iPod/i.test(navigator.userAgent)) {
		if(!simul){
			var error=null;
			try {
				var req=new XMLHttpRequest();
				req.open('GET',  '/!ozexec', false);
				req.setRequestHeader("runtime", $runtime);
				req.setRequestHeader("opcode", opcode);
				if(args!=null)req.setRequestHeader("args", encodeURIComponent(JSON.stringify(args)));
				req.send(null);
				switch(req.status){
				case 200:return eval(req.responseText);
				case 500:error=new Error(req.responseText);break;
				case 404:simul=true;break;
				}
			}catch(e){}
			if(error)throw error;
		}
		if(simul){console.debug("OZTotoFramework::exec> op:'"+opcode+"' args:"+JSON.stringify(args));return 0;}
	}
	else if (/Android/i.test(navigator.userAgent)) {
        return eval(_toto_interface_.exec($runtime,opcode,JSON.stringify(args)));
	}
	else if (/WebView/i.test(navigator.userAgent)) {
		if ($runtime == null) return;
		var url = 'ozexec?' + 'runtime=' + $runtime + '&' + 'opcode=' + opcode + '&' + 'jsonobject=' + encodeURIComponent(JSON.stringify(args));
		if (url.length > 2083) {
			_args = encodeURIComponent(JSON.stringify(args));
			url = 'ozexec?' + 'runtime=' + $runtime + '&' + 'opcode=' + opcode;
		}
		document.location.href = url;
		return _res;
	}
}
    
var $bind=function(obj,className){obj.__class__=className;$exec("bind",{__class__:className,__id__:obj.__id__});}
var $$=OZTotoFramework.prototype;
$$.__ozexec_result__=undefined;
$$.__version__ = "1.7.0.0";
$$.__id__=0;
$$.__ref__={};
$$.__ar__={};
$$.__types__={};
$$._dispatchCallback = {};
$$._dispatchCallback[0] = null;  // OZTotoFramework.util.confirm
$$._dispatchCallback[1] = null;  // OZTotoFramework.storage.putHTTP
$$._dispatchCallback[2] = null;  // OZTotoPDFViewer.createViewer
$$.__call__=function(obj,name,args){return $exec("call",{__class__:obj.__class__,__id__:obj.__id__,__name__:name,__args__:args});}
$$.__addCallbackListener__=function(name, callback)
{
    OZTotoObject.prototype.addEventListener(name, callback);
}
$$.__dispatchCallback__=function(name, event, index)
{
    setTimeout(function(){
       if ($$._dispatchCallback[index] != null) {
           OZTotoObject.prototype.dispatchEvent(name, event);
           if (index != 2) {
               OZTotoObject.prototype.removeEventListener(name, $$._dispatchCallback[index]);
               $$._dispatchCallback[index] = null;
           }
       }
   });
}
    
$$.__setDivContentRect__=function(divID)
{
    if (document.getElementById(divID) != undefined && document.getElementById(divID) != null) {
        var __left__   = document.getElementById(divID).getBoundingClientRect().left;
        var __top__    = document.getElementById(divID).getBoundingClientRect().top;
        var __width__  = document.getElementById(divID).getBoundingClientRect().width;
        var __height__ = document.getElementById(divID).getBoundingClientRect().height;
        return __left__+","+__top__+","+__width__+","+__height__;
    }
    else {
        console.debug('invalid oztotoframework.js error. setDivContentRect()');
    }
    
    return "";
}

    
OZTotoFramework.__proto__=$$;
OZTotoFramework.__ref__[0]=OZTotoFramework;

$$.__applyResult=function(res) {
	_res=res;
}

$$.__getArgs=function () {
    return _args;
}

$$.__getUserAgent=function() {
	return navigator.userAgent;
}

var $doInit=function(obj,proto,args){
	if(!proto)return;
	var superProto=proto.__proto__;
	if(superProto){
		var superInit=superProto.__init__;
		if(superInit && superInit.length==0)$doInit(obj,superProto);
	}
	if(proto.__init__)proto.__init__.apply(obj,args);
}
$$.__init__=function(obj,$class,args){$doInit(obj,$class.prototype,args);}
$$.__defineClass__ = function(className, superClass){
	if($$.__types__[className]!=undefined)
		throw new Error("already exist class: "+className);
	var $super = superClass?superClass:$$.Object;
	var classObject = eval("var "+className+"=function(){$doInit(this,this.__proto__,arguments);}; "+className);
	classObject.prototype.__proto__ = $super.prototype;
	classObject.prototype.__class__ = className;
	classObject.prototype.__init__ = null;
	$$.__types__[className]=classObject;
	$exec("defineClass",{name:className});
	return classObject;
}

$$.__seqArid__ = 0;
$$.createAsyncReturn=function(return_callback) {
	var arid = "_AR"+(++$$.__seqArid__)+"_";
	var onResult = function(event) {
		this.removeEventListener(arid, onResult);
		if(return_callback) return_callback(event["result"]);
	};
	$$.addEventListener(arid, onResult);
	$$.__ar__[arid]=onResult;
	return arid;
};
$$.cancelAsyncReturn=function(arid) {
	var handler=$$.__ar__[arid];
	$$.removeEventListener(arid, handler);
	delete $$.__ar__[arid];
};
$$.applyAsyncReturn=function(arid,returnValue) {
	$$.dispatchEvent(arid, {"result":returnValue});
	$$.cancelAsyncReturn(arid);
};

var OZTotoObject=function(){$doInit(this,this.__proto__,arguments);};
OZTotoObject.prototype.__init__=function(){
	this.__id__=OZTotoObject.__seq__++;$$.__ref__[this.__id__]=this;
	if(this.__class__)
		$bind(this,this.__class__);
};
OZTotoObject.__seq__=1;
OZTotoObject.prototype.addEventListener=function(eventName,callback){
	if(!this.__events__)this.__events__={};
	var events=this.__events__[eventName];
	if(events==null){
		events=[];
		this.__events__[eventName]=events;
	}
	events.push(callback);
};
OZTotoObject.prototype.removeEventListener=function(eventName,callback){
	if(!this.__events__)return;
	var events=this.__events__[eventName];
	if(events){
		var i=events.indexOf(callback);
		if(i>=0){
			var handler=events.splice(i, 1)[0];
			if(typeof handler=='number')$exec('event.remove',{handler:handler});
		}
		if(!events.length)delete this.__events__[eventName];
	}
};
OZTotoObject.prototype.dispatchEvent=function(eventName,args){
	var event=args?args:({});
	event.__eventName__=eventName;
	try{
		if(!this.__events__) return event;
		var events=this.__events__[eventName];
	
		if (events) {
			for(var i=0;i<events.length;i++){
				var handler=events[i];
				switch(typeof handler){
					case 'function':handler(event);break;
					case 'number':{
						var event2=$exec('event.callback',{handler:handler,event:event});
						for(var k in event2)event[k]=event2[k];
						break;
					}
					default:(0,eval)(handler);break;
				}
			}
		}
		return event;
	}finally{
		$$.__lastevent__ = event;
		$exec("setLastEvent",{event:event});
	}
};
$$.Object=OZTotoObject;
$$.__proto__ = $$.Object.prototype;

$runtime=$exec("init",{location:location.href,version:$$.__version__});
$exec("defineClass",{name:"OZTotoFramework"});
$bind($$,"OZTotoFramework");
window.addEventListener("load",function(){window.setTimeout(function(){$exec("pageLoaded");});});
window.addEventListener("unload",function(){$exec("pageUnload");});
return OZTotoFramework;
}();

(function(){var $=OZTotoFramework.prototype;var $$=OZTotoFramework;

var OZTotoEnvironment = OZTotoFramework.__defineClass__("OZTotoEnvironment");
OZTotoEnvironment.prototype.isOnline=function(){return $$.__call__(this,"isOnline", []);};
OZTotoEnvironment.prototype.getScriptVersion=function(){return $$.__call__(this,"getScriptVersion", []);}
OZTotoEnvironment.prototype.getEngineVersion=function(){return $$.__call__(this,"getEngineVersion", []);}
OZTotoEnvironment.prototype.getAppVersion=function(){return $$.__call__(this,"getAppVersion", []);}
OZTotoEnvironment.prototype.getAppInfo=function(){return $$.__call__(this,"getAppInfo", []);}
OZTotoEnvironment.prototype.getPackageName=function(){return $$.__call__(this,"getPackageName", []);}
OZTotoEnvironment.prototype.getArchivePath=function(){return $$.__call__(this,"getArchivePath", []);}
OZTotoEnvironment.prototype.isNetworkAlive=function(){return $$.__call__(this,"isNetworkAlive", []);};

var OZTotoDevice = OZTotoFramework.__defineClass__("OZTotoDevice");
OZTotoDevice.prototype.getOSName=function(){return $$.__call__(this,"getOSName", []);};
OZTotoDevice.prototype.getOSVersion=function(){return $$.__call__(this,"getOSVersion", []);};
OZTotoDevice.prototype.getModelName=function(){return $$.__call__(this,"getModelName", []);};
OZTotoDevice.prototype.getUUID=function(){return $$.__call__(this,"getUUID", []);};
OZTotoDevice.prototype.getDeviceName=function(){return $$.__call__(this,"getDeviceName", []);};
OZTotoDevice.prototype.getManufacturer=function(){return $$.__call__(this,"getManufacturer", []);};
OZTotoDevice.prototype.getBrand=function(){return $$.__call__(this,"getBrand", []);};

var OZTotoUtil = OZTotoFramework.__defineClass__("OZTotoUtil");
OZTotoUtil.prototype.trace=function(string) {
$$.__call__(this,"trace",[string]);

};
OZTotoUtil.prototype.alert = function (message, title) {
    if (message == null) message = "";
    if (title == null) title = "";
    $$.__call__(this,"alert",[message,title]);
};
OZTotoUtil.prototype.messageService = function (numbers, message) {
    if (numbers == null) numbers = "";
    if (message == null) message = "";
    $$.__call__(this,"messageService",[numbers,message]);
};
OZTotoUtil.prototype.callService = function (number) {
    if (number == null) number = "";
    $$.__call__(this,"callService",[number]);
};
OZTotoUtil.prototype.getPhoneNumber = function () {
    return $$.__call__(this,"getPhoneNumber",[]);
};
OZTotoUtil.prototype.confirm=function(message,title,button1,button2,callback) {
	if ($$._dispatchCallback[0] == null) {
        $$._dispatchCallback[0] = callback;
        if (callback != null)
            $$.__addCallbackListener__("OZTotoUtil.confirm.event", $$._dispatchCallback[0]);

        if (/WebView/i.test(navigator.userAgent)) {
            if (button1 == null || button1 == "") button1 = "OK";
            if (button2 == null) button2 = "";
        }
        $$.__call__(this,"confirm",[message,title,button1,button2]);
    }
};
 
var OZTotoIndicator = OZTotoFramework.__defineClass__("OZTotoIndicator");
OZTotoIndicator.prototype.setOption=function(option) {
    if (option == null)
        return;
$$.__call__(this,"setOption",[option]);
};
OZTotoIndicator.prototype.start=function(width, height) {
    if (width == null)
        width = 0;
    if (height == null)
        height = 0;
$$.__call__(this,"start",[width, height]);
};
OZTotoIndicator.prototype.stop=function(interval) {
    if (interval == null)
        interval = 0;
$$.__call__(this,"stop",[interval]);
};
 
var OZTotoHttpClient = OZTotoFramework.__defineClass__("OZTotoHttpClient");
OZTotoHttpClient.prototype.send=function(options) {
	var ar_error = $$.createAsyncReturn(function(event){
		if(options.error != null) options.error(event);
		$$.cancelAsyncReturn(ar_success);
	});
	var ar_success = $$.createAsyncReturn(function(event){
		if(options.success != null) options.success(event);
		$$.cancelAsyncReturn(ar_error);
	});
	try {
		return $$.__call__(this,"send", [options, ar_success, ar_error]);
	}catch(e){
		$$.cancelAsyncReturn(ar_success);
		$$.cancelAsyncReturn(ar_error);
	}
};

//OZTotoStorageData is OZTotoStorage data wrapper class.
var OZTotoStorageData = OZTotoFramework.__defineClass__("OZTotoStorageData");
OZTotoStorageData.prototype.__init__=function(obj){ this.category = obj.category; this.key = obj.key; };
OZTotoStorageData.prototype.size = function(){ return $$.__call__(this,"size",[this.category,this.key]);}
OZTotoStorageData.prototype.append = function(value){ $$.__call__(this,"append",[this.category,this.key, value]); }
OZTotoStorageData.prototype.get = function(){ return $$.__call__(this,"get",[this.category,this.key]); }

var OZTotoStorage = OZTotoFramework.__defineClass__("OZTotoStorage");
OZTotoStorage.prototype.get=function(categoryName,key){return $$.__call__(this,"get",[categoryName,key]);};
OZTotoStorage.prototype.data=function(categoryName,key){	var obj = $$.__call__(this,"data",[categoryName,key]); if (obj == null){ return null; }else { return new OZTotoStorageData(obj); } };
OZTotoStorage.prototype.put=function(categoryName,key,value){return $$.__call__(this,"put",[categoryName,key,value]);};
OZTotoStorage.prototype.putHTTP=function(categoryName,key,url,callback) {
    if ($$._dispatchCallback[1] == null) {
        $$._dispatchCallback[1] = callback;
        if (callback != null)
            $$.__addCallbackListener__("OZTotoStorage.putHTTP.event", $$._dispatchCallback[1]);
        $$.__call__(this,"putHTTP",[categoryName,key,url]);
    }
};
OZTotoStorage.prototype.remove=function(categoryName,key){return $$.__call__(this,"remove",[categoryName,key]);};
OZTotoStorage.prototype.contains=function(categoryName,key){return $$.__call__(this,"contains",[categoryName,key]);};
OZTotoStorage.prototype.listKeys=function(categoryName){return $$.__call__(this,"listKeys",[categoryName]);};
OZTotoStorage.prototype.containsCategory=function(categoryName){return $$.__call__(this,"containsCategory",[categoryName]);};
OZTotoStorage.prototype.removeCategory=function(categoryName){return $$.__call__(this,"removeCategory",[categoryName]);};
OZTotoStorage.prototype.listCategories=function(){return $$.__call__(this,"listCategories",[]);};

var OZTotoNavigator = OZTotoFramework.__defineClass__("OZTotoNavigator");
//OZTotoNavigator.prototype.goHome=function(){return $$.__call__(this,"goHome",[]);};
OZTotoNavigator.prototype.setVisible=function(visible){ $$.__call__(this,"setVisible",[visible]);};
OZTotoNavigator.prototype.isVisible=function(){return $$.__call__(this,"isVisible",[]);};
OZTotoNavigator.prototype.setMenuButtonVisible=function(visible){ $$.__call__(this,"setMenuButtonVisible",[visible]);};
OZTotoNavigator.prototype.isMenuButtonVisible=function(){return $$.__call__(this,"isMenuButtonVisible",[]);};


$$.device=new OZTotoDevice();
$$.environment=new OZTotoEnvironment();
$$.http=new OZTotoHttpClient();
$$.storage=new OZTotoStorage();
$$.navigator=new OZTotoNavigator();
$$.util=new OZTotoUtil();
$$.indicator=new OZTotoIndicator();
}());

(function(){var $$=OZTotoFramework; /* OZViewer */
//OZExportData is oz.export data wrapper class.
var OZExportData = OZTotoFramework.__defineClass__("OZExportData");
OZExportData.prototype.__init__=function(obj){ this.filename = obj.filename; };
OZExportData.prototype.size = function(){ return $$.__call__(this,"size",[this.filename]);}

var Document = $$.__defineClass__("OZViewerDocument");
var OZViewerDocument = Document.prototype;
OZViewerDocument.__init__=function(viewerId){return $$.__call__(this,"__init__",[viewerId]);}
OZViewerDocument.setChartStyle=function(style){return $$.__call__(this,"SetChartStyle",[style]);};
OZViewerDocument.pingOZServer = function (ipOrUrl, port)
{
    if (/WebView/i.test(navigator.userAgent)) {
        if (port == null) port = -1;
    }
    return $$.__call__(this, "PingOZServer", [ipOrUrl, port]);
};
OZViewerDocument.getGlobal = function (key, nIndex)
{
    if (/WebView/i.test(navigator.userAgent)) {
        if (nIndex == null) nIndex = 0;
    }
    return $$.__call__(this, "GetGlobal", [key, nIndex]);
};
OZViewerDocument.setGlobal = function (key, varValue, nIndex)
{
    if (/WebView/i.test(navigator.userAgent)) {
        if (nIndex == null) nIndex = 0;
    }
    return $$.__call__(this, "SetGlobal", [key, varValue, nIndex]);
};
OZViewerDocument.getTitle=function(){return $$.__call__(this,"GetTitle",[]);};
OZViewerDocument.getPaperWidth=function(){return $$.__call__(this,"GetPaperWidth",[]);};
OZViewerDocument.getPaperHeight=function(){return $$.__call__(this,"GetPaperHeight",[]);};
OZViewerDocument.triggerExternalEvent = function (param1, param2, param3, param4, callback)
{
    var arid = "";
    if (param1 == null) param1 = "";
    if (param2 == null) param2 = "";
    if (param3 == null) param3 = "";
    if (param4 == null) param4 = "";
	param1 = param1 + "";
	param2 = param2 + "";
	param3 = param3 + "";
	param4 = param4 + "";
    if (callback != null) {
        arid = $$.createAsyncReturn(callback);
    }

    return $$.__call__(this, "TriggerExternalEvent", [param1, param2, param3, param4, arid]);
};
OZViewerDocument.triggerExternalEventByDocIndex = function (docIndex, param1, param2, param3, param4, callback)
{
    var arid = "";
    if (param1 == null) param1 = "";
    if (param2 == null) param2 = "";
    if (param3 == null) param3 = "";
    if (param4 == null) param4 = "";
	param1 = param1 + "";
	param2 = param2 + "";
	param3 = param3 + "";
	param4 = param4 + "";
    if (callback != null) {
        arid = $$.createAsyncReturn(callback);
    }

    return $$.__call__(this, "TriggerExternalEventByDocIndex", [docIndex, param1, param2, param3, param4, arid]);
};

var $class2 = $$.__defineClass__("OZPDFViewer");
$$.OZPDFViewer = $class2;
var OZPDFViewer = $class2.prototype;
OZPDFViewer.createViewer=function(viewerTarget,filePath,option,callback)
{
    var isCallback = false;
    $$._dispatchCallback[2] = callback;
    if ($$._dispatchCallback[2] != null) {
        $$.__addCallbackListener__("OZPDFViewer.createViewer.callback", $$._dispatchCallback[2]);
        isCallback = true;
    }
 
    if (option == null) option = new Object();
    return $$.__call__(this,"createViewer",["OZPDFViewer",viewerTarget,filePath,option,isCallback]);
};
OZPDFViewer.setVisible=function(isVisible){return $$.__call__(this,"setVisible",[isVisible]);};
OZPDFViewer.isVisible=function(){return $$.__call__(this,"isVisible",[]);};
OZPDFViewer.dispose=function(){return $$.__call__(this,"Dispose",[]);};

var $class = $$.__defineClass__("OZViewer");
$$.OZViewer = $class;
var OZViewer = $class.prototype;
OZViewer.__init__=function(){
	this.document = new Document(this.__id__);
}
OZViewer.createViewer=function(viewerTarget,param,delimiter,closemessage)
{
	if (closemessage == null) closemessage = "";
	return $$.__call__(this,"createViewer",["OZViewer",viewerTarget,param,delimiter,closemessage]);
};
OZViewer.createReport = function (param, delimiter)
{
    if (/WebView/i.test(navigator.userAgent)) {
        if (delimiter == null) delimiter = "\n";
    }
    return $$.__call__(this, "CreateReport", [param, delimiter]);
};
OZViewer.newReport=function(param,delimiter){return $$.__call__(this,"NewReport",[param,delimiter]);};
OZViewer.setCloseMessage=function(msg){return $$.__call__(this,"setCloseMessage",[msg]);};
OZViewer.setTarget=function(target){return $$.__call__(this,"setTarget",[target]);};
OZViewer.setVisible=function(isVisible){return $$.__call__(this,"setVisible",[isVisible]);};
OZViewer.isVisible=function(){return $$.__call__(this,"isVisible",[]);};
OZViewer.dispose=function(){return $$.__call__(this,"Dispose",[]);};
OZViewer.rebind = function (nIndex, type, param, delimiter, keepediting)
{
    if (/WebView/i.test(navigator.userAgent) || /Android/i.test(navigator.userAgent)) {
        if (param == null) param = "";
        if (delimiter == null) delimiter = "\n";
        if (keepediting == null) keepediting = true;
    }
    return $$.__call__(this, "ReBind", [nIndex, type, param, delimiter, keepediting]);
};
OZViewer.getInformation=function (item,return_callback){return $$.__call__(this,"GetInformation",[item, $$.createAsyncReturn(return_callback)]);};
OZViewer.script=function(str){return $$.__call__(this,"Script",[str]);};
OZViewer.scriptEx=function(cmd,param,delimiter,return_callback){return $$.__call__(this,"ScriptEx",[cmd,param,delimiter, $$.createAsyncReturn(return_callback)]);};
OZViewer.setHelpURL=function(helpURL){return $$.__call__(this,"setHelpURL",[helpURL]);};
OZViewer.updateLayout=function(){return $$.__call__(this,"updateLayout",[]);};
OZViewer.getExportPath=function(){return $$.__call__(this,"getExportPath",[]);};
OZViewer.getExportData=function(filename){var obj = $$.__call__(this,"getExportData",[filename]); if (obj==null){ return null; }else { return new OZExportData(obj); } };
OZViewer.removeExportData=function(filename){var obj = $$.__call__(this,"removeExportData",[filename]);};
}());
