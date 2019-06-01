using Assets.Script.Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;

public class AddCelestialObjectsToMap : MonoBehaviour
{

   private Vector3 _playerHeadPos = Vector3.zero;
    public LongLat _location;
    void Start()
    {
        StartCoroutine(GetRequest("http://ip-api.com/json"));

        //Todo find correct rotation 
        // RenderSettings.skybox.SetFloat("_Rotation", 40);
        var stars = new List<Star> {
            new Star { Name = "Vega", Coordinates = new Vector3(40f*(Mathf.Sin(Mathf.PI * 18 * 15 / 180) * Mathf.Cos(Mathf.PI * 38 / 180)), 
            40f * (Mathf.Sin(Mathf.PI * 18*15 / 180)*Mathf.Sin(Mathf.PI*38/180)), 40f) },
       };

        var solarsystem = new List<Star> {
        
            new Star { Name = "Trappist 1", Coordinates = new Vector3(14.22f, 7.11f, 40f) },
            new Star { Name = "Proxima Centauri", Coordinates = new Vector3(13f, 11f, 40f) } };
        var orbGlow = Resources.Load("OrbGlow", typeof(Material)) as Material;
       var glowBig = Resources.Load("GlowBig", typeof(Material)) as Material;
        foreach (var star in solarsystem)
        {

            GenerateMarkers(star, glowBig, star.Name);
        }
        foreach (var star in stars)
        {
            GenerateMarkers(star, orbGlow, "star");

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

    void GenerateMarkers(Star star, Material material, string tag) {
        var marker = new GameObject();
        marker = GameObject.CreatePrimitive(PrimitiveType.Quad);
        marker.GetComponent<Renderer>().material = material;
        marker.transform.position = star.Coordinates;
        marker.transform.localScale = new Vector3(5, 5, 5);
        marker.name =tag;
        GameObject text = new GameObject();
        TextMesh t = text.AddComponent<TextMesh>();
        t.text = star.Name;
        t.fontSize = 14;
        t.transform.localEulerAngles += new Vector3(0, 0, 0);
        t.transform.localPosition += star.Coordinates;
    }

    IEnumerator GetRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();
        _location =  JsonConvert.DeserializeObject<LongLat>(uwr.downloadHandler.text);


    }
}
