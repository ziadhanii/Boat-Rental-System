// File: BoatSystem.Application/AdminService.cs
using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using System.Threading.Tasks;

namespace BoatSystem.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBoatRepository _boatRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly ICustomerRepository _customerRepository;

        public AdminService(IUserRepository userRepository, IBoatRepository boatRepository, IReservationRepository reservationRepository, ICustomerRepository customerRepository)
        {
            _userRepository = userRepository;
            _boatRepository = boatRepository;
            _reservationRepository = reservationRepository;
            _customerRepository = customerRepository;
        }

        public async Task ApproveUserRegistrationAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                user.IsApproved = true;
                await _userRepository.UpdateAsync(user);
            }
        }

        public async Task RejectUserRegistrationAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var relatedCustomers = await _customerRepository.GetByUserIdAsync(userId);
                foreach (var customer in relatedCustomers)
                {
                    await _customerRepository.DeleteAsync(customer.Id);
                }

                await _userRepository.DeleteAsync(userId);
            }
        }

        public async Task ApproveBoatRegistrationAsync(int boatId)
        {
            var boat = await _boatRepository.GetByIdAsync(boatId);
            if (boat != null)
            {
                boat.IsApproved = true;
                await _boatRepository.UpdateAsync(boat);
            }
        }

        public async Task RejectBoatRegistrationAsync(int boatId)
        {
            var boat = await _boatRepository.GetByIdAsync(boatId);
            if (boat != null)
            {
                await _boatRepository.DeleteAsync(boatId);
            }
        }

        public async Task<IEnumerable<Reservation>> MonitorReservationsAsync()
        {
            var reservations = await _reservationRepository.GetAllAsync();
            foreach (var reservation in reservations)
            {
                Console.WriteLine($"Reservation ID: {reservation.Id}, Customer: {reservation.CustomerId}, Boat: {reservation.BoatId}, Status: {reservation.Status}");
            }
            return reservations;
        }




    }
}
