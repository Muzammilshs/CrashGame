using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class GetJson : MonoBehaviour
{
    #region Creating Instance
    private static GetJson _instance;
    public static GetJson instance
    {
        get
        {
            if (_instance == null)
                _instance = FindFirstObjectByType<GetJson>();
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
            _instance = this;

    }
    #endregion

    public void GetJsonFromServer(string apiURL, Action<string, bool> methodCall)
    {
        StartCoroutine(GetJsonFromServerUsingAPI(apiURL, methodCall));
    }
    IEnumerator GetJsonFromServerUsingAPI(string apiURL, Action<string, bool> methodCall)
    {
        apiURL = APIStrings.getDelayTimeBetweenRoundsAPIURL;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiURL))
        {
            webRequest.timeout = 15; // Optional: Set a timeout for the request
            yield return webRequest.SendWebRequest(); // Send the web request

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                // Get the JSON response
                string jsonResponse = webRequest.downloadHandler.text;
                Debug.Log("JSON Response: " + jsonResponse);
                methodCall?.Invoke(jsonResponse, true);

            }
            else
            {
                // Log the error
                Debug.LogError("Error fetching data: " + webRequest.error);
                methodCall?.Invoke(webRequest.downloadHandler.text, false);
            }
        }
    }
}
