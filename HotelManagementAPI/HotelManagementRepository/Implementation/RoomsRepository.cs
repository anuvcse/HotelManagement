using HotelManagementCommon.Entities;
using HotelManagementRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementRepository.Implementation
{
    public class RoomsRepository : IRoomsRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public RoomsRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }
        public async Task<int> AddRooms(RoomsEntity roomsEntity)
        {
            _applicationDbContext.Rooms.Add(roomsEntity);
            return await _applicationDbContext.SaveChangesAsync();
        }
    }
}
