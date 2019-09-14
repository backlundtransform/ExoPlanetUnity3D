using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Models
{

        public class ExoSolarSystem
        {
            public string Name { get; set; }

            public LongLat Coordinate { get; set; }

            public int? Color { get; set; }

            public int? Luminosity { get; set; }

            public decimal Radius { get; set; }

            public decimal? HabZoneMin { get; set; }

            public decimal? HabZoneMax { get; set; }

            public List<Planet> Planets { get; set; }

            public string Message { get; set; }
        }
 
}
