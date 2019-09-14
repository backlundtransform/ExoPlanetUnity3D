using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Models
{
    public class Star
    {
        public string Name { get; set; }
        public Vector3 Coordinates { get; set; }
        public LongLat Coordinate { get; set; }

        public bool HasHab { get; set; }
        public StarType? Color { get; set; }
        public LumType? Luminosity { get; set; }
        public List<Planet> Planets { get; set; }
        public int? NoHabPlanets { get; set; }
        public int? NoPlanets { get; set; }
        public decimal? Temp { get; set; }
    
        public int Constellation { get; set; }
        public string Type { get; set; }

        public decimal? Mass { get; set; }

        public int Magnitude { get; set; }

        public decimal Radius { get; set; }

        public decimal? RadiusSu { get; set; }

        public decimal? Age { get; set; }

        public decimal? HabZoneMin { get; set; }
        public decimal? HabZoneMax { get; set; }

        public string Message { get; set; }

    }
}
