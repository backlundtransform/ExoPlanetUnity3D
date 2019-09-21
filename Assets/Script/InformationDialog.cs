using Assets.Script.Models;
using Assets.Script.Services;
using Newtonsoft.Json;
using System;
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
           
            var planetname = PlayerPrefs.GetString("PlanetId");
            var starname = PlayerPrefs.GetString("starId");


            var text = (int)1.3d / 10;

            if (planetname== starname)
            {
                StartCoroutine(GetExoPStarByName($"{_url}ExoSolarSystems/GetExoSolarSystemByName?name={starname}", $"{_url}ExoSolarSystems/ExoPlanets?%24filter=Name%20eq%20%27"));
            }
            else
            {
                StartCoroutine(GetExoPlanetByName($"{_url}ExoSolarSystems/ExoPlanets?%24filter=Name%20eq%20%27{HttpUtility.UrlEncode(planetname)}%27"));
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
            infoarray.AddIfNotNull(planet.Name, 1);
            infoarray.AddIfNotNull($"Radius {planet.RadiusEu.DecimalRound()}*Earth", planet.RadiusEu);
            infoarray.AddIfNotNull($"Mass {planet.Mass.DecimalRound()}*Earth", planet.Mass);
            infoarray.AddIfNotNull($"Density {planet.Density.DecimalRound()} Earth", planet.Density);
            infoarray.AddIfNotNull($"Gravity {planet.Gravity.DecimalRound()}*Earth", planet.Gravity);
            infoarray.AddIfNotNull($"Surface Pressure {planet.SurfacePressure.DecimalRound()}*Earth", planet.SurfacePressure);
            infoarray.AddIfNotNull($"Escape Velocity {planet.EscapeVelocity.DecimalRound()}*Earth",planet.EscapeVelocity);
            infoarray.AddIfNotNull($"Min Temperature {planet.TempMin.DecimalRound()} °C", planet.TempMin);
            infoarray.AddIfNotNull($"Temperature {planet.Temp.DecimalRound()} °C", planet.Temp);
            infoarray.AddIfNotNull($"Max Temperature {planet.TempMax.DecimalRound()} °C", planet.TempMax);
            infoarray.AddIfNotNull($"It takes {planet.Period.DecimalRound()} days for the\n planet to complete an entire\n revolution around its star", planet.Period);
            infoarray.AddIfNotNull($"Distance from star {planet.MeanDistance.DecimalRound()} AU", planet.MeanDistance);
            infoarray.AddIfNotNull($"Discovered year {(int)planet.DiscYear}", planet.DiscYear);
            infoarray.AddIfNotNull($"Discovered by {PlanetService.Discmethods[planet.DiscMethod]}", PlanetService.Discmethods[planet.DiscMethod] == null? null: (decimal?)1);

            var planetmat= Resources.Load(PlanetService.GetPlanetType(planet.Img.Uri, planet.Id), typeof(Material)) as Material;
            var planetobject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            planetobject.transform.position = new Vector3(0f, 7.11f, 40f);
            planetobject.transform.localScale = new Vector3(25, 25, 25);
            planetobject.GetComponent<Renderer>().material = planetmat;
            var text = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
            text.fontSize = 8;
            text.transform.localPosition = new Vector3(0, 0, 8);
            text.text = string.Join("\n", infoarray);



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
            var sunmat = Resources.Load(Enum.GetName(typeof(StarType), exostar.Color), typeof(Material)) as Material;
            var sun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sun.transform.position = new Vector3(0f, 7.11f, 40f);
            sun.transform.localScale = new Vector3(25, 25, 25);
            sun.GetComponent<Renderer>().material = sunmat;
            infoarray.AddIfNotNull(exostar.Name,1);
            infoarray.AddIfNotNull($"Type {exostar.Type}", 1);
            infoarray.AddIfNotNull($"Radius {exostar.RadiusSu.DecimalRound()}*Sun", exostar.RadiusSu);
            infoarray.AddIfNotNull($"Mass {exostar.Mass.DecimalRound()}*Sun", exostar.Magnitude);
            infoarray.AddIfNotNull($"Age {exostar.Age.DecimalRound()} Gyrs", exostar.Age);
            infoarray.AddIfNotNull($"Temperature {exostar.Temp.DecimalRound()} °C", planet.Temp);
            infoarray.AddIfNotNull($"Star is located {star.Planets.First().StarDistance.DecimalRound()} lightyears from Earth", star.Planets.First().StarDistance);
            var text = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
            text.fontSize = 8;
            text.transform.localPosition = new Vector3(0, 0, 8);
            text.text = string.Join("\n", infoarray);


        }

    }
}