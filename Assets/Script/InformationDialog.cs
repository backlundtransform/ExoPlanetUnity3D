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


            var infoarray = new List<string>();
            infoarray.AddIfNotNull($"Radius {planet.RadiusEu}*Earth", planet.RadiusEu);
            infoarray.AddIfNotNull($"Mass {planet.Mass}*Earth", planet.Mass);
            infoarray.AddIfNotNull($"Gravity {planet.Gravity}*Earth", planet.Gravity);
            infoarray.AddIfNotNull($"Surface Pressure {planet.SurfacePressure}*Earth", planet.SurfacePressure);
            infoarray.AddIfNotNull($"Escape Velocity {planet.EscapeVelocity}*Earth", planet.EscapeVelocity);



            for (var i = 0; i < infoarray.Count - 1; i++)
            {

                var m_Text = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();

                m_TextContainer = GetComponent<TextContainer>();

                m_Text.fontSize = 8;
                m_Text.transform.localPosition = new Vector3(i, 0, 8);

                m_Text.text = infoarray[i];
            }

        }

    }
}