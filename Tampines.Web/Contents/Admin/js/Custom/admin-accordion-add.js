$(document).ready(function () {
    initializeValidation();
    $('input[name=PdfFile]').change(function () {
        var filename = $(this).val();
        if (filename.substring(3, 11) == 'fakepath') {
            filename = filename.substring(12);
        }
        document.getElementById("PdfFileValue").value = filename;
    });
    $(document).on("click", "#AddAccordionSubmit", AddAccordionSubmit);
    $(document).on("click", "#EditAccordionSubmit", EditAccordionSubmit)
    $(document).on("click", "#AddPdfSubmit", AddPdfSubmit)
    $(document).on("click", "#accordianPdfDeletePopup", AccordionPdfDeletePopup);
    $(document).on("click", "#btnconfirm_delete", submitDelete);
    $(document).on("click", ".EditAccordionPdfPopup", EditAccordionPdf);
    $(document).on("click", "#EditPdfSubmit", EditPdfSubmit)
});
function EditAccordionPdf() {
    var Encdetail = $(this).attr("data-uniquecode");
    var EncGUID = $(this).attr("data-uniquecode1");
    var path = apppath + "/Admin/ResidentServices/EditAccordionPdfPartialView/" + Encdetail + "/" + EncGUID;
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }

            $("#EditPDFForm").html('');
            $("#EditPDFForm").append(response);
        },
        failure: function (response) {
            console.log(response);
            $("#EditPDFForm").append(('Error occured'));
        }
    });
}
function AddAccordionSubmit() {
    if ($("#AddAccordionForm").valid()) {
        $("#AddAccordionForm").attr({
            action: "/admin/ResidentServices/AddAccordion"
        }).submit();
    }
}
function EditAccordionSubmit() {
    if ($("#EditAccordionForm").valid()) {
        $("#EditAccordionForm").attr({
            action: "/admin/ResidentServices/EditAccordion"
        }).submit();
    }
}
function AddPdfSubmit() {
    if ($("#AddPDFForm").valid()) {
        $("#AddPDFForm").attr({
            action: "/admin/ResidentServices/AddPDF"
        }).submit();
    }
}
function EditPdfSubmit() {
    if ($("#EditPDFForm").valid()) {
        $("#EditPDFForm").attr({
            action: "/admin/ResidentServices/EditPDF"
        }).submit();
    }
}
function AccordionPdfDeletePopup() {
    $('#btnconfirm_delete').attr('data-value', $(this).attr("data-uniquecode"));
}
function submitDelete() {
    $("#dynamic_container").html('').append($("<input>", {
        id: "EncDetail",
        name: "EncDetail",
        type: "hidden",
        value: $('#btnconfirm_delete').data('value')
    }));

    $("#Form").attr({
        action: "/Admin/ResidentServices/DeleteAccordionPdf"
    }).submit();
}
function initializeValidation() {
    jQuery.validator.addMethod("alphanumeric", function (value, element) {
        return this.optional(element) || /^[ A-Za-z0-9_.#^%!*|+-]*$/i.test(value);
    }, "Please avoid using special character like (/',&(){}=\?<>$");

    $("#AddAccordionForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {
            Title: {
                required: true,
                remote: {
                    url: apppath + "/Admin/ResidentServices/CheckAccordionTitle",
                    type: "GET",
                    async: true,
                    cache: false,
                    data: {
                        Title: function () {
                            return $("#Title").val();
                        },
                        EncDetail: function () {
                            return $("#GUID").val() || null;
                        }
                    },
                    dataType: 'json'
                }
            },
            ShortDescription: {
                required: true
            }
        }

    });

    $("#EditAccordionForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {
            Title: {
                required: true,
                remote: {
                    url: apppath + "/Admin/ResidentServices/CheckAccordionTitle",
                    type: "GET",
                    async: true,
                    cache: false,
                    data: {
                        TicketNumber: function () {
                            return $("#Title").val();
                        },
                        EncDetail: function () {
                            return $("#GUID").val() || null;
                        }
                    },
                    dataType: 'json'
                }
            },
            ShortDescription: {
                required: true
            }
        }

    });

    $("#AddPDFForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {
            PdfTitle: {
                required: true
            },
            "PdfFile": {
                required: true,
                extension: 'pdf'

            }
        },
        messages: {
            "PdfFile": {
                required: "Please upload pdf file"
            }
        }

    });

    $("#EditPDFForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {
            PdfTitle: {
                required: true
            },
            "PdfFile": {
                extension: 'pdf'

            }
        },
        messages: {
            "PdfFile": {
                required: "Please upload pdf file"
            }
        }


    });
}