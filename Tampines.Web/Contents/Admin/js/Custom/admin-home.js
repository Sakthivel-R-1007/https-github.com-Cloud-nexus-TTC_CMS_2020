$(document).ready(function () {

    $('#ChangesUpdate').modal('show');
    $(document).on("click", "#addhomebannerbtn", function () {
        var modal = document.getElementById("addHome");
        $(modal).modal('show');
        AddHomeBanner();
    });


    $(document).on("click", "#edithomebannerbtn", function () {
        var modal = document.getElementById("editHome");
        $(modal).modal('show');

        EditHomeBanner($(this).attr("data-id"));
    });


    // Home Banner Delete
    $(document).on("click", "#deletehomebannerbtn", function () {
        var modal = document.getElementById("delete");
        $(modal).modal('show');
        $("#DeleteBannerId").val(($(this).attr("data-id")));
    });
    $(document).on("click", "#DeleteBannerbtn", function () {
        $("#DeleteHomeBannerForm").submit();
    });
    //

    $(document).on("click", "#sortSeqbtn", function () {
        var modal = document.getElementById("sortSeq");
        $(modal).modal('show');
        SortHomebanner();
    });

    $(document).on("click", "#HomeBannerSortingsubmitBtn", function () {

        var a = 1;
        $('ul.ui-sortable>li').each(function () {
            var data = $(this).attr('data-id');
            $('.cmxform').append('<input id="' + a + '" type="hidden" name=sort[' + a + '].GUID  value="' + data + '"> ');
            $('.cmxform').append('<input id="' + a + '" type="hidden" name=sort[' + a + '].ReOrder  value="' + a + '"> ');
            a++;
        });
        $("#SortHomeBannerForm").submit();

    });



    $(document).on("click", "#editaboutusbannerbtn", function () {
        var modal = document.getElementById("aboutUsedit");
        $(modal).modal('show');
        EditAboutUsBanner($(this).attr("data-id"));
    });


    $(document).on("click", "#editourtownbannerbtn", function () {
        var modal = document.getElementById("OurTownedit");
        $(modal).modal('show');
        EditOurTownBanner($(this).attr("data-id"));
    });

    $(document).on("click", "#editServicesBannerbtn", function () {
        var modal = document.getElementById("ResidentServicesdit");
        $(modal).modal('show');
        EditServicesBanner($(this).attr("data-id"));
    });

    $(document).on("click", "#editeventbannerbtn", function () {
        var modal = document.getElementById("eventBanneredit");
        $(modal).modal('show');
        EditEventBanner($(this).attr("data-id"));
    });


    $(document).on("click", "#editContactUsbannerbtn", function () {
        var modal = document.getElementById("ContactUsBanneredit");
        $(modal).modal('show');
        EditContactUsBanner($(this).attr("data-id"));
    });


    

});



function EditHomeBanner(EncryptedId) {

    var path = apppath + "/Admin/EditHomeBanner/" + EncryptedId;
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }
            // console.log(response);
            $("#EditHomeBannerForm").html('');
            $("#EditHomeBannerForm").append(response);
            initializeEditHomeBannerValidate();

        },
        failure: function (response) {
            console.log(response);
            $("#EditHomeBannerForm").append(('Error occured'));
        }
    });
}

function AddHomeBanner() {

    var path = apppath + "/Admin/Banner/AddHomeBanner";
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }
            // console.log(response);
            $("#AddHomeBannerForm").html('');
            $("#AddHomeBannerForm").append(response);
            initializeAddHomeBannerValidate();

        },
        failure: function (response) {
            console.log(response);
            $("#AddHomeBannerForm").append(('Error occured'));
        }
    });
}


function initializeAddHomeBannerValidate() {


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

    $.validator.addMethod("ResourcesCount1", function (value) {
        var words = value.replace(/\s/g, "");
        var val = words.length;
        return val < 15;
    }, 'You can enter max 15 words');

    $.validator.addMethod("ResourcesCount2", function (value) {
        var words = value.replace(/\s/g, "");
        var val = words.length;
        return val < 15;
    }, 'You can enter max 15 words');

    $.validator.addMethod("ResourcesCount3", function (value) {
        var words = value.replace(/\s/g, "");
        var val = words.length;
        return val < 50;
    }, 'You can enter max 50 words');

    jQuery.validator.addMethod('filesize', function (value, element, param) {
        return this.optional(element) || (element.files[0].size <= param)
    }, 'File size must be less than 2MB');

    $("#AddHomeBannerForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {

            Header1: { required: true, ResourcesCount1: true },
            Header2: { required: true, ResourcesCount2: true },
            Header3: { required: true, ResourcesCount3: true },
            ReadMore: { required: true },
            LinkTarget: { required: true },

            "Image": {
                required: true,
                dimention: [1920, 960],
                filesize: 2097152, /*2MB*/
                extension: 'jpg|jpeg|png'
            },
        }
    });




    InitializeWordCountHeader1();
    InitializeWordCountHeader2();
    InitializeWordCountHeader3();



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
        document.getElementById("ImageFileNames").value = filename;
    });

    $("#Image").on("change", function () {
        $("#ImageFilePath").attr("src", "");
        var photo = this;
        $img = $("#ImageFilePath");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));

    });

    $(document).on("click", "#addhomebanner", function () {


        if ($("#AddHomeBannerForm").valid()) {

            $("#AddHomeBannerForm").submit()
        }
    });


}

function initializeEditHomeBannerValidate() {


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

    $.validator.addMethod("ResourcesCount1", function (value) {
        var words = value.replace(/\s/g, "");
        var val = words.length;
        return val < 15;
    }, 'You can enter max 15 words');

    $.validator.addMethod("ResourcesCount2", function (value) {
        var words = value.replace(/\s/g, "");
        var val = words.length;
        return val < 15;
    }, 'You can enter max 15 words');

    $.validator.addMethod("ResourcesCount3", function (value) {
        var words = value.replace(/\s/g, "");
        var val = words.length;
        return val < 50;
    }, 'You can enter max 50 words');

    jQuery.validator.addMethod('filesize', function (value, element, param) {
        return this.optional(element) || (element.files[0].size <= param)
    }, 'File size must be less than 2MB');

    $("#EditHomeBannerForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {

            Header1: { required: true, ResourcesCount1: true },
            Header2: { required: true, ResourcesCount2: true },
            Header3: { required: true, ResourcesCount3: true },
            ReadMore: { required: true },
            LinkTarget: { required: true },

            "Image": {
               // required: true,
                dimention: [1920, 960],
                filesize: 2097152, /*2MB*/
                extension: 'jpg|jpeg|png'
            },
        }
    });




    InitializeWordCountHeader1();
    InitializeWordCountHeader2();
    InitializeWordCountHeader3();



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
        document.getElementById("ImageFileNames").value = filename;
    });

    $("#Image").on("change", function () {
        $("#ImageFilePath").attr("src", "");
        var photo = this;
        $img = $("#ImageFilePath");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));

    });

    $(document).on("click", "#edithomebanner", function () {


        if ($("#EditHomeBannerForm").valid()) {

            $("#EditHomeBannerForm").submit()
        }
    });


}

function InitializeWordCountHeader1() {
    var value = $('#Header1').val();
    var word = value.replace(/\s/g, "");
    $('#wordCount1').html(word.length);
    $("#Header1").on("keyup", function (e) {
        var value = $('#Header1').val();
        var words = value.replace(/\s/g, "");
        $('#wordCount1').html(words.length);
    });

}

function InitializeWordCountHeader2() {
    var value = $('#Header2').val();
    var word = value.replace(/\s/g, "");
    $('#wordCount2').html(word.length);
    $("#Header2").on("keyup", function (e) {
        var value = $('#Header2').val();
        var words = value.replace(/\s/g, "");
        $('#wordCount2').html(words.length);
    });

}

function InitializeWordCountHeader3() {
    var value = $('#Header3').val();
    var word = value.replace(/\s/g, "");
    $('#wordCount3').html(word.length);
    $("#Header3").on("keyup", function (e) {
        var value = $('#Header3').val();
        var words = value.replace(/\s/g, "");
        $('#wordCount3').html(words.length);
    });

}

function SortHomebanner() {
    var path = apppath + "/Admin/Banner/SortHomeBanner"
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }
            // console.log(response);
            $("#SortHomeBannerForm").html('');
            $("#SortHomeBannerForm").append(response);
            $(document).ready(function () {
                $("#sortSeq #sortable").sortable({
                    stop: function (event, ui) {

                        var itemOrder = $('#sortSeq #sortable').sortable("toArray");
                        for (var i = 0; i < itemOrder.length; i++) {
                            var res = itemOrder[i].split("_");
                            var Id = "#" + itemOrder[i];
                            var NewId = 'Set_' + i;
                        }

                    }
                });
                $("#sortSeq #sortable").disableSelection();
            });//ready

        },
        failure: function (response) {
            console.log(response);
            $("#SortHomeBannerForm").append(('Error occured'));
        }
    });
}



function EditAboutUsBanner(EncryptedId) {

    var path = apppath + "/Admin/EditAboutUsBanner/" + EncryptedId;
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }
            // console.log(response);
            $("#EditAboutUsBannerForm").html('');
            $("#EditAboutUsBannerForm").append(response);
            initializeEditAboutUsBannerValidate();

        },
        failure: function (response) {
            console.log(response);
            $("#EditAboutUsBannerForm").append(('Error occured'));
        }
    });
}


function initializeEditAboutUsBannerValidate() {


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



    jQuery.validator.addMethod('filesize', function (value, element, param) {
        return this.optional(element) || (element.files[0].size <= param)
    }, 'File size must be less than 2MB');

    $("#EditAboutUsBannerForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {


            "Image": {
                // required: true,
                dimention: [1920, 600],
                filesize: 2097152, /*2MB*/
                extension: 'jpg|jpeg|png'
            },
        }
    });







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
        document.getElementById("ImageFileNames").value = filename;
    });

    $("#Image").on("change", function () {
        $("#ImageFilePath").attr("src", "");
        var photo = this;
        $img = $("#ImageFilePath");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));

    });

    $(document).on("click", "#dboutussubmitbtn", function () {


        if ($("#EditAboutUsBannerForm").valid()) {

            $("#EditAboutUsBannerForm").submit()
        }
    });


}


function EditOurTownBanner(EncryptedId) {

    var path = apppath + "/Admin/EditOurTownBanner/" + EncryptedId;
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }
            // console.log(response);
            $("#EditOurTownBannerForm").html('');
            $("#EditOurTownBannerForm").append(response);
            initializeEditOurTownBannerValidate();

        },
        failure: function (response) {
            console.log(response);
            $("#EditOurTownBannerForm").append(('Error occured'));
        }
    });
}

function initializeEditOurTownBannerValidate() {


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



    jQuery.validator.addMethod('filesize', function (value, element, param) {
        return this.optional(element) || (element.files[0].size <= param)
    }, 'File size must be less than 2MB');

    $("#EditOurTownBannerForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {


            "Image": {
                // required: true,
                dimention: [1920, 600],
                filesize: 2097152, /*2MB*/
                extension: 'jpg|jpeg|png'
            },
        }
    });







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
        document.getElementById("ImageFileNames").value = filename;
    });

    $("#Image").on("change", function () {
        $("#ImageFilePath").attr("src", "");
        var photo = this;
        $img = $("#ImageFilePath");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));

    });

    $(document).on("click", "#ourtownsubmitbtn", function () {


        if ($("#EditOurTownBannerForm").valid()) {

            $("#EditOurTownBannerForm").submit()
        }
    });


}


function EditServicesBanner(EncryptedId) {

    var path = apppath + "/Admin/EditResidentServicesBanner/" + EncryptedId;
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }
            // console.log(response);
            $("#EditResidentServicesBannerForm").html('');
            $("#EditResidentServicesBannerForm").append(response);
            initializeEditServicesBannerValidate();

        },
        failure: function (response) {
            console.log(response);
            $("#EditResidentServicesBannerForm").append(('Error occured'));
        }
    });
}

function initializeEditServicesBannerValidate() {


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



    jQuery.validator.addMethod('filesize', function (value, element, param) {
        return this.optional(element) || (element.files[0].size <= param)
    }, 'File size must be less than 2MB');

    $("#EditResidentServicesBannerForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {


            "Image": {
                // required: true,
                dimention: [1920, 600],
                filesize: 2097152, /*2MB*/
                extension: 'jpg|jpeg|png'
            },
        }
    });







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
        document.getElementById("ImageFileNames").value = filename;
    });

    $("#Image").on("change", function () {
        $("#ImageFilePath").attr("src", "");
        var photo = this;
        $img = $("#ImageFilePath");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));

    });

    $(document).on("click", "#ServicesBannersubmitbtn", function () {


        if ($("#EditResidentServicesBannerForm").valid()) {

            $("#EditResidentServicesBannerForm").submit()
        }
    });


}



function EditTEMPOBanner(EncryptedId) {

    var path = apppath + "/Admin/EditTEMPOBanner/" + EncryptedId;
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }
            // console.log(response);
            $("#EditTEMPOBannerForm").html('');
            $("#EditTEMPOBannerForm").append(response);
            initializeTEMPOBannerValidate();

        },
        failure: function (response) {
            console.log(response);
            $("#EditTEMPOBannerForm").append(('Error occured'));
        }
    });
}

function initializeTEMPOBannerValidate() {


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



    jQuery.validator.addMethod('filesize', function (value, element, param) {
        return this.optional(element) || (element.files[0].size <= param)
    }, 'File size must be less than 2MB');

    $("#EditTEMPOBannerForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {


            "Image": {
                // required: true,
                dimention: [1920, 600],
                filesize: 2097152, /*2MB*/
                extension: 'jpg|jpeg|png'
            },
        }
    });







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
        document.getElementById("ImageFileNames").value = filename;
    });

    $("#Image").on("change", function () {
        $("#ImageFilePath").attr("src", "");
        var photo = this;
        $img = $("#ImageFilePath");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));

    });

    $(document).on("click", "#tempoBannersubmitbtn", function () {


        if ($("#EditTEMPOBannerForm").valid()) {

            $("#EditTEMPOBannerForm").submit()
        }
    });


}


function EditEventBanner(EncryptedId) {

    var path = apppath + "/Admin/EditEventBanner/" + EncryptedId;
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }
            // console.log(response);
            $("#EditEventBannerForm").html('');
            $("#EditEventBannerForm").append(response);
            initializeEditEventBannerValidate();

        },
        failure: function (response) {
            console.log(response);
            $("#EditEventBannerForm").append(('Error occured'));
        }
    });
}

function initializeEditEventBannerValidate() {


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



    jQuery.validator.addMethod('filesize', function (value, element, param) {
        return this.optional(element) || (element.files[0].size <= param)
    }, 'File size must be less than 2MB');

    $("#EditEventBannerForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {


            "Image": {
                // required: true,
                dimention: [1920, 600],
                filesize: 2097152, /*2MB*/
                extension: 'jpg|jpeg|png'
            },
        }
    });







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
        document.getElementById("ImageFileNames").value = filename;
    });

    $("#Image").on("change", function () {
        $("#ImageFilePath").attr("src", "");
        var photo = this;
        $img = $("#ImageFilePath");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));

    });

    $(document).on("click", "#eventBannersubmitbtn", function () {


        if ($("#EditEventBannerForm").valid()) {

            $("#EditEventBannerForm").submit()
        }
    });


}




function EditContactUsBanner(EncryptedId) {

    var path = apppath + "/Admin/EditContactUsBanner/" + EncryptedId;
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }
            // console.log(response);
            $("#EditContactUsBannerForm").html('');
            $("#EditContactUsBannerForm").append(response);
            initializeContactUsBannerValidate();

        },
        failure: function (response) {
            console.log(response);
            $("#EditContactUsBannerForm").append(('Error occured'));
        }
    });
}

function initializeContactUsBannerValidate() {


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



    jQuery.validator.addMethod('filesize', function (value, element, param) {
        return this.optional(element) || (element.files[0].size <= param)
    }, 'File size must be less than 2MB');

    $("#EditContactUsBannerForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {


            "Image": {
                // required: true,
                dimention: [1920, 600],
                filesize: 2097152, /*2MB*/
                extension: 'jpg|jpeg|png'
            },
        }
    });







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
        document.getElementById("ImageFileNames").value = filename;
    });

    $("#Image").on("change", function () {
        $("#ImageFilePath").attr("src", "");
        var photo = this;
        $img = $("#ImageFilePath");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));

    });

    $(document).on("click", "#contactBannersubmitbtn", function () {


        if ($("#EditContactUsBannerForm").valid()) {

            $("#EditContactUsBannerForm").submit()
        }
    });


}
