namespace Insightify.Web.Gateway.Validators.Common
{
    public class PostsValidationConstants
    {
        public class Common
        {
            public const int MaxUrlLength = 2048;
        }

        public class Post
        {
            public const int MinTitleLength = 2;
            public const int MaxTitleLength = 75;
            public const int MinDescriptionLength = 10;
            public const int MaxDescriptionLength = 1000;
        }
        public class Comment
        {

            public const int MinContentLength = 10;
            public const int MaxContentLength = 200;
        }
    }
}
