using Assets.Script.Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;
using Assets.Script.Models.Geo;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class AddCelestialObjectsToMap : MonoBehaviour
{
    private string _url = "https://exoplanethunter.com/api/";
    public LongLat _location;
    public List<Star> _stars;
    public static List<Planet> _planets;

    private void Awake()
    {
        SceneManager.LoadScene("SolarInfo", LoadSceneMode.Single);
        StartCoroutine(GetAllPlanetsRequest($"{_url}ExoSolarSystems/GetAllPlanets"));
        StartCoroutine(GetStarMarkerRequest($"{_url}Maps/StarMarkers"));

    }



  private void GenerateMarkers(Star star, Material material, string tag)
    {
        var marker = new GameObject();
        marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        marker.GetComponent<Renderer>().material = material;
        marker.transform.position = star.Coordinates;
        marker.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        marker.name = tag;
  

    }

  private IEnumerator GetRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();
        _location = JsonConvert.DeserializeObject<LongLat>(uwr.downloadHandler.text);
    }

    public Vector3 SphericalToCartesian(float radius, float polar, float elevation)
    {
        var outCart = new Vector3();
        float a = radius * Mathf.Cos(Mathf.PI * elevation / 180);
        outCart.x = a * Mathf.Cos(Mathf.PI * polar / 180);
        outCart.y = radius * Mathf.Sin(Mathf.PI * elevation / 180);
        outCart.z = a * Mathf.Sin(Mathf.PI * polar / 180);
        return outCart;
    }

    private IEnumerator GetAllPlanetsRequest(string uri)
    {
        if (_planets == null) { 
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();
            _planets = JsonConvert.DeserializeObject<List<Planet>>(uwr.downloadHandler.text);
        }

            var solarsystem = _planets.Where(p=>p.Coordinate.Longitude!=null && p.Coordinate.Latitude!=null).Select(o => new Star { Name = o.Star.Name, Color=o.Star.Color, HasHab =o.Star.NoHabPlanets>0, Coordinates = SphericalToCartesian(30, (float)o.Coordinate.Longitude, (float)o.Coordinate.Latitude) });
     
        foreach (var star in solarsystem)
            {
             var glowBig = Resources.Load(star.Color!=null?Enum.GetName(typeof(StarType), star.Color):"Sun", typeof(Material)) as Material;
             GenerateMarkers(star, glowBig, star.Name);
            }
  

    }

    private IEnumerator GetStarMarkerRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();
        _stars = JsonConvert.DeserializeObject<FeatureCollection>(uwr.downloadHandler.text).Features.Select(o => new Star { Name = o.Properties.Name, Coordinates = SphericalToCartesian(30, o.Geometry.Coordinates.First(), o.Geometry.Coordinates.Last()) }).ToList();
      
       var glowBig = Resources.Load("Star_White", typeof(Material)) as Material;
        foreach (var star in _stars)
        {
            GenerateMarkers(star, glowBig, $" {star.Name}");

        }

    }
}
