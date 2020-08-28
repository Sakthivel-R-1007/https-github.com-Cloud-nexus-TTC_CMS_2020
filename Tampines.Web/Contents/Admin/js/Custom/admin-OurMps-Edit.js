$(document).ready(function () {
    initializeValidation()
    $(document).on("click", "#publishToLiveBtn", publishToLive);
    $(document).on("click", "#previewBtn", submitPreview);
    $(document).on("click", ".BackBtn", Backurl);


    $('input[name=Image]').change(function () {
        var _URL = window.URL || window.webkitURL;
        var image, file;

        if ((file = this.files[0])) {
            image = new Image();
            image.onload = function () {
                $('#Image').data('imageWidth', this.width);
                $('#Image').data('imageHeight', this.height);
                //alert("The image width is " + this.width + " and image height is " + this.height);
            };
            image.src = _URL.createObjectURL(file);
        }
        var filename = $(this).val();
        if (filename.substring(3, 11) == 'fakepath') {
            filename = filename.substring(12);
        }
        document.getElementById("ThumbnailImageFileName").value = filename;
    });


    $("#Image").on("change", function () {
        $("#ThumbnailImageFilePath").attr("src", "");
        var photo = this;
        $img = $("#ThumbnailImageFilePath");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));
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



    $(document).on("click", "#deletebtn", function () {
        var modal = document.getElementById("delete");
        $(modal).modal('show');
        $("#deleteeventId").val(($(this).attr("data-uniquecode")));
    });

    $(document).on("click", "#confirm_deleteiconBtn", function () {

        $("#DeleteOurMpsForm").attr({
            action: apppath + "/Admin/AboutUs/DeleteOurMps"
        }).removeAttr("target").submit();


    });




    // LinksContants Sorting
    $(document).on("click", "#OurMpssortSequence", function () {
        var modal = document.getElementById("sortLinksContacts");
        $(modal).modal('show');
        SortOurMps($(this).attr("data-id"));
    });

    $(document).on("click", "#OurMpssortingsubmitBtn", function () {

        var a = 1;
        $('ul.ui-sortable>li').each(function () {
            var data = $(this).attr('data-id');
            $('.cmxform').append('<input id="' + a + '" type="hidden" name=sort[' + a + '].GUID  value="' + data + '"> ');
            $('.cmxform').append('<input id="' + a + '" type="hidden" name=sort[' + a + '].ReOrder  value="' + a + '"> ');
            a++;
        });
        $("#SortOurMpsForm").submit();

    });


});
function publishToLive() {

    $("#AddOurMpsForm").attr({
        action: apppath + "/Admin/AboutUs/EditMPs"
    }).removeAttr("target").submit();

}

function submitPreview() {
    $("#AddOurMpsForm").attr({
        action: "/Admin/AboutUs/Preview",
        target: "_blank"
    }).submit();
    return false;
}



function SortOurMps(GUID) {
    var path = apppath + "/Admin/AboutUs/SortOurMps"
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }
            // console.log(response);
            $("#SortOurMpsForm").html('');
            $("#SortOurMpsForm").append(response);
            $(document).ready(function () {
                $("#sortLinksContacts #sortable").sortable({
                    stop: function (event, ui) {

                        var itemOrder = $('#sortLinksContacts #sortable').sortable("toArray");
                        for (var i = 0; i < itemOrder.length; i++) {
                            var res = itemOrder[i].split("_");
                            var Id = "#" + itemOrder[i];
                            var NewId = 'Set_' + i;
                        }

                    }
                });
                $("#sortLinksContacts #sortable").disableSelection();
            });//ready

        },
        failure: function (response) {
            console.log(response);
            $("#SortOurMpsForm").append(('Error occured'));
        }
    });
}



function initializeValidation() {

    jQuery("#AddOurMpsForm").validate({
        ignore: [],
        debug: false,
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {
        
            Name: { required: true },
            Image: {
               // required: true,
                dimention: [384, 368],
                extension: 'jpg|jpeg|png'
            },

            ImageAltTag: { required: true },
            FacebookLink: {
                required: true,
                url: true
            },
            InstagramLink: {
                required: true,
                url: true
                
            },
            Division: { required: true },
            Designation: {
                required: function (e) {
                    CKEDITOR.instances[e.id].updateElement();
                    var editorcontent = e.value.replace(/<[^>]*>/gi, '');
                    return editorcontent.length === 0;
                },

            },
            MeetThePeople: {
                required: function (e) {
                    CKEDITOR.instances[e.id].updateElement();
                    var editorcontent = e.value.replace(/<[^>]*>/gi, '');
                    return editorcontent.length === 0;
                },

            },
            CommunityClub: {
                required: function (e) {
                    CKEDITOR.instances[e.id].updateElement();
                    var editorcontent = e.value.replace(/<[^>]*>/gi, '');
                    return editorcontent.length === 0;
                },

            },
            
        },
        messages:
        {
            Designation: {
                required: "This field is required."
            },
            CommunityClub: {
                required: "This field is required."
            },
            MeetThePeople: {
                required: "This field is required."
            },
        }
    });

}
function Backurl() {

    window.location.href = "/Admin/AboutUs/OurMPs"
}
