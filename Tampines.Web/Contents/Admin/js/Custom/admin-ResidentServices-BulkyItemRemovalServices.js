$(document).ready(function () {

    initializeValidation()

    $(document).on("click", "#previewBtn", submitPreview);
    $(document).on("click", "#publishToLiveBtn", publishToLive);


    function publishToLive() {

        $("#BulkyItemRemovalServicesForm").attr({
            action: apppath + "/Admin/ResidentServices/BulkyItemRemovalServices"
        }).removeAttr("target").submit();

    }



    function submitPreview() {
        $("#BulkyItemRemovalServicesForm").attr({
            action: "/Admin/ResidentServices/BulkyItemRemovalServicesPreview",
            target: "_blank"
        }).submit();
        return false;
    }




});





function initializeValidation() {

    jQuery("#BulkyItemRemovalServicesForm").validate({
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

