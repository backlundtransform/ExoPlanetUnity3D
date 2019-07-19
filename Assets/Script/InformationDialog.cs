using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Assets.Script
{
    public class InformationDialog : MonoBehaviour
    {


        private void Awake()
        {
        
        }

        private void Update()
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
             SceneManager.LoadScene("PlanetSystem", LoadSceneMode.Single);
            }
        }

        private void Start()
        {
           
        }

    }
}
