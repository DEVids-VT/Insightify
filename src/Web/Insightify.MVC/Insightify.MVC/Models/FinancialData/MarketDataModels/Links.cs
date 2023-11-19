using Newtonsoft.Json;

namespace Insightify.MVC.Models.FinancialData.MarketDataModels
{
    public class Links
    {
        public List<string> Homepage { get; set; }

        public List<string> BlockchainSite { get; set; }

        public List<string> OfficialForumUrl { get; set; }

        public List<string> ChatUrl { get; set; }

        public List<string> AnnouncementUrl { get; set; }

        public string TwitterScreenName { get; set; }

        public string FacebookUsername { get; set; }

        public object BitcointalkThreadIdentifier { get; set; }

        public string TelegramChannelIdentifier { get; set; }

        public string SubredditUrl { get; set; }

        public ReposUrl ReposUrl { get; set; }
    }
}