using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementCommon.Entities
{
    [Table("CustomerDetails")]
    [Index(nameof(Email), IsUnique = true)]
    public class CustomerDetailsEntity
    {

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }

        [Required]
        public string Email { get; set; }

    }
}
