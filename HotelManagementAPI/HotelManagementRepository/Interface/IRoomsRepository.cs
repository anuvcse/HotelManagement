
using HotelManagementCommon.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementRepository.Interface
{
    public interface IRoomsRepository
    {
        Task<int> AddRooms(RoomsEntity roomsEntity);


    }
}
