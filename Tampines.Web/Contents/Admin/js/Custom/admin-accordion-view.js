$(document).ready(function () {
    $(document).on("click", "#accordianDeletePopup", AccordionDeletePopup);
    $(document).on("click", "#btnconfirm_delete", submitDelete);
    $(document).on("click", "#publishToLive", PublishToLive);
    $(document).on("click", "#previewBtn", PreviewSurvey);
});
function PreviewSurvey() {

    $("#Form").attr({
        action: "/admin/ResidentServices/PreviewAccordion",
        target: "_blank"
    }).submit();

}
function PublishToLive() {
    $("#Form").attr({
        action: "/Admin/ResidentServices/PublishAccordionToLive"
    }).removeAttr("target").submit();

}
function AccordionDeletePopup() {
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
        action: "/Admin/ResidentServices/DeleteAccordion"
    }).removeAttr("target").submit();
}
$('input[name=uploadAttach_1]').change(function () {
    var fileName = document.getElementById("uploadAttach_1").files.item(0).name;
    document.getElementById("uploadFile_1").value = fileName;
});
$('input[name=uploadAttach_2]').change(function () {
    var fileName = document.getElementById("uploadAttach_2").files.item(0).name;
    document.getElementById("uploadFile_2").value = fileName;
});