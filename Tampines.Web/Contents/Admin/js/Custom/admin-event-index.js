$(document).ready(function () {
    intializepagination();

    $(document).on("click", "#addeventbtn", function () {
        var modal = document.getElementById("add");
        $(modal).modal('show');
        AddEventForm();
    });

    $(document).on("click", "#addsectionbtn", function () {
        AddSectionForm();
    });



    $(document).on("click", ".linelink", function () {

        var count = $('.addSectionForm').length;
        var id = $(this).attr("id");
        var arr = id.split('_');
        id = arr[1];
        var tableclose = '#' + id;
        $(tableclose).closest("table").remove();
        count = count - 1;
        id = parseInt(id) + 1;
        var id1;
        for (var i = id; i <= count; i++) {
            id1 = parseInt(i) - 1;
            $('#deldiv_' + i + '').attr({ id: 'deldiv_' + id1 + '', name: 'deldiv[' + id1 + ']' });
            $('#EventSection_' + i + '_With').attr({ id: 'EventSection_' + id1 + '_With', name: 'EventSection[' + id1 + '].With' });
            $('#EventSection_' + i + '_Venue').attr({ id: 'EventSection_' + id1 + '_Venue', name: 'EventSection[' + id1 + '].Venue' });
            $('#EventSection_' + i + '_Time').attr({ id: 'EventSection_' + id1 + '_Time', name: 'EventSection[' + id1 + '].Time' });


        }

    });

    $(document).on("click", ".editEventsbtn", function () {
        var modal = document.getElementById("edit");
        $(modal).modal('show');
        EditEventForm($(this).attr("data-id"));
    });

    $(document).on("click", "#deletebtn", function () {
        var modal = document.getElementById("delete");
        $(modal).modal('show');
        $("#EncryptedId").val($(this).attr("data-uniquecode"));
    });

    $(document).on('click', "#deleteeventbtn", function () {
        $("#DeleteEventForm").submit()
    });
});


function intializepagination() {
    $(".pagination").pagination({
        items: $('.pagination').data('totalitems'),
        itemsOnPage: 10,
        currentPage: $('.pagination').data('pageindex'),
        hrefTextPrefix: "",
        prevText: "&laquo;",
        nextText: "&raquo;",
        cssStyle: "",
        onPageClick: function (pageNumber, event) {
            event.preventDefault();
            bindPageItems({
                PageNumber: pageNumber

            });
            $('.pagination').attr('data-pageindex', pageNumber);
        }
    });
}

var pageSize = 10;
function bindPageItems(e) {

    var path = apppath + "/Admin/Event/PartialVieweEvents/" + e.PageNumber

    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response == '') {
                response = '<tr><td colspan="6" class="center">No Results Found</td></tr>';
            }

            $("#EventsTbl tbody").html(response);
            var pageIndex = parseInt($('.pagination').attr('data-pageindex'));
            //console.log(pageIndex);
            var startItem = (pageIndex == 1 ? 0 : (pageIndex - 1) * pageSize) + 1;
            var lastItem = startItem + ($("#EventsTbl tbody tr").length - 1);
            $(".page").html("Showing " + startItem + " to " + lastItem + " of " + $('.pagination').data('totalitems') + "entries");

        },
        failure: function (response) {
            console.log(response);
            $("#EventsTbl tbody").html('<tr><td colspan="7" class="center error">Error occured</td></tr>');
        }
    });

}



function AddSectionForm() {


    var rowCount = $('.addSectionForm').length + 0;


    var Loction = '<table cellpadding="0" cellspacing="0" border="0" class="addSectionForm" id="' + rowCount + '">' +
        '<tr><td class="fieldname">With</td><td><input type="text"  id="EventSection_' + rowCount + '_With" name="EventSection[' + rowCount + '].With"></td></tr>' +
        '<tr><td class="fieldname">Venue</td><td><input type="text" id="EventSection_' + rowCount + '_Venue" name="EventSection[' + rowCount + '].Venue"></td></tr>' +
        '<tr><td class="fieldname">Time</td><td><input type="text" class="timepicker" id="EventSection_' + rowCount + '_Time" name="EventSection[' + rowCount + '].Time"></td></tr>' +
        '<tr><td class="center" colspan="2"><a id="deldiv_' + rowCount + '" class="submitBtn linelink floatRight" href="javascript:void(0)">Delete</a></td></tr>' +
        '</table>';
    $('#tablecontent').append(Loction);
    $(function () {
        $('input.timepicker').timepicker();
    });

}


function AddEventForm() {

    var path = apppath + "/Admin/Event/Add";
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }
            // console.log(response);
            $("#AddEventForm").html('');
            $("#AddEventForm").append(response);
            initializeAddEventValidate();
            $(".calendar").datepicker({
                dateformat: 'dd/mm/yy',
                //mindate: new date(),
            });


            $(function () {
                $('input.timepicker').timepicker();
            });

           

        },
        failure: function (response) {
            console.log(response);
            $("#AddEventForm").append(('Error occured'));
        }
    });
}


function initializeAddEventValidate() {


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
            Title: {
                required: true
                //remote: {
                //    url: apppath + "/Admin/Event/CheckEventTitle",
                //    type: "GET",
                //    async: true,
                //    cache: false,
                //    data: {
                //        EventTitle: function () {
                //            return $("#Title").val();
                //        },
                //        EncDetail: function () {
                //            return null;
                //        }
                //    },
                //    dataType: 'json'
                //}
            },

            Date: { required: true },

            FacebookLink: { required: true, url: true },
            InstagramLink: { required: true, url: true },
            Image: {
                required: true,
                dimention: [680, 366],
                extension: 'jpg|jpeg|png'
            },
        },
        messages:
        {
            Issue: {
                required: "This field is required."
            },
            Contact: {
                required: "This field is required."
            },
            Title: { remote: 'Title is duplicated' }
        }
    });


    $(document).on('change', 'input[name=Image]', function () {
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


    $(document).on('change', '#Image', function () {
        $("#ImageFilePath").attr("src", "");
        var photo = this;
        $img = $("#ImageFilePath");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));
    });

    $(document).on('click', "#addeventPublishbtn", function () {


        $("#AddEventForm").submit()

    })

}

function EditEventForm(EncDetail) {

    var path = apppath + "/Admin/Event/Edit/" + EncDetail;
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }
            // console.log(response);
            $("#EditEventForm").html('');
            $("#EditEventForm").append(response);
            initializeEditEventValidate();
            $(".calendar").datepicker({
                dateformat: 'dd/mm/yy',
                //mindate: new date(),
            });


            $(function () {
                $('input.timepicker').timepicker();
            });



        },
        failure: function (response) {
            console.log(response);
            $("#EditEventForm").append(('Error occured'));
        }
    });
}


function initializeEditEventValidate() {


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
            Title: {
                required: true
                //remote: {
                //    url: apppath + "/Admin/Event/CheckEventTitle",
                //    type: "GET",
                //    async: true,
                //    cache: false,
                //    data: {
                //        EventTitle: function () {
                //            return $("#Title").val();
                //        },
                //        EncDetail: function () {
                //            return $("#GUID").val();
                //        }
                //    },
                //    dataType: 'json'
                //}
            },

            Date: { required: true },

            FacebookLink: { required: true, url: true },
            InstagramLink: { required: true, url: true },
            Image: {
                //required: true,
                dimention: [680, 366],
                extension: 'jpg|jpeg|png'
            },
        },
        messages:
        {
            Issue: {
                required: "This field is required."
            },
            Contact: {
                required: "This field is required."
            },
            Title: { remote: 'Title is duplicated' }
        }
    });


    $(document).on('change', 'input[name=Image]', function () {
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


    $(document).on('change', '#Image', function () {
        $("#ImageFilePath").attr("src", "");
        var photo = this;
        $img = $("#ImageFilePath");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));
    });

    $(document).on('click', "#editeventPublishbtn", function () {


        $("#EditEventForm").submit()

    })

}