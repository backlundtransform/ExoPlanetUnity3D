using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 1.0f;
    public float zstep = 1.0f;
    public float xstep = 1.0f;
    public float ystep = 1.0f;


    void Awake()
    {
     
        Camera.main.transform.position = new Vector3(0.85f, 1.0f, -3.0f);

    }

    void Update()
    {
     
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Camera.main.transform.position = new Vector3(xstep, ystep--, zstep);
          

        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Camera.main.transform.position = new Vector3(xstep--, ystep, zstep);


        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            Camera.main.transform.position = new Vector3(xstep, ystep, ++zstep);


        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            Camera.main.transform.position = new Vector3(xstep, ystep, --zstep);


        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Camera.main.transform.position = new Vector3(xstep++, ystep, zstep);


        }



        if (Input.GetKey(KeyCode.DownArrow))
        {
            Camera.main.transform.position = new Vector3(xstep, ystep++, zstep);


        

        }

    }
}
