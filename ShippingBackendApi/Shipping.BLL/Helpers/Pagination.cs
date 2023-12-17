using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Helpers
{
    public class Pagination<T> where T : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public IReadOnlyList<T> Data { get; set; }

        public Pagination(int pageIndex,int pageSize,int pageCount,IReadOnlyList<T> data) 
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            PageCount = pageCount;
            Data = data;
        }
    }
}
