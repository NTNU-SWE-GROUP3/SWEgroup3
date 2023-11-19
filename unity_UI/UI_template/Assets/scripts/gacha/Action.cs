using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using MiniJSON;
using ResultAnimation;
using PurchaseControl;

public class Action : MonoBehaviour
{
    [SerializeField] string apiUrl = "http://127.0.0.1:5000/gacha/draw";       // call API endpoint

    // default playerId = 1, mode = coin, times = 1
    [SerializeField] string playerId = "1";
    [SerializeField] string mode = "coin";
    [SerializeField] GotchaPanel gotchaPanel;
    [SerializeField] GameObject messagePanel;
    [SerializeField] GameObject resultPanel;
    [SerializeField] GameObject purchasePanel;
    [SerializeField] GameObject mask;
    [SerializeField] GameObject okButton1;
    [SerializeField] GameObject okButton10;
    [SerializeField] GameObject gachaResult1;
    [SerializeField] GameObject gachaResult10;
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;
    [SerializeField] Button buyButton;
    [SerializeField] Button cancelButton;
    public Animator gachaAnimator1;
    public Animator gachaAnimator10;
    public AnimationController animationController;
    public PurchaseController purchaseController;
    public ErrorMessage errorController;
    public bool yesClicked = false;
    public bool noClicked = false;
    public bool buyClicked = false;
    public bool cancelClicked = false;
    public string response;

    [System.Serializable]
    public class apiResponse
    {
        public string id;
        public string type;
        public string note;
    }

    void Awake()
    {
        Init();
    }

    void Init()
    {
        messagePanel.SetActive(false);
        resultPanel.SetActive(false);
        mask.SetActive(false);
        purchasePanel.SetActive(false);
        gachaResult1.SetActive(false);
        gachaResult10.SetActive(false);
        okButton1.SetActive(false);
        okButton10.SetActive(false);
        yesClicked = false;
        noClicked = false;
        buyClicked = false;
        cancelClicked = false;
    }

    public IEnumerator ExecuteDraw(string times, string mode)
    {
        messagePanel.SetActive(true);   // Show confirmation dialog
        mask.SetActive(true);
        yesClicked = false;
        noClicked = false;

        yesButton.onClick.AddListener(() => OnYesButtonClick());
        noButton.onClick.AddListener(() => OnNoButtonClick());
        while (!yesClicked && !noClicked)
        {
            yield return null;
        }
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        if (yesClicked)
        {
            if (mode == "cash")
            {
                purchaseController.OpenPurchasePanel();

                buyButton.onClick.AddListener(() => OnBuyButtonClick());
                cancelButton.onClick.AddListener(() => OnCancelButtonClick());
                while (!buyClicked && !cancelClicked)
                {
                    yield return null;
                }
                buyButton.onClick.RemoveAllListeners();
                cancelButton.onClick.RemoveAllListeners();

                if (buyClicked)
                {
                    Debug.Log("Get info, start drawing...");
                    StartCoroutine(SendRequest(playerId, mode, times));
                }
                else if (cancelClicked)
                {
                    Debug.Log("Canceled");
                }
            }
            else if (mode == "coin")
            {
                Debug.Log("Yes, Start Drawing");
                StartCoroutine(SendRequest(playerId, mode, times));
            }
        }
        else if (noClicked)
        {
            Debug.Log("noClicked");
        }

        messagePanel.SetActive(false);   // Hide confirmation dialog
        yesClicked = false;
        noClicked = false;
    }

    bool InputChecker()
    {
        Debug.Log("Check input");
        if (purchasePanel != null)
        {
            // Get all InputFields under the purchasePanel
            InputField[] inputFields = purchasePanel.GetComponentsInChildren<InputField>();

            // Iterate through each InputField
            foreach (InputField inputField in inputFields)
            {
                if (string.IsNullOrEmpty(inputField.text))
                {
                    return false;
                }
            }

            return true;
        }
        else
        {
            Debug.LogError("purchasePanel is null. Please assign a valid GameObject reference.");
            return false;
        }
    }

    void OnBuyButtonClick()
    {
        if (InputChecker())
        {
            // if (!purchaseController.CardNumberCheck()){
            //     purchaseController.DisplayMessage("Please enter a valid card number.");
            //     Debug.Log("Invalid card number.");
            //     return ;
            // }
            buyClicked = true;
            cancelClicked = false;
            purchasePanel.SetActive(false);
        }
        else
        {
            purchaseController.DisplayMessage("Please fill in all the required fields");
            Debug.Log("There are some empty fields.");
            return ;
        }


    }

    void OnCancelButtonClick()
    {
        cancelClicked = true;
        buyClicked = false;
        purchasePanel.SetActive(false);
    }

    void OnYesButtonClick()
    {
        yesClicked = true;
        noClicked = false;
        messagePanel.SetActive(false);
        resultPanel.SetActive(false);
        // mask.SetActive(false);
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
        string times = isSingleDraw ? "1" : "10";

        switch (gotchaPanel.currentPage)
        {
            case 1:
                mode = "coin";
                Debug.Log("Coin Mode");
                StartCoroutine(ExecuteDraw(times, mode));
                break;
            case 2:
                mode = "cash";
                Debug.Log("Cash Mode");
                StartCoroutine(ExecuteDraw(times, mode));
                break;
            default:
                Debug.Log("Failed to get mode.");
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
            errorController.ShowErrorMessage("Please check your network connection.");
            Debug.Log("failed");
            Debug.LogError(www.error);
        }
        else
        {
            response = www.downloadHandler.text;

            ShowResponse(response);
            if (int.Parse(times) == 1)
            {
                gachaResult1.SetActive(true);
                StartCoroutine(ShowResponseAnimation1(response));
            }
            else if (int.Parse(times) == 10)
            {
                gachaResult10.SetActive(true);
                StartCoroutine(ShowResponseAnimation10(response));
            }
            // Debug.Log("API Response: " + response);
        }
    }

    IEnumerator ShowResponseAnimation1(string response)
    {
        gachaAnimator1.SetTrigger("ShowAnimate");
        yield return new WaitForSecondsRealtime(gachaAnimator1.GetCurrentAnimatorStateInfo(0).length);

        okButton1.SetActive(true);
    }
    IEnumerator ShowResponseAnimation10(string response)
    {
        gachaAnimator10.SetTrigger("ShowAnimate");
        yield return new WaitForSecondsRealtime(gachaAnimator10.GetCurrentAnimatorStateInfo(0).length * 8);

        okButton10.SetActive(true);
    }

    void ShowResponse(string response)
    {
        resultPanel.SetActive(true);

        List<object> jsonArray = Json.Deserialize(response) as List<object>;

        if (jsonArray != null)
        {
            animationController.DisplayCardResults(jsonArray);
            foreach (var item in jsonArray)
            {
                // Check if each item is a dictionary
                Dictionary<string, object> dict = item as Dictionary<string, object>;
                if (dict != null)
                {
                    // Access values by key
                    string id = dict["id"].ToString();
                    string type = dict["type"].ToString();
                    string note = dict["note"].ToString();

                    Debug.Log("ID: " + id + ", Type: " + type + ", Note: " + note);
                }
            }
        }
        else
        {
            Debug.LogError("Failed to parse JSON array.");
        }
        // Debug.Log(response);
    }


}
