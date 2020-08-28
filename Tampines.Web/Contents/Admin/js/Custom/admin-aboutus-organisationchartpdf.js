

$(document).ready(function () {

   
    initializeValidation()
    $('#ChangesUpdate').modal('show');

    $(document).on("click", "#publishToLiveBtn", publishToLive);


    function publishToLive() {

        $("#TownMapForm").attr({
            action: apppath + "/Admin/AboutUs/OrganisationChart"
        }).removeAttr("target").submit();

    }



  

});


function initializeValidation() {


    jQuery.validator.setDefaults({
        ignore: [],
    });



    $(document).on('change', 'input[name=PDFFile]', function () {
        var _URL = window.URL || window.webkitURL;
        var image, file;

       
        var filename = $(this).val();
        if (filename.substring(3, 11) == 'fakepath') {
            filename = filename.substring(12);
        }
        document.getElementById("PDFFileNames").value = filename;
    });

    jQuery("#TownMapForm").validate({
        ignore: [],
        debug: false,
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {
           
            PDFFile: {
               // required: true,
               
                extension: 'pdf'
            }
            
        }
    });




    

}