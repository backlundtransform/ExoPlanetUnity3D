using Assets.Script;
using UnityEngine;
using UnityEngine.UI;

public class RotatePlanet : MonoBehaviour
{
  
    public float xAngle, yAngle, zAngle;
    public float SkyboxSpeed = 1f;

    private GameObject _sun, _planet;

  
    internal void Start()
    {
        Debug.Log(SceneVariables.StarId);
        //Todo find correct rotation 
        RenderSettings.skybox.SetFloat("_Rotation", 90);

      

        Material Planet = Resources.Load("Planet_B", typeof(Material)) as Material;
        Material Sun = Resources.Load("Sun", typeof(Material)) as Material;
        _sun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _sun.transform.position = new Vector3(14.22f, 7.11f, 40f);
        _sun.transform.localScale = new Vector3(20, 20, 20);
        _sun.GetComponent<Renderer>().material = Sun;
        _sun.name = "Sun";

        _planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _planet.GetComponent<Renderer>().material = Planet;
        _planet.transform.position = new Vector3(4.22f, 4f, 0f);
        _sun.transform.localScale = new Vector3(2, 2, 2);
        _planet.name = "Earth";


        GameObject text = new GameObject();
        TextMesh t = text.AddComponent<TextMesh>();
        t.text = PlayerPrefs.GetString("starId");
        t.fontSize = 30;
        t.transform.localEulerAngles += new Vector3(0, 0, 0);
        t.transform.localPosition += new Vector3(14.22f, 7.11f, 40f);
    }


    internal void Update()
    {
      
        _planet.transform.RotateAround(_sun.transform.localPosition, Vector3.up, Time.deltaTime);
    }
}
