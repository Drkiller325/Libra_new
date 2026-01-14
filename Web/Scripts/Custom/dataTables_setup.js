function initDataTables() {
    $('[data-table]').each(function () {
        if ($.fn.DataTable.isDataTable(this)) return;

        const key = this.dataset.table;
        const config = tableList[key]?.();

        if (!config) return;

        $(this).DataTable(config);
    });
}

window.initTableInteractions = function () {

    let selectedUser = null;

    const table = $('#usersTable').DataTable();

    $('#usersTable tbody').off('click').on('click', 'tr', function () {

        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
            selectedUser = null;
            $('#editUserBtn').prop('disabled', true);
            $('#UserDetailsBtn').prop('disabled', true);
        } else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            selectedUser = table.row(this).data();
            $('#editUserBtn').prop('disabled', false);
            $('#userDetailsBtn').prop('disabled', false);
        }

    });

    $('#addUserBtn').off('click').on('click', function () {
        loadPage('/admin/GetAddUser');
    });

    $('#editUserBtn').off('click').on('click', function () {
        loadPage(`/admin/GetEditUser/${selectedUser.Id}`);
    });

    $('#userDetailsBtn').off('click').on('click', function () {
        loadPage(`/admin/GetUserDetails/${selectedUser.Id}`);
    });
}; 