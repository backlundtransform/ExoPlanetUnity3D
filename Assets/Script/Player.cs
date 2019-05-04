using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{

    public float speed = 1.0f;
    public float zstep = 1.0f;
    public float xstep = 1.0f;
    public float ystep = 1.0f;
    public GameObject world;
  


    void Start()
    {


        world = GameObject.FindWithTag("Player");
        world.transform.position = new Vector3(xstep, ystep, zstep);


    }
  

    void Update()
    {
        world = GameObject.FindWithTag("Player");
        if (Input.GetButtonDown("Fire1")) {
            Camera.main.transform.position = new Vector3(xstep, ystep, zstep++);
        }
        if (OVRInput.Get(OVRInput.RawButton.DpadUp))
        {
            world.transform.position = new Vector3(xstep, ystep, zstep++);
        }

        if (OVRInput.Get(OVRInput.Button.DpadDown))
        {
            world.transform.position = new Vector3(xstep, ystep, zstep--);
        }
    
    }
}
