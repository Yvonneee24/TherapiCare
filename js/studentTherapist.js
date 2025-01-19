var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/therapist/therapistlist/getall' },
        "columns": [
            { data: 'name', "width": "15%" },
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
                    <a href="/Therapist/Reports/ViewAllReportsForStudent?id=${data}"class="btn btn-info view-reports"> <i class='bx bx-detail'></i> View All Report</a>
                    <a href="/Therapist/Reports/Create?studentId=${data}" class="btn btn-primary"> <i class='bx bxs-file-plus'></i> Create New Report </a>
                    
                     </div>`
                },
                "width": "25%"
            }
        ]
    });
}
$(document).ready(function () {
    $('.view-reports').click(function (e) {
        e.preventDefault();
        var studentId = $(this).data('student-id');

        // Perform AJAX request to check if reports exist
        $.get('@Url.Action("ViewAllReportsForStudent", "Reports")', { id: studentId })
            .done(function () {
                // Redirect to view reports action
                window.location.href = '@Url.Action("ViewAllReportsForStudent", "Reports", new { area = "Therapist" })?id=' + studentId;
            })
            .fail(function () {
                // Show modal popup if no reports found
                $('#noReportsModal').modal('show');
            });
    });
});