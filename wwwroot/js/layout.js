$(document).ready(function () {
	var user = GetCookie('UserLoggedIn');
	if (user == "" || user == undefined) {
		$(".loggedIn").hide();
	} else {
		$(".notLoggedIn").hide();
	}
	var user = GetCookie('ShoppingCard');
	if (user == "" || user == undefined) {
		$(".Subtot").hide();
		$(".Afrekenen").hide();
	} 
	else {
		$(".ShoppingLeeg").hide();
	}
	var width = $(window).width();
	StyleDropdown();

	$(window).resize(function () {
		width = $(window).width();
		StyleDropdown();
	});

	function StyleDropdown() {
		if (width >= 575) {
			var dropdown = $(".dropdown-menu");
			dropdown.css("background-color", "#f2f2f2");
			dropdown.removeClass("bg-transparent border-0");
		}
		else {
			var dropdown = $(".dropdown-menu");
			dropdown.css("background-color", "");
			dropdown.addClass("bg-transparent border-0");
		}
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