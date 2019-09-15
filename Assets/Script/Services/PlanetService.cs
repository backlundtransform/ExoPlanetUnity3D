using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Services
{
   public class PlanetService
    {
       public static string GetPlanetType(string type)
        {
            if (type == "stone")
            {
                return "Planet_B";
            }
            if (type == "coldstone")
            {
                return "Planet_E";
            }
            if (type == "hotstone")
            {
                return "Planet_L";
            }
            if (type == "coldsuperearth")
            {
                return "Planet_J";
            }
            if (type == "superearth")
            {
                return "Planet_D";
            }
            if (type == "hotsuperearth")
            {
                return "Planet_G";
            }
            if (type == "neptunian")
            {
                return "Gas_Planet_A";
            }
            if (type == "jovian")
            {
                return "Gas_Planet_G";
            }
            if (type == "hotjupiter")
            {
                return "Gas_Planet_G";
            }

            return "Planet_F";
        }

    }
}
