using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCelestialObjectsToMap : MonoBehaviour
{
    private GameObject _planet;
    void Start()
    {

        Material Planet = Resources.Load("Planet_B", typeof(Material)) as Material;

        _planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _planet.GetComponent<Renderer>().material = Planet;
        _planet.transform.position = new Vector3(14.22f, 7.11f, 40f);

        _planet.transform.localScale = new Vector3(10, 10, 10);
        _planet.name = "Ea";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
