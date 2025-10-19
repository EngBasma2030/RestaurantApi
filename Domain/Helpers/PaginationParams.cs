using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public class PaginationParams
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? SearchTerm { get; set; } // ممكن نستخدمها للبحث بالاسم أو الحالة
        public string? Status { get; set; }     // للفلترة حسب حالة الطلب
    }
}
