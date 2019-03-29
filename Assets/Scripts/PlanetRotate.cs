using System;
using System.Collections;
using UnityEngine;

public class PlanetRotate : MonoBehaviour
{
    public float xAngle, yAngle, zAngle;
    private GameObject _sun, _planet;
    public Material selfMat, worldMat;
    // Start is called before the first frame update
    void Start()
    {
        _planet = GameObject.FindGameObjectWithTag("Planet");
        _sun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _sun.transform.position = new Vector3(14.22f, 7.11f, 40f);
        _sun.name = "Sun";
        _planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _planet.GetComponent<Renderer>().material = selfMat;
        _planet.transform.position = new Vector3(4.22f, 4f, 0f);
        _planet.name = "Earth";
    }

    // Update is called once per frame
    void Update()
    {

        _planet.transform.RotateAround(_sun.transform.localPosition, Vector3.up, Time.deltaTime);
    }
     
}
