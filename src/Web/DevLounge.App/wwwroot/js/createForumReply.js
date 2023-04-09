const NESTED_REPLY_DELIMITER = '<=>&<=>&<=>&<=>';

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

const getCurrentThreadId = () => {
    const urlFragments = window.location.href.split("/");
    const threadId = urlFragments[urlFragments.length - 1];

    return threadId;
}

const createForumReplyApi = (forumReplyContent, threadId) => {
    return fetch(`/Threads/${threadId}/Replies/Create`, {
        method: 'POST',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            content: forumReplyContent,
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
            const forumReply = createNode(parsedReply);
            forumThreadRepliesContainer.append(forumReply);
            forumReply.scrollIntoView();

            refreshUserReplies(userId, userRepliesCount);
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
                    <div><a class="forum-reply-index-actions-hyperlink" href="#$id">#$id</a></div>
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

const forumReplyToReplyCancelButton = '<button class="forum-thread-details-button forum-thread-details-reply-button">Cancel</button>';

const forumReplyCreationTemplate =
    `<div id="forum-thread-reply-to-reply-container" class="forum-thread-reply">
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

const forumReplyNestedCreationTemplate =
`<=>&<=>&<=>&<=>
$createdByUsername
$createdOn
$id
$content
<=>&<=>&<=>&<=>
`;

const forumReplyNestedTemplate =
    `<div class="nesting-container">
        <div class="forum-thread-reply">
            <header>
                <img class="forum-thread-reply-user-thumbnail" src="/images/icons/default-user-thumbnail.png" alt="" />
                <h1 class="forum-thread-reply-user-name">$createdByUsername</h1>
            </header>
            <main>
                <header>
                    <span class="forum-reply-timestamp">$createdOn</span>
                    <div class="forum-reply-index-actions">
                        <div><img class="forum-reply-index-actions-share" src="/images/icons/share.svg" alt="..." /></div>
                        <div><a class="forum-reply-index-actions-hyperlink" href="#$id">#$id</a></div>
                    </div>
                </header>
                <main>
                    $content
                </main>
            </main>
        </div>
    </div>`;

const forumPostReplyButton = document.querySelector('#forum-post-reply-post-button');
const forumReplyButtons = document.querySelectorAll('.forum-thread-details-reply-button');

forumReplyButtons.forEach(replyButton => {
    replyButton.addEventListener('click', (e) => {

        // Prepare necessary data
        const forumReplyToBeRepliedTo = e.target.parentElement.parentElement.parentElement;
        const forumReplies = e.target.parentElement.parentElement.parentElement.parentElement;
        const nextReplySibling = forumReplyToBeRepliedTo.nextSibling.nextSibling;

        const forumReplyId = forumReplyToBeRepliedTo.id.replace('forum-thread-reply-', '');

        if (nextReplySibling) {
            // Create necessary nodes
            const cancelButton = createNode(forumReplyToReplyCancelButton);
            const replyToReplyContainer = createNode(forumReplyCreationTemplate);

            const clearReplyToReplyElements = () => {
                replyToReplyContainer.remove();
                cancelButton.remove();
            }

            // Hide [Reply] button and show [Cancel] button
            e.target.style.display = "none";
            cancelButton.addEventListener('click', () => {
                e.target.style.display = "block";
                clearReplyToReplyElements();
            });
            e.target.parentElement.append(cancelButton);

            // Reply to Reply button attach event
            replyToReplyContainer.querySelector('#forum-post-reply-to-reply-post-button').addEventListener('click', () => {
                const forumReplyContent = document.querySelector('#forum-post-reply-to-reply-content');

                fetch(`/Threads/${getCurrentThreadId()}/Replies/${forumReplyId}`)
                    .then(res => res.json())
                    .then(json => {
                        const nestedReplyContent = json.content;
                        const nestedReplyContentElements = nestedReplyContent.split(NESTED_REPLY_DELIMITER).filter(element => element.trim().length > 0);
                        const nestedReplyNestedContent =
                            `${NESTED_REPLY_DELIMITER}\n${nestedReplyContentElements.splice(0, nestedReplyContentElements.length - 1).join(NESTED_REPLY_DELIMITER).trim()}\n${NESTED_REPLY_DELIMITER}`;
                        const nestedReplyPureContent = nestedReplyContentElements[nestedReplyContentElements.length - 1].trim();

                        const parsedNestedReply = forumReplyNestedCreationTemplate
                            .replace('$createdByUsername', json.createdBy.userName)
                            .replace('$createdOn', formatDate(json.createdOn))
                            .replace('$id', json.id)
                            .replace('$content', nestedReplyPureContent);

                        createForumReplyApi(nestedReplyNestedContent + parsedNestedReply + forumReplyContent.value, getCurrentThreadId())
                            .then(() => {
                                forumReplyContent.value = '';
                                clearReplyToReplyElements()
                            });
                    });

                
            });

            // Attach Reply to Reply Container
            forumReplies.insertBefore(replyToReplyContainer, nextReplySibling);
        } else {
            document.querySelector('#forum-post-reply-content').scrollIntoView();
            document.querySelector('#forum-post-reply-content').focus();
        }
    });
});

forumPostReplyButton.addEventListener('click', () => {
    const forumReplyContent = document.querySelector('#forum-post-reply-content');

    createForumReplyApi(forumReplyContent.value, getCurrentThreadId()).then(() => forumReplyContent.value = '');
});