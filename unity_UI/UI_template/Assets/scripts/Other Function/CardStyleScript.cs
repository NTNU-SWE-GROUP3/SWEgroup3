using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text;

public class UIManager : MonoBehaviour
{
    //Panel
    private GameObject SkinPanel;
    public GameObject MaskPanel;
    public GameObject WarningPanel;
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

    // private static string serverUrl = "http://127.0.0.1:5050";
    private static string serverUrl = "http://140.122.185.169:5050";
    private string serverURL_equip = serverUrl + "/card_style/equip_card_style";
    // private string serverURL_find_card_style = serverUrl + "/card_style/"
    private string authToken = "";
    // private string authToken = "token123";

    //WarningPanel

    public Text Warning_Title;
    public Text Warning_Message;
    public Button Warning_ConfirmButton;

    private DontDestroy userdata;


    //card style
    string [][] cardStyleList = new string[][]
    {
        new string [] {"1", "Civilian (Frozen)", "Dress up your civilian card with Frozen style!", "Regular"},
        new string [] {"2", "Assassin (Frozen)", "Dress up your Assassin card with Frozen style!", "Regular"},
        new string [] {"3", "King (Frozen)", "Dress up your King card with Frozen style!", "Regular"},
        new string [] {"4", "Knight (Frozen)", "Dress up your Knight card with Frozen style!", "Regular"},
        new string [] {"5", "Prince (Frozen)", "Dress up your Prince card with Frozen style!", "Regular"},
        new string [] {"6", "Queen (Frozen)", "Dress up your Queen card with Frozen style!", "Regular"},
        new string [] {"7", "Civilian (Aladin)", "Dress up your civilian card with Aladin style!", "Regular"},
        new string [] {"8", "Assassin (Aladin)", "Dress up your Assassin card with Aladin style!", "Regular"},
        new string [] {"9", "King (Aladin)", "Dress up your King card with Aladin style!", "Regular"},
        new string [] {"10", "Knight (Aladin)", "Dress up your Knight card with Aladin style!", "Regular"},
        new string [] {"11", "Prince (Aladin)", "Dress up your Prince card with Aladin style!", "Regular"},
        new string [] {"12", "Queen (Aladin)", "Dress up your Queen card with Aladin style!", "Regular"},
        new string [] {"13", "Civilian (Alice In Wonderland)", "Dress up your civilian card with Alice In Wonderland style!", "Regular"},
        new string [] {"14", "Assassin (Alice In Wonderland)", "Dress up your Assassin card with Alice In Wonderland style!", "Regular"},
        new string [] {"15", "King (Alice In Wonderland)", "Dress up your King card with Alice In Wonderland style!", "Regular"},
        new string [] {"16", "Knight (Alice In Wonderland)", "Dress up your Knight card with Alice In Wonderland style!", "Regular"},
        new string [] {"17", "Prince (Alice In Wonderland)", "Dress up your Prince card with Alice In Wonderland style!", "Regular"},
        new string [] {"18", "Queen (Alice In Wonderland)", "Dress up your Queen card with Alice In Wonderland style!", "Regular"},
        new string [] {"19", "Civilian (Cinderella)", "Dress up your civilian card with Cinderella style!", "Regular"},
        new string [] {"20", "Assassin (Cinderella)", "Dress up your Assassin card with Cinderella style!", "Regular"},
        new string [] {"21", "King (Cinderella)", "Dress up your King card with Cinderella style!", "Regular"},
        new string [] {"22", "Knight (Cinderella)", "Dress up your Knight card with Cinderella style!", "Regular"},
        new string [] {"23", "Prince (Cinderella)", "Dress up your Prince card with Cinderella style!", "Regular"},
        new string [] {"24", "Queen (Cinderella)", "Dress up your Queen card with Cinderella style!", "Regular"},
        new string [] {"25", "Civilian (Romeo and Julliette)", "Dress up your civilian card with Romeo and Julliette style!", "Regular"},
        new string [] {"26", "Assassin (Romeo and Julliette)", "Dress up your Assassin card with Romeo and Julliette style!", "Regular"},
        new string [] {"27", "King (Romeo and Julliette)", "Dress up your King card with Romeo and Julliette style!", "Regular"},
        new string [] {"28", "Knight (Romeo and Julliette)", "Dress up your Knight card with Romeo and Julliette style!", "Regular"},
        new string [] {"29", "Prince (Romeo and Julliette)", "Dress up your Prince card with Romeo and Julliette style!", "Regular"},
        new string [] {"30", "Queen (Romeo and Julliette)", "Dress up your Queen card with Romeo and Julliette style!", "Regular"},
        new string [] {"31", "Civilian (Poker)", "Dress up your civilian card with Poker style!", "Regular"},
        new string [] {"32", "Assassin (Poker)", "Dress up your Assassin card with Poker style!", "Regular"},
        new string [] {"33", "King (Poker)", "Dress up your King card with Poker style!", "Regular"},
        new string [] {"34", "Knight (Poker)", "Dress up your Knight card with Poker style!", "Regular"},
        new string [] {"35", "Prince (Poker)", "Dress up your Prince card with Poker style!", "Regular"},
        new string [] {"36", "Queen (Poker)", "Dress up your Queen card with Poker style!", "Regular"},
        new string [] {"37", "Civilian (Chinese Chess)", "Dress up your civilian card with Chinese Chess style!", "Regular"},
        new string [] {"38", "Assassin (Chinese Chess)", "Dress up your Assassin card with Chinese Chess style!", "Regular"},
        new string [] {"39", "King (Chinese Chess)", "Dress up your King card with Chinese Chess style!", "Regular"},
        new string [] {"40", "Knight (Chinese Chess)", "Dress up your Knight card with Chinese Chess style!", "Regular"},
        new string [] {"41", "Prince (Chinese Chess)", "Dress up your Prince card with Chinese Chess style!", "Regular"},
        new string [] {"42", "Queen (Chinese Chess)", "Dress up your Queen card with Chinese Chess style!", "Regular"},
        new string [] {"43", "Civilian (Japanese Chess)", "Dress up your civilian card with Japanese Chess style!", "Regular"},
        new string [] {"44", "Assassin (Japanese Chess)", "Dress up your Assassin card with Japanese Chess style!", "Regular"},
        new string [] {"45", "King (Japanese Chess)", "Dress up your King card with Japanese Chess style!", "Regular"},
        new string [] {"46", "Knight (Japanese Chess)", "Dress up your Knight card with Japanese Chess style!", "Regular"},
        new string [] {"47", "Prince (Japanese Chess)", "Dress up your Prince card with Japanese Chess style!", "Regular"},
        new string [] {"48", "Queen (Japanese Chess)", "Dress up your Queen card with Japanese Chess style!", "Regular"},
        new string [] {"49", "Civilian (Snow White)", "Dress up your civilian card with Snow White style!", "Regular"},
        new string [] {"50", "Assassin (Snow White)", "Dress up your Assassin card with Snow White style!", "Regular"},
        new string [] {"51", "King (Snow White)", "Dress up your King card with Snow White style!", "Regular"},
        new string [] {"52", "Knight (Snow White)", "Dress up your Knight card with Snow White style!", "Regular"},
        new string [] {"53", "Prince (Snow White)", "Dress up your Prince card with Snow White style!", "Regular"},
        new string [] {"54", "Queen (Snow White)", "Dress up your Queen card with Snow White style!", "Regular"},
    };

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

        //Fetching user's token
        userdata = FindObjectOfType<DontDestroy>();
        // DontDestroy userdata = FindObjectOfType<DontDestroy>();
        if(userdata != null)
        {
            authToken = userdata.token;
            Debug.Log("Token value: " + authToken);
            // StartCoroutine(logdata(authToken));
        }
        else
        {
            Debug.LogError("DontDestroy script not found!");
        }
    }

    private void ClosePanel()
    {
        SkinPanel.SetActive(false);
        MaskPanel.SetActive(false);
        currentSkinIndex = 0;
        CurrentCharactor = "";
    }

    private IEnumerator logdata(int targetCardStyleId)
    {
        // Debug.log("log data target card style id: ", targetCardStyleId);
        yield return StartCoroutine(userdata.Init_Card_Skill_Account_Data(authToken));

        CardName.text = userdata.characterDataList[targetCardStyleId].CardName;
        CardInfo.text = userdata.characterDataList[targetCardStyleId].CardDescription;
        Rarity.text = userdata.characterDataList[targetCardStyleId].CardProbability;
        // Debug.log("Character description: " + cardDescription);
    }

    private IEnumerator EquipSkinGetStatus(string targetCardStyleID, string targetCharacterType)
    {
        Debug.Log("EquipSkinGetStatus started");

        if (string.IsNullOrEmpty(authToken))
        {
            Debug.LogError("Authentication token is missing. User may not be logged in.");
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("tokenId", authToken); // 
        form.AddField("targetCardStyleId", targetCardStyleID);
        form.AddField("targetCharacterType", targetCharacterType);
        // Debug.Log("Form Contents: " + FormContentsToString(form));
        Debug.Log("Token: " + authToken);
        Debug.Log("targetCardStyleID: " + targetCardStyleID);
        Debug.Log("targetCharacterType: " + targetCharacterType);

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL_equip, form))
        {
            yield return www.SendWebRequest();

            Debug.Log("Response: " + www.downloadHandler.text);

            // Check for errors
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning(www.error);
                // Warning Panel
                Warning_Title.text = "Attention!";
                Warning_Message.text = "Please check your network connection";
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
                        Warning_Title.text = "Congratulations!";
                        Warning_Message.text = "Equip success!";
                        WarningPanel.SetActive(true);
                        break;
                    case "200021":
                        Debug.Log("Equip failure");
                        // Warning Panel
                        Warning_Title.text = "Sorry!";
                        Warning_Message.text = "You have failed to equip this item";
                        WarningPanel.SetActive(true);
                        break;
                    case "200022":
                        Debug.Log("Item doesn't exist in inventory");
                        // Warning Panel
                        Warning_Title.text = "Oops!";
                        Warning_Message.text = "It seems like you do not have this item in inventory";
                        WarningPanel.SetActive(true);
                        break;
                }
            }
        }
    }

    private int getTargetCardStyleId()
    {
        int idCount = 0;
        switch(CurrentCharactor)
        {
            case "King":
                idCount = 2;
                break;
            case "Queen":
                idCount = 5;
                break;
            case "Prince":
                idCount = 4;
                break;
            case "Knight":
                idCount = 3;
                break;
            case "Civil":
                idCount = 0;
                break;
            case "Killer":
                idCount = 1;
                break;
        }
        string targetCharacterType = (idCount+1).ToString();
        Debug.Log($"targetCharacterType_input: {targetCharacterType}");
        switch(currentSkinIndex)
        {
            case 0: //Aladin
                idCount += 7;
                break;
            case 1: //Alice in wonderland
                idCount += 13;
                break;
            case 2: //Chinese chess
                idCount += 37;
                break;
            case 3: //Cinderella
                idCount += 19;
                break;
            case 4: //Frozen
                idCount += 1;
                break;
            case 5: //Japanese chess
                idCount += 43;
                break;
            case 6: //Poker
                idCount += 31;
                break;
            case 7: //Romeo and Juliette
                idCount += 25;
                break;
            case 8: //Snow white
                idCount += 49;
                break;
        }
        return idCount;
    }
    private void EquipSkin()
    {
        Debug.Log("EquipSkin button clicked!");
        //calculate target card style id
        int idCount = 0;
        switch(CurrentCharactor)
        {
            case "King":
                idCount = 2;
                break;
            case "Queen":
                idCount = 5;
                break;
            case "Prince":
                idCount = 4;
                break;
            case "Knight":
                idCount = 3;
                break;
            case "Civil":
                idCount = 0;
                break;
            case "Killer":
                idCount = 1;
                break;
        }
        string targetCharacterType = (idCount+1).ToString();
        Debug.Log($"targetCharacterType_input: {targetCharacterType}");
        switch(currentSkinIndex)
        {
            case 0: //Aladin
                idCount += 7;
                break;
            case 1: //Alice in wonderland
                idCount += 13;
                break;
            case 2: //Chinese chess
                idCount += 37;
                break;
            case 3: //Cinderella
                idCount += 19;
                break;
            case 4: //Frozen
                idCount += 1;
                break;
            case 5: //Japanese chess
                idCount += 43;
                break;
            case 6: //Poker
                idCount += 31;
                break;
            case 7: //Romeo and Juliette
                idCount += 25;
                break;
            case 8: //Snow white
                idCount += 49;
                break;
        }
        string targetCardStyleID = idCount.ToString();
        Debug.Log($"targetCardStyleID_input: {targetCardStyleID}");
        StartCoroutine(EquipSkinGetStatus(targetCardStyleID, targetCharacterType));
    }

    private void SellSkin()
    {
        //To be continue
        Warning_Title.text = "Sorry...";
        Warning_Message.text = "This feature is not yet available. Please wait for the next update";
        WarningPanel.SetActive(true);
    }

    private void OnNextButtonClicked()
    {
        //slove bug
        PreviousSkinButton.interactable = false;
        NextSkinButton.interactable = false;

        currentSkinIndex = (currentSkinIndex + 1) % skincount;
        UpdateSkinImage();
        UpdateCardStyleData();
    }

    private void OnPrevButtonClicked()
    {
        //slove bug
        PreviousSkinButton.interactable = false;
        NextSkinButton.interactable = false;

        currentSkinIndex = (currentSkinIndex - 1 + skincount) % skincount;
        UpdateSkinImage();
        UpdateCardStyleData();
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

    private void UpdateCardStyleData()
    {
        int target = getTargetCardStyleId();
        CardName.text = cardStyleList[target-1][1];
        CardInfo.text = cardStyleList[target-1][2];
        Rarity.text = cardStyleList[target-1][3]; 
    }

    private void ViewKingSkin()
    {
        SkinPanel.SetActive(true);
        CurrentCharactor = "King";
        UpdateSkinImage();
        UpdateCardStyleData();
        NextSkinButton.onClick.AddListener(OnNextButtonClicked);
        PreviousSkinButton.onClick.AddListener(OnPrevButtonClicked);
        CurrentSkinTypeText.text = "國王卡牌造型";
        SellButton.onClick.AddListener(SellSkin);
        EquipButton.onClick.AddListener(EquipSkin);
    }

    private void ViewQueenSkin()
    {
        SkinPanel.SetActive(true);
        CurrentCharactor = "Queen";
        UpdateSkinImage();
        UpdateCardStyleData();
        NextSkinButton.onClick.AddListener(OnNextButtonClicked);
        PreviousSkinButton.onClick.AddListener(OnPrevButtonClicked);
        CurrentSkinTypeText.text = "皇后卡牌造型";
        SellButton.onClick.AddListener(SellSkin);
        EquipButton.onClick.AddListener(EquipSkin);
    }

    private void ViewPrinceSkin()
    {
        SkinPanel.SetActive(true);
        CurrentCharactor = "Prince";
        UpdateSkinImage();
        UpdateCardStyleData();
        NextSkinButton.onClick.AddListener(OnNextButtonClicked);
        PreviousSkinButton.onClick.AddListener(OnPrevButtonClicked);
        CurrentSkinTypeText.text = "王子卡牌造型";
        SellButton.onClick.AddListener(SellSkin);
        EquipButton.onClick.AddListener(EquipSkin);
    }

    private void ViewKnightSkin()
    {
        SkinPanel.SetActive(true);
        CurrentCharactor = "Knight";
        UpdateSkinImage();
        UpdateCardStyleData();
        NextSkinButton.onClick.AddListener(OnNextButtonClicked);
        PreviousSkinButton.onClick.AddListener(OnPrevButtonClicked);
        CurrentSkinTypeText.text = "騎士卡牌造型";
        SellButton.onClick.AddListener(SellSkin);
        EquipButton.onClick.AddListener(EquipSkin);
    }

    private void ViewCivillianSkin()
    {
        SkinPanel.SetActive(true);
        CurrentCharactor = "Civil";
        UpdateSkinImage();
        UpdateCardStyleData();
        NextSkinButton.onClick.AddListener(OnNextButtonClicked);
        PreviousSkinButton.onClick.AddListener(OnPrevButtonClicked);
        CurrentSkinTypeText.text = "平民卡牌造型";
        int target = getTargetCardStyleId();
        StartCoroutine(logdata(target));
        SellButton.onClick.AddListener(SellSkin);
        EquipButton.onClick.AddListener(EquipSkin);
    }

    private void ViewAssassinSkin()
    {
        SkinPanel.SetActive(true);
        CurrentCharactor = "Killer";
        UpdateSkinImage();
        UpdateCardStyleData();
        NextSkinButton.onClick.AddListener(OnNextButtonClicked);
        PreviousSkinButton.onClick.AddListener(OnPrevButtonClicked);
        CurrentSkinTypeText.text = "殺手卡牌造型";
        int target = getTargetCardStyleId();
        StartCoroutine(logdata(target));
        SellButton.onClick.AddListener(SellSkin);
        EquipButton.onClick.AddListener(EquipSkin);
    }

    private string FormContentsToString(WWWForm form)
    {
        StringBuilder sb = new StringBuilder();

        foreach (var fieldName in form.headers.Keys)
        {
            sb.AppendLine($"{fieldName}: {form.headers[fieldName]}");
        }

        return sb.ToString();
    }

    // private IEnumerator logdata(string token)
    // {
    //     yield return StartCoroutine(userdata.Init_Card_Skill_Account_data(token));
    // }



}
