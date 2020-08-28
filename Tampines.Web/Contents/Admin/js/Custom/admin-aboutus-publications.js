$(document).ready(function () {
    $(".Numeric").numeric({
        allowMinus: false,
        allowThouSep: false,
        maxDigits: 4
    });


    $(document).on("click", "#addannualrepot", function () {
        var modal = document.getElementById("addReport");
        $(modal).modal('show');
        AddAnnualrepot();
    });

    $(document).on("click", "#editannualrepot", function () {
        var modal = document.getElementById("editReport");
        $(modal).modal('show');
        EditAnnualrepot($(this).attr("data-uniquecode"));
    });


    $(document).on("click", "#addPressBtn", function () {
        var modal = document.getElementById("addRelease");
        $(modal).modal('show');
        AddPressRelease();
    });


    $(document).on("click", "#editPressbtn", function () {
        var modal = document.getElementById("editRelease");
        $(modal).modal('show');
        EditPressRelease($(this).attr("data-uniquecode"));
    });


});



function AddAnnualrepot() {

    var path = apppath + "/Admin/AboutUs/AddAnnualReports"
    $.ajax({
        url: path,
        //type: 'GET',
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }
          //  $("#EditNewsLetterForm").html('');
            $("#AddAnnualReportForm").html('');
            $("#AddAnnualReportForm").append(response);
            initializeAddAnnualrepotValidation();

        },
        failure: function (response) {
            console.log(response);
            $("#AddAnnualReportForm").append(('Error occured'));
        }
    });
}


function initializeAddAnnualrepotValidation() {
    $(".Numeric").numeric({
        allowMinus: false,
        allowThouSep: false,
        maxDigits: 4
    });

    jQuery.validator.setDefaults({
        ignore: []
    });
    jQuery.validator.addMethod('dimention', function (value, element, param) {
        if (element.files.length == 0) {
            return true;
        }
        var width = $(element).data('imageWidth');
        //alert(width);
        var height = $(element).data('imageHeight');
        //alert(height);
        if (width == param[0] && height == param[1]) {
            return true;
        } else {
            return false;
        }
    }, 'Please upload an image with valid dimension');

    jQuery.validator.addMethod('filesize', function (value, element, param) {
        return this.optional(element) || (element.files[0].size <= param)
    }, 'File size must be less than 2MB');

    $("#AddAnnualReportForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {

            Year: {
                required: true
            },

            Title: {
                required: true

            },
            LargeImage: {
                required: true,
                extension: 'jpg|jpeg|png',
                dimention: [800, 1132]
            },
            FileName: {
                required: true,
                extension: 'pdf'

            },

        }


    });




    $(document).on('change', 'input[name=LargeImage]', function () {
        var _URL = window.URL || window.webkitURL;
        var image, file;

        if ((file = this.files[0])) {
            image = new Image();
            image.onload = function () {
                $('#LargeImage').data('imageWidth', this.width);
                $('#LargeImage').data('imageHeight', this.height);

            };
            image.src = _URL.createObjectURL(file);
        }
        var filename = $(this).val();
        if (filename.substring(3, 11) == 'fakepath') {
            filename = filename.substring(12);
        }
        document.getElementById("LargeImageFileName").value = filename;
    });


    //$("#LargeImage").on("change", function () {
    $(document).on('change', '#LargeImage', function () {
        $("#LargeImageFilePath").attr("src", "");
        var photo = this;
        $img = $("#LargeImageFilePath");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));
    });


    $(document).on('change', '#FileName', function () {
        $("#PdfFileName").val("");

        var filename = $(this).val();
        if (filename.substring(3, 11) == 'fakepath') {
            filename = filename.substring(12);
        }
        document.getElementById("PdfFileName").value = filename;
    });


    $(document).on("click", "#addreportsubmitBtn", addpublishToLive);
   
}

function addpublishToLive() {

    $("#AddAnnualReportForm").attr({
        action: apppath + "/Admin/AboutUs/AddAnnualReports"
    }).removeAttr("target").submit();

}


function EditAnnualrepot(EncryptedId) {

    var path = apppath + "/Admin/AboutUs/EditAnnualReports/" + EncryptedId
    $.ajax({
        url: path,
        //type: 'GET',
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }
            //  $("#EditNewsLetterForm").html('');
            $("#EditAnnualReportForm").html('');
            $("#EditAnnualReportForm").append(response);
            initializeEditAnnualrepotValidation();
            $(".Numeric").numeric({
                allowMinus: false,
                allowThouSep: false,
                maxDigits:4
            });
        },
        failure: function (response) {
            console.log(response);
            $("#EditAnnualReportForm").append(('Error occured'));
        }
    });
}


function initializeEditAnnualrepotValidation() {

    $(".Numeric").numeric({
        allowMinus: false,
        allowThouSep: false,
        maxDigits: 4
    });

    jQuery.validator.setDefaults({
        ignore: []
    });
    jQuery.validator.addMethod('dimention', function (value, element, param) {
        if (element.files.length == 0) {
            return true;
        }
        var width = $(element).data('imageWidth');
        //alert(width);
        var height = $(element).data('imageHeight');
        //alert(height);
        if (width == param[0] && height == param[1]) {
            return true;
        } else {
            return false;
        }
    }, 'Please upload an image with valid dimension');

    jQuery.validator.addMethod('filesize', function (value, element, param) {
        return this.optional(element) || (element.files[0].size <= param)
    }, 'File size must be less than 2MB');

    $("#EditAnnualReportForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {

            Year: {
                required: true
            },

            Title: {
                required: true

            },
            LargeImage: {
               // required: true,
                extension: 'jpg|jpeg|png',
                dimention: [800, 1132]
            },
            FileName: {
                //required: true,
                extension: 'pdf'

            },

        }


    });




    $(document).on('change', 'input[name=LargeImage]', function () {
        var _URL = window.URL || window.webkitURL;
        var image, file;

        if ((file = this.files[0])) {
            image = new Image();
            image.onload = function () {
                $('#LargeImage').data('imageWidth', this.width);
                $('#LargeImage').data('imageHeight', this.height);

            };
            image.src = _URL.createObjectURL(file);
        }
        var filename = $(this).val();
        if (filename.substring(3, 11) == 'fakepath') {
            filename = filename.substring(12);
        }
        document.getElementById("LargeImageFileName").value = filename;
    });


    //$("#LargeImage").on("change", function () {
    $(document).on('change', '#LargeImage', function () {
        $("#LargeImageFilePath").attr("src", "");
        var photo = this;
        $img = $("#LargeImageFilePath");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));
    });


    $(document).on('change', '#FileName', function () {
        $("#PdfFileName").val("");

        var filename = $(this).val();
        if (filename.substring(3, 11) == 'fakepath') {
            filename = filename.substring(12);
        }
        document.getElementById("PdfFileName").value = filename;
    });


    $(document).on("click", "#editreportsubmitBtn", EditpublishToLive);

}

function EditpublishToLive() {

    $("#EditAnnualReportForm").attr({
        action: apppath + "/Admin/AboutUs/EditAnnualReports"
    }).removeAttr("target").submit();

}


function AddPressRelease() {

    var path = apppath + "/Admin/AboutUs/AddPressRelease"
    $.ajax({
        url: path,
        //type: 'GET',
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }
            //  $("#EditNewsLetterForm").html('');
            $("#AddPressReleaseForm").html('');
            $("#AddPressReleaseForm").append(response);

            $(".calendar").datepicker({
                dateformat: 'dd/mm/yy',
                //mindate: new date(),
            });
            initializeAddPressReleaseValidation();
        },
        failure: function (response) {
            console.log(response);
            $("#AddPressReleaseForm").append(('Error occured'));
        }
    });
}


function initializeAddPressReleaseValidation() {
   

    jQuery.validator.setDefaults({
        ignore: []
    });


    jQuery.validator.addMethod('filesize', function (value, element, param) {
        return this.optional(element) || (element.files[0].size <= param)
    }, 'File size must be less than 2MB');

    $("#AddPressReleaseForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {

            Date: {
                required: true
            },

            Title: {
                required: true

            },
         
            FileName: {
                required: true,
                extension: 'pdf'

            },

        }


    });




   

    $(document).on('change', '#FileName', function () {
        $("#PdfFileName").val("");

        var filename = $(this).val();
        if (filename.substring(3, 11) == 'fakepath') {
            filename = filename.substring(12);
        }
        document.getElementById("PdfFileName").value = filename;
    });

    $(document).on("click", "#addpressubmitBtn", addPresspublishToLive);
    function addPresspublishToLive() {

        $("#AddPressReleaseForm").attr({
            action: apppath + "/Admin/AboutUs/AddPressRelease"
        }).removeAttr("target").submit();

    }
   

}


function EditPressRelease(EncryptedId) {

    var path = apppath + "/Admin/AboutUs/EditPressRelease/" + EncryptedId
    $.ajax({
        url: path,
        //type: 'GET',
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }
            //  $("#EditNewsLetterForm").html('');
            $("#EditPressReleaseForm").html('');
            $("#EditPressReleaseForm").append(response);
            $(".calendar").datepicker({
                dateformat: 'dd/mm/yy',
                //mindate: new date(),
            });
            initializeEditPressReleaseValidation();
            
        },
        failure: function (response) {
            console.log(response);
            $("#EditPressReleaseForm").append(('Error occured'));
        }
    });
}


function initializeEditPressReleaseValidation() {


    jQuery.validator.setDefaults({
        ignore: []
    });


    jQuery.validator.addMethod('filesize', function (value, element, param) {
        return this.optional(element) || (element.files[0].size <= param)
    }, 'File size must be less than 2MB');

    $("#EditPressReleaseForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {

            Date: {
                required: true
            },

            Title: {
                required: true

            },

            FileName: {
                
                extension: 'pdf'

            },

        }


    });






    $(document).on('change', '#FileName', function () {
        $("#PdfFileName").val("");

        var filename = $(this).val();
        if (filename.substring(3, 11) == 'fakepath') {
            filename = filename.substring(12);
        }
        document.getElementById("PdfFileName").value = filename;
    });

    $(document).on("click", "#editpressubmitBtn", EditPresspublishToLive);
    function EditPresspublishToLive() {

        $("#EditPressReleaseForm").attr({
            action: apppath + "/Admin/AboutUs/EditPressRelease"
        }).removeAttr("target").submit();

    }


}





