// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    $('#dataTable').dataTable({
        processing: true,
        searching: true,
        paging: true
    });

    $("#btnShowModal").click(function () {
        $("#viewImgModal").modal('show');
    });

    $("#btnHideModal").click(function () {
        $("#viewImgModal").modal('hide');
        $("#addImgModal").modal('hide');
    });


    $("#btnAddHideModal").click(function () {
        $("#addImgModal").modal('hide');
    });

    $("#btnShowAddModal").click(function () {
        $("#addImgModal").modal('show');
    });

});
function imgClicked(imgID) {
    document.getElementById("showImg").src = imgID.src;
    $("#viewImgModal").modal('show');

}


function imgAddClicked() {
    $("#addImgModal").modal('show');

}