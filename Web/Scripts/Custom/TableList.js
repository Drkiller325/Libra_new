var tableList = {
    usersTable: () => ({
        ajax: {
            url: '/Admin/GetUsers',
            type: 'GET',
            dataSrc: ''
        },
        columns: [
            { visible: false, data: 'Id' },
            { data: 'Name', title: 'Name' },
            { data: 'Email', title: 'Email' },
            { data: 'Login', title: 'Login' },
            { data: 'Telephone', title: 'Telephone' },
            { data: 'UserRole', title: 'UserRole' }
        ],
        language: {
            search: "Filter records:", // Custom search placeholder
            lengthMenu: "Show _MENU_ entries",
            info: "Showing _START_ to _END_ of _TOTAL_ users",
            paginate: {
                firstLast: false,
                previous: "Prev",
                next: "Next"
            }
        },
        layout: {
            bottomEnd: {
                paging: {
                    firstLast: false
                }
            }
        }
    }),
};