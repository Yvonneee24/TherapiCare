var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: 'schedules/getall' },
        "columns": [
            { data: 'therapyProgram.name'},
            { data: 'date'},
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="schedules/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                    </div>`
                },
                "width": "15%"
            }
        ]
    });
}
