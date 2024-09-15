﻿using BoatSystem.Core.DTOs;
using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoatSystem.Application.Services
{
    public class BoatService : IBoatService
    {
        private readonly IBoatRepository _boatRepository;
        private readonly IOwnerRepository _ownerRepository;

        public BoatService(IBoatRepository boatRepository, IOwnerRepository ownerRepository)
        {
            _boatRepository = boatRepository;
            _ownerRepository = ownerRepository;
        }

        public async Task<BoatDetailsDto> AddBoatAsync(BoatDto boatDto)
        {
            var boat = new Boat
            {
                Name = boatDto.Name,
                Description = boatDto.Description,
                Capacity = boatDto.Capacity,
                ReservationPrice = boatDto.ReservationPrice,
                OwnerId = boatDto.OwnerId,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _boatRepository.AddAsync(boat);

            return new BoatDetailsDto
            {
                Id = boat.Id,
                Name = boat.Name,
                Description = boat.Description,
                Capacity = boat.Capacity,
                ReservationPrice = boat.ReservationPrice,
                OwnerId = boat.OwnerId,
                Status = boat.Status,
                CreatedAt = boat.CreatedAt,
                UpdatedAt = boat.UpdatedAt
            };
        }

        public async Task<IEnumerable<BoatSummaryDto>> GetBoatsByNameAsync(string name)
        {
            var boats = await _boatRepository.GetBoatsByNameAsync(name);
            return boats.Select(boat => new BoatSummaryDto
            {
                Name = boat.Name,
                Description = boat.Description,
                Capacity = boat.Capacity,
                ReservationPrice = boat.ReservationPrice,
                OwnerId = boat.OwnerId,
                Status = boat.Status
            }).ToList(); 
        }

        public async Task<IEnumerable<BoatApprovalDto>> GetUnapprovedBoatsAsync()
        {
            var boats = await _boatRepository.GetUnapprovedBoatsAsync();
            return boats.Select(boat => new BoatApprovalDto
            {
                Id = boat.Id,
                Name = boat.Name,
                Description = boat.Description,
                Capacity = boat.Capacity,
                ReservationPrice = boat.ReservationPrice,
                OwnerId = boat.OwnerId,
                Status = boat.Status
            }).ToList(); 
        }

        public async Task<BoatDetailsDto> GetBoatByIdAsync(int id)
        {
            var boat = await _boatRepository.GetByIdAsync(id);
            if (boat == null)
                return null;

            return new BoatDetailsDto
            {
                Name = boat.Name,
                Description = boat.Description,
                Capacity = boat.Capacity,
                ReservationPrice = boat.ReservationPrice,
                OwnerId = boat.OwnerId,
                Status = boat.Status,
                CreatedAt = boat.CreatedAt,
                UpdatedAt = boat.UpdatedAt
            };
        }

        public async Task<IEnumerable<Boat>> GetBoatsByOwnerIdAsync(int ownerId)
        {
            return await _boatRepository.GetBoatsByOwnerIdAsync(ownerId);
        }
        public async Task<int?> GetOwnerIdByUserIdAsync(string userId)
        {
            var owner = await _ownerRepository.GetOwnerByUserIdAsync(userId);
            return owner?.Id;
        }



        public async Task<IEnumerable<BoatDto>> GetAvailableBoatsAsync()
        {
            return await _boatRepository.GetAvailableBoatsAsync();
        }
    }
}
