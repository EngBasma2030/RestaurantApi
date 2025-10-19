using Shared.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllAsync();
        Task<PaymentDto> GetByIdAsync(int id);
        Task<IEnumerable<PaymentDto>> GetByOrderIdAsync(int orderId);
        Task<IEnumerable<PaymentDto>> GetByMethodAsync(string method);
        Task<PaymentDto> CreateAsync(CreatePaymentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
