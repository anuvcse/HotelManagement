using HotelManagementCommon.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementRepository.Interface
{
    public interface IBookingDetailsRepository
    {
        Task<List<History>> GetHistoryBookingByUser(int CustomerID);
        Task<List<RoomStatus>> DisplayBookedRooms(DateTime startDate, DateTime endDate);
        Task<int> BookRoom(BookingDetailsEntity bookkingDetailsEntity);
    }
}
