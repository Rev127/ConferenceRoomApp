using ConferenceRoomAppAPI.Services.Dtos.HallServicesDtos;
using ConferenceRoomAppAPI.Services.Interfaces;
using ConferenceRoomAppAPI.Data.Models;
using ConferenceRoomAppAPI.Data.Context;
using ConferenceRoomAppAPI.Services.Exceptions.HallServicesExceptions;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomAppAPI.Services.Services
{
    public class HallServicesServices : IHallServicesServices
    {
        private readonly ConferenceRoomContext _context;
        public HallServicesServices(ConferenceRoomContext context)
        {
            _context = context;
        }

        private async Task<HallServices> GetHallServicesByIdInternalAsync(int hallServicesId)
        {
            var hallServices = await _context.HallServices.FindAsync(hallServicesId);
            if (hallServices is null)
            {
                throw new HallServicesNotExistsException();
            }
            return hallServices;
        }

        public async Task CreateHallServicesAsync(CreateHallServicesDto hallServicesDto)
        {
            var hallServices = new HallServices
            {
                Name = hallServicesDto.Name,
                Price = hallServicesDto.Price
            };

            await _context.HallServices.AddAsync(hallServices);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHallServicesAsync(int hallServicesId)
        {
            var hallServices = await this.GetHallServicesByIdInternalAsync(hallServicesId);

            _context.HallServices.Remove(hallServices);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GetHallServicesDto>> GetAllHallServicesAsync()
        {
            return await _context.HallServices.Select(hs => new GetHallServicesDto
            {
                Id = hs.Id,
                Name = hs.Name,
                Price = hs.Price
            }).ToListAsync();
        }

        public async Task<GetHallServicesDto> GetHallServicesByIdAsync(int hallServicesId)
        {
            var hallServices = await this.GetHallServicesByIdInternalAsync(hallServicesId);

            return new GetHallServicesDto
            {
                Id = hallServices.Id,
                Name = hallServices.Name,
                Price = hallServices.Price
            };
        }

        public async Task UpdateHallServicesAsync(UpdateHallServicesDto hallServicesDto)
        {
            var hallServices = await this.GetHallServicesByIdInternalAsync(hallServicesDto.Id);

            if (hallServicesDto.Name is not null)
            {
                hallServices.Name = hallServicesDto.Name;
            }

            if (hallServicesDto.Price is not null)
            {
                hallServices.Price = hallServicesDto.Price.Value;
            }

            _context.HallServices.Update(hallServices);
            await _context.SaveChangesAsync();
        }

        
    }
}
