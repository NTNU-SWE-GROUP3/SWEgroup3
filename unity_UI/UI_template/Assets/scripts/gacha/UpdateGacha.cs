using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using MiniJSON;
using UnityEngine;

public class UpdateGacha : MonoBehaviour
{

    public static string serverUrl = "http://140.122.185.169:5050";
    public string serverURL_playerdata = serverUrl + "/user_information/getplayerdata";

    private DontDestroy userdata;
    public SkillCardDisplay skillCardDisplay;
    public GameObject UserSettingWarningPanel;
    public Text UseSettingMessage;
    public Text NoticeTitleText;
    public Text UserCoinsDisplay;

    void Awake()
    {
        userdata = FindObjectOfType<DontDestroy>();
    }

    public void UpdateUserGeneralData()
    {
        StartCoroutine(PlayerDataRequest(userdata.token));
        Debug.Log("Try to Update User General Data");
    }

    public void UpdateUserBackpack()
    {
        StartCoroutine(skillCardDisplay.DisplaySkillStyle());
        Debug.Log("Try to Update User Backpack");
    }
    private IEnumerator PlayerDataRequest(string player_token)
    {
        WWWForm form = new WWWForm();
        form.AddField("Token", userdata.token); // 

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL_playerdata, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning(www.error);

                // Warning Panel
                UserSettingWarningPanel.SetActive(true);
                NoticeTitleText.text = ("網路連接錯誤");
                UseSettingMessage.text = "Please check your internet connection";
                Debug.Log("Internet error");

            }
            else
            {

                string responseText = www.downloadHandler.text;
                UserInformationResponseData responseData = JsonUtility.FromJson<UserInformationResponseData>(responseText);
                switch (responseData.status)
                {
                    case "400004":
                        Debug.Log("Get player data successfully");
                        UserCoinsDisplay.text = responseData.coin.ToString();

                        break;
                    case "403011":
                        break;

                }
            }
        }
    }
}
