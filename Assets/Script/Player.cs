using UnityEngine;

public class Player : MonoBehaviour
{
    // Adjust the speed for the application.
    public float speed = 1.0f;
    public float step = 1.0f;

    // The target (cylinder) position.
    public Transform target;

    void Awake()
    {
        // Position the cube at the origin.
        transform.position = new Vector3(0.0f, 0.0f, 0.0f);

        // Create and position the cylinder. Reduce the size.
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Camera.main.transform.position = new Vector3(0.85f, 1.0f, -3.0f);

        // Grab cylinder values and place on the target.
        target = cylinder.transform;
        target.transform.localScale = new Vector3(0.15f, 1.0f, 0.15f);
        target.transform.position = new Vector3(0.8f, 0.0f, 0.8f);

        // Position the camera.
        Camera.main.transform.position = new Vector3(0.85f, 1.0f, -3.0f);
        Camera.main.transform.localEulerAngles = new Vector3(15.0f, -20.0f, -0.5f);

        // Create and position the floor.
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
        floor.transform.position = new Vector3(0.0f, -1.0f, 0.0f);
    }

    void Update()
    {
     
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Camera.main.transform.position = new Vector3(0.85f, 1.0f, step++);
          

            target.position  = new Vector3(0.0f, 0.0f, step++);

        }
     
    }
}
