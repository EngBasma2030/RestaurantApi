using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Payment
{
    public class CreatePaymentDto
    {
        public int OrderId { get; set; }
        public string Method { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = "Paid";
    }
}
