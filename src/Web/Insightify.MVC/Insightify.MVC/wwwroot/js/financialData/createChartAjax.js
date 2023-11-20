function loadChart(currency) {
    $(document).ready(function () {
        $.ajax({
            url: '/financialData/chart',
            data: { currency: currency },
            type: 'GET',
            success: function (result) {
                const formattedPrices = result.model.prices.map(item => ({
                    time: new Date(item.timestamp * 1000).toISOString().split('T')[0],
                    value: item.value
                }));
                createChat(formattedPrices, 'chart-container', 'Light', '400', '800');
            }
        });
    });
};