using System;

namespace HotelManagement_User.Models
{
    public class BookingDetails
    {
        public int RoomNo { get; set; }
        public string RoomType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
