using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementCommon.Entities
{
    [Table("Rooms")]
    public class RoomsEntity
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomNo { get; set; }
        public string RoomType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;


    }
}
