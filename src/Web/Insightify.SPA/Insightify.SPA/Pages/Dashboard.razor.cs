using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Insightify.SPA.Pages
{
    public partial class Dashboard
    {
        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;

        private string Theme { get; set; } = "Light";

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("createChart", ChartDatas(500), "price-card", Theme, "600", "900");

                await JSRuntime.InvokeVoidAsync("createChart", ChartDatas(400), "change-card", Theme, "200", "600");

                await JSRuntime.InvokeVoidAsync("createLineChart", CandlestickDatas(400), "status-card", Theme, "320", "200");

                await JSRuntime.InvokeVoidAsync("createLineChart", CandlestickDatas(500), "analytics-card", Theme, "200", "200");
            }
        }

        private ChartData[] ChartDatas(int number)
        {
            ChartData[] data = new ChartData[number];
            Random rand = new Random();

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new ChartData
                {
                    Time = DateTime.Now.AddDays(i),
                    Value = rand.Next(1, 20)
                };
            }

            return data;
        }

        private CandlestickData[] CandlestickDatas(int number)
        {
            CandlestickData[] data = new CandlestickData[number];
            Random rand = new Random();

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new CandlestickData
                {
                    Time = DateTime.Now.AddDays(i),
                    Open = rand.Next(1, 20),
                    High = rand.Next(20, 40),
                    Low = rand.Next(1, 20),
                    Close = rand.Next(1, 20)
                };
            }

            return data;
        }
    }
    public class ChartData
    {
        public DateTime Time { get; set; }
        public double Value { get; set; }
    }

    public class CandlestickData
    {
        public DateTime Time { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
    }
}
