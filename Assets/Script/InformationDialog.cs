using Assets.Script.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
namespace Assets.Script
{
    public class InformationDialog : MonoBehaviour
    {
        private readonly string _url = "https://exoplanethunter.com/api/";
        private TextMeshPro m_Text;
        private TextContainer m_TextContainer;

        public void Start()
        {
       

        }

        private void Awake()
        {
            var planetname = PlayerPrefs.GetString("PlanetId");
     

       StartCoroutine(GetExoPlanetByName($"{_url}ExoSolarSystems/ExoPlanets?%24filter=Name%20eq%20%27{HttpUtility.UrlEncode(planetname)}%27"));
        

        }

        private void Update()
        {

            //  StartCoroutine(GetExoPlanetByName($"{_url}ExoPlanets?%24filter=Name eq '{Uri.EscapeUriString(planetname)}'"));
       
            if (OVRInput.GetDown(OVRInput.Button.Back))
            {
                SceneManager.LoadScene("PlanetSystem", LoadSceneMode.Single);

            }
       


        }
        private IEnumerator GetExoPlanetByName(string uri)
        {
           UnityWebRequest uwr = UnityWebRequest.Get(uri);
            yield return uwr.SendWebRequest();
        
            
                var planet = JsonConvert.DeserializeObject<List<Planet>>(uwr.downloadHandler.text);
                m_Text = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();

                m_TextContainer = GetComponent<TextContainer>();

                m_Text.transform.localPosition = new Vector3(0, 0, 8);
                //OVRNodeStateProperties.GetNodeStatePropertyVector3(UnityEngine.XR.XRNode.Head, NodeStatePropertyType.Position, OVRPlugin.Node.Head, OVRPlugin.Step.Render, out _playerHeadPos);
                //t.transform.LookAt(new Vector3(_playerHeadPos.x, _playerHeadPos.y, _playerHeadPos.z));
                //t.transform.Rotate(Vector3.up - new Vector3(0, 180, 0));
                // Set the text
                m_Text.text = $"Radius {planet.First().RadiusEu}*Earth"; ;
         
       
        }

    }
}
