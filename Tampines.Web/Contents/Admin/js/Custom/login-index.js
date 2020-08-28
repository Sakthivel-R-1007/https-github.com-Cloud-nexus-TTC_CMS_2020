$(document).ready(function () {

	IntializeValidation();
	IntializeModelClose();
	loadCaptcha();
	$("#reload").click(loadCaptcha);
	$('#ForceLoginPopup').modal('show');
	$('.close-modal').hide();
	$("#yes").on("click", function () {
		$("#ForceLoginForm").submit();
	});

	$("#no").on("click", function () {
		$("#ForceLoginPopup").css("display", "none");
	});

	

	$(document).on("click", ".submitBtn", submitPreview);

	function submitPreview() {

		if ($("#LoginForm").valid()) {

			$("#LoginForm").attr({
				action: apppath + "/Login/Index"
			}).removeAttr("target").submit();
		}

	}
});

function IntializeValidation() {

	$("#LoginForm").validate({
		errorPlacement: function (error, element) {
			$("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
		},
		rules: {
			UserName: {
				required: true
			},
			Password: {
				required: true
			},
			Captcha: {
				required: true,
				equalTo: "#SecurityCode"
			}
		},
		messages: {
			UserName: {
				required: "Please enter the UserName.",
			},
			Password: {
				required: "Please enter the password."
			},
			Captcha: {
				required: "Please enter the Captcha.",
				equalTo: "Please enter valid Captcha"
			}
		}
	});
}

function IntializeModelClose() {
	$('a.openModal').click(function (event) {
		$(this).modal({
			fadeDuration: 250,
			showClose: false
		});
		return false;
	});
}





function loadCaptcha() {
	$("#imgCap").val('');
	$.ajax({
		type: "GET",
		url: apppath + '/Login/GetCaptcha',
		contentType: "image/png",
		success: function (data) {
			$('#imgCap').attr('src', "data:image/png;base64," + data.captchaImage);
			$("#SecurityCode").val(data.code);
		},
		error: function (error, txtStatus) {
			console.log(txtStatus);
		}
	});
}







