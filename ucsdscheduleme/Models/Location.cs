using System.ComponentModel.DataAnnotations;

namespace ucsdscheduleme.Models
{
    public class Location
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Building { get; set; }
        public int RoomNumber { get; set; }
    }
}