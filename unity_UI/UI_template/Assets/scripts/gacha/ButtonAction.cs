using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ButtonAction : MonoBehaviour
{
    [SerializeField] string apiUrl = "http://127.0.0.1:5000/gacha/draw";       // call API endpoint

    // default playerId = 1, mode = coin, times = 1
    [SerializeField] string playerId = "1";
    [SerializeField] string mode = "coin";
    private GotchaPanel gotchaPanel;
    public GameObject messagePanel;
    public GameObject mask;
    public Button yesButton;
    public Button noButton;
    bool yesClicked = false;
    bool noClicked = false;

    void Start()
    {
        messagePanel.SetActive(false);
        mask.SetActive(false);
        yesClicked = false;
        noClicked = false;
    }
    void Awake()
    {
        gotchaPanel = GetComponentInChildren<GotchaPanel>();
    }

    public IEnumerator CashModeDraw(string times)
    {
        messagePanel.SetActive(true);   // Show confirmation dialog
        mask.SetActive(true);
        yesClicked = false;
        noClicked = false;

        while (!yesClicked && !noClicked)
        {
            yesButton.onClick.AddListener(() => OnYesButtonClick());
            noButton.onClick.AddListener(() => OnNoButtonClick());
            yield return null;
        }

        if (yesClicked)
        {
            Debug.Log("Yes, Start Drawing");
            mode = "cash";
            StartCoroutine(SendRequest(playerId, mode, times));
        }
        else if (noClicked)
        {
            Debug.Log("noClicked");
        }

        messagePanel.SetActive(false);   // Hide confirmation dialog
        yesClicked = false;
        noClicked = false;
    }

    void OnYesButtonClick()
    {
        yesClicked = true;
        noClicked = false;
        messagePanel.SetActive(false);
        mask.SetActive(false);
    }
    void OnNoButtonClick()
    {
        yesClicked = false;
        noClicked = true;
        messagePanel.SetActive(false);
        mask.SetActive(false);
    }

    public void DrawButton(bool isSingleDraw)
    {
        string times = isSingleDraw ? "1" : "5";

        switch (gotchaPanel.currentPage)
        {
            case 1:
                mode = "coin";
                StartCoroutine(SendRequest(playerId, mode, times));
                break;
            case 2:
                StartCoroutine(CashModeDraw(times));
                break;
            default:
                break;

        };

        Debug.Log(isSingleDraw ? "Single" : "Mult");
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