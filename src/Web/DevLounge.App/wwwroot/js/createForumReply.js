// Credit: https://stackoverflow.com/a/12409344
const formatDate = (dateString) => {
    const today = new Date(dateString);
    const yyyy = today.getFullYear();
    let mm = today.getMonth() + 1;
    let dd = today.getDate();

    if (dd < 10) dd = '0' + dd;
    if (mm < 10) mm = '0' + mm;

    return dd + '/' + mm + '/' + yyyy;
}

const createNode = (html) => {
    const node = document.createElement('temp');
    node.innerHTML = html;
    return node.children[0];
}

const forumReplyTemplate =
    `<div class="forum-thread-reply">
        <header>
            <img class="forum-thread-reply-user-thumbnail" src="/images/icons/default-user-thumbnail.png" alt="" />
            <h1 class="forum-thread-reply-user-name">$createdByUsername</h1>
            <div class="forum-thread-reply-user-info">
                <div>
                    <span>Joined:</span>
                    <span>$createdByRegisteredOn</span>
                </div>
                <div>
                    <span>Threads:</span>
                    <span>$createdByThreads</span>
                </div>
                <div>
                    <span>Replies:</span>
                    <span>$createdByReplies</span>
                </div>
                <div>
                    <span>Score:</span>
                    <span>$createdByScore</span>
                </div>
            </div>
        </header>
        <main>
            <header>
                <span class="forum-reply-timestamp">$createdOn</span>
                <div class="forum-reply-index-actions">
                    <div><img class="forum-reply-index-actions-share" src="/images/icons/share.svg" alt="..." /></div>
                    <div><a class="forum-reply-index-actions-hyperlink" href="#">#$id</a></div>
                </div>
            </header>
            <main>
                $content
            </main>
            <footer>
                <button class="forum-thread-details-button">Reply</button>
            </footer>
        </main>
    </div>`;

const forumPostReplyButton = document.querySelector('#forum-post-reply-post-button');

forumPostReplyButton.addEventListener('click', () => {
    const urlFragments = window.location.href.split("/");
    const threadId = urlFragments[urlFragments.length - 1];

    const forumReplyContent = document.querySelector('#forum-post-reply-content');

    fetch(`/Threads/${threadId}/Replies/Create`, {
        method: 'POST',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            content: forumReplyContent.value,
            threadId: threadId
        })
    })
        .then(res => res.json())
        .then(json => {
            const parsedReply = forumReplyTemplate
                .replace('$createdByUsername', json.createdBy.userName)
                .replace('$createdByRegisteredOn', formatDate(json.createdBy.registeredOn))
                .replace('$createdByThreads', !!json.createdBy.threadsCreated ? json.createdBy.threadsCreated.length : 0)
                .replace('$createdByReplies', !!json.createdBy.repliesCreated ? json.createdBy.repliesCreated.length : 0)
                .replace('$createdByScore', 0)
                .replace('$createdOn', formatDate(json.createdOn))
                .replace('$id', json.id)
                .replace('$content', json.content);

            const forumThreadRepliesContainer = document.querySelector('#forum-thread-replies');

            forumThreadRepliesContainer.append(createNode(parsedReply));
            forumReplyContent.value = '';
        });
});