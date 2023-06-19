using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insightify.Framework.MongoDb.Abstractions.Configuration
{
    public record MongoSettings
    {
        public string Database { get; init; } = null!;
        public string Url { get; init; } = null!;

        public bool SoftDeleteEnabled { get; init; } = true;
        public int SoftDeletePollInMinutes { get; init; } = 60;
        public int SoftDeleteRetentionInDays { get; init; } = 28;
    }
}
