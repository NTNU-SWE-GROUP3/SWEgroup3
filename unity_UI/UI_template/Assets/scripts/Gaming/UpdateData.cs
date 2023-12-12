using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateData : MonoBehaviour
{
    private DontDestroy userdata;

    public void SetUserdata()
    {

    }

    void Init()
    {
        userdata = FindObjectOfType<DontDestroy>();
    }
    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("id"));
        Init();
        Debug.Log(GameController.WinOrLose);
    }

    // IEnumerator SendRequest(string playerId,string status)
    // {
    //     WWWForm form = new WWWForm();

    //     form.AddField("account_id", playerId);

    //     UnityWebRequest www = UnityWebRequest.Post(apiUrl, form);
    //     yield return www.SendWebRequest();

    //     if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
    //     {
    //         errorController.ShowErrorMessage("Please check your network connection.");
    //         // Debug.Log("failed");
    //         Debug.LogError(www.error);
    //     }
    //     else
    //     {
    //         response = www.downloadHandler.text;

    //         List<object> jsonArray = Json.Deserialize(response) as List<object>;
    //         Dictionary<string, object> check = jsonArray[0] as Dictionary<string, object>;

    //         int checkId = int.Parse(check["id"].ToString());

    //         backToLogin.check_id = checkId;
    //         Debug.Log("check_id: " + checkId);
    //         if (checkId < 0)
    //         {
    //             string message = check["note"].ToString();
    //             errorController.ShowErrorMessage(message);
    //         }
    //         else
    //         {
    //             ShowResponse(response);
    //             if (int.Parse(times) == 1)
    //             {
    //                 gachaResult1.SetActive(true);
    //                 StartCoroutine(ShowResponseAnimation1(response));
    //             }
    //             else if (int.Parse(times) == 10)
    //             {
    //                 gachaResult10.SetActive(true);
    //                 StartCoroutine(ShowResponseAnimation10(response));
    //             }
    //         }
    //     }
    // }
}
