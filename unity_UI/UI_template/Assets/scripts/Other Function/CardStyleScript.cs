using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Panel
    private GameObject SkinPanel;
    public GameObject MaskPanel;
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

    }

    private void ClosePanel()
    {
        SkinPanel.SetActive(false);
        MaskPanel.SetActive(false);
        currentSkinIndex = 0;
        CurrentCharactor = "";
}

    private void EquipSkin()
    {
        //To be continue
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












}
