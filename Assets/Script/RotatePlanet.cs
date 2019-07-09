using Assets.Script;
using Assets.Script.Models;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class RotatePlanet : MonoBehaviour
{
  
    private GameObject _sun, _planet;
    private string _url = "https://exoplanethunter.com/api/";

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
        Material Planet = Resources.Load("Planet_B", typeof(Material)) as Material;
        Material Sun = Resources.Load("Sun", typeof(Material)) as Material;
        _sun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _sun.transform.position = new Vector3(14.22f, 7.11f, 40f);
        _sun.transform.localScale = new Vector3(20, 20, 20);
        _sun.GetComponent<Renderer>().material = Sun;
        _sun.name = solarsystem.Name;

        _planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _planet.GetComponent<Renderer>().material = Planet;
        _planet.transform.position = new Vector3(14.22f, 7.11f, 20f);
        _sun.transform.localScale = new Vector3(2, 2, 2);
        _planet.name = "Earth";

    }

    public void Update()
    {
        _planet.transform.RotateAround(_sun.transform.localPosition, Vector3.up, Time.deltaTime);
    }
}
