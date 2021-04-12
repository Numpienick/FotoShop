$(document).ready(function () {
	$("#editPage").click(function () {
		$(".editable").each(function (index, item) {
			$(item).attr('contenteditable', 'true');

			$(item).css("border", "solid 2px");
			$(item).css("border-color", "#dc3545");
		});
		$("#editPage").attr('hidden', 'hidden');
		$("#savePage").removeAttr('hidden');
	});

	$(document).on('click', '#savePage', function () {
		var myData = {
			Photo_id: $("#photoId").val(),
			Title: $("#title").text(),
			Description: $("#description").text(),
			Price: $("#price").text()
		}
		$.ajax({
			url: "/PhotoPage?handler=SavePhoto",
			type: "POST",
			contentType: 'application/json',
			headers: {
				RequestVerificationToken:
					$('input:hidden[name="__RequestVerificationToken"]').val()
			},
			data: JSON.stringify(myData),
			success: function (data) {
				if (data == "success") {
					location.reload();
				}
				if (data == "failed") {
					$("#alert").html($(
						`<div class="alert alert-danger text-center" role="alert">
                        <strong>Oeps!</strong> Er is iets misgegaan, zorg ervoor dat u de prijs goed heeft ingevuld (XXX.XX).
                    </div>`));
				}
			}
		});
	});

	$(document).on('click', '#deleteBtn', function () {
		$(this).attr('hidden', 'hidden');
		$("#confirmDeleteBtn").removeAttr('hidden');
	});
});