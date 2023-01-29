const openAdminPanelDeleteModal = (entity, id, name, deleteUrl) => {
    document.querySelector('.forum-admin-panel-delete-modal').style.display = 'block';
    document.querySelectorAll('.forum-admin-panel-delete-modal-entity').forEach(element => {
        element.textContent = entity;
    });
    document.querySelectorAll('.forum-admin-panel-delete-modal-id').forEach(element => {
        element.textContent = id;
    });
    document.querySelectorAll('.forum-admin-panel-delete-modal-name').forEach(element => {
        element.textContent = name;
    });
    document.querySelectorAll('.forum-admin-panel-delete-modal-form').forEach(element => {
        element.action = deleteUrl;
    });
}

const closeAdminPanelDeleteModal = () => {
    document.querySelector('.forum-admin-panel-delete-modal').style.display = 'none';
}

document.querySelectorAll('.forum-admin-panel-delete-modal-backdrop, .forum-admin-panel-delete-modal-confirm, .forum-admin-panel-delete-modal-cancel')
.forEach(closingElement => {
    closingElement.addEventListener('click', (event) => {
        closeAdminPanelDeleteModal();
    });
});