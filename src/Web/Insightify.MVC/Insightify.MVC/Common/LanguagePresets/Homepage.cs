using Insightify.MVC.Models;

namespace Insightify.MVC.Common.LanguagePresets
{
    public static class Homepage
    {
        public static HomeLanguageModel BulgarianHomepage { get => Bulgarian(); }
        public static HomeLanguageModel EnglishHomepage { get => English(); }

        private static HomeLanguageModel Bulgarian()
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
                        new ListElement
                        {
                            Text = "Доверен от повече от",
                            Highlight = "80 милиона",
                            Description = "потребители по целия свят"
                        },
                        new ListElement
                        {
                            Text = "Лидер в регулаторните",
                            Highlight = "за съответствие и сигурност",
                            Description = "сертификати"
                        },
                        new ListElement
                        {
                            Text = "В индустрията",
                            Highlight = "най-цялостното застрахователно покритие",
                            Description = "и проверено доказателство"
                        }
                    },
                CenterText = "Първи стъпки",
                LogIn = "Вход",
                SignUp = "Регистрация",
                Policy = "Политика за поверителност",
                Terms = "Условия за ползване",
                Language = "ENGLISH",
                LanguageImg = "/images/Flag_of_the_United_Kingdom.svg.png",
                LanguageProp = "en"
            };
        }

        private static HomeLanguageModel English()
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
                    new ListElement
                    {
                        Text = "Trusted by more than",
                        Highlight = "80M",
                        Description = "users world-wide"
                    },
                    new ListElement
                    {
                        Text = "Leader in regulatory",
                        Highlight = "compliance and security",
                        Description = "certifications"
                    },
                    new ListElement
                    {
                        Text = "The industry’s",
                        Highlight = "most comprehensive insurance coverage",
                        Description = "and verified proof"
                    }
                },
                CenterText = "Get started",
                LogIn = "Log in",
                SignUp = "Sign up",
                Policy = "Privacy policy",
                Terms = "Terms of use",
                Language = "БЪЛГАРСКИ",
                LanguageImg = "/images/flag_of_bulgaria.svg",
                LanguageProp = "bg"
            };
        }
    }
}
