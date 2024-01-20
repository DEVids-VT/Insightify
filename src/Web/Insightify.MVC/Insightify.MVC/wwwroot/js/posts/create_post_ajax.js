$(document).ready(function () {
    $('#submitBtn').click(function () {
        var title = document.getElementById('postTitle').value;
        var description = document.getElementById('postDescription').value;
        var imageData = document.getElementById('image').files[0];

        var formData = new FormData();
        formData.append('Title', title);
        formData.append('Description', description);
        formData.append('Image', imageData);

        $.ajax({
            url: '/posts/create',
            type: 'POST',
            processData: false,
            contentType: false,
            data: formData,
            success: function (data) {
                console.log(data);
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
});