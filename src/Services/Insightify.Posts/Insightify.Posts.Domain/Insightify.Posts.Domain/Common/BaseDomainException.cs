using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insightify.Posts.Domain.Common
{
    public abstract class BaseDomainException : Exception
    {
        private string? _error;

        public string Error
        {
            get => _error ?? base.Message;
            set => _error = value;
        }
    }
}
