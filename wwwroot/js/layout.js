async function loadHTML(selector, url) {
    const response = await fetch(url);
    document.querySelector(selector).innerHTML = await response.text();
}

function toggleSidebar() {
    document.getElementById('sidebar').classList.toggle('hidden');
    document.getElementById('content').classList.toggle('expanded');
}

function renderAuthBlock() {
    const isAuthenticated = true; // ← получаешь с backend / localStorage
    const userName = "User";

    const block = document.getElementById("auth-block");

    block.innerHTML = isAuthenticated
        ? `<span>Уведомл.</span>
           <span>${userName}</span>
           <a href="/logout">Выйти</a>`
        : `<span>Уведомл.</span>
           <a href="/login">Войти</a>`;
}

async function loadPage() {
    const page = new URLSearchParams(location.search).get("page") || "dashboard";

    await loadHTML("#header", "/partials/header.html");
    await loadHTML("#sidebar", "/partials/sidebar.html");
    await loadHTML("#content", `/pages/${page}.html`);

    renderAuthBlock();
}

loadPage();
