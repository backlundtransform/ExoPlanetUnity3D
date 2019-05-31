using Assets.Script.Models;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AddCelestialObjectsToMap : MonoBehaviour
{

    void Start()
    {

        //Todo find correct rotation 
        RenderSettings.skybox.SetFloat("_Rotation", 10);
        var stars = new List<Star> { new Star { Name = "Vega", Coordinates = new Vector3((float)(Math.Sin(Math.PI * 18 * 15 / 180) * Math.Cos(Math.PI * 38 / 180)), (float)(Math.Sin(Math.PI * 18*15 / 180)*Math.Sin(Math.PI*38/180)), 40f) }, new Star { Name = "Trappist 1", Coordinates = new Vector3(14.22f, 7.11f, 40f) }, new Star { Name = "Proxima Centauri", Coordinates = new Vector3(0f, 0f, 40f) } };
        Material OrbGlow = Resources.Load("OrbGlow", typeof(Material)) as Material;

        foreach (var star in stars) {
            var marker = new GameObject();
            marker = GameObject.CreatePrimitive(PrimitiveType.Quad);
            marker.GetComponent<Renderer>().material = OrbGlow;
            marker.transform.position = star.Coordinates;

            marker.transform.localScale = new Vector3(5, 5, 5);
            marker.name =star.Name;
        }
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
