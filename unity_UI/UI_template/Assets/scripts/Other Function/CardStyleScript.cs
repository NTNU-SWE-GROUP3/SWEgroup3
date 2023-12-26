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


    private static string[] skinFolders = { "Aladin", "Alice in wonderland", "Chinese chess", "Cinderella", "Frozen", "Japanese chess", "chess", "Romet and Juliette", "Snow White", "Poker" };
    private int skincount = skinFolders.Length;
    private int currentSkinIndex = 0;
    private string CurrentCharactor = "";

    // private static string serverUrl = "http://127.0.0.1:5050";
    private static string serverUrl = "http://140.122.185.169:5050";
    private string serverURL_equip = serverUrl + "/card_style/equip_card_style";
    private string serverURL_find_card_style = serverUrl + "/card_style/check_card_style";
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
        new string [] {"1", "平民 (冰雪奇緣)", "將平民套用冰雪奇緣造型!", "普通"},
        new string [] {"2", "殺手 (冰雪奇緣)", "將殺手套用冰雪奇緣造型!", "普通"},
        new string [] {"3", "國王 (冰雪奇緣)", "將國王套用冰雪奇緣造型!", "普通"},
        new string [] {"4", "騎士 (冰雪奇緣)", "將騎士套用冰雪奇緣造型!", "普通"},
        new string [] {"5", "王子 (冰雪奇緣)", "將王子套用冰雪奇緣造型!", "普通"},
        new string [] {"6", "皇后 (冰雪奇緣)", "將皇后套用冰雪奇緣造型!", "普通"},
        new string [] {"7", "平民 (阿拉丁)", "將平民套用阿拉丁造型!", "普通"},
        new string [] {"8", "殺手 (阿拉丁)", "將殺手套用阿拉丁造型!", "普通"},
        new string [] {"9", "國王 (阿拉丁)", "將國王套用阿拉丁造型!", "普通"},
        new string [] {"10", "騎士 (阿拉丁)", "將騎士套用阿拉丁造型!", "普通"},
        new string [] {"11", "王子 (阿拉丁)", "將王子套用阿拉丁造型!", "普通"},
        new string [] {"12", "皇后 (阿拉丁)", "將皇后套用阿拉丁造型!", "普通"},
        new string [] {"13", "平民 (愛麗絲夢遊仙境)", "將平民套用愛麗絲夢遊仙境造型!", "普通"},
        new string [] {"14", "殺手 (愛麗絲夢遊仙境)", "將殺手套用愛麗絲夢遊仙境造型!", "普通"},
        new string [] {"15", "國王 (愛麗絲夢遊仙境)", "將國王套用愛麗絲夢遊仙境造型!", "普通"},
        new string [] {"16", "騎士 (愛麗絲夢遊仙境)", "將騎士套用愛麗絲夢遊仙境造型!", "普通"},
        new string [] {"17", "王子 (愛麗絲夢遊仙境)", "將王子套用愛麗絲夢遊仙境造型!", "普通"},
        new string [] {"18", "皇后 (愛麗絲夢遊仙境)", "將皇后套用愛麗絲夢遊仙境造型!", "普通"},
        new string [] {"19", "平民 (灰姑娘)", "將平民套用灰姑娘造型!", "普通"},
        new string [] {"20", "殺手 (灰姑娘)", "將殺手套用灰姑娘造型!", "普通"},
        new string [] {"21", "國王 (灰姑娘)", "將國王套用灰姑娘造型!", "普通"},
        new string [] {"22", "騎士 (灰姑娘)", "將騎士套用灰姑娘造型!", "普通"},
        new string [] {"23", "王子 (灰姑娘)", "將王子套用灰姑娘造型!", "普通"},
        new string [] {"24", "皇后 (灰姑娘)", "將皇后套用灰姑娘造型!", "普通"},
        new string [] {"25", "平民 (羅密歐與茱麗葉)", "將平民套用羅密歐與茱麗葉造型!", "普通"},
        new string [] {"26", "殺手 (羅密歐與茱麗葉)", "將殺手套用羅密歐與茱麗葉造型!", "普通"},
        new string [] {"27", "國王 (羅密歐與茱麗葉)", "將國王套用羅密歐與茱麗葉造型!", "普通"},
        new string [] {"28", "騎士 (羅密歐與茱麗葉)", "將騎士套用羅密歐與茱麗葉造型!", "普通"},
        new string [] {"29", "王子 (羅密歐與茱麗葉)", "將王子套用羅密歐與茱麗葉造型!", "普通"},
        new string [] {"30", "皇后 (羅密歐與茱麗葉)", "將皇后套用羅密歐與茱麗葉造型!", "普通"},
        new string [] {"31", "平民 (西洋棋)", "將平民套用西洋棋造型!", "普通"},
        new string [] {"32", "殺手 (西洋棋)", "將殺手套用西洋棋造型!", "普通"},
        new string [] {"33", "國王 (西洋棋)", "將國王套用西洋棋造型!", "普通"},
        new string [] {"34", "騎士 (西洋棋)", "將騎士套用西洋棋造型!", "普通"},
        new string [] {"35", "王子 (西洋棋)", "將王子套用西洋棋造型!", "普通"},
        new string [] {"36", "皇后 (西洋棋)", "將皇后套用西洋棋造型!", "普通"},
        new string [] {"37", "平民 (象棋)", "將平民套用象棋造型!", "普通"},
        new string [] {"38", "殺手 (象棋)", "將殺手套用象棋造型!", "普通"},
        new string [] {"39", "國王 (象棋)", "將國王套用象棋造型!", "普通"},
        new string [] {"40", "騎士 (象棋)", "將騎士套用象棋造型!", "普通"},
        new string [] {"41", "王子 (象棋)", "將王子套用象棋造型!", "普通"},
        new string [] {"42", "皇后 (象棋)", "將皇后套用象棋造型!", "普通"},
        new string [] {"43", "平民 (將棋)", "將平民套用將棋造型!", "普通"},
        new string [] {"44", "殺手 (將棋)", "將殺手套用將棋造型!", "普通"},
        new string [] {"45", "國王 (將棋)", "將國王套用將棋造型!", "普通"},
        new string [] {"46", "騎士 (將棋)", "將騎士套用將棋造型!", "普通"},
        new string [] {"47", "王子 (將棋)", "將王子套用將棋造型!", "普通"},
        new string [] {"48", "皇后 (將棋)", "將皇后套用將棋造型!", "普通"},
        new string [] {"49", "平民 (白雪公主)", "將平民套用白雪公主造型!", "普通"},
        new string [] {"50", "殺手 (白雪公主)", "將殺手套用白雪公主造型!", "普通"},
        new string [] {"51", "國王 (白雪公主)", "將國王套用白雪公主造型!", "普通"},
        new string [] {"52", "騎士 (白雪公主)", "將騎士套用白雪公主造型!", "普通"},
        new string [] {"53", "王子 (白雪公主)", "將王子套用白雪公主造型!", "普通"},
        new string [] {"54", "皇后 (白雪公主)", "將皇后套用白雪公主造型!", "普通"},
        new string [] {"55", "平民 (撲克)", "將平民套用撲克造型!", "普通"},
        new string [] {"56", "殺手 (撲克)", "將殺手套用撲克造型!", "普通"},
        new string [] {"57", "國王 (撲克)", "將國王套用撲克造型!", "普通"},
        new string [] {"58", "騎士 (撲克)", "將騎士套用撲克造型!", "普通"},
        new string [] {"59", "王子 (撲克)", "將王子套用撲克造型!", "普通"},
        new string [] {"60", "皇后 (撲克)", "將皇后套用撲克造型!", "普通"},
    };


    private void Start()
    {
        currentSkinIndex = 0;
        CurrentCharactor = "";
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
        NextSkinButton.onClick.AddListener(OnNextButtonClicked);
        PreviousSkinButton.onClick.AddListener(OnPrevButtonClicked);

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
        currentSkinIndex = 0;
        CurrentCharactor = "";
        SkinPanel.SetActive(false);
        MaskPanel.SetActive(false);
        
    }

    // private IEnumerator logdata(int targetCardStyleId)
    // {
    //     // Debug.log("log data target card style id: ", targetCardStyleId);
    //     yield return StartCoroutine(userdata.Init_Card_Skill_Account_Data(authToken));

    //     CardName.text = userdata.characterDataList[targetCardStyleId].CardName;
    //     CardInfo.text = userdata.characterDataList[targetCardStyleId].CardDescription;
    //     Rarity.text = userdata.characterDataList[targetCardStyleId].CardProbability;
    //     // Debug.log("Character description: " + cardDescription);
    // }

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
                Warning_Title.text = "請注意!";
                Warning_Message.text = "請檢查您的網路";
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
                        Warning_Title.text = "恭喜您!";
                        Warning_Message.text = "裝備成功!";
                        WarningPanel.SetActive(true);
                        break;
                    case "200021":
                        Debug.Log("Equip failure");
                        // Warning Panel
                        Warning_Title.text = "非常抱歉!";
                        Warning_Message.text = "裝備失敗";
                        WarningPanel.SetActive(true);
                        break;
                    case "200022":
                        Debug.Log("Item doesn't exist in inventory");
                        // Warning Panel
                        Warning_Title.text = "糟糕!";
                        Warning_Message.text = "您的背包沒有這個造型。裝備失敗QQ";
                        WarningPanel.SetActive(true);
                        break;
                }
            }
        }
    }

    private IEnumerator SkinAvailabilityGetStatus(string targetCardStyleID)
    {
        Debug.Log("SkinAvailabilityGetStatus started");

        if (string.IsNullOrEmpty(authToken))
        {
            Debug.LogError("Authentication token is missing. User may not be logged in.");
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("tokenId", authToken); // 
        form.AddField("targetCardStyleId", targetCardStyleID);
        // Debug.Log("Form Contents: " + FormContentsToString(form));
        Debug.Log("Token: " + authToken);
        Debug.Log("targetCardStyleID: " + targetCardStyleID);

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL_find_card_style, form))
        {
            yield return www.SendWebRequest();

            Debug.Log("Response: " + www.downloadHandler.text);

            // Check for errors
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning(www.error);
                // Warning Panel
                Warning_Title.text = "請注意!";
                Warning_Message.text = "請檢查您的網路";
                WarningPanel.SetActive(true);
            }
            else //equip the skin
            {
                string availabilityStatus = www.downloadHandler.text;
                Debug.Log("Availability Status: " + availabilityStatus);

                ResponseData responseData = JsonUtility.FromJson<ResponseData>(availabilityStatus);
                Color currentColor = CurrentSkinImage.color;
                switch(responseData.status)
                {
                    case "200001":
                        Debug.Log("Card is available!");
                        currentColor.a = 1f;
                        CurrentSkinImage.color = currentColor;
                        break;
                    case "200022":
                        Debug.Log("Card is not available");
                        currentColor.a = 0.5f;
                        CurrentSkinImage.color = currentColor;
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
            case 6: //Chess
                idCount += 31;
                break;
            case 7: //Romeo and Juliette
                idCount += 25;
                break;
            case 8: //Snow white
                idCount += 49;
                break;
            case 9: //Poker
                idCount += 55;
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
            case 9: //Snow white
                idCount += 55;
                break;
        }
        string targetCardStyleID = idCount.ToString();
        Debug.Log($"targetCardStyleID_input: {targetCardStyleID}");
        StartCoroutine(EquipSkinGetStatus(targetCardStyleID, targetCharacterType));
    }

    private void UpdateSkinImageOpacity()
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
            case 6: //Chess
                idCount += 31;
                break;
            case 7: //Romeo and Juliette
                idCount += 25;
                break;
            case 8: //Snow white
                idCount += 49;
                break;
            case 9: //Poker
                idCount += 55;
                break;
        }
        string targetCardStyleID = idCount.ToString();
        Debug.Log($"targetCardStyleID_input: {targetCardStyleID}");
        StartCoroutine(SkinAvailabilityGetStatus(targetCardStyleID));
    }

    private void SellSkin()
    {
        //To be continue
        Warning_Title.text = "非常抱歉。。。";
        Warning_Message.text = "這個功能還沒辦法是用。請耐心等下一個更新版。。";
        WarningPanel.SetActive(true);
    }

    private void OnNextButtonClicked()
    {
        Debug.Log("current index : " +currentSkinIndex);
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
        Debug.Log("current index : " +currentSkinIndex);
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
        UpdateSkinImageOpacity();
        PreviousSkinButton.interactable = true;
        NextSkinButton.interactable = true;
    }

    private void UpdateCardStyleData()
    {
        int target = getTargetCardStyleId();
        CardName.text = cardStyleList[target-1][1];
        CardInfo.text = cardStyleList[target-1][2];
        Rarity.text = cardStyleList[target-1][3]; 

        if((target >= 13 && target <= 18) || (target >=25 && target <= 30))
        {
            CardName.fontSize = 40;
        }
        else
        {
            CardName.fontSize = 50;
        }
    }

    private void ViewKingSkin()
    {
        SkinPanel.SetActive(true);
        CurrentCharactor = "King";
        UpdateSkinImage();
        UpdateCardStyleData();
        CurrentSkinTypeText.text = "國王造型";
        SellButton.onClick.AddListener(SellSkin);
        EquipButton.onClick.AddListener(EquipSkin);
    }

    private void ViewQueenSkin()
    {
        SkinPanel.SetActive(true);
        CurrentCharactor = "Queen";
        UpdateSkinImage();
        UpdateCardStyleData();
        CurrentSkinTypeText.text = "皇后造型";
        SellButton.onClick.AddListener(SellSkin);
        EquipButton.onClick.AddListener(EquipSkin);
    }

    private void ViewPrinceSkin()
    {
        SkinPanel.SetActive(true);
        CurrentCharactor = "Prince";
        UpdateSkinImage();
        UpdateCardStyleData();
        CurrentSkinTypeText.text = "王子造型";
        SellButton.onClick.AddListener(SellSkin);
        EquipButton.onClick.AddListener(EquipSkin);
    }

    private void ViewKnightSkin()
    {
        SkinPanel.SetActive(true);
        CurrentCharactor = "Knight";
        UpdateSkinImage();
        UpdateCardStyleData();
        CurrentSkinTypeText.text = "騎士造型";
        SellButton.onClick.AddListener(SellSkin);
        EquipButton.onClick.AddListener(EquipSkin);
    }

    private void ViewCivillianSkin()
    {
        SkinPanel.SetActive(true);
        CurrentCharactor = "Civil";
        UpdateSkinImage();
        UpdateCardStyleData();
        CurrentSkinTypeText.text = "平民造型";
        int target = getTargetCardStyleId();
        // StartCoroutine(logdata(target));
        SellButton.onClick.AddListener(SellSkin);
        EquipButton.onClick.AddListener(EquipSkin);
    }

    private void ViewAssassinSkin()
    {
        SkinPanel.SetActive(true);
        CurrentCharactor = "Killer";
        UpdateSkinImage();
        UpdateCardStyleData();
        CurrentSkinTypeText.text = "殺手造型";
        int target = getTargetCardStyleId();
        // StartCoroutine(logdata(target));
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
