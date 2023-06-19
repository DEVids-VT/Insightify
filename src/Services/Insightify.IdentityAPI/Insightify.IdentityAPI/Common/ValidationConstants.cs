namespace Insightify.IdentityAPI.Common
{
    public static class ValidationConstants
    {
        public static class Register
        {
            public static class Messages
            {
                public const string UsernameMaxLength = "Username must be less than {0} symbols.";

                public const string UsernameMinLength = "Username must be at least {0} symbols";

                public const string PasswordMaxLength = "Password must be less than {0} symbols.";

                public const string PasswordMinLength = "Password must be at least {0} symbols";
            }

            public static class Validation
            {
                public const int UsernameMaxLength = 20;

                public const int UsernameMinLength = 4;

                public const int PasswordMaxLength = 30;

                public const int PasswordMinLength = 8;
            }
        }
    }
}
