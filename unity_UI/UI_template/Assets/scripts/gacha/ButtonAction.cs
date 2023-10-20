using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;



public class ButtonAction : MonoBehaviour
{
    // call api
    [SerializeField] string apiUrl = "http://127.0.0.1:5000/gacha/draw";
    
    // default playerId = 1, mode = coin, times = 1
    [SerializeField] string playerId = "1";
    [SerializeField] string mode = "coin";
    [SerializeField] string times = "1";

    public void SingleDrawButton()
    {
        StartCoroutine(SendRequest(playerId, mode, times));
        Debug.Log("Single");
    }

    IEnumerator SendRequest(string playerId, string mode, string times)
    {
        WWWForm form = new WWWForm();

        form.AddField("mode", mode);
        form.AddField("account_id", playerId);
        form.AddField("times", times);

        UnityWebRequest www = UnityWebRequest.Post(apiUrl, form);
        // UnityWebRequest www = UnityWebRequest.Get(apiUrl);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("failed");
            Debug.LogError(www.error);
        }
        else
        {
            string response = www.downloadHandler.text;
            Debug.Log("API Response: " + response);
            // 在这里处理API的响应，可以根据需要执行其他操作
        }
    }


}