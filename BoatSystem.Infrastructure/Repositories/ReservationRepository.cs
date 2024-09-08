using BoatSystem.Core.Entities;
using BoatSystem.Infrastructure.Data;
using BoatSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace BoatSystem.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Trip)
                .Include(r => r.Boat)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
            {
                throw new KeyNotFoundException($"Reservation with ID {id} not found.");
            }

            return reservation;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Trip)
                .Include(r => r.Boat)
                .ToListAsync();
        }

        public async Task AddAsync(Reservation reservation)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var boat = await _context.Boats.FindAsync(reservation.BoatId);
                if (boat == null)
                {
                    throw new KeyNotFoundException($"Boat with ID {reservation.BoatId} not found.");
                }

                var totalPeopleReserved = await _context.Reservations
                    .Where(r => r.BoatId == reservation.BoatId && r.TripId == reservation.TripId)
                    .SumAsync(r => r.NumPeople);

                if (totalPeopleReserved + reservation.NumPeople > boat.Capacity)
                {
                    throw new InvalidOperationException($"The boat's capacity is exceeded. Available capacity is {boat.Capacity - totalPeopleReserved}.");
                }

                reservation.CanceledAt = null; // Ensure canceledAt is null initially
                await _context.Reservations.AddAsync(reservation);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            var existingReservation = await GetByIdAsync(reservation.Id);

            if (existingReservation == null)
            {
                throw new KeyNotFoundException($"Reservation with ID {reservation.Id} not found.");
            }

            if (reservation.NumPeople > existingReservation.NumPeople)
            {
                var boat = await _context.Boats.FindAsync(reservation.BoatId);
                if (boat == null)
                {
                    throw new KeyNotFoundException($"Boat with ID {reservation.BoatId} not found.");
                }

                var totalPeopleReserved = await _context.Reservations
                    .Where(r => r.BoatId == reservation.BoatId && r.TripId == reservation.TripId)
                    .SumAsync(r => r.NumPeople) - existingReservation.NumPeople;

                if (totalPeopleReserved + reservation.NumPeople > boat.Capacity)
                {
                    throw new InvalidOperationException($"The boat's capacity is exceeded. Available capacity is {boat.Capacity - totalPeopleReserved}.");
                }
            }

            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reservation = await GetByIdAsync(id);
            if (reservation == null)
            {
                throw new KeyNotFoundException($"Reservation with ID {id} not found.");
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task CancelReservationAsync(int id)
        {
            var reservation = await GetByIdAsync(id);
            if (reservation == null)
            {
                throw new KeyNotFoundException($"Reservation with ID {id} not found.");
            }

            if (reservation.CanceledAt.HasValue)
            {
                throw new InvalidOperationException("Reservation is already canceled.");
            }

            reservation.CanceledAt = DateTime.UtcNow; // Set canceledAt to current time
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByTripIdAsync(int tripId)
        {
            return await _context.Reservations
                .Where(r => r.TripId == tripId)
                .Include(r => r.Customer)
                .Include(r => r.Trip)
                .Include(r => r.Boat)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAllIncludingDetailsAsync()
        {
            return await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Trip)
                .Include(r => r.Boat)
                .ToListAsync();
        }

        public async Task<Reservation> GetByIdIncludingDetailsAsync(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Trip)
                .Include(r => r.Boat)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
            {
                throw new KeyNotFoundException($"Reservation with ID {id} not found.");
            }

            return reservation;
        }
    }
}
