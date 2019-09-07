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
       
            if (OVRInput.GetDown(OVRInput.Button.Back))
            {
                SceneManager.LoadScene("PlanetSystem", LoadSceneMode.Single);

            }
       


        }
        private IEnumerator GetExoPlanetByName(string uri)
        {
           UnityWebRequest uwr = UnityWebRequest.Get(uri);
            yield return uwr.SendWebRequest();
        
            
                var planet = JsonConvert.DeserializeObject<List<Planet>>(uwr.downloadHandler.text).First();
             

            var infoarray = new Dictionary<string,string>();
            infoarray.AddIfNotNull("Radius",$"{ planet.RadiusEu}*Earth", planet.RadiusEu);
            infoarray.AddIfNotNull("Mass",$"{planet.Mass}*Earth", planet.Mass);
            infoarray.AddIfNotNull("Gravity",$"{planet.Gravity}*Earth", planet.Gravity);
            infoarray.AddIfNotNull("Surface Pressure",$"{planet.SurfacePressure}*Earth", planet.SurfacePressure);
            infoarray.AddIfNotNull("Escape Velocity",$"{planet.EscapeVelocity}*Earth", planet.EscapeVelocity);

            var win = Screen.width * 0.6f;
            var w1 = win * 0.35f; var w2 = win * 0.35f;

            //find out how dispalay a table in vr World Space UI
            foreach (var item in infoarray)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(item.Key, GUILayout.Width(w1));
                GUILayout.Label(item.Value, GUILayout.Width(w2));
                GUILayout.EndHorizontal();

            }

        }

    }
}
