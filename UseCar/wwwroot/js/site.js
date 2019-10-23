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
function ExistDialog(text) {
    $.showAlert({
        title: 'แจ้งเตือน',
        body: 'ข้อมูล <b>' + text + '</b> มีอยู่ในระบบแล้ว'
    });
}
//dropdown car data
function BrandSelect() {
    $('#generationId > option').not(':first').remove();
    var _brandId = $('#brandId').val();
    $.ajax({
        url: '/SharedData/GetGenerationByBrandId',
        type: 'GET',
        data: {
            brandId: _brandId
        },
        success: function (data) {
            data.forEach(function (item) {
                $("#generationId").append(new Option(item.text, item.value));
            })
        }
    })
}
function GenerationSelect() {
    $('#faceId > option').not(':first').remove();
    var _generationId = $('#generationId').val();
    $.ajax({
        url: '/SharedData/GetFaceById',
        type: 'GET',
        data: {
            generationId: _generationId
        },
        success: function (data) {
            data.forEach(function (item) {
                $("#faceId").append(new Option(item.text, item.value));
            });
        }
    })
}
function FaceSelect() {
    $('#subFaceId > option').not(':first').remove();
    var _faceId = $('#faceId').val();
    $.ajax({
        url: '/SharedData/GetSubFaceByFaceId',
        type: 'GET',
        data: {
            faceId: _faceId
        },
        success: function (data) {
            data.forEach(function (item) {
                $("#subFaceId").append(new Option(item.text, item.value));
            });
        }
    })
}