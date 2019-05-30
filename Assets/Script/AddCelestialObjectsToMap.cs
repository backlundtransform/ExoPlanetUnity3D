using UnityEngine;

public class AddCelestialObjectsToMap : MonoBehaviour
{
    private GameObject _planet;
    void Start()
    {

        Material Planet = Resources.Load("OrbGlow", typeof(Material)) as Material;

        _planet = GameObject.CreatePrimitive(PrimitiveType.Quad);
        _planet.GetComponent<Renderer>().material = Planet;
        _planet.transform.position = new Vector3(14.22f, 7.11f, 40f);

        _planet.transform.localScale = new Vector3(20, 20, 20);
        _planet.name = "Trappist 1";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
