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
        new string [] {"1", "平民 (冰雪奇緣)", "將你的平民卡牌套上冰雪奇緣造型!", "普通"},
        new string [] {"2", "殺手 (冰雪奇緣)", "將你的平民卡牌套上冰雪奇緣造型!", "普通"},
        new string [] {"3", "國王 (冰雪奇緣)", "將你的平民卡牌套上冰雪奇緣造型!", "普通"},
        new string [] {"4", "騎士 (冰雪奇緣)", "將你的平民卡牌套上冰雪奇緣造型!", "普通"},
        new string [] {"5", "王子 (冰雪奇緣)", "將你的平民卡牌套上冰雪奇緣造型!", "普通"},
        new string [] {"6", "皇后 (冰雪奇緣)", "將你的平民卡牌套上冰雪奇緣造型!", "普通"},
        new string [] {"7", "平民 (阿拉丁)", "將你的平民卡牌套上阿拉丁造型!", "普通"},
        new string [] {"8", "殺手 (阿拉丁)", "將你的平民卡牌套上阿拉丁造型!", "普通"},
        new string [] {"9", "國王 (阿拉丁)", "將你的平民卡牌套上阿拉丁造型!", "普通"},
        new string [] {"10", "騎士 (阿拉丁)", "將你的平民卡牌套上阿拉丁造型!", "普通"},
        new string [] {"11", "王子 (阿拉丁)", "將你的平民卡牌套上阿拉丁造型!", "普通"},
        new string [] {"12", "皇后 (阿拉丁)", "將你的平民卡牌套上阿拉丁造型!", "普通"},
        new string [] {"13", "平民 (愛麗絲夢遊仙境)", "將你的平民卡牌套上愛麗絲夢遊仙境造型!", "普通"},
        new string [] {"14", "殺手 (愛麗絲夢遊仙境)", "將你的平民卡牌套上愛麗絲夢遊仙境造型!", "普通"},
        new string [] {"15", "國王 (愛麗絲夢遊仙境)", "將你的平民卡牌套上愛麗絲夢遊仙境造型!", "普通"},
        new string [] {"16", "騎士 (愛麗絲夢遊仙境)", "將你的平民卡牌套上愛麗絲夢遊仙境造型!", "普通"},
        new string [] {"17", "王子 (愛麗絲夢遊仙境)", "將你的平民卡牌套上愛麗絲夢遊仙境造型!", "普通"},
        new string [] {"18", "皇后 (愛麗絲夢遊仙境)", "將你的平民卡牌套上愛麗絲夢遊仙境造型!", "普通"},
        new string [] {"19", "平民 (灰姑娘)", "將你的平民卡牌套上灰姑娘造型!", "普通"},
        new string [] {"20", "殺手 (灰姑娘)", "將你的平民卡牌套上灰姑娘造型!", "普通"},
        new string [] {"21", "國王 (灰姑娘)", "將你的平民卡牌套上灰姑娘造型!", "普通"},
        new string [] {"22", "騎士 (灰姑娘)", "將你的平民卡牌套上灰姑娘造型!", "普通"},
        new string [] {"23", "王子 (灰姑娘)", "將你的平民卡牌套上灰姑娘造型!", "普通"},
        new string [] {"24", "皇后 (灰姑娘)", "將你的平民卡牌套上灰姑娘造型!", "普通"},
        new string [] {"25", "平民 (羅密歐與茱麗葉)", "將你的平民卡牌套上羅密歐與茱麗葉造型!", "普通"},
        new string [] {"26", "殺手 (羅密歐與茱麗葉)", "將你的平民卡牌套上羅密歐與茱麗葉造型!", "普通"},
        new string [] {"27", "國王 (羅密歐與茱麗葉)", "將你的平民卡牌套上羅密歐與茱麗葉造型!", "普通"},
        new string [] {"28", "騎士 (羅密歐與茱麗葉)", "將你的平民卡牌套上羅密歐與茱麗葉造型!", "普通"},
        new string [] {"29", "王子 (羅密歐與茱麗葉)", "將你的平民卡牌套上羅密歐與茱麗葉造型!", "普通"},
        new string [] {"30", "皇后 (羅密歐與茱麗葉)", "將你的平民卡牌套上羅密歐與茱麗葉造型!", "普通"},
        new string [] {"31", "平民 (撲克)", "將你的平民卡牌套上撲克造型!", "普通"},
        new string [] {"32", "殺手 (撲克)", "將你的平民卡牌套上撲克造型!", "普通"},
        new string [] {"33", "國王 (撲克)", "將你的平民卡牌套上撲克造型!", "普通"},
        new string [] {"34", "騎士 (撲克)", "將你的平民卡牌套上撲克造型!", "普通"},
        new string [] {"35", "王子 (撲克)", "將你的平民卡牌套上撲克造型!", "普通"},
        new string [] {"36", "皇后 (撲克)", "將你的平民卡牌套上撲克造型!", "普通"},
        new string [] {"37", "平民 (象棋)", "將你的平民卡牌套上象棋造型!", "普通"},
        new string [] {"38", "殺手 (象棋)", "將你的平民卡牌套上象棋造型!", "普通"},
        new string [] {"39", "國王 (象棋)", "將你的平民卡牌套上象棋造型!", "普通"},
        new string [] {"40", "騎士 (象棋)", "將你的平民卡牌套上象棋造型!", "普通"},
        new string [] {"41", "王子 (象棋)", "將你的平民卡牌套上象棋造型!", "普通"},
        new string [] {"42", "皇后 (象棋)", "將你的平民卡牌套上象棋造型!", "普通"},
        new string [] {"43", "平民 (將其)", "將你的平民卡牌套上將其造型!", "普通"},
        new string [] {"44", "殺手 (將其)", "將你的平民卡牌套上將其造型!", "普通"},
        new string [] {"45", "國王 (將其)", "將你的平民卡牌套上將其造型!", "普通"},
        new string [] {"46", "騎士 (將其)", "將你的平民卡牌套上將其造型!", "普通"},
        new string [] {"47", "王子 (將其)", "將你的平民卡牌套上將其造型!", "普通"},
        new string [] {"48", "皇后 (將其)", "將你的平民卡牌套上將其造型!", "普通"},
        new string [] {"49", "平民 (白雪公主)", "將你的平民卡牌套上白雪公主造型!", "普通"},
        new string [] {"50", "殺手 (白雪公主)", "將你的平民卡牌套上白雪公主造型!", "普通"},
        new string [] {"51", "國王 (白雪公主)", "將你的平民卡牌套上白雪公主造型!", "普通"},
        new string [] {"52", "騎士 (白雪公主)", "將你的平民卡牌套上白雪公主造型!", "普通"},
        new string [] {"53", "王子 (白雪公主)", "將你的平民卡牌套上白雪公主造型!", "普通"},
        new string [] {"54", "皇后 (白雪公主)", "將你的平民卡牌套上白雪公主造型!", "普通"},
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
        Warning_Title.text = "非常抱歉。。。";
        Warning_Message.text = "這個功能還沒辦法是用。請耐心等下一個更新版。。";
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
