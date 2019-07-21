using Assets.Script.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
namespace Assets.Script
{
    public class InformationDialog : MonoBehaviour
    {
        private readonly string _url = "https://exoplanethunter.com/api/";

        public void Start()
        {
            var planetname = PlayerPrefs.GetString("PlanetId");
          
            StartCoroutine(GetExoPlanetByName($"{_url}ExoPlanets?%24filter=Name eq '{Uri.EscapeUriString(planetname)}'"));

        }

        private void Awake()
        {
          
        }

        private void Update()
        {
            if (OVRInput.GetDown(OVRInput.Button.Back))
            {
                SceneManager.LoadScene("PlanetSystem", LoadSceneMode.Single);

            }
       


        }
        private IEnumerator GetExoPlanetByName(string uri)
        {
            UnityWebRequest uwr = UnityWebRequest.Get(uri);
            yield return uwr.SendWebRequest();
            var  planet = JsonConvert.DeserializeObject<Planet>(uwr.downloadHandler.text);
            GameObject text = GameObject.FindGameObjectsWithTag("Info")[0];
            TextMeshPro t = text.GetComponent<TextMeshPro>();
            t.text = $"Radius {Math.Round(planet.RadiusEu,2)}*Earth";
       
        }


    }
}
