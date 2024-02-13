using Insightify.MVC.Models;

namespace Insightify.MVC.Common.LanguagePresets
{
    public static class Homepage
    {
        public static HomeLanguageModel Bulgarian { get => Bg(); }
        public static HomeLanguageModel English { get => En(); }

        private static HomeLanguageModel Bg()
        {
            return new HomeLanguageModel
            {
                VideoText = new VideoText
                {
                    Title = "Световната",
                    Highlight = "Водеща",
                    Subtitle = "Платформа за криптовалути"
                },
                Subtitle = "Купете Bitcoin, Ethereum и всички ваши любими криптовалути",
                Ul = new List<ListElement>
                    {
                        new() {
                            Text = "Доверен от повече от",
                            Highlight = "80 милиона",
                            Description = "потребители по целия свят"
                        },
                        new() {
                            Text = "Лидер в регулаторните",
                            Highlight = "за съответствие и сигурност",
                            Description = "сертификати"
                        },
                        new() {
                            Text = "В индустрията",
                            Highlight = "най-цялостното застрахователно покритие",
                            Description = "и проверено доказателство"
                        }
                    },
                CenterText = "Първи стъпки",
                GetStarted = "Вход",
                Policy = "Политика за поверителност",
                Terms = "Условия за ползване",
                Language = "ENGLISH",
                LanguageImg = "/images/flag_of_the_united_kingdom.svg.png",
                LanguageProp = "en"
            };
        }

        private static HomeLanguageModel En()
        {
            return new HomeLanguageModel
            {
                VideoText = new VideoText
                {
                    Title = "The World’s",
                    Highlight = "Leading",
                    Subtitle = "Cryptocurrency Platform"
                },
                Subtitle = "Buy Bitcoin, Ethereum, and all your favourite crypto",
                Ul = new List<ListElement>
                {
                    new() {
                        Text = "Trusted by more than",
                        Highlight = "80M",
                        Description = "users world-wide"
                    },
                    new() {
                        Text = "Leader in regulatory",
                        Highlight = "compliance and security",
                        Description = "certifications"
                    },
                    new() {
                        Text = "The industry’s",
                        Highlight = "most comprehensive insurance coverage",
                        Description = "and verified proof"
                    }
                },
                CenterText = "Get started",
                GetStarted = "Join Us",
                Policy = "Privacy policy",
                Terms = "Terms of use",
                Language = "БЪЛГАРСКИ",
                LanguageImg = "/images/flag_of_bulgaria.svg",
                LanguageProp = "bg"
            };
        }
    }
}
