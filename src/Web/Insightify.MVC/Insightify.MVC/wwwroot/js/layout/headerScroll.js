$(document).ready(function () {
    $(window).scroll(function () {
        var scroll = $(window).scrollTop();
        var header = $('#header');
        var initialColor = '#ececec';
        var darkerColor = '#ffffff';

        if (scroll > 50) {
            var ratio = (scroll - 50) / 150;
            var newColor = blendColors(initialColor, darkerColor, ratio);
            header.css('background-color', newColor);
        } else {
            header.css('background-color', initialColor);
        }
    });

    function blendColors(color1, color2, ratio) {
        ratio = Math.min(1, Math.max(0, ratio));
        var r = Math.round(parseInt(color1.substring(1, 3), 16) * (1 - ratio) + parseInt(color2.substring(1, 3), 16) * ratio);
        var g = Math.round(parseInt(color1.substring(3, 5), 16) * (1 - ratio) + parseInt(color2.substring(3, 5), 16) * ratio);
        var b = Math.round(parseInt(color1.substring(5, 7), 16) * (1 - ratio) + parseInt(color2.substring(5, 7), 16) * ratio);
        return '#' + r.toString(16) + g.toString(16) + b.toString(16);
    }
});