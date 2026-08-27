using ConferenceRoomAppAPI.Services.Dtos.HallDtos;
using ConferenceRoomAppAPI.Services.Interfaces;
using ConferenceRoomAppAPI.Data.Context;
using ConferenceRoomAppAPI.Data.Models;
using ConferenceRoomAppAPI.Services.Exceptions.HallExceptions;
using ConferenceRoomAppAPI.Services.Exceptions.HallServicesExceptions;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ConferenceRoomAppAPI.Services.Dtos.HallServicesDtos;

namespace ConferenceRoomAppAPI.Services.Services
{
    public class HallsService : IHallsServices
    {
        private readonly ConferenceRoomContext _context;
        private readonly IHallServices _hallServicesServices;

        public HallsService(ConferenceRoomContext context, IHallServices hallServicesServices)
        {
            _context = context;
            _hallServicesServices = hallServicesServices;
        }

        private async Task<Halls> GetHallByIdInternalAsync(int hallId)
        {
            var hall = await _context.Halls
                .Include(h => h.Services)
                .FirstOrDefaultAsync(h => h.Id == hallId);

            if (hall is null)
            {
                throw new HallNotExistsException(hallId);
            }

            return hall;
        }

        private async Task<List<GetHallServicesDto>> GetExistingHallServices(CreateHallDto hallDto)
        {
            var hallServices = hallDto.HallServices.ToList();
            var allServices = await _hallServicesServices.GetAllHallServicesAsync();

            return allServices
                .Where(s => hallServices.Any(hs => hs.Name == s.Name && hs.Price == s.Price))
                .ToList();
        }

        private async Task<List<GetHallServicesDto>> GetExistingHallServices(UpdateHallDto hallDto)
        {
            var hallServices = hallDto.HallServices.ToList();
            var allServices = await _hallServicesServices.GetAllHallServicesAsync();

            return allServices
                .Where(s => hallServices.Any(hs => hs.Name == s.Name && hs.Price == s.Price))
                .ToList();
        }

        private GetHallDto GetHallDtoInternal(Halls hall)
        {
            var hallServicesIds = hall.Services.Select(s => s.Id).ToList() ?? new List<int>();
            return new GetHallDto
            {
                Id = hall.Id,
                Name = hall.Name,
                Capacity = hall.Capacity,
                PricePerHour = hall.PricePerHour,
                HallServicesIds = hallServicesIds
            };
        }

        public async Task<int> CreateHallAsync(CreateHallDto hallDto)
        {
            var existingHall = _context.Halls.FirstOrDefault(h => h.Name == hallDto.Name);

            // Check if a hall with the same name already exists
            if (existingHall is not null)
            {
                throw new HallAlreadyExistsException(hallDto.Name);
            }

            // Check if the provided hall services exist in the database
            var existingServices = await this.GetExistingHallServices(hallDto);

            if (!existingServices.Any())
            {
                throw new HallServicesNotExistsException();
            }

            var hall = new Halls
            {
                Name = hallDto.Name,
                Capacity = hallDto.Capacity,
                PricePerHour = hallDto.PricePerHour,
                Services = await _context.HallServices
                    .Where(s => existingServices.Select(es => es.Id).Contains(s.Id))
                    .ToListAsync()
            };


            await _context.Halls.AddAsync(hall);
            await _context.SaveChangesAsync();

            return hall.Id;
        }

        public async Task DeleteHallAsync(int hallId)
        {
            var hall = await this.GetHallByIdInternalAsync(hallId);

             _context.Halls.Remove(hall);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GetHallDto>> GetAllHallsAsync()
        {
            var halls = await _context.Halls.Include(h => h.Services).ToListAsync();
            return halls.Select(h => this.GetHallDtoInternal(h)).ToList();
        }

        public async Task<GetHallDto> GetHallByIdAsync(int hallId)
        {
            var hall = await this.GetHallByIdInternalAsync(hallId);

            return this.GetHallDtoInternal(hall);
        }

        public async Task UpdateHallAsync(UpdateHallDto hallDto)
        {
            var hall = await this.GetHallByIdInternalAsync(hallDto.Id);

            // Update the capacity if provided
            if (hallDto.Capacity.HasValue && hallDto.Capacity.Value > 0)
            {
                hall.Capacity = hallDto.Capacity.Value;
            }

            // Update the name if provided
            if (!string.IsNullOrEmpty(hallDto.Name))
            {
                hall.Name = hallDto.Name;
            }

            // Update the price per hour if provided
            if (hallDto.PricePerHour.HasValue && hallDto.PricePerHour.Value > 0)
            {
                hall.PricePerHour = hallDto.PricePerHour.Value;
            }

            // Update the hall services if provided
            var existingServices = await this.GetExistingHallServices(hallDto);

            if (existingServices.Any())
            {
                hall.Services = await _context.HallServices
                    .Where(s => existingServices.Select(es => es.Id).Contains(s.Id))
                    .ToListAsync();
            }

            _context.Halls.Update(hall);
            await _context.SaveChangesAsync();
        
        }


    }
}
