$(document).ready(function () {
    intializepagination();

   
});


function intializepagination() {
    $(".pagination").pagination({
        items: $('.pagination').data('totalitems'),
        itemsOnPage: 10,
        currentPage: $('.pagination').data('pageindex'),
        hrefTextPrefix: "",
        prevText: "◄",
        nextText: "►",
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

    var path = apppath + "/Event/PartialVieweEvents/" + e.PageNumber

    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response == '') {
                response = '<li>No Results Found</li>';
            }

            $("#eventresult").html(response);
            var pageIndex = parseInt($('.pagination').attr('data-pageindex'));
            //console.log(pageIndex);
            var startItem = (pageIndex == 1 ? 0 : (pageIndex - 1) * pageSize) + 1;
          

        },
        failure: function (response) {
            console.log(response);
            $("#eventresult").html('<li>No Results Found</li>');
        }
    });

}


