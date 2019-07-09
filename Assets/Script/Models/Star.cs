using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Models
{
    public class Star
    {
        public string Name { get; set; }
        public Vector3 Coordinates { get; set; }
        public LongLat Coordinate { get; set; }
        public double Radius { get; set; }
        public double HabZoneMin { get; set; }
        public double HabZoneMax { get; set; }
        public StarType Color { get; set; }
        public LumType Luminosity { get; set; }
        public List<Planet> Planets { get; set; }

    }
}
