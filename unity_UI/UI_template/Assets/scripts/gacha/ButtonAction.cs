using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public enum GatchaMode {
    coin,
    cash
}

public class ButtonAction : MonoBehaviour
{
    // call api
    [SerializeField] string apiUrl = "http://127.0.0.1:5000/gacha/draw";
    
    // default playerId = 1, mode = coin, times = 1
    [SerializeField] string playerId = "1";
    [SerializeField] string mode = "coin";
    // [SerializeField] GatchaMode mode = GatchaMode.coin;

    private GotchaPanel gotchaPanel;

    void Awake()
    {
        gotchaPanel = GetComponentInChildren<GotchaPanel>();
    }

    public void DrawButtonSingle()
    {
        switch (gotchaPanel.currentPage)
        {
            case 1:
                mode = "coin";
                break;
            case 2:
                mode = "cash";
                break;

            default:
                break;
        };
        StartCoroutine(SendRequest(playerId, mode, "1"));   
        Debug.Log("Single");
    }

    public void DrawButtonMult()
    {
        StartCoroutine(SendRequest(playerId, mode, "5"));
        Debug.Log("Mult");
    }

    IEnumerator SendRequest(string playerId, string mode, string times)
    {
        WWWForm form = new WWWForm();

        form.AddField("mode", mode);
        form.AddField("account_id", playerId);
        form.AddField("times", times);

        UnityWebRequest www = UnityWebRequest.Post(apiUrl, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("failed");
            Debug.LogError(www.error);
        }
        else
        {
            string response = www.downloadHandler.text;
            
            ShowResponse(response);
            // Debug.Log("API Response: " + response);

        }
    }

    void ShowResponse(string response)
    {
        Debug.Log(response);
    }

}