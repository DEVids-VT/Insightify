document.addEventListener('DOMContentLoaded', function () {
    var textContent = document.getElementById('text-content');
    var imageContent = document.getElementById('image-content');
    var previewContent = document.getElementById('preview-content');

    var btnPost = document.getElementById('btn-post');
    var btnImage = document.getElementById('btn-image');
    var btnPreview = document.getElementById('btn-preview');
    var btnSubmit = document.getElementById('submit');

    var textDisplayImage = document.getElementById('text-display-image');
    var displayText = document.getElementById('display-text');
    var displayPreview = document.getElementById('display-preview');
    var previewDisplayImage = document.getElementById('preview-display-image');

    function showContent(content) {
        textContent.classList.add('hidden');
        imageContent.classList.add('hidden');
        previewContent.classList.add('hidden');

        content.classList.remove('hidden');
    }

    showContent(textContent);

    btnPost.addEventListener('click', function () {
        showContent(textContent);
    });

    btnImage.addEventListener('click', function () {
        showContent(imageContent);
    });

    btnPreview.addEventListener('click', function () {
        showContent(previewContent);
    });

    textDisplayImage.addEventListener('click', function () {
        showContent(imageContent);
    });

    displayText.addEventListener('click', function () {
        showContent(textContent);
    });

    displayPreview.addEventListener('click', function () {
        showContent(previewContent);
    });

    previewDisplayImage.addEventListener('click', function () {
        showContent(imageContent);
    });
});