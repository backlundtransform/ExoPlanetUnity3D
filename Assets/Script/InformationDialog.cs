using Assets.Script.Models;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
namespace Assets.Script
{
    public class InformationDialog : MonoBehaviour
    {
        private readonly string _url = "https://exoplanethunter.com/api/";


        private void Awake()
        {
            if (SceneManager.GetActiveScene().name == "PlanetInfo")
            {
                var planetname = PlayerPrefs.GetString("PlanetId");
                StartCoroutine(GetExoPlanetByName($"{_url}ExoSolarSystems/ExoPlanets?%24filter=Name%20eq%20%27{HttpUtility.UrlEncode(planetname)}%27"));
            }

            if (SceneManager.GetActiveScene().name == "SolarInfo")
            {
                var starname = PlayerPrefs.GetString("starId");

                StartCoroutine(GetExoPStarByName($"{_url}GetExoSolarSystemByName?name%20eq%20%27{HttpUtility.UrlEncode(starname)}%27", $"{_url}ExoSolarSystems/ExoPlanets?%24filter=Name%20eq%20%27"));

            }

        }

        private void Update()
        {

            if (OVRInput.GetDown(OVRInput.Button.Back))
            {
                SceneManager.LoadScene("PlanetSystem", LoadSceneMode.Single);
            }

        }
        private IEnumerator GetExoPlanetByName(string uri)
        {
            UnityWebRequest uwr = UnityWebRequest.Get(uri);
            yield return uwr.SendWebRequest();

            var planet = JsonConvert.DeserializeObject<List<Planet>>(uwr.downloadHandler.text).First();
            var infoarray = new List<string>();
            infoarray.AddIfNotNull($"Radius {planet.RadiusEu}*Earth", planet.RadiusEu);
            infoarray.AddIfNotNull($"Mass {planet.Mass}*Earth", planet.Mass);
            infoarray.AddIfNotNull($"Density {planet.Density} Earth", planet.Density);
            infoarray.AddIfNotNull($"Gravity {planet.Gravity}*Earth", planet.Gravity);
            infoarray.AddIfNotNull($"Surface Pressure {planet.SurfacePressure}*Earth", planet.SurfacePressure);
            infoarray.AddIfNotNull($"Escape Velocity {planet.EscapeVelocity}*Earth",planet.EscapeVelocity);
            infoarray.AddIfNotNull($"Min Temperature {planet.TempMin} °C", planet.EscapeVelocity);
            infoarray.AddIfNotNull($"Temperature {planet.Temp} °C", planet.Temp);
            infoarray.AddIfNotNull($"Max Temperature {planet.TempMax} °C", planet.TempMax);
            infoarray.AddIfNotNull($"It takes {planet.Period} days for the planet to complete an entire revolution around its star °C", planet.Period);
            infoarray.AddIfNotNull($"Distance from Star {planet.MeanDistance} AU", planet.MeanDistance);
            infoarray.AddIfNotNull($"Discovered year {planet.DiscYear}", planet.DiscYear);

            //TO DO ADD DISC METHOD AND HABITABLE
            var r_Text = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
            r_Text.fontSize = 8;
            r_Text.transform.localPosition = new Vector3(0, 0, 8);
            r_Text.text = string.Join("\n", infoarray);



        }

        private IEnumerator GetExoPStarByName(string staruri, string planeturi)
        {
            UnityWebRequest uwr = UnityWebRequest.Get(staruri);
            yield return uwr.SendWebRequest();
            var star = JsonConvert.DeserializeObject<ExoSolarSystem>(uwr.downloadHandler.text);

            uwr = UnityWebRequest.Get($"{planeturi}{HttpUtility.UrlEncode(star.Planets.First().Name)}%27");
            yield return uwr.SendWebRequest();
            var planet= JsonConvert.DeserializeObject<List<Planet>>(uwr.downloadHandler.text).First();
            var exostar = planet.Star;
            var infoarray = new List<string>();
            infoarray.AddIfNotNull(exostar.Name, exostar.HabZoneMax);

            var s_Text = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
            s_Text.fontSize = 8;
            s_Text.transform.localPosition = new Vector3(0, 0, 8);
            s_Text.text = string.Join("\n", infoarray);


        }

    }
}