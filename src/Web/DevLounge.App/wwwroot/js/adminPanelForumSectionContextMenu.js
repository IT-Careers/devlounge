const contextMenu = document.querySelector('.forum-admin-panel-context-menu');

document.body.addEventListener('click', (event) => {
    if (!contextMenu.contains(event.target)) {
        contextMenu.style.display = 'none';
    }
}, true);

document.querySelectorAll(".forum-admin-panel-sections .forum-admin-panel-column-element-actions img")
    .forEach(optionsButton => {
        optionsButton.addEventListener("click", (event) => {
            contextMenu.style.left = (event.clientX + 25) + 'px';
            contextMenu.style.top = (event.clientY - 65) + 'px';
            contextMenu.style.display = 'inline-flex';

            // Set Option Endpoints
            document.getElementById('forum-admin-panel-context-menu-details').href
                = '/Administration/Sections/' + optionsButton.id;
            document.getElementById('forum-admin-panel-context-menu-modify').href
                = '/Administration/Sections/Edit/' + optionsButton.id;
            document.getElementById('forum-admin-panel-context-menu-delete').addEventListener('click', (event) => {
                openAdminPanelDeleteModal('Forum Section',
                    optionsButton.id,
                    optionsButton.dataset.name,
                    '/Administration/Sections/Delete/' + optionsButton.id);
            });
        });
    });