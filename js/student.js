var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/adminlist/getall' },
        "columns": [
            { data: 'name', "width": "15%" },
            {
                data: 'dob',
                "render": function (data) {
                    var date = new Date(data);
                    return date.toLocaleDateString();
                },
                "width": "15%"
            },
            {
                data: 'gender',
                "render": function (data) {
                    return data === 0 ? 'Male' : 'Female';
                },
                "width": "10%"
            },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/admin/adminlist/details?id=${data}" class="btn btn-primary">Details</a>          
                     </div>`
                },
                "width": "25%"
            }
        ]
    });
}
