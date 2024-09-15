using BoatSystem.Application.Services;
using BoatSystem.Core.DTOs;
using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using BoatSystem.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Application.Commands.AdditionalServiceCommand
{
    public class CreateAdditionalServiceCommand : IRequest<AdditionalServiceDto>
    {
        public CreateAdditionalServiceDto CreateAdditionalServiceDto { get; set; }
    }
    public class UpdateAdditionalServiceCommand : IRequest<bool>
    {
        public UpdateAdditionalServiceDto UpdateAdditionalServiceDto { get; set; }
    }
    public class DeleteAdditionalServiceCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }



    public class CreateAdditionalServiceCommandHandler : IRequestHandler<CreateAdditionalServiceCommand, AdditionalServiceDto>
    {
        private readonly IAdditionalServiceRepository _repository;

        public CreateAdditionalServiceCommandHandler(IAdditionalServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<AdditionalServiceDto> Handle(CreateAdditionalServiceCommand request, CancellationToken cancellationToken)
        {
            // التحقق من القيم الواردة
            Console.WriteLine($"OwnerId: {request.CreateAdditionalServiceDto.OwnerId}");

            var addition = new Addition
            {
                OwnerId = request.CreateAdditionalServiceDto.OwnerId,
                Name = request.CreateAdditionalServiceDto.Name,
                Description = request.CreateAdditionalServiceDto.Description,
                Price = request.CreateAdditionalServiceDto.Price,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdAddition = await _repository.AddAsync(addition);

            return new AdditionalServiceDto
            {
                Id = createdAddition.Id,
                OwnerId = createdAddition.OwnerId,
                Name = createdAddition.Name,
                Description = createdAddition.Description,
                Price = createdAddition.Price,
                CreatedAt = createdAddition.CreatedAt,
                UpdatedAt = createdAddition.UpdatedAt
            };
        }
    }
    public class UpdateAdditionalServiceCommandHandler : IRequestHandler<UpdateAdditionalServiceCommand, bool>
    {
        private readonly IAdditionalServiceRepository _repository;

        public UpdateAdditionalServiceCommandHandler(IAdditionalServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateAdditionalServiceCommand request, CancellationToken cancellationToken)
        {
            var addition = new Addition
            {
                Id = request.UpdateAdditionalServiceDto.Id,
                OwnerId = request.UpdateAdditionalServiceDto.OwnerId, 
                Name = request.UpdateAdditionalServiceDto.Name,
                Description = request.UpdateAdditionalServiceDto.Description,
                Price = request.UpdateAdditionalServiceDto.Price,
                UpdatedAt = DateTime.UtcNow
            };

            return await _repository.UpdateAsync(addition);
        }
    }

    public class DeleteAdditionalServiceCommandHandler : IRequestHandler<DeleteAdditionalServiceCommand, bool>
    {
        private readonly IAdditionalService _additionalService;

        public DeleteAdditionalServiceCommandHandler(IAdditionalService additionalService)
        {
            _additionalService = additionalService;
        }

        public async Task<bool> Handle(DeleteAdditionalServiceCommand request, CancellationToken cancellationToken)
        {
            return await _additionalService.DeleteAsync(request.Id);
        }
    }
}
