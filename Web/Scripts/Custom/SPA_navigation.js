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

            SpaDataTables.initialize();

            if (pushState) {
                history.pushState({}, "", url);
            }
        }, 200);
    }

    document.addEventListener("click", e => {
        const link = e.target.closest("a[data-spa]");
        if (!link) return;

        e.preventDefault();
        loadPage(link.href);
    });

    window.addEventListener("popstate", () => {
        loadPage(location.pathname, false);
    });

    window.SpaNavigation = { loadPage };

})();