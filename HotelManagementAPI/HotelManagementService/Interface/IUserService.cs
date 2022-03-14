using HotelManagementCommon.Entities;
using HotelManagementCommon.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementService.Interface
{
    public interface IUserService
    {
        Task<bool> BookRoom(BookUserRoom bookUserRoom);
        Task<List<History>> GetHistory(string Email);
    }
}
