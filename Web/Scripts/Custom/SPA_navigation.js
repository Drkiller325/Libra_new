(function () {

    const content = document.getElementById("spa-content");

    // inject html into body
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

            runPageScripts(newContent);

            if (pushState) {
                history.pushState({}, "", url);
            }
        }, 200);
    }

    function runPageScripts(content) {
        content.querySelectorAll('script').forEach(oldScript => {
            const newScript = document.createElement('script');

            if (oldScript.src) {
                newScript.src = oldScript.src; // external script
            } else {
                newScript.textContent = oldScript.textContent; // inline script
            }

            document.head.appendChild(newScript).parentNode.removeChild(newScript);
        });
        //initDataTables();
        //initTableInteractions("usersTable", "Admin", "User");
        //initTableInteractions("PosesTable", "Pos", "Pos");
        //initTableInteractions("IssuesTable", "Issue", "Issue");
        //initTableInteractions("PosTable_Issue", "Issue", "IssuePos");
    }

    window.loadPage = loadPage;

    //override <a> logic
    //document.addEventListener("click", e => {
    //    const link = e.target.closest("a[data-spa]");
    //    if (!link) return;

    //    e.preventDefault();
    //    loadPage(link.href);
    //});

    //Cancel and back btn functionality
    //document.addEventListener("click", function (e) {
    //    const btn = e.target.closest("#btnCancel");
    //    if (!btn) return;

    //    e.preventDefault();

    //    const path = window.location.pathname; // e.g. "/Admin/GetEditUser/2"
    //    const parts = path.split("/").filter(p => p); // ["Admin", "GetEditUser", "2"]

    //    if (parts.length > 0) {
    //        const base = "/" + parts[0]; // "/Admin" or "/Pos"
    //        loadPage(base);
    //    } else {
    //        loadPage("/Home/Index");
    //    }
    //});

    // back btn in browser function
    //window.addEventListener("popstate", (event) => {
    //    loadPage(location.pathname, false); // false = don't push state again
    //});


})();