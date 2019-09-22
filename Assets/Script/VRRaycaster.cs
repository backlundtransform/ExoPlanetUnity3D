using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class VRRaycaster : MonoBehaviour
{

    [System.Serializable]
    public class Callback : UnityEvent<Ray, RaycastHit> { }

    public Transform leftHandAnchor = null;
    public Transform rightHandAnchor = null;
    public Transform centerEyeAnchor = null;
    public LineRenderer lineRenderer = null;
    public float maxRayDistance = 500.0f;
    public LayerMask excludeLayers;
    public Callback raycastHitCallback;
    public GameObject world;
    private Vector3 _playerHeadPos = Vector3.zero;
    public bool isLoaded = false;
    public void Awake()
    {

        if (leftHandAnchor == null)
        {
            Debug.LogWarning("Assign LeftHandAnchor in the inspector!");
            GameObject left = GameObject.Find("LeftHandAnchor");
            if (left != null)
            {
                leftHandAnchor = left.transform;
            }
        }
        if (rightHandAnchor == null)
        {
            Debug.LogWarning("Assign RightHandAnchor in the inspector!");
            GameObject right = GameObject.Find("RightHandAnchor");
            if (right != null)
            {
                rightHandAnchor = right.transform;
            }
        }
        if (centerEyeAnchor == null)
        {
            Debug.LogWarning("Assign CenterEyeAnchor in the inspector!");
            GameObject center = GameObject.Find("CenterEyeAnchor");
            if (center != null)
            {
                centerEyeAnchor = center.transform;
            }
        }
     

    }

 public Transform Pointer
    {
        get
        {
            OVRInput.Controller controller = OVRInput.GetConnectedControllers();
            if ((controller & OVRInput.Controller.LTrackedRemote) != OVRInput.Controller.None)
            {
                return leftHandAnchor;
            }
            else if ((controller & OVRInput.Controller.RTrackedRemote) != OVRInput.Controller.None)
            {
                return rightHandAnchor;
            }
        
            return centerEyeAnchor;
        }
    }
  private  IEnumerator Wait(GameObject text, RaycastHit hit)
    {
        PlayerPrefs.SetInt("hit", Convert.ToInt32(hit.collider.gameObject.name[0] != ' '));
        PlayerPrefs.Save();
        lineRenderer.material.color = hit.collider.gameObject.name[0] != ' ' ? Color.green : Color.magenta;
        yield return new WaitForSeconds(1);
        Destroy(text);
        PlayerPrefs.SetInt("hit", 0);
        PlayerPrefs.Save();
        lineRenderer.material.color = Color.magenta;
    }
        private void GenerateDialog(GameObject text, RaycastHit hit)
    {
        TextMesh t = text.AddComponent<TextMesh>();
        t.text = hit.collider.gameObject.name;
        t.fontSize = 14;
        t.transform.localPosition += hit.collider.gameObject.transform.position;
        OVRNodeStateProperties.GetNodeStatePropertyVector3(UnityEngine.XR.XRNode.Head, NodeStatePropertyType.Position, OVRPlugin.Node.Head, OVRPlugin.Step.Render, out _playerHeadPos);
        t.transform.LookAt(new Vector3(_playerHeadPos.x, _playerHeadPos.y, _playerHeadPos.z));
        t.transform.Rotate(Vector3.up - new Vector3(0, 180, 0));
    }

    public void Update()
    {
        if (isLoaded) {
            if (lineRenderer == null)
            {
                Debug.LogWarning("Assign a line renderer in the inspector!");
                lineRenderer = gameObject.AddComponent<LineRenderer>();
                lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                lineRenderer.receiveShadows = false;
                lineRenderer.widthMultiplier = 0.02f;
                lineRenderer.material = new Material(Shader.Find("Sprites/Default"))
                {
                    color = Color.magenta
                };
            }
            Transform pointer = Pointer;
        if (pointer == null)
        {
            return;
        }

        Ray laserPointer = new Ray(pointer.position, pointer.forward);

        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, laserPointer.origin);
            lineRenderer.SetPosition(1, laserPointer.origin + laserPointer.direction * maxRayDistance);
        }


            RaycastHit hit;
          
        if (Physics.Raycast(laserPointer, out hit, maxRayDistance, ~excludeLayers))
        {
               
                if (lineRenderer != null)
                {
                lineRenderer.SetPosition(1, hit.point);
               
                }
                if (GameObject.Find("text") == null)
                {
                    GameObject text = new GameObject();
                    text.name = "text";
                    GenerateDialog(text, hit);
                    StartCoroutine(Wait(text, hit));
                }
             

                if (SceneManager.GetActiveScene().name == "PlanetSystem") {
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
                    {
                        
                            PlayerPrefs.SetString("PlanetId", hit.collider.gameObject.name);
                            PlayerPrefs.Save();
                            SceneManager.LoadScene("PlanetInfo", LoadSceneMode.Single);

                      
                    }
                 world = GameObject.FindWithTag("Player");
                world.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + 10, hit.collider.gameObject.transform.position.z - 10);


            }
            if (SceneManager.GetActiveScene().name == "StarMap" && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                
              if (hit.collider.gameObject.name[0]!= ' ') { 
                   PlayerPrefs.SetString("starId", hit.collider.gameObject.name);
                   PlayerPrefs.Save();
                   SceneManager.LoadScene("PlanetSystem", LoadSceneMode.Single);
                }
                  
             } 

            if (raycastHitCallback != null)
            {
                raycastHitCallback.Invoke(laserPointer, hit);
            }
          }
        }
        isLoaded = true;

    }
 
}