(function () {

    window.SpaDataTables = {
        datasets: {},

        registerDataset(name, data) {
            this.datasets[name] = data;
        },

        initialize() {
            const tables = document.querySelectorAll("table[data-dt='true']");

            tables.forEach(table => {
                if (table.classList.contains("dt-initialized")) return;

                const datasetName = table.dataset.dataset;
                const data = SpaDataTables.datasets[datasetName] || [];

                const columns = Array.from(
                    table.querySelectorAll("thead th")
                ).map(th => ({
                    data: th.dataset.field || th.textContent.trim()
                }));

                $(table).DataTable({
                    data,
                    columns,
                    layout: {
                        bottomEnd: {
                            paging: {
                                firstLast: false
                            }
                        }
                    }
                });

                table.classList.add("dt-initialized");
            });
        }
    };

})();