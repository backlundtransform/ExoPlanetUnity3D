using Assets.Script;
using Assets.Script.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class RotatePlanet : MonoBehaviour
{
  
    private GameObject _sun;

    private List<GameObject> _planets;
    private readonly string _url = "https://exoplanethunter.com/api/";

    public void Start()
    {
      var solarname  =PlayerPrefs.GetString("starId");
        StartCoroutine(GetExoSolarSystemByName($"{_url}ExoSolarSystems/GetExoSolarSystemByName?name={solarname}"));

    }
    private IEnumerator GetExoSolarSystemByName(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();
       var solarsystem= JsonConvert.DeserializeObject<Star>(uwr.downloadHandler.text);
  
        Material Sun = Resources.Load(Enum.GetName(typeof(StarType), solarsystem.Color), typeof(Material)) as Material;
        _sun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _sun.transform.position = new Vector3(14.22f, 7.11f, 40f);
        var starradius = 0.05f * (float)solarsystem.Radius;
        _sun.transform.localScale = new Vector3(starradius, starradius, starradius);
        _sun.GetComponent<Renderer>().material = Sun;
        _sun.name = solarsystem.Name;
        var habmin = new GameObject { name = "HabZoneMin" };
        var habmax = new GameObject { name = "HabZoneMax" };
        habmin.transform.position = _sun.transform.position;
        habmax.transform.position = _sun.transform.position;
     
        habmin.DrawCircle(Vector3.Distance(_sun.transform.position, new Vector3(14.22f, 7.11f, 40f - 0.05f * (float)solarsystem.HabZoneMin)), 0.03f, Color.red);
        habmax.DrawCircle(Vector3.Distance(_sun.transform.position, new Vector3(14.22f, 7.11f, 40f - 0.05f * (float)solarsystem.HabZoneMax)), 0.03f, Color.blue);
        _planets = new List<GameObject>();

        foreach (var planet in solarsystem.Planets) {
            if (!_planets.Any(p => p.name == planet.Name))
            {
                Material Planet = Resources.Load(GetPlanetType(planet.Img.Uri), typeof(Material)) as Material;
            var planetobject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            planetobject.GetComponent<Renderer>().material = Planet;
            var distance = 0.05f * (planet.StarDistance == null ? 0f : (float)planet.StarDistance);
            var radius= 0.05f *(float)planet.Radius;
            var path = new GameObject { name = "Path" };
            path.transform.position = _sun.transform.position;

            planetobject.transform.position = new Vector3(14.22f, 7.11f, 40f-distance);
            path.DrawCircle(Vector3.Distance(_sun.transform.position, planetobject.transform.position), 0.01f, Color.white);
            planetobject.transform.localScale = new Vector3(radius, radius, radius);
            planetobject.name = planet.Name;
        
            _planets.Add(planetobject);
            }

        }

    }

    private string GetPlanetType(string type) {
        if (type == "stone")
        {
            return "Planet_B";
        }
        if (type == "coldstone")
        {
            return "Planet_E";
        }
        if (type == "hotstone") {
            return "Planet_L";
        }
        if (type == "coldsuperearth") {
            return "Planet_J";
        }
        if (type == "superearth") {
            return "Planet_D";
        }
        if (type == "hotsuperearth") {
            return "Planet_G";
        }
        if (type == "neptunian") {
            return "Gas_Planet_A";
        }
        if (type == "jovian") {
            return "Gas_Planet_G";
        }
        if (type == "hotjupiter") {
            return "Gas_Planet_G";
        }

        return "Planet_F";
    }

    public void Update()
    {
        var i = 0.1f* _planets.Count();
        foreach (var planet in _planets.GroupBy(p => p.name).Select(g => g.FirstOrDefault()).ToList())
        {
            planet.transform.RotateAround(_sun.transform.localPosition, Vector3.up, Time.deltaTime+i);

            i = i - 0.1f;
        }
    }
}
