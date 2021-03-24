$(document).ready(function () {
	//var user = GetCookie('UserLoggedIn');
	//if (user == "") {
	//	$(".loggedIn").hide();
	//} else {
	//	$(".notLoggedIn").hide();
	//}

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
				location.reload;
			}
		});
	});	

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
