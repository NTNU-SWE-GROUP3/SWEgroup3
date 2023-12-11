using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Panel
    private GameObject SkinPanel;
    public GameObject MaskPanel;
    private GameObject WarningPanel;
    //Skin Panel
    public Image CurrentSkinImage;
    public Button PreviousSkinButton;
    public Button NextSkinButton;
    public Text CurrentSkinTypeText;
    public Text CardName;
    public Text CardInfo;
    public Text Rarity;
    public Button ExitButton;
    public Button SellButton;
    public Button EquipButton;
    //Page1
    public Button KingButton;
    public Button QueenButton;
    public Button PrinceButton;
    public Button KnightButton;
    public Button CivillianButton;
    public Button AssassinButton;


    private static string[] skinFolders = { "Aladin", "Alice in wonderland", "Chinese chess", "Cinderella", "Frozen", "Japanese chess", "Poker", "Romet and Juliette", "Snow White" };
    private int skincount = skinFolders.Length;
    private int currentSkinIndex = 0;
    private string CurrentCharactor = "";

    private static string serverUrl = "http://127.0.0.1:5050";
    private string serverURL_equip = serverUrl + "/card_style/equip_card_style";
    private string authToken = "12345";
    // public string[,] cardStyleList = {
    //     {"id", "1"},
    //     {"account_id", "1"},
    //     {"card_style_id", "3"},
    //     {"equip_status", "0"}
    // };
    private List<DontDestroy.UserCardData> userCardStyleList;


    //WarningPanel 

    public TMP_Text Warning_Message;
    public Button Warning_ConfirmButton;

    private void Start()
    {

        SkinPanel = GameObject.Find("SkinPanel");
        SkinPanel.SetActive(false);
        //NextSkinButton.onClick.AddListener(OnNextButtonClicked);
        //PreviousSkinButton.onClick.AddListener(OnPrevButtonClicked);
        //UpdateSkinImage();
        ExitButton.onClick.AddListener(ClosePanel);
        KingButton.onClick.AddListener(ViewKingSkin);
        QueenButton.onClick.AddListener(ViewQueenSkin);
        PrinceButton.onClick.AddListener(ViewPrinceSkin);
        KnightButton.onClick.AddListener(ViewKnightSkin);
        CivillianButton.onClick.AddListener(ViewCivillianSkin);
        AssassinButton.onClick.AddListener(ViewAssassinSkin);

        //Fetching user's card data list
        GameObject dontDestroyObject = GameObject.Find("DontDestroy");

        if (dontDestroyObject != null)
        {
            // Access the DontDestroy script
            DontDestroy dontDestroyScript = dontDestroyObject.GetComponent<DontDestroy>();

            if (dontDestroyScript != null)
            {
                // Access the UserCardDataList
                userCardStyleList = dontDestroyScript.UserCardDataList;

                // Now you can use userCardList for your logic
                foreach (var userCardData in userCardList)
                {
                    Debug.Log($"CardID: {userCardData.CardID}, EquipStatus: {userCardData.EquipStatus}");
                }
            }
            else
            {
                Debug.LogError("DontDestroy script not found on the GameObject.");
            }
        }
        else
        {
            Debug.LogError("GameObject with DontDestroy script not found in the scene.");
        }
    }

    private void ClosePanel()
    {
        SkinPanel.SetActive(false);
        MaskPanel.SetActive(false);
        currentSkinIndex = 0;
        CurrentCharactor = "";
}

    private IEnumerator EquipSkinGetStatus(string targetCardStyleID)
    {
        //To be continue
        if (string.IsNullOrEmpty(authToken))
        {
            Debug.LogError("Authentication token is missing. User may not be logged in.");
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("Token", authToken); // 
        form.AddField("targetCardStyleID", targetCardStyleID);
        using (UnityWebRequest www = UnityWebRequest.Post(serverURL_equip, form))
        {
            yield return www.SendWebRequest();

            // Check for errors
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning(www.error);
                // Warning Panel
                Warning_Message.SetText("Please check your network connection");
                WarningPanel.SetActive(true);
            }
            else //equip the skin
            {
                string equipStatus = www.downloadHandler.text;
                Debug.Log("Equip Status: " + equipStatus);

                ResponseData responseData = JsonUtility.FromJson<ResponseData>(equipStatus);
                switch(responseData.status)
                {
                    case "200001":
                        Debug.Log("Equip success!");
                        Warning_Message.SetText("Equip success!");
                        WarningPanel.SetActive(true);
                        //change the skin of the card~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~·
                        break;
                    case "200021":
                        Debug.Log("User doesn't have this item");
                        // Warning Panel
                        Warning_Message.SetText("You do not have this item");;
                        WarningPanel.SetActive(true);
                        break;
                    case "403011":
                        Debug.Log("Token expired");
                        // Warning Panel
                        Warning_Message.SetText("Session time out. Please re-sign in");
                        WarningPanel.SetActive(true);
                        break;
                }
            }
        }
    }

    private void EquipSkin()
    {
        for(int i=0; i<userCardStyleList.count; i++)
        {
            //set all the equip status of the skins to 0
            UserCardData userCardData = userCardStyleList[i];

            if (userCardData.EquipStatus == "1")
            {
                // Do something with the card data, for example, send it to the backend
                SendDataToBackend(userCardData);
            }
        }
        //calculate target card style id
        string targetCardStyleID = "";
        StartCoroutine(EquipSkinGetStatus(targetCardStyleID));
    }

    private void SellSkin()
    {
        //To be continue
    }

    private void OnNextButtonClicked()
    {
        //slove bug
        PreviousSkinButton.interactable = false;
        NextSkinButton.interactable = false;

        currentSkinIndex = (currentSkinIndex + 1) % skincount;
        UpdateSkinImage();
    }

    private void OnPrevButtonClicked()
    {
        //slove bug
        PreviousSkinButton.interactable = false;
        NextSkinButton.interactable = false;

        currentSkinIndex = (currentSkinIndex - 1 + skincount) % skincount;
        UpdateSkinImage();
    }

    private void UpdateSkinImage()
    {
        string currentSkinFolder = skinFolders[currentSkinIndex];
        string path = "images/Skin/" + currentSkinFolder + "/" + CurrentCharactor;
        Sprite sprite = Resources.Load<Sprite>(path);

        if (sprite != null) { 
            CurrentSkinImage.sprite = sprite;
            Debug.Log(skinFolders[currentSkinIndex]);
            Debug.Log(path);
        }
        else
            Debug.LogError("Image not found at path: " + path);
        PreviousSkinButton.interactable = true;
        NextSkinButton.interactable = true;
    }

    private void ViewKingSkin()
    {
        SkinPanel.SetActive(true);
        CurrentCharactor = "King";
        UpdateSkinImage();
        NextSkinButton.onClick.AddListener(OnNextButtonClicked);
        PreviousSkinButton.onClick.AddListener(OnPrevButtonClicked);
        CurrentSkinTypeText.text = "國王卡牌造型";
        CardName.text = "開發中..."; //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        CardInfo.text = "開發中..."; //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        Rarity.text = "開發中..."; //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        SellButton.onClick.AddListener(SellSkin);
        EquipButton.onClick.AddListener(EquipSkin);
    }

    private void ViewQueenSkin()
    {
        SkinPanel.SetActive(true);
        CurrentCharactor = "Queen";
        UpdateSkinImage();
        NextSkinButton.onClick.AddListener(OnNextButtonClicked);
        PreviousSkinButton.onClick.AddListener(OnPrevButtonClicked);
        CurrentSkinTypeText.text = "皇后卡牌造型";
        CardName.text = "開發中..."; //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        CardInfo.text = "開發中..."; //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        Rarity.text = "開發中..."; //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        SellButton.onClick.AddListener(SellSkin);
        EquipButton.onClick.AddListener(EquipSkin);
    }

    private void ViewPrinceSkin()
    {
        SkinPanel.SetActive(true);
        CurrentCharactor = "Prince";
        UpdateSkinImage();
        NextSkinButton.onClick.AddListener(OnNextButtonClicked);
        PreviousSkinButton.onClick.AddListener(OnPrevButtonClicked);
        CurrentSkinTypeText.text = "王子卡牌造型";
        CardName.text = "開發中..."; //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        CardInfo.text = "開發中..."; //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        Rarity.text = "開發中..."; //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        SellButton.onClick.AddListener(SellSkin);
        EquipButton.onClick.AddListener(EquipSkin);
    }

    private void ViewKnightSkin()
    {
        SkinPanel.SetActive(true);
        CurrentCharactor = "Knight";
        UpdateSkinImage();
        NextSkinButton.onClick.AddListener(OnNextButtonClicked);
        PreviousSkinButton.onClick.AddListener(OnPrevButtonClicked);
        CurrentSkinTypeText.text = "騎士卡牌造型";
        CardName.text = "開發中..."; //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        CardInfo.text = "開發中..."; //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        Rarity.text = "開發中..."; //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        SellButton.onClick.AddListener(SellSkin);
        EquipButton.onClick.AddListener(EquipSkin);
    }

    private void ViewCivillianSkin()
    {
        SkinPanel.SetActive(true);
        CurrentCharactor = "Civil";
        UpdateSkinImage();
        NextSkinButton.onClick.AddListener(OnNextButtonClicked);
        PreviousSkinButton.onClick.AddListener(OnPrevButtonClicked);
        CurrentSkinTypeText.text = "平民卡牌造型";
        CardName.text = "開發中..."; //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        CardInfo.text = "開發中..."; //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        Rarity.text = "開發中..."; //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        SellButton.onClick.AddListener(SellSkin);
        EquipButton.onClick.AddListener(EquipSkin);
    }

    private void ViewAssassinSkin()
    {
        SkinPanel.SetActive(true);
        CurrentCharactor = "Killer";
        UpdateSkinImage();
        NextSkinButton.onClick.AddListener(OnNextButtonClicked);
        PreviousSkinButton.onClick.AddListener(OnPrevButtonClicked);
        CurrentSkinTypeText.text = "殺手卡牌造型";
        CardName.text = "開發中..."; //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        CardInfo.text = "開發中..."; //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        Rarity.text = "開發中..."; //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        SellButton.onClick.AddListener(SellSkin);
        EquipButton.onClick.AddListener(EquipSkin);
    }

    private void SendDataToBackend(UserCardData cardData)
    {
        StartCoroutine(SendCardDataToBackend(cardData));
    }

    private IEnumerator SendCardDataToBackend(UserCardData cardData)
    {        
        // Create a WWWForm and add data to it
        WWWForm form = new WWWForm();
        form.AddField("CardID", cardData.CardID);
        form.AddField("EquipStatus", cardData.EquipStatus);

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning(www.error);
                Debug.Log("Internet error @ SendCardDataToBackend");
            }
            else
            {
                Debug.Log("Card data sent to the backend successfully");
            }
        }
    }










}
