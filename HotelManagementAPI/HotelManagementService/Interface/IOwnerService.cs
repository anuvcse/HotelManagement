
using HotelManagementCommon.Entities;
using HotelManagementCommon.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementService.Interface
{
    public interface IOwnerService
    {
        User Login(User user);
        Task<int> AddRooms(RoomsEntity rooms);
        Task<List<RoomStatus>> CheckAvailableRooms(DateTime date);
    }
}
