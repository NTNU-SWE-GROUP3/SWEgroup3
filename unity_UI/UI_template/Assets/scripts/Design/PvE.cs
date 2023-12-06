using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using UnityEngine.Networking;
using MiniJSON;
using UnityEngine.SceneManagement;

public class PvE : MonoBehaviour
{
    string tokenId = "token123";
    string apiUrl = "http://140.122.185.169:5050/gaming/get_player_info";
    string response = "";
    void Start()
    {

    }

    public void PvEButton()
    {
        // get token id from login first
        StartCoroutine(SendRequest(tokenId));
    }

    IEnumerator SendRequest(string tokenId)
    {
        Debug.Log("SendRequest");
        WWWForm form = new WWWForm();

        form.AddField("token_id", tokenId);

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
            Dictionary<string, object> jsonDict = Json.Deserialize(response) as Dictionary<string, object>;
            if (jsonDict != null)
            {
                int id = int.Parse(jsonDict["account_id"].ToString());
                int coin = int.Parse(jsonDict["coin"].ToString());
                int exp = int.Parse(jsonDict["experience"].ToString());
                int level = int.Parse(jsonDict["level"].ToString());
                string nick_name = jsonDict["nickname"].ToString();
                string rank = jsonDict["rank"].ToString();
                int ranked_XP = int.Parse(jsonDict["ranked_XP"].ToString());
                int win_streak = int.Parse(jsonDict["ranked_winning_streak"].ToString());
                int total_match = int.Parse(jsonDict["total_match"].ToString());
                int total_win = int.Parse(jsonDict["total_win"].ToString());
                SceneManager.LoadScene(2);
            }
            else
            {
                Debug.LogError("Failed to parse JSON data.");
            }
        }
    }
}
