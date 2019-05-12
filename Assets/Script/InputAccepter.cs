using UnityEngine;

public class InputAccepter : MonoBehaviour
{
    public float speed = 1.0f;
    public float zstep = 1.0f;
    public float xstep = 1.0f;
    public float ystep = 1.0f;
    public GameObject world;
    private void Awake()
    {
        Player.onTriggerDown += TriggerDown;
        Player.onTuchpadDown += TriggerDown;
        Player.onTuchpadUp += TriggerUp;
    }

    void Start()
    {
        world = GameObject.FindWithTag("Player");
        world.transform.position = new Vector3(xstep, ystep, zstep);
    }

    void TriggerDown()
    {
        world = GameObject.FindWithTag("Player");
        world.transform.position = new Vector3(world.transform.position.x , world.transform.position.y, world.transform.position.y+1);
    }
    void TriggerUp()
    {
        world = GameObject.FindWithTag("Player");
        world.transform.position = new Vector3(world.transform.position.x, world.transform.position.y, world.transform.position.z-1);
    }
}
