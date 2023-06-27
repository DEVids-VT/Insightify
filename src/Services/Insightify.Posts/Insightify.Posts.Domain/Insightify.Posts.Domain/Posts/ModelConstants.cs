using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insightify.Posts.Domain.Posts
{
    public class ModelConstants
    {
        public class Common
        {
            public const int MaxUrlLength = 2048;
            public const int Zero = 0;
        }

        public class Post
        {
            public const int MinTitleLength = 2;
            public const int MaxTitleLength = 75;
            public const int MinDescriptionLength = 10;
            public const int MaxDescriptionLength = 1000;
        }
    }
}
