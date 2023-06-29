using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Domain.Common;

namespace Insightify.Posts.Domain.Posts.Exceptions
{
    internal class InvalidPostException : BaseDomainException
    {
        public InvalidPostException()
        {
        }

        public InvalidPostException(string error) => this.Error = error;
    }
}
