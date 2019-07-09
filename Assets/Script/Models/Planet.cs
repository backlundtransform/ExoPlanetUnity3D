
namespace Assets.Script.Models
{
    public class Planet
    {
        public string Name { get; set; }
        public Star Star { get; set; } 
        public LongLat Coordinate { get; set; }
    }
}
