using BoatSystem.Core.DTOs;
using BoatSystem.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Application.Queries.Cstomer
{
    using BoatSystem.Core.DTOs;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class CustomerWalletQuery : IRequest<CustomerWalletDto>
    {
        public int CustomerId { get; set; }
    }

    public class UpdateCustomerWalletCommand : IRequest<bool>
    {
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
    }

    public class CustomerWalletQueryHandler : IRequestHandler<CustomerWalletQuery, CustomerWalletDto>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerWalletQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerWalletDto> Handle(CustomerWalletQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            if (customer == null) return null;

            return new CustomerWalletDto
            {
                CustomerId = customer.Id,
                Balance = customer.WalletBalance
            };
        }
    }

    public class UpdateCustomerWalletCommandHandler : IRequestHandler<UpdateCustomerWalletCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<UpdateCustomerWalletCommandHandler> _logger;

        public UpdateCustomerWalletCommandHandler(ICustomerRepository customerRepository, ILogger<UpdateCustomerWalletCommandHandler> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateCustomerWalletCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            if (customer == null) return false;

            _logger.LogInformation($"Current Balance: {customer.WalletBalance}, Amount to Add: {request.Amount}");

            if (request.Amount <= 0)
            {
                _logger.LogWarning("Invalid Amount: Amount must be positive.");
                return false;
            }

            // حساب الرصيد الجديد
            decimal newBalance = customer.WalletBalance + request.Amount;

            _logger.LogInformation($"New Balance Calculation: {customer.WalletBalance} + {request.Amount} = {newBalance}");

            customer.WalletBalance = newBalance;

            await _customerRepository.UpdateAsync(customer);

            _logger.LogInformation($"Updated Balance: {customer.WalletBalance}");

            return true;
        }
    }
}