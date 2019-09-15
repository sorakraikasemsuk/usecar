// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function ResponseDialog(code,location) {
    if (code === 200) {
        $.showAlert({
            title: "แจ้งเตือน",
            body: "บันทึกข้อมูลสำเร็จ",
            onDispose: function () {
                window.location.href = location;
            }
        });
    } else {
        $.showAlert({
            title: "แจ้งเตือน",
            body: "บันทึกข้อมูลเกิดข้อผิดพลาด",
            onDispose: function () {
                window.location.reload();
            }
        });
    }
}