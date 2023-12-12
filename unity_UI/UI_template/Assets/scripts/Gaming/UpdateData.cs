using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class UpdateData : MonoBehaviour
{
    private DontDestroy userdata;
    int playerId = 0;
    string apiUrl = "http://140.122.185.169:5050/gaming/game_finish";
    string response = "";


    public void UpdateUserData()
    {
        Debug.Log("WinOrLose: " + GameController.WinOrLose);
        // StartCoroutine(SendRequest());
    }
    public IEnumerator SendRequest()
    {
        WWWForm form = new WWWForm();

        form.AddField("account_id",  playerId.ToString());
        form.AddField("end_status", GameController.WinOrLose);

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

    void Init()
    {
        userdata = FindObjectOfType<DontDestroy>();
        playerId = PlayerPrefs.GetInt("id");
        Debug.Log(playerId);
    }
    void Start()
    {
        Init();
        // UpdateUserData();
    }
}
