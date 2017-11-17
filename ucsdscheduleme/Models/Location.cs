using System.ComponentModel.DataAnnotations;

namespace ucsdscheduleme.Models
{
    public class Location
    {
        public int Id { get; set; }
        [StringLength(10)]
        public string Building { get; set; }
        [StringLength(10)]
        public string RoomNumber { get; set; }
    }
}