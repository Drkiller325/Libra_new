(function () {

    const content = document.getElementById("spa-content");

    async function loadPage(url, pushState = true) {
        content.classList.add("loading");

        const response = await fetch(url, {
            headers: { "X-Requested-With": "XMLHttpRequest" }
        });

        const html = await response.text();
        const doc = new DOMParser().parseFromString(html, "text/html");
        const newContent = doc.querySelector("#spa-content");

        setTimeout(() => {
            content.innerHTML = newContent.innerHTML;
            content.classList.remove("loading");


            initDataTables();
            initTableInteractions("usersTable", "Admin", "User");


            if (pushState) {
                history.pushState({}, "", url);
            }
        }, 200);
    }

    window.loadPage = loadPage;

    $(document).ready(function () {
        $('input[type=text], input[type=password], input[type=email], select').on('input', function () {
            var fieldName = $(this).attr('name');
            $('span[data-valmsg-for="' + fieldName + '"]').text('');
            $(this).removeClass('is-invalid');
        });
    });

    document.addEventListener("click", e => {
        const link = e.target.closest("a[data-spa]");
        if (!link) return;

        e.preventDefault();
        loadPage(link.href);
    });

    document.addEventListener("click", function (e) {
        const btn = e.target.closest("#btnCancel");
        if (!btn) return;

        e.preventDefault();

        if (history.length > 1) {
            history.back();
        } else {
            loadPage("/Home/Index", false);
        }
    });

    window.addEventListener("popstate", (event) => {
        loadPage(location.pathname, false); // false = don't push state again
    });


})();