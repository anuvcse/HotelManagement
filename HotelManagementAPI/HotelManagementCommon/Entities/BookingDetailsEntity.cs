using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelManagementCommon.Entities
{
    [Table("BookingDetails")]
    public class BookingDetailsEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingID { get; set; }
        [ForeignKey("Rooms")]
        public int RoomNo { get; set; }

        [ForeignKey("CustomerDetails")]
        public int CustomerID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual RoomsEntity Rooms { get; set; }
        public virtual CustomerDetailsEntity CustomerDetails { get; set; }

    }
}
