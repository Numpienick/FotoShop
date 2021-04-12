﻿$(document).ready(function () {
	var user = GetCookie('UserLoggedIn');
	if (user == "" || user == undefined) {
		$(".loggedIn").hide();
	} else {
		$(".notLoggedIn").hide();
	}
	var user1 = GetCookie('EmptyShoppingCard');
	if (user1 == "" || user1 == undefined) {
	 $(".LeegS").hide();
	} 
	else {
		$(".Item_payment").hide();
	}
	var width = $(window).width();
	StyleDropdown();

	$(window).resize(function () {
		width = $(window).width();
		StyleDropdown();
	});

	var span = document.getElementById('someID');
	var count = GetCookie('ShoppingCartI');
	while( span.firstChild ) {
		span.removeChild( span.firstChild );
	}

	if (count == "" || count == undefined){
		$("#someID").hide();
	}
	else{
		span.appendChild( document.createTextNode(GetCookie('ShoppingCartI')) );
	}

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