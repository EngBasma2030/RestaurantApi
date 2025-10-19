using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using ServiceAbstraction;
using Shared.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PaymentDto>> GetAllAsync()
        {
            var payments =await _paymentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task<PaymentDto> GetByIdAsync(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<IEnumerable<PaymentDto>> GetByOrderIdAsync(int orderId)
        {
            var payments = await _paymentRepository.GetByOrderIdAsync(orderId);
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task<IEnumerable<PaymentDto>> GetByMethodAsync(string method)
        {
            var payments = await _paymentRepository.GetByMethodAsync(method);
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task<PaymentDto> CreateAsync(CreatePaymentDto dto)
        {
            var payment = _mapper.Map<Payment>(dto);
            await _paymentRepository.AddAsync(payment);
            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null) return false;

            await _paymentRepository.DeleteAsync(payment);
            return true;
        }
    }
}
