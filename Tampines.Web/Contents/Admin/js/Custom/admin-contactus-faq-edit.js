$(document).ready(function () {

    initializeValidation()

    $(document).on("click", "#previewBtn", submitPreview);
    $(document).on("click", "#publishToLiveBtn", publishToLive);


    function publishToLive() {

        $("#AddQuestionForm").attr({
            action: apppath + "/Admin/ContactUs/EditQuestion"
        }).removeAttr("target").submit();

    }



    function submitPreview() {
        $("#AddQuestionForm").attr({
            action: "/Admin/ContactUs/FAQPreview",
            target: "_blank"
        }).submit();
        return false;
    }




});





function initializeValidation() {
    jQuery.validator.setDefaults({
        ignore: [],
    });

    jQuery("#AddQuestionForm").validate({
        ignore: [],
      
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {
            Question: { required: true },
            Content: {
                required: function (e) {
                    CKEDITOR.instances[e.id].updateElement();
                    var editorcontent = e.value.replace(/<[^>]*>/gi, '');
                    return editorcontent.length === 0;
                },
            },
        },
        messages:
        {

          
            Contact: {
                required: "This field is required."
            }
        }
    });

}

