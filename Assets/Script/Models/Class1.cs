using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Models
{

    public class Img
    {
        public string uri { get; set; }
    }

    public class Coordinate
    {
        public double longitude { get; set; }
        public double latitude { get; set; }
    }

    public class Star1
    {
        public object noHabPlanets { get; set; }
        public int noPlanets { get; set; }
        public double temp { get; set; }
        public string name { get; set; }
        public int constellation { get; set; }
        public string type { get; set; }
        public int color { get; set; }
        public object mass { get; set; }
        public int magnitude { get; set; }
        public double radius { get; set; }
        public double radiusSu { get; set; }
        public int luminosity { get; set; }
        public object age { get; set; }
        public double habZoneMin { get; set; }
        public double habZoneMax { get; set; }
        public object message { get; set; }
    }

    public class RootObject
    {
        public int id { get; set; }
        public string name { get; set; }
        public Img img { get; set; }
        public double period { get; set; }
        public object hzd { get; set; }
        public object hzc { get; set; }
        public object hza { get; set; }
        public object hzi { get; set; }
        public string type { get; set; }
        public int comp { get; set; }
        public int atmosphere { get; set; }
        public int habType { get; set; }
        public double meanDistance { get; set; }
        public double distance { get; set; }
        public double esi { get; set; }
        public object sph { get; set; }
        public double discYear { get; set; }
        public int discMethod { get; set; }
        public double radius { get; set; }
        public object eccentricity { get; set; }
        public double radiusEu { get; set; }
        public Coordinate coordinate { get; set; }
        public object starDistance { get; set; }
        public Star1 star { get; set; }
        public double temp { get; set; }
        public double tempMin { get; set; }
        public object mass { get; set; }
        public object density { get; set; }
        public object gravity { get; set; }
        public object surfacePressure { get; set; }
        public object escapeVelocity { get; set; }
        public int massType { get; set; }
        public double tempMax { get; set; }
        public int tempZone { get; set; }
        public bool hab { get; set; }
        public object moons { get; set; }
        public object message { get; set; }
    }
}
