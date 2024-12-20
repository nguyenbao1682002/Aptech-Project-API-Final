﻿$(document).on("click", ".delete-link", function (e) {
    e.preventDefault();
    var id = $(this).data("id");
    var confirmMessage = $(this).data("confirm");

    $("#confirmMessage").text(confirmMessage);
    $("#confirmDelete").data("id", id); // Lưu trữ ID để sử dụng trong xử lý xóa

    $("#confirmModal").modal("show");
});
$(document).on("click", "#confirmDelete", function (e) {
    e.preventDefault();
    var id = $(this).data("id");
    //if (confirm($(this).data("confirm"))) {
    $.ajax({
        url: "/Admin/ExamAdmin/DeleteId/" + id,
        dataType: "json",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        success: function (res) {
            if (res.status == true) {
                window.location.href = '/Admin/ExamAdmin';
                //$("#getCodeModal").modal("toggle");
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    //}
});