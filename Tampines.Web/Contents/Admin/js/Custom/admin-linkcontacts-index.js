$(document).ready(function () {
    intializepagination();
    $('#ChangesUpdate').modal('show');



    // LinksContants Sorting
    $(document).on("click", "#LinksContactssortSequence", function () {
        var modal = document.getElementById("sortLinksContacts");
        $(modal).modal('show');
        SortLinksContacts($(this).attr("data-id"));
    });

    $(document).on("click", "#LinksContactssortingsubmitBtn", function () {

        var a = 1;
        $('ul.ui-sortable>li').each(function () {
            var data = $(this).attr('data-id');
            $('.cmxform').append('<input id="' + a + '" type="hidden" name=sort[' + a + '].GUID  value="' + data + '"> ');
            $('.cmxform').append('<input id="' + a + '" type="hidden" name=sort[' + a + '].ReOrder  value="' + a + '"> ');
            a++;
        });
        $("#SortLinksContactsForm").submit();

    });


    $(document).on("click", "#deletebtn", function () {
        var modal = document.getElementById("delete");
        $(modal).modal('show');
        $("#deleteeventId").val(($(this).attr("data-uniquecode")));
    });

    $(document).on("click", "#confirm_deleteiconBtn", function () {

        $("#DeleteLinksConatcsForm").attr({
            action: apppath + "/Admin/ContactUs/DeleteLinksContact"
        }).removeAttr("target").submit();


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
                PageNumber: pageNumber,
                Year: $("#Year").val() || '',
                NewsType: $("#NewsType").val() || ''

            });
            $('.pagination').attr('data-pageindex', pageNumber);
        }
    });
}


var pageSize = 10;
function bindPageItems(e) {


  

    var path = apppath + "/Admin/ContactUs/PartialViewLinksContents/" + e.PageNumber

    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response == '') {
                response = '<tr><td colspan="6" class="center">No Results Found</td></tr>';
            }

            $("#LinksContentsTbl tbody").html(response);
            var pageIndex = parseInt($('.pagination').attr('data-pageindex'));
            //console.log(pageIndex);
            var startItem = (pageIndex == 1 ? 0 : (pageIndex - 1) * pageSize) + 1;
            var lastItem = startItem + ($("#LinksContentsTbl tbody tr").length - 1);
            $(".page").html("Showing " + startItem + " to " + lastItem + " of " + $('.pagination').data('totalitems') + "entries");

        },
        failure: function (response) {
            console.log(response);
            $("#LinksContentsTbl tbody").html('<tr><td colspan="7" class="center error">Error occured</td></tr>');
        }
    });

}

function SortLinksContacts(GUID) {
    var path = apppath+"/Admin/ContactUs/SortLinksContacts"
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }
            // console.log(response);
            $("#SortLinksContactsForm").html('');
            $("#SortLinksContactsForm").append(response);
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
            $("#SortLinksContactsForm").append(('Error occured'));
        }
    });
}
