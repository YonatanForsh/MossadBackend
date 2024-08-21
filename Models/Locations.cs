using System.ComponentModel.DataAnnotations;

namespace MossadBackend.Models
{
    public class Locations
    {
        [Key]
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Locations() { }
        public Locations(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
