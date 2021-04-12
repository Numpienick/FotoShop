$(document).ready(function () {

	$('#toggleSidebar').on('click', function () {
		$('#sidebar').toggleClass('active');
	});

	var content = $('.content');
	$.get("UserAccount?handler=UpdateUserInfoPartial").done(function (data) {
		content.html(data);
	});

	$(document).on('click', '#updateUserInfo', function () {
		var url = $(this).data('url');
		$.get(url).done(function (data) {
			content.html(data);
		});
	});

	content.on('click', '#submitUser', function () {
		event.preventDefault();
		var form = $(this).parents('.container').find('form');
		var actionUrl = form.attr('action');
		var dataToSend = form.serialize();

		$.post(actionUrl, dataToSend).done(function (data) {
			content.html(data);
			var newBody = $('form', data);
			var updated = newBody.find('[id="validationSum"]').html() != "";
			var isValid = newBody.find('[name="IsValid"]').val() == 'True';
			if (isValid) {
				if (updated) {
					$("#alert").html($(
						`<div class="alert alert-success text-center" role="alert">
                        <strong>Gelukt!</strong> Uw gegevens zijn succesvol gewijzigd.
                    </div>`));
				}
				else {
					$("#alert").html($(
						`<div class="alert alert-danger text-center" role="alert">
                        <strong>Oeps!</strong> Er is iets misgegaan, uw gegevens zijn niet gewijzigd.
                    </div>`));
				}
			}
		});
	});
});