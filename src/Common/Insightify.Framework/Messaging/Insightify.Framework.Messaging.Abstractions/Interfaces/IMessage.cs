using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insightify.Framework.Messaging.Abstractions.Interfaces
{
    public interface IMessage
    {
        Guid Id { get; init; }
        DateTime CreationDate { get; init; }
    }
}
