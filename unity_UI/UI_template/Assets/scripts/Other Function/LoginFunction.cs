using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginButtonLogMessage : MonoBehaviour
{
    public Button login_btn;
    public string serverURL = "http://127.0.0.1:5000/login";

    public TMP_InputField accountInput;
    public TMP_InputField passwordInput;

    private void Start()
    {
        login_btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        // 获取 EnterAccount 文本框的值
        

        string account = accountInput.text;
        Debug.Log("account = " + account);


        // 获取 EnterEmail 文本框的值
        string password = passwordInput.text;
        Debug.Log("password = " + password);


        StartCoroutine(SendPostRequest(account, password));
    }

    private IEnumerator SendPostRequest(string account, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("Account", account); // 使用 "Account" 作为键
        form.AddField("Password", password); // 使用 "Email" 作为键

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning(www.error);
                // 处理错误情况
            }
            else
            {
                Debug.Log("POST request successful");
                
                string responseText = www.downloadHandler.text;
                Debug.Log("Server Response: " + responseText);
            }
        }
    }
}
