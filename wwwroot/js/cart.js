$(document).ready(function () {
	var cart = GetCookie('EmptyShoppingCard');
	if (cart == "" || cart == undefined) {
		$("#emptyCart").hide();
		$(".Main_col").show();
	}
	else {
		$(".Main_col").hide();
		$("#emptyCart").show();
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
	}
});