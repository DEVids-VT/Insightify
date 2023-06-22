using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insightify.Framework.Pagination.Abstractions
{
    public interface IPage<T> : IPage, IEnumerable<T>
    {

    }

    public interface IPage
    {
        public int CurrentPage { get; }

        public int PageSize { get; }

        public int TotalCount { get; }
    }
}
