using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ButtonLogMessage : MonoBehaviour
{
    public Button btn;
    public string serverURL = "http://127.0.0.1:5000/test"; // 本地端flask伺服器地址 // 先用本地端處理

    private void Start()
    {
        btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        StartCoroutine(SendPostRequest());
    }

    private IEnumerator SendPostRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("key", "value"); // 傳遞數據示範

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
                //做到這裡給設計做跳轉就好了 （沒有成功的情況）
            }
            else
            {
                Debug.Log("POST request successful");
                // 做到這裡給設計做跳轉就好了（有收到訊息，在對應的條件像是成功/失敗時加上註解，接下來設計會接手）
            }
        }
    }

}
