const forumCategoriesContextMenu = document.querySelector('#forum-categories-context-menu');

document.body.addEventListener('click', (event) => {
    if (!forumCategoriesContextMenu.contains(event.target)) {
        forumCategoriesContextMenu.style.display = 'none';
    }
}, true);

document.querySelectorAll(".forum-admin-panel-categories .forum-admin-panel-column-element-actions img")
    .forEach(optionsButton => {
        optionsButton.addEventListener("click", (event) => {
            forumCategoriesContextMenu.style.left = (event.clientX + 25) + 'px';
            forumCategoriesContextMenu.style.top = (event.clientY - 65) + 'px';
            forumCategoriesContextMenu.style.display = 'inline-flex';

            // Set Option Endpoints
            forumCategoriesContextMenu.querySelector('#forum-admin-panel-context-menu-details').href
                = '/Categories/' + optionsButton.id;
            forumCategoriesContextMenu.querySelector('#forum-admin-panel-context-menu-modify').href
                = '/Administration/Categories/Edit/' + optionsButton.id;
            forumCategoriesContextMenu.querySelector('#forum-admin-panel-context-menu-delete').addEventListener('click', (event) => {
                openAdminPanelDeleteModal('Forum Category',
                    optionsButton.id,
                    optionsButton.dataset.name,
                    '/Administration/Categories/Delete/' + optionsButton.id);
            });
        });
    });