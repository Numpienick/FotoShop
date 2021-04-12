$(document).ready(function () {
	var user = GetCookie('UserLoggedIn');
	if (user == "" || user == undefined) {
		$(".loggedIn").hide();
		$(".notLoggedIn").show();
	} else {
		$(".notLoggedIn").hide();
		$(".loggedIn").show();
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

	var placeholderElement = $('#modal-placeholder');
	$(document).on('click', '[data-toggle="ajax-modal"]', function () {
		$(".modal-backdrop").remove();
		var url = $(this).data('url');
		$.get(url).done(function (data) {
			placeholderElement.html(data);
			placeholderElement.find('.modal').modal('show');
		});
	});

	placeholderElement.on('click', '[data-save="modal"]', function (event) {
		event.preventDefault();

		var form = $(this).parents('.modal').find('form');
		var actionUrl = form.attr('action');
		var dataToSend = form.serialize();

		$.post(actionUrl, dataToSend).done(function (data) {
			var newBody = $('.modal-body', data);
			placeholderElement.find('.modal-body').replaceWith(newBody);

			var isValid = newBody.find('[name="IsValid"]').val() == 'True';
			if (isValid) {
				placeholderElement.find('.modal').modal('hide');
				location.reload();
			}
		});
	});

	$("#logOut").click(function () {
		var url = $(this).data('url');
		$.get(url).done(function () {
			location.replace("Index");
		});
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

	//Uitzetten rechtermuisknop -> gebruikt om afbeelding niet te laten copiëren
	//Credits to https://stackoverflow.com/questions/24020321/how-to-disable-save-image-as-option-on-right-click/
	$("body").on("contextmenu", "img", function(e) {
		return false;
	});
	
});