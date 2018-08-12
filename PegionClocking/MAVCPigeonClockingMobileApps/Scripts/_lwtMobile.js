
function redirect(url) {
	//app.scroller().reset();
    
	app.navigate(url);
	//app.showLoading();

}

function locationRedirect(url) {
	//this will re instanciate kendo app
	//document.location.href = url;
	app.navigate(url);
	//app.showLoading();

}

function ErrorDisplay(lblName) {  
   $('#' + lblName).addClass('validationerror');
}

AddAntiForgeryToken = function (data) {
	data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();    
    return data;
};

function uncapitalize(str) {
    return str.substring(0, 1).toLowerCase() + str.substring(1);
}

function validateWithRegex(RegexKey, value, bValueCanBeEmpty) {
	if (!bValueCanBeEmpty || value != '') {
		var validRegex = new RegExp(RegexKey);
		return validRegex.test(value);
	}
	else {
		return true;
	}
}

function DisplayJasonError(e) {	
	var msg = JSON.parse(e.xhr.responseText);
	alert(msg.errorMessage);
}

function CompareWinSessionid( url) {
	var sessionid;
	$.ajax({
		type: "POST",
		url: url,
		dataType: "json",
		success: function (data) {
			var response = data.response;
			//alert(response.Message)
			if (response.Success) {
				sessionid = response.Message;
				if (sessionid != "" && sessionid != uniquewindowid) {
					//alert('session:' + sessionid + ' vs windowid:' + uniquewindowid);
					uniquewindowid = sessionid;
					redirectToPortalMain();
				}
			}
		},
		error: function (xhr, status, error) {
			//alert(error);
		}
	});
}

function ResetGroupButton() {
	var buttongroup = $(".km-buttongroup").children(".km-state-active").parent();
	$(buttongroup).children("li.km-state-active").removeClass("km-state-active");

}

function s4() {
	return Math.floor((1 + Math.random()) * 0x10000)
             .toString(16)
             .substring(1);
};

function guid() {
	return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
}

function formatDollar(value) {
	return kendo.toString(parseFloat(value), 'c');
}

function formatDate(value) {
	return kendo.toString(value, 'MM/dd/yyyy'); 
}

