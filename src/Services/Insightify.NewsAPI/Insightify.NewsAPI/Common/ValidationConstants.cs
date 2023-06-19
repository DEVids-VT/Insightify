namespace Insightify.NewsAPI.Common
{
    public class ValidationConstants
    {
        public class LiveNews
        {
            public class Messages
            {
                public const string AuthorMaxLength = "Author's name is too long";
                             
                public const string TitleMinLength = "Title is too short";
                public const string TitleMaxLength = "Title is too long";
                             
                public const string DescriptionMinLength = "Description is too short";
                public const string DescriptionMaxLength = "Description is too long";
            }

            public class Validation
            {
                public const int AuthorMaxLength = 50;

                public const int TitleMinLength = 5;
                public const int TitleMaxLength = 25;

                public const int DescriptionMinLength = 50;
                public const int DescriptionMaxLength = 300;
            }
        }
    }
}
