$(document).ready(function () {
    $.ajax({
        url: '/news/news',
        data: { page: 1, pageSize: 8, json: true },
        type: 'GET',
        success: function (result) {
            result.forEach(function (item) {
                var newsHtml = `
                                   <div class="card">
                                    ${item.image ? `<img class="img" src="${item.image}" />` : `<img class="img" src="https://nbhc.ca/sites/default/files/styles/article/public/default_images/news-default-image%402x_0.png?itok=B4jML1jF" />`}
                                    <div class="content">
                                        <div class="info">
                                            <p class="author">${item.author}</p>
                                            <p class="timestamp">${item.createdDateTime}3</p>
                                        </div>
                                        <h4 class="heading">${item.title}e</h4>
                                        <p class="description">${item.description}</p>
                                    </div>
                                </div>`;

                $('#news-container').append(newsHtml);
            });
        }
    });
});
