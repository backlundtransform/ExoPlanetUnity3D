using System;
using System.Collections.Generic;
using System.Linq;


namespace Assets.Script.Services
{
    public class PlanetService
    {
        public static List<string> Discmethods = new List<string> { "radial velocity method", "transit method", "microlensing", "astrometry", "pulsartiming", "imaging", "primarytransit  method", null };
       public static string GetPlanetType(string type)
        {
            var list = new List<string>();

            if (type == "stone")
            {
                 list = new List<string>() {  "Planet_B" };

              
            }
            if (type == "coldstone")
            {
               list = new List<string>() { "Planet_A", "Planet_E" };
              
            }
            if (type == "hotstone")
            {
              list = new List<string>() { "Planet_K", "Planet_L" };
      
              
            }
            if (type == "coldsuperearth")
            {
              list = new List<string>() { "Planet_I", "Planet_J" };
              
            }
            if (type == "superearth")
            {
               list = new List<string>() { "Planet_H", "Planet_D" };
             
            }
            if (type == "hotsuperearth")
            {
                list = new List<string>() { "Planet_C", "Planet_G" };
             
            }
            if (type == "neptunian")
            {

                list = new List<string>()
                {
                     "Gas_Planet_I",
                     "Gas_Planet_L",
                     "Gas_Planet_H",
                     "Gas_Planet_D",
                     "Gas_Planet_I",
                     "Gas_Planet_L",
                     "Gas_Planet_H",
                     "Gas_Planet_D"
                };
        
            }
            if (type == "jovian")
            {
                list = new List<string>()
                {    "Gas_Planet_C",
                     "Gas_Planet_K",
                     "Gas_Planet_F",
                     "Gas_Planet_E",
                     "Gas_Planet_G",
                };
               
            }
            if (type == "hotjupiter")
            {
                list = new List<string>()
                {
                    "Gas_Planet_J",
                    "Gas_Planet_B",     
                };
            
            }
            if (type == "noimg")
            {
                list = new List<string>()
                {
                  "Planet_F"

                };

            }

            return list.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        }

    }
}
