$(document).ready(function () {

    initializeValidation()
  
    $(document).on("click", "#previewBtn", submitPreview);
    $(document).on("click", ".BackBtn", Backurl);
    $(document).on("click", "#publishToLiveBtn", publishToLive);


    function publishToLive() {
      
        $("#AddLinksContactForm").attr({
            action: apppath + "/Admin/ContactUs/AddLinksContacts"
        }).removeAttr("target").submit();

    }



    function submitPreview() {
        $("#AddLinksContactForm").attr({
            action: "/Admin/ContactUs/Preview",
            target: "_blank"
        }).submit();
        return false;
    }





    





});





function initializeValidation()
{
  
    jQuery("#AddLinksContactForm").validate({
        ignore: [],
        debug: false,
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {
            Organisation: { required: true },
            
            Issue: {
                required: function (e) {
                    CKEDITOR.instances[e.id].updateElement();
                    var editorcontent = e.value.replace(/<[^>]*>/gi, '');
                    return editorcontent.length === 0;
                },

            },
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

            Issue: {
                required: "This field is required."
            },
            Contact: {
                required: "This field is required."
            }
        }
    });

}


function Backurl() {

    window.location.href = "/Admin/ContactUs/ViewLinksContact"
}






