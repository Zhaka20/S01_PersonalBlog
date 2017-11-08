setCommentSubmitHandler();
setCommentReplyHandler();
setAjaxVoteHanlders();
function setCommentSubmitHandler() {
    var commentForm = $('.posts-details-page .comment-form').first();
    commentForm.on('submit', eventHandler);
    function eventHandler(e) {
        e.preventDefault();
        var targetCommentForm = $(e.target);
        if (targetCommentForm.valid()) {
            var submitBtn = $('input[type="submit"]', targetCommentForm).prop('disabled', true);
            var url = targetCommentForm.attr('action');
            $.ajax({
                url: url,
                type: 'POST',
                data: targetCommentForm.serialize(),
                success: function (response) {
                    $(".posts-details-page .post-comments").prepend(response);
                },
                error: function (err) {
                },
                complete: function () {
                    submitBtn.prop('disabled', false);
                }
            });
        }
    }
}
function setCommentReplyHandler() {
    $('.post-comments').on('click', '[data-comment-form-trigger]', replyLinkClickEH);
}
function replyLinkClickEH(e) {
    e.preventDefault();
    var targetCommentDiv = e.target.closest('[data-comment-container] .comment');
    var commentForm = $('.posts-details-page #comment-reply-form');
    $('textarea', commentForm).val('');
    var linkURL = e.target.getAttribute('href');
    commentForm.attr('action', linkURL);
    commentForm.appendTo(targetCommentDiv);
    commentForm.show();
    commentForm.on('submit', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var targetCommentForm = $(e.target);
        if (targetCommentForm.valid()) {
            var submitBtn = $('input[type="submit"]', targetCommentForm).prop('disabled', true);
            var url = targetCommentForm.attr('action');
            $.ajax({
                url: url,
                type: 'POST',
                data: targetCommentForm.serialize(),
                success: function (response) {
                    response = $(response);
                    var marginFixDiv = $("<div>", { "class": "fix-margin" });
                    marginFixDiv.append(response);
                    $(targetCommentDiv).after(marginFixDiv);
                    targetCommentForm.hide();
                },
                error: function (err) {
                },
                complete: function () {
                    submitBtn.prop('disabled', false);
                    commentForm.off(e);
                }
            });
        }
    });
}
function setAjaxVoteHanlders() {
    var likePostForm = $(".post-like-vote-form");
    likePostForm.on('submit', likePostSubmitHandler);
    var dislikePostForm = $(".post-dislike-vote-form");
    dislikePostForm.on('submit', dislikePostSubmitHandler);
}
function likePostSubmitHandler(e) {
    e.preventDefault();
    var submitBtn = $('button[type="submit"]', e.target).prop('disabled', true);
    var url = "/api" + e.target.getAttribute('action');
    $.ajax({
        url: url,
        dataType: 'json',
        type: 'POST',
        data: $(e.target).serialize(),
        success: function (response) {
            $('.badge.likes-badge').text(response.Likes);
            $('.badge.dislikes-badge').text(response.Dislikes);
            $('.post-liked-indicator,.post-disliked-indicator').addClass('hidden');
            if (response.Value === true) {
                $('.post-liked-indicator').removeClass('hidden');
            }
        },
        error: function (err) {
            alert("Error");
        },
        complete: function () {
            submitBtn.prop('disabled', false);
        }
    });
}
function dislikePostSubmitHandler(e) {
    e.preventDefault();
    var submitBtn = $('button[type="submit"]', e.target).prop('disabled', true);
    var url = "/api" + e.target.getAttribute('action');
    $.ajax({
        url: url,
        type: 'POST',
        data: $(e.target).serialize(),
        dataType: 'json',
        success: function (response) {
            $('.badge.likes-badge').text(response.Likes);
            $('.badge.dislikes-badge').text(response.Dislikes);
            $('.post-liked-indicator,.post-disliked-indicator').addClass('hidden');
            if (response.Value === false) {
                $('.post-disliked-indicator').removeClass('hidden');
            }
        },
        error: function (err) {
            alert("Error");
        },
        complete: function () {
            submitBtn.prop('disabled', false);
        }
    });
}
//# sourceMappingURL=site.js.map