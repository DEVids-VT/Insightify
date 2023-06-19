using Microsoft.Extensions.Primitives;

namespace Insightify.NotificationsAPI.Constants
{
    public static class Validation
    {
        public static class Notification
        {
            public const int TitleMinLength = 4;
                             
            public const int TitleMaxLength = 20;

            public const int DescriptionMinLength = 20;

            public const int DescriptionMaxLength = 400;
        }
    }
}
