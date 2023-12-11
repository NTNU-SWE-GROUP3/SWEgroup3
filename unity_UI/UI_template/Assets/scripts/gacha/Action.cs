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
    [SerializeField] string apiUrl = "http://127.0.0.1:5050/gacha/draw";       // call API endpoint

    // default playerId = 1, mode = coin, times = 1
    [SerializeField] string playerId = "1";
    [SerializeField] string mode = "coin";
    [SerializeField] GotchaPanel gotchaPanel;
    [SerializeField] GameObject messagePanel;
    [SerializeField] GameObject resultPanel;
    [SerializeField] GameObject purchasePanel;
    [SerializeField] GameObject duplicatePanel;
    [SerializeField] GameObject duplicatePanelTexts;
    [SerializeField] GameObject mask;
    [SerializeField] GameObject okButton1;
    [SerializeField] GameObject okButton10;
    [SerializeField] GameObject gachaResult1;
    [SerializeField] GameObject gachaResult10;
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;
    [SerializeField] Button buyButton;
    [SerializeField] Button cancelButton;
    [SerializeField] Button OkButton;
    public Animator gachaAnimator1;
    public Animator gachaAnimator10;
    public AnimationController animationController;
    public PurchaseController purchaseController;
    public ErrorMessage errorController;
    public ImageManager imageManager;
    public BackToLogin backToLogin;
    public bool yesClicked = false;
    public bool noClicked = false;
    public bool buyClicked = false;
    public bool cancelClicked = false;
    public bool okButtonClicked = false;
    public bool duplicate = false;

    private string response;
    private DontDestroy userdata;


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
        Debug.Log("Token value in Action:" + userdata.token);
    }

    void PanelInit()
    {
        messagePanel.SetActive(false);
        resultPanel.SetActive(false);
        purchasePanel.SetActive(false);
        duplicatePanel.SetActive(false);
        mask.SetActive(false);
    }

    void FlagInit()
    {
        yesClicked = false;
        noClicked = false;
        buyClicked = false;
        cancelClicked = false;
        okButtonClicked = false;
    }

    void OthersInit()
    {
        gachaResult1.SetActive(false);
        gachaResult10.SetActive(false);
        okButton1.SetActive(false);
        okButton10.SetActive(false);
    }

    void PlayerInfoInit()
    {
        userdata = FindObjectOfType<DontDestroy>();
    }

    void Init()
    {
        PanelInit();
        FlagInit();
        OthersInit();
        PlayerInfoInit();
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
            if (!purchaseController.CardNumberCheck())
            {
                purchaseController.DisplayMessage("Please enter a valid card number.");
                Debug.Log("Invalid card number.");
                return;
            }
            buyClicked = true;
            cancelClicked = false;
            purchasePanel.SetActive(false);
        }
        else
        {
            purchaseController.DisplayMessage("Please fill in all the required fields");
            Debug.Log("There are some empty fields.");
            return;
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
    public void OnOKButtonClick()
    {
        okButtonClicked = true;
        duplicate = false;
        // mask.SetActive(false);
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
    IEnumerator SendRequest(string tokenId, string mode, string times)
    {
        WWWForm form = new WWWForm();

        form.AddField("mode", mode);
        form.AddField("token_id", "token456");
        form.AddField("times", times);

        UnityWebRequest www = UnityWebRequest.Post(apiUrl, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            errorController.ShowErrorMessage("Please check your network connection.");
            // Debug.Log("failed");
            Debug.LogError(www.error);
        }
        else
        {
            response = www.downloadHandler.text;

            List<object> jsonArray = Json.Deserialize(response) as List<object>;
            Dictionary<string, object> check = jsonArray[0] as Dictionary<string, object>;

            int checkId = int.Parse(check["id"].ToString());
            backToLogin.check_id = checkId;
            if (checkId < 0)
            {
                string message = check["note"].ToString();
                errorController.ShowErrorMessage(message);
            }
            else
            {
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
            }
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
            Dictionary<string, object> check = jsonArray[0] as Dictionary<string, object>;
            Text[] duplicateTexts = duplicatePanelTexts.GetComponentsInChildren<Text>();
            foreach (Text textElement in duplicateTexts)
            {
                textElement.text = string.Empty;
            }
            // Debug.LogWarning(duplicateTexts[0].text);
            int index = 0;

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
                    if (note == "-1")
                    {
                        duplicate = true;
                        if (type == "skill")
                        {
                            duplicateTexts[index].text = imageManager.GetSkillName(int.Parse(id));
                        }
                        else if (type == "card_style")
                        {
                            duplicateTexts[index].text = imageManager.GetCardStyleName(int.Parse(id));
                        }
                        index++;
                    }
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
    public void ShowDuplicates()
    {
        if (duplicate)
        {
            duplicatePanel.SetActive(true);
            if (mask.activeSelf)
            {
                Debug.Log("Mask is already active.");
            }
            else
            {
                mask.SetActive(true);
            }
        }
        else
        {
            mask.SetActive(false);
        }
    }


}
