using ConferenceRoomAppAPI.Services.Dtos.UserDtos;
using ConferenceRoomAppAPI.Services.Interfaces;
using ConferenceRoomAppAPI.Data.Context;
using ConferenceRoomAppAPI.Data.Models;
using ConferenceRoomAppAPI.Services.Exceptions;
using ConferenceRoomAppAPI.Services.Exceptions.UserExceptions;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomAppAPI.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly ConferenceRoomContext _context;
        public UserServices(ConferenceRoomContext context)
        {
            _context = context;
        }

        private GetUserDtos GetUserDtoInternal(Users user)
        {
            return new GetUserDtos
            {
                Id = user.Id,
                Name = user.UserName
            };
        }
        public async Task<List<GetUserDtos>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users.Select(user => GetUserDtoInternal(user)).ToList();
        }

        public async Task<List<GetUserDtos>> GetUsersByNameAsync(string name)
        {
            var user = await _context.Users.Where(u => u.UserName.Contains(name)).ToListAsync();

            if (!user.Any())
            {
                throw new UserNotFoundException();
            }

            return user.Select(GetUserDtoInternal).ToList();
        }


    }
}
