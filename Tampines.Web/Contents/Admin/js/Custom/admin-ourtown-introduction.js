

$(document).ready(function () {

   
    initializeValidation()
    $('#ChangesUpdate').modal('show');
    $(document).on("click", "#previewBtn", submitPreview);
    $(document).on("click", "#publishToLiveBtn", publishToLive);


    function publishToLive() {

        $("#OurTownForm").attr({
            action: apppath + "/Admin/OurTown/Introduction"
        }).removeAttr("target").submit();

    }



    function submitPreview() {
        $("#OurTownForm").attr({
            action: "/Admin/OurTown/Preview",
            target: "_blank"
        }).submit();
        return false;
    }

});


function initializeValidation() {


    jQuery.validator.setDefaults({
        ignore: [],
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

    jQuery("#AddEventForm").validate({
        ignore: [],
        debug: false,
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {
           
            Image1: {
               // required: true,
                dimention: [600, 400],
                extension: 'jpg|jpeg|png'
            },
            ImageAltTag1: { required: true },
            Image2: {
                //required: true,
                dimention: [600, 400],
                extension: 'jpg|jpeg|png'
            },
            ImageAltTag2: { required: true },
            Image3: {
               // required: true,
                dimention: [600, 400],
                extension: 'jpg|jpeg|png'
            },
            ImageAltTag3: { required: true },
            Contact: {
                required: function (e) {
                    CKEDITOR.instances[e.id].updateElement();
                    var editorcontent = e.value.replace(/<[^>]*>/gi, '');
                    return editorcontent.length === 0;
                },
            }
        }
    });


    $(document).on('change', 'input[name=Image1]', function () {
        var _URL = window.URL || window.webkitURL;
        var image, file;

        if ((file = this.files[0])) {
            image = new Image();
            image.onload = function () {
                $('#Image1').data('imageWidth', this.width);
                $('#Image1').data('imageHeight', this.height);

            };
            image.src = _URL.createObjectURL(file);
        }
        var filename = $(this).val();
        if (filename.substring(3, 11) == 'fakepath') {
            filename = filename.substring(12);
        }
        document.getElementById("ImageFileNames1").value = filename;
    });


    $(document).on('change', '#Image1', function () {
        $("#ImageFilePath1").attr("src", "");
        var photo = this;
        $img = $("#ImageFilePath1");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));
    });


    $(document).on('change', 'input[name=Image2]', function () {
        var _URL = window.URL || window.webkitURL;
        var image, file;

        if ((file = this.files[0])) {
            image = new Image();
            image.onload = function () {
                $('#Image2').data('imageWidth', this.width);
                $('#Image2').data('imageHeight', this.height);

            };
            image.src = _URL.createObjectURL(file);
        }
        var filename = $(this).val();
        if (filename.substring(3, 11) == 'fakepath') {
            filename = filename.substring(12);
        }
        document.getElementById("ImageFileNames2").value = filename;
    });


    $(document).on('change', '#Image2', function () {
        $("#ImageFilePath2").attr("src", "");
        var photo = this;
        $img = $("#ImageFilePath2");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));
    });

    $(document).on('change', 'input[name=Image3]', function () {
        var _URL = window.URL || window.webkitURL;
        var image, file;

        if ((file = this.files[0])) {
            image = new Image();
            image.onload = function () {
                $('#Image3').data('imageWidth', this.width);
                $('#Image3').data('imageHeight', this.height);

            };
            image.src = _URL.createObjectURL(file);
        }
        var filename = $(this).val();
        if (filename.substring(3, 11) == 'fakepath') {
            filename = filename.substring(12);
        }
        document.getElementById("ImageFileNames3").value = filename;
    });


    $(document).on('change', '#Image3', function () {
        $("#ImageFilePath3").attr("src", "");
        var photo = this;
        $img = $("#ImageFilePath3");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));
    });


    

}