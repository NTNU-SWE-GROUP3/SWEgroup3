using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UpdateData : MonoBehaviour
{
    string apiUrl = "http://140.122.185.169:5050/gaming/game_finish";
    private DontDestroy userdata;
    int playerId;
    string response;

    public void SetUserdata()
    {
        Debug.Log("Status: " + GameController.WinOrLose);
        Debug.Log("PlayerId: " + playerId);
        StartCoroutine(SendRequest(playerId.ToString(), GameController.WinOrLose));
    }

    void Init()
    {
        userdata = FindObjectOfType<DontDestroy>();
        playerId = PlayerPrefs.GetInt("id");
    }
    void Start()
    {
        Init();
    }

    IEnumerator SendRequest(string playerId, string status)
    {
        WWWForm form = new WWWForm();

        form.AddField("account_id", playerId);
        form.AddField("end_status", status);

        UnityWebRequest www = UnityWebRequest.Post(apiUrl, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            response = www.downloadHandler.text;
            Debug.Log(response);
        }
    }
}
