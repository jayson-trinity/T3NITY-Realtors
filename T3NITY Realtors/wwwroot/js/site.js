// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    $('#dataTable').dataTable({
        processing: true,
        searching: true,
        paging: true
    });
    var imdId = "";
    $("#btnShowModal").click(function () {
        $("#viewImgModal").modal('show');
    });

    $("#btnHideModal").click(function () {
        $("#viewImgModal").modal('hide');
    });



});
function imgClicked(imgID) {
    //alert(imgID.id);
    //console.log(imgID);
   document.getElementById("showImg").src = imgID.src ;
    $("#viewImgModal").modal('show');

}