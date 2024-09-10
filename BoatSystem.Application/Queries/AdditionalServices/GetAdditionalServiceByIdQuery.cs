using BoatSystem.Application.Services;
using BoatSystem.Core.DTOs;
using BoatSystem.Core.Interfaces; // Ensure you have the correct namespace for IAdditionalService
using BoatSystem.Core.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BoatSystem.Application.Queries.AdditionalServices
{
    // طلب لاسترجاع خدمة إضافية بناءً على معرف الخدمة
    public class GetAdditionalServiceByIdQuery : IRequest<AdditionalServiceDto>
    {
        public int Id { get; set; }
    }

    // طلب لاسترجاع جميع الخدمات الإضافية بناءً على OwnerId
    public class GetAllAdditionalServicesQuery : IRequest<IEnumerable<AdditionalServiceDto>>
    {
        // No need for OwnerId here if you're fetching all services
    }

    // طلب لاسترجاع جميع الخدمات الإضافية بناءً على OwnerId
    public class GetAdditionalServicesByOwnerIdQuery : IRequest<IEnumerable<AdditionalServiceDto>>
    {
        public int OwnerId { get; set; }
    }

    // معالج طلب لاسترجاع خدمة إضافية بناءً على معرف الخدمة
    public class GetAdditionalServiceByIdQueryHandler : IRequestHandler<GetAdditionalServiceByIdQuery, AdditionalServiceDto>
    {
        private readonly IAdditionalService _additionalService;

        public GetAdditionalServiceByIdQueryHandler(IAdditionalService additionalService)
        {
            _additionalService = additionalService;
        }

        public async Task<AdditionalServiceDto> Handle(GetAdditionalServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var addition = await _additionalService.GetByIdAsync(request.Id);

            if (addition == null)
            {
                return null; // Handle case where service is not found
            }

            return new AdditionalServiceDto
            {
                Id = addition.Id,
                OwnerId = addition.OwnerId,
                Name = addition.Name,
                Description = addition.Description,
                Price = addition.Price,
                CreatedAt = addition.CreatedAt,
                UpdatedAt = addition.UpdatedAt
            };
        }
    }

    // معالج طلب لاسترجاع جميع الخدمات الإضافية بناءً على OwnerId
    public class GetAllAdditionalServicesHandler : IRequestHandler<GetAllAdditionalServicesQuery, IEnumerable<AdditionalServiceDto>>
    {
        private readonly IAdditionalServiceRepository _additionalServiceRepository;

        public GetAllAdditionalServicesHandler(IAdditionalServiceRepository additionalServiceRepository)
        {
            _additionalServiceRepository = additionalServiceRepository;
        }

        public async Task<IEnumerable<AdditionalServiceDto>> Handle(GetAllAdditionalServicesQuery request, CancellationToken cancellationToken)
        {
            var additions = await _additionalServiceRepository.GetAllAsync(); // استخدام GetAllAsync بدلاً من GetByOwnerIdAsync

            return additions.Select(a => new AdditionalServiceDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                OwnerId=a.OwnerId ,
                Price = a.Price,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt
            });
        }
    }

    // معالج طلب لاسترجاع جميع الخدمات الإضافية بناءً على OwnerId
    public class GetAdditionalServicesByOwnerIdQueryHandler : IRequestHandler<GetAdditionalServicesByOwnerIdQuery, IEnumerable<AdditionalServiceDto>>
    {
        private readonly IAdditionalService _additionalService;

        public GetAdditionalServicesByOwnerIdQueryHandler(IAdditionalService additionalService)
        {
            _additionalService = additionalService;
        }

        public async Task<IEnumerable<AdditionalServiceDto>> Handle(GetAdditionalServicesByOwnerIdQuery request, CancellationToken cancellationToken)
        {
            var additions = await _additionalService.GetByOwnerIdAsync(request.OwnerId);

            return additions.Select(addition => new AdditionalServiceDto
            {
                Id = addition.Id,
                OwnerId = addition.OwnerId,
                Name = addition.Name,
                Description = addition.Description,
                Price = addition.Price,
                CreatedAt = addition.CreatedAt,
                UpdatedAt = addition.UpdatedAt
            });
        }
    }
}
