using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class LoginInfo
{
    public string status;
    public string tokenId;
    public static LoginInfo CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<LoginInfo>(jsonString);
    }
}


public class login : MonoBehaviour
{
    private const string apiUri = "http://localhost:80/api/login";

    public TMP_InputField EnterAccount;
    public TMP_InputField EnterPassword;

    public void OnClick()
    {
        StartCoroutine("OnSend", apiUri);
    }

    IEnumerator OnSend(string uri)
    {
        WWWForm form = new();
        form.AddField("account", EnterAccount.textComponent.text);
        form.AddField("password", EnterPassword.textComponent.text);

        UnityWebRequest webRequest = UnityWebRequest.Post(uri, form);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        yield return webRequest.SendWebRequest();

        string[] pages = uri.Split('/');
        int page = pages.Length - 1;

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                LoginInfo loginInfo = LoginInfo.CreateFromJSON(webRequest.downloadHandler.text);
                if(loginInfo.status == "success")
                {
                    PlayerPrefs.SetString("tokenId", loginInfo.tokenId);
                }
                else if(loginInfo.status == "failure")
                {

                }
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
