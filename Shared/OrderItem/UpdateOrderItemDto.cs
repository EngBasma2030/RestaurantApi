using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderItem
{
    public class UpdateOrderItemDto
    {
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
    }
}
