$(document).ready(function () {
    $.ajax({
        url: '/posts/feed',
        data: { page: 1, pageSize: 4, json: true },
        type: 'GET',
        success: function (result) {
            result.forEach(function (post) {
                var postHtml = `
                            <div class="post-container">
                                <header class="post-header">
                                    <div class="post-header-info">
                                        <div class="post-header-image">
                                                <img class="header-img-data" src="/images/navbar/shadow-boy-white-eyes-unique-cool-pfp-nft-13yuypusuweug9xn.jpg" />
                                            <h1 class="header-img-text">David Petkov</h1>
                                                <img class="uploaded-image-file" src="${post.imageUrl}" alt="Image" />
                                        </div>
                                        <div class="post-header-hour">
                                            <h1><strong>‧</strong></h1>
                                            <h1 class="headerhour-text">2 hr. ago</h1>
                                        </div>
                                    </div>
                                    <div class="post-header-buttons">
                                        <a class="follow-btn">Follow</a>
                                        <a class="report-btn"><i class="fa fa-flag"></i></a>
                                    </div>
                                </header>
                                <div class="post-content">
                                    <div class="post-content-bg"></div>
                                    <h1 class="content-title">${post.title}</h1>
                                    <p class="content-text-container">
                                        ${post.description}
                                    </p>
                                </div>
                                <div class="post-footer">
                                    <div class="footer-buttons">
                                        <a class="footer-btn-action" href="#"><i class="fa fa-heart"></i> ${post.likeCount}</a>
                                        <a class="footer-btn-action" href="#"><i class="fa fa-comment"></i> ${post.commentCount}</a>
                                        <a class="footer-btn-action" href="#"><i class="fa fa-share"></i> Share</a>
                                    </div>
                                </div>
                            </div>
                            <div class="under-post-line"></div>`;

                $('#posts-container').append(postHtml);
            });
        }
    });
});
