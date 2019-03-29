using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float xAngle, yAngle, zAngle;
    public Material selfMat, worldMat;
    private GameObject sphere1, sphere2;

    void Start()
    {
        sphere1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere1.transform.position = new Vector3(0.75f, 0.0f, 0.0f);
        sphere1.GetComponent<Renderer>().material = selfMat;
        sphere1.name = "Sun";

        sphere2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere2.transform.position = new Vector3(-0.75f, 0.0f, 0.0f);
        sphere2.GetComponent<Renderer>().material = worldMat;
        sphere2.name = "Planet";

    }

    void Update()
    {
        sphere2.transform.Rotate(xAngle, yAngle, zAngle, Space.World);

    }
}
