using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static UnityAction<bool> onHasController = null;
    public static UnityAction  onTriggerDown = null;
    public static UnityAction onTriggerUp= null;
    public static UnityAction onTuchpadUp = null;
    public static UnityAction onTuchpadDown = null;
    public static UnityAction onBackButtonDown = null;
    private bool hasController = false;
    public float speed = 1.0f;
    public float zstep = 1.0f;
    public float xstep = 1.0f;
    public float ystep = 1.0f;
    public GameObject world;
    private bool inputActive = true;
    public Transform controller;
    public static bool _leftHanded { get; private set; }
    System.IO.StreamWriter _recording;

    private void Awake()
    {
        OVRManager.HMDMounted += PlayerFound;
        OVRManager.HMDUnmounted += PlayerLost;
    }

    private void OnDestroy()
    {
        OVRManager.HMDMounted -= PlayerFound;
        OVRManager.HMDUnmounted -= PlayerLost;
    }

    void Start()
    {

        _leftHanded = OVRInput.GetControllerPositionTracked(OVRInput.Controller.LTouch);
        world = GameObject.FindWithTag("Player");
        world.transform.position = new Vector3(xstep, ystep, zstep);

    }

    private bool CheckForController(bool currentValue)
    {
       bool controllerCheck= OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote) || OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote);

        if(currentValue == controllerCheck)
        {
            return currentValue;
        }
        onHasController?.Invoke(currentValue);
        return currentValue;
    }
    void Update()
    {
        if (!inputActive) {
            return;
        }
        hasController = CheckForController(hasController);

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) {
            onTriggerDown?.Invoke();

        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            onTriggerUp?.Invoke();

        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryTouchpad))
        {
            onTuchpadUp?.Invoke();

        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {
            onTuchpadDown?.Invoke();
        }

        if (OVRInput.GetDown(OVRInput.Button.Back))
        {
            onBackButtonDown?.Invoke();
        }
        float triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
     

    }
    private void PlayerFound()
    {
        inputActive = true;
    }
    private void PlayerLost()
    {
        inputActive =false;

    }
}
