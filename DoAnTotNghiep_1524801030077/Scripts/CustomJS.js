var preciousContent;
var justTextContent;
var justHtmlContent;
var editor;
var table;

function ConvertDate(date) {
    var temp = [];
    temp = date.split('-');
    return temp[1] + "-" + temp[0] + "-" + temp[2];
}

function InitEditor(id) {
    editor = new Quill(id, {
        theme: "snow", modules: {
            toolbar: [[{ font: [] }, { size: [] }], ["bold", "italic", "underline", "strike"], [{
                color: []
            }, { background: [] }], [{
                script: "super"
            }, {
                script: "sub"
            }], [{ header: [!1, 1, 2, 3, 4, 5, 6] }, "blockquote", "code-block"], [{
                list: "ordered"
            }, { list: "bullet" }, { indent: "-1" }, { indent: "+1" }], ["direction", {
                align: []
            }], ["link", "image", "video", "formula"], ["clean"]]
        }
    });

    editor.on('text-change', function () {
        var delta = editor.getContents();
        var text = editor.getText();
        var justHtml = editor.root.innerHTML;
        preciousContent = JSON.stringify(delta);
        justTextContent = text;
        justHtmlContent = justHtml;
    });
}
//chuyển ngày thành tuần trong năm
function getWeekNumber(d) {
    d = new Date(Date.UTC(d.getFullYear(), d.getMonth(), d.getDate()));
    d.setUTCDate(d.getUTCDate() + 4 - (d.getUTCDay() || 7));
    var yearStart = new Date(Date.UTC(d.getUTCFullYear(), 0, 1));
    var weekNo = Math.ceil((((d - yearStart) / 86400000) + 1) / 7);
    return [d.getUTCFullYear(), weekNo];
}
//chuyển tuần trong năm thành ngày
function dateFromWeekNumber(year, week) {
    var d = new Date(year, 0, 1);
    var dayNum = d.getDay();
    var diff = --week * 7;
    if (!dayNum || dayNum > 4) {
        diff += 7;
    }
    d.setDate(d.getDate() - d.getDay() + ++diff);
    return d;
}
//init table
function InitTable(id) {
    return $(id).DataTable({
        paging: true,
        ordering: true,
        searching: true,
        "iDisplayLength": 25,
    });
}
//show alert
function ShowAlert(status, mess) {
    //[Success,]-headText
    //[success,warning,error,info]-icon
    $.toast({
        heading: 'Thông báo',
        text: mess,
        showHideTransition: 'slide',
        icon: status,
        position: 'top-left'
    });
}
function getselect2loaigiocongtac(id) {
    $.ajax({
        url: '/WorkingHourDefine/GetAllDetail',
        method: 'Post'
    }).done(function (data) {
        //console.log(data)
        $(id).selectpicker('destroy');
        $(id).empty();
        for (let i = 0; i < data.returnData.length; i++) {
            $(id).append(`<option value="${data.returnData[i]["pid"]}">${data.returnData[i]["content"]}</option>`);
        }
        $(id).selectpicker({
            size: 50
        });

    });

}
function getselect2congtac(id) {
    $.ajax({
        url: '/ExtratimeActiveTemplate/GetAll',
        method: 'Post'
    }).done(function (data) {
        console.log(data)
        $(id).selectpicker('destroy');
        $(id).empty();
        for (let i = 0; i < data.jsData.length; i++) {
            $(id).append(`<option value="${data.jsData[i]["PID"]}">${data.jsData[i]["Content"]}</option>`);
        }
        $(id).selectpicker({
            size: 50
        });

    });
}
function getselect2role(id) {
    $.ajax({
        url: '/Data/GetAllRole',
        method: 'Post'
    }).done(function (data) {
        //console.log(data)
        $(id).selectpicker('destroy');
        $(id).empty();
        for (let i = 0; i < data.jsData.length; i++) {
            $(id).append(`<option value="${data.jsData[i]["Code"]}">${data.jsData[i]["Name"]}</option>`);
        }
        $(id).selectpicker({
            size: 50
        });

    });

}
function getselect2User(id) {
    $.ajax({
        url: '/Data/GetAllUser',
        method: 'Post'
    }).done(function (data) {
        console.log(data)
        $(id).selectpicker('destroy');
        $(id).empty();
        for (let i = 0; i < data.jsData.length; i++) {
            $(id).append(`<option value="${data.jsData[i]["msgv"]}">${data.jsData[i]["msgv"]}-${data.jsData[i]["fullName"]}</option>`);
        }
        $(id).selectpicker({
            size: 50
        });
        $('.dropdown-toggle').css('width', '30%')

    });

}