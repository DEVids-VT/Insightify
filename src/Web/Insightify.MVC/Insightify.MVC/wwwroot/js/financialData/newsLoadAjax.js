$(document).ready(function () {
    $.ajax({
        url: '/news/news',
        data: { page: 1, pageSize: 4, json: true },
        type: 'GET',
        success: function (result) {
            result.forEach(function (item) {
                var newsHtml = `
                                    <div class="news-container-single">
                                        <div class="news-main">
                                            <div class="news-header-hour">
                                                <h1 class="news-headerhour-title">${item.author}</h1>
                                                <h1><strong>‧</strong></h1>
                                                <h1 class="news-headerhour-text">${item.createdDateTime}</h1>
                                            </div>
                                            <div class="news-content">
                                                <h1 class="news-content-title">${item.description}</h1>
                                            </div>
                                        </div>
                                        <div>
                                            <img class="news-image" src="/images/camera_lense_0.jpeg" />
                                       </div>
                                    </div>
                                    <div class="news-under-post-line"></div>`;

                $('#news-container').append(newsHtml);
            });
        }
    });
});
