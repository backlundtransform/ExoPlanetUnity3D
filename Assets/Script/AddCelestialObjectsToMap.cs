using Assets.Script.Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;
using Assets.Script.Models.Geo;
using System.Linq;

public class AddCelestialObjectsToMap : MonoBehaviour
{

    private Vector3 _playerHeadPos = Vector3.zero;
    public LongLat _location;
    public List<Star> _stars;
    void Start()
    {
        // StartCoroutine(GetRequest("http://ip-api.com/json"));
        StartCoroutine(GetStarMarkerRequest("http://exoplanethunter.com/api/Maps/StarMarkers"));
        //Todo find correct rotation 
        // RenderSettings.skybox.SetFloat("_Rotation", 40);


        var solarsystem = new List<Star> {

            new Star { Name = "Trappist 1", Coordinates = new Vector3(14.22f, 7.11f, 40f) },
            new Star { Name = "Proxima Centauri", Coordinates = new Vector3(13f, 11f, 40f) } };

        var glowBig = Resources.Load("Planet_B", typeof(Material)) as Material;

        foreach (var star in solarsystem)
        {
            GenerateMarkers(star, glowBig, star.Name);
        }

    }

    // Update is called once per frame
    void Update()
    {


        OVRNodeStateProperties.GetNodeStatePropertyVector3(UnityEngine.XR.XRNode.Head,
        NodeStatePropertyType.Position,
        OVRPlugin.Node.Head,
        OVRPlugin.Step.Render,
        out _playerHeadPos);
        //Debug.Log("Head: " + _playerHeadPos);

    }

    void GenerateMarkers(Star star, Material material, string tag)
    {
        var marker = new GameObject();
        marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        marker.GetComponent<Renderer>().material = material;
        marker.transform.position = star.Coordinates;
        marker.transform.localScale = new Vector3(1, 1, 1);
        marker.name = tag;
      //  GameObject text = new GameObject();
      //  TextMesh t = text.AddComponent<TextMesh>();
      //  t.text = star.Name;
      //  t.fontSize = 10;
      //  t.transform.localEulerAngles += new Vector3(90, 0, 0);
      //  t.transform.localPosition += star.Coordinates;


    }

    IEnumerator GetRequest(string uri)
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

    IEnumerator GetStarMarkerRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();
        _stars = JsonConvert.DeserializeObject<FeatureCollection>(uwr.downloadHandler.text).Features.Select(o => new Star { Name = o.Properties.Name, Coordinates = SphericalToCartesian(30, o.Geometry.Coordinates.First(), o.Geometry.Coordinates.Last()) }).ToList();

        var orbGlow = Resources.Load("Sun", typeof(Material)) as Material;
        foreach (var star in _stars)
        {
            GenerateMarkers(star, orbGlow, "star");

        }

    }
}
