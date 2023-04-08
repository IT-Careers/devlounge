// Credit: https://stackoverflow.com/a/12409344
const formatDate = (dateString) => {
    return timestampFormatUtility.mapTimestamp(new Date(dateString));
}

const createNode = (html) => {
    const node = document.createElement('temp');
    node.innerHTML = html;
    return node.children[0];
}

const refreshUserReplies = (userId, actualRepliesCount) => {
    const userRepliesCountElementClass = 'forum-thread-reply-user-' + userId + '-replies-count';

    [...document.getElementsByClassName(userRepliesCountElementClass)].forEach(repliesCountElement => {
        repliesCountElement.textContent = actualRepliesCount
    });
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
                    <span class="forum-thread-reply-user-$createdById-replies-count">$createdByReplies</span>
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
                <button class="forum-thread-details-button forum-thread-details-reply-button">Reply</button>
            </footer>
        </main>
    </div>`;

const forumReplyCreationTemplate =
    `<div class="forum-thread-reply">
        <header class="forum-reply-to-reply-header">
        </header>
        <main>
            <main>
                <textarea id="forum-post-reply-to-reply-content" class="forum-thread-post-reply-discussion" placeholder="Write your reply..." name="discussion"></textarea>
            </main>
            <footer>
                <button id="forum-post-reply-to-reply-post-button" class="forum-thread-details-button">Post Reply</button>
            </footer>
        </main>
    </div>`;

const forumPostReplyButton = document.querySelector('#forum-post-reply-post-button');
//document.querySelector('.forum-thread-details-button').parentElement.parentElement.parentElement.nextSibling.nextSibling
const forumReplyButtons = document.querySelectorAll('.forum-thread-details-reply-button');

forumReplyButtons.forEach(replyButton => {
    replyButton.addEventListener('click', (e) => {
        const forumReplyToBeRepliedTo = e.target.parentElement.parentElement.parentElement;
        const forumReplies = e.target.parentElement.parentElement.parentElement.parentElement;
        const nextReplySibling = forumReplyToBeRepliedTo.nextSibling.nextSibling;
        if (nextReplySibling) {
            forumReplies.insertBefore(createNode(forumReplyCreationTemplate), nextReplySibling);
            // Is not last
        } else {
            // is last
        }
    });
});

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
            const userId = json.createdBy.id;
            const userThreadsCount = !!json.createdBy.threadsCreated ? json.createdBy.threadsCreated.length : 0;
            const userRepliesCount = !!json.createdBy.repliesCreated ? json.createdBy.repliesCreated.length : 0;

            const parsedReply = forumReplyTemplate
                .replace('$createdById', userId)
                .replace('$createdByUsername', json.createdBy.userName)
                .replace('$createdByRegisteredOn', formatDate(json.createdBy.registeredOn))
                .replace('$createdByThreads', userThreadsCount)
                .replace('$createdByReplies', userRepliesCount)
                .replace('$createdByScore', 0)
                .replace('$createdOn', formatDate(json.createdOn))
                .replace('$id', json.id)
                .replace('$content', json.content);

            const forumThreadRepliesContainer = document.querySelector('#forum-thread-replies');
            forumThreadRepliesContainer.append(createNode(parsedReply));

            refreshUserReplies(userId, userRepliesCount);

            forumReplyContent.value = '';
        });
});