$(document).ready(function () {

    initializeValidation()

    $(document).on("click", "#previewBtn", submitPreview);
    $(document).on("click", "#publishToLiveBtn", publishToLive);


    function publishToLive() {

        $("#AboutUsIntroductionForm").attr({
            action: apppath + "/Admin/AboutUs/Introduction"
        }).removeAttr("target").submit();

    }



    function submitPreview() {
        $("#AboutUsIntroductionForm").attr({
            action: "/Admin/AboutUs/IntroductionPreview",
            target: "_blank"
        }).submit();
        return false;
    }




});





function initializeValidation() {

    jQuery("#AboutUsIntroductionForm").validate({
        ignore: [],
        debug: false,
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {
          
            Contact: {
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

