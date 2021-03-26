$(document).ready(function () {
	var user = GetCookie('UserLoggedIn');
	if (user == "") {
		$(".loggedIn").hide();
	} else {
		$(".notLoggedIn").hide();
	}

	// Credits to https://www.w3schools.com/js/js_cookies.asp
	function GetCookie(cname) {
		var name = cname + "=";
		var decodedCookie = decodeURIComponent(document.cookie);
		var ca = decodedCookie.split(';');
		for (var i = 0; i < ca.length; i++) {
			var c = ca[i];
			while (c.charAt(0) == ' ') {
				c = c.substring(1);
			}
			if (c.indexOf(name) == 0) {
				return c.substring(name.length, c.length);
			}
		}
		return "";
	}
});