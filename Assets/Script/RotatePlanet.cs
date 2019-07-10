using Assets.Script;
using Assets.Script.Models;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
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
  
        Material Sun = Resources.Load("Sun", typeof(Material)) as Material;
        _sun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _sun.transform.position = new Vector3(14.22f, 7.11f, 40f);
        _sun.transform.localScale = new Vector3(2, 2, 2);
        _sun.GetComponent<Renderer>().material = Sun;
        _sun.name = solarsystem.Name;
        _planets = new List<GameObject>();

       

        foreach (var planet in solarsystem.Planets) {
            Material Planet = Resources.Load("Planet_B", typeof(Material)) as Material;
            var planetobject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            planetobject.GetComponent<Renderer>().material = Planet;
            var distance = 0.05f * (planet.StarDistance == null ? 0f : (float)planet.StarDistance);
            var radius= 0.05f * (planet.Radius == null ? 0f : (float)planet.Radius);
            planetobject.transform.position = new Vector3(14.22f, 7.11f, distance);
            planetobject.transform.localScale = new Vector3(radius, radius, radius);
            planetobject.name = planet.Name;
            _planets.Add(planetobject);
         
        }

    }

    public void Update()
    {
        var i = 0f;
        foreach (var _planet in _planets)
        {
             _planet.transform.RotateAround(_sun.transform.localPosition, Vector3.up, Time.deltaTime+i);

            i = i + 0.1f;
        }
    }
}
