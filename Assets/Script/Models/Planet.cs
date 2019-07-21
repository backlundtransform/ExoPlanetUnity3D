namespace Assets.Script.Models
{
    public class Planet
    {
        public string Name { get; set; }
        public Star Star { get; set; } 
        public LongLat Coordinate { get; set; }
        public PlanetType Img { get; set; }
        public double? Eccentricity { get; set; }
       public double? StarDistance { get; set; }
        public double? Radius { get; set; }
        public bool Hab { get; internal set; }
        public double RadiusEu { get; set; }
    }
}
