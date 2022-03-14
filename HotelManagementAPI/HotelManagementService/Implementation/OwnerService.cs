
using HotelManagementCommon.Entities;
using HotelManagementCommon.Models;
using HotelManagementRepository.Interface;
using HotelManagementService.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementService.Implementation
{
    public class OwnerService : IOwnerService
    {
        private ILogger<OwnerService> _logger;
        private IConfiguration _configuration;
        private IRoomsRepository _roomsRepository;
        private IBookingDetailsRepository _bookingDetailsRepository;
        private List<User> users = new List<User> { new User { UserName = "anu", Password = "test@123", UserId=1 } };
        public OwnerService(IRoomsRepository roomsRepository, IBookingDetailsRepository bookingDetailsRepository,ILogger<OwnerService> logger, IConfiguration configuration)
        {
            _roomsRepository = roomsRepository ?? throw new ArgumentNullException(nameof(roomsRepository));
            _bookingDetailsRepository = bookingDetailsRepository ?? throw new ArgumentNullException(nameof(bookingDetailsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public async Task<int> AddRooms(RoomsEntity rooms)
        {

            return await _roomsRepository.AddRooms(rooms);
        }

        public async Task<List<RoomStatus>> CheckAvailableRooms(DateTime date)
        {
           return await _bookingDetailsRepository.DisplayBookedRooms(date, date);
        }

        public User Login(User user)
        {
            User userdetails = users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            if (userdetails == null)
                return null;
            user.Token =  generateJwtToken(user);
            return user;
             
        }
        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Secret"].ToString());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UserId", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
