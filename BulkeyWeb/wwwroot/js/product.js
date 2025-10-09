$(document).ready(function () {
    LoadDataTable();
});

function LoadDataTable() {
    $('#tblData').DataTable({
        "ajax": {
            "url": "/admin/product/getall"
        },
        "columns": [
            { data: "title", "width": "15%" },
            { data: "author", "width": "15%" },
            { data: "isbn", "width": "15%" },   // 👈 You wrote `ispn` before
            { data: "listPrice", "width": "15%" }
        ]
    });
}
