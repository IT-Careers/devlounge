const forumSectionsContextMenu = document.querySelector('#forum-sections-context-menu');

document.body.addEventListener('click', (event) => {
    if (!forumSectionsContextMenu.contains(event.target)) {
        forumSectionsContextMenu.style.display = 'none';
    }
}, true);

document.querySelectorAll(".forum-admin-panel-sections .forum-admin-panel-column-element-actions img")
    .forEach(optionsButton => {
        optionsButton.addEventListener("click", (event) => {
            forumSectionsContextMenu.style.left = (event.clientX + 25) + 'px';
            forumSectionsContextMenu.style.top = (event.clientY - 65) + 'px';
            forumSectionsContextMenu.style.display = 'inline-flex';

            // Set Option Endpoints
            forumSectionsContextMenu.querySelector('#forum-admin-panel-context-menu-details').href
                = '/Administration/Sections/' + optionsButton.id;
            forumSectionsContextMenu.querySelector('#forum-admin-panel-context-menu-modify').href
                = '/Administration/Sections/Edit/' + optionsButton.id;
            forumSectionsContextMenu.querySelector('#forum-admin-panel-context-menu-delete').addEventListener('click', (event) => {
                openAdminPanelDeleteModal('Forum Section',
                    optionsButton.id,
                    optionsButton.dataset.name,
                    '/Administration/Sections/Delete/' + optionsButton.id);
            });
        });
    });