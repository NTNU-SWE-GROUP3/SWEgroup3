using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class Pg1PanelController : MonoBehaviour
{
    public GameObject KingPanel;
    public GameObject QueenPanel;
    public GameObject PrincePanel;
    public GameObject KnightPanel;
    public GameObject CivilianPanel;
    public GameObject AssassinPanel;

    public Button SellKingSkinButton;
    public Button EquipKingSkinButton;
    public Button SellQueenSkinButton;
    public Button EquipQueenSkinButton;
    public Button SellPrinceSkinButton;
    public Button EquipPrinceSkinButton;
    public Button SellKnightSkinButton;
    public Button EquipKnightSkinButton;
    public Button SellCivilianSkinButton;
    public Button EquipCivilianSkinButton;
    public Button SellAssassinSkinButton;
    public Button EquipAssassinSkinButton;

     //WarningPanel 
    public GameObject WarningPanel;
    public TMP_Text Warning_Message;
    public Button Warning_ConfirmButton;

    //ConfirmationPanel 
    public GameObject ConfirmationPanel;
    public TMP_Text Confirmation_Message;
    public Button Confirmation_ConfirmButton;
    public Button Confirmation_UndoButton;

    //URL
    private static string serverUrl = "http://127.0.0.1:80";
    private string serverURL_equip_card_style = serverUrl + "/card_style/equip_card_style";
    private string serverURL_sell_card_style = serverUrl + "/card_style/sell_card_style";

                                                             
    // Start is called before the first frame update
    void Start()
    {
        BackToSkinMain(); 
        Warning_ConfirmButton.onClick.AddListener(CloseWarning);
        SellKingSkinButton.onClick.AddListener(ConfirmSell);
        SellQueenSkinButton.onClick.AddListener(ConfirmSell);
        SellPrinceSkinButton.onClick.AddListener(ConfirmSell);
        SellKnightSkinButton.onClick.AddListener(ConfirmSell);
        SellCivilianSkinButton.onClick.AddListener(ConfirmSell);
        SellAssassinSkinButton.onClick.AddListener(ConfirmSell);

        EquipKingSkinButton.onClick.AddListener(EquipKingSkin);
        EquipQueenSkinButton.onClick.AddListener(EquipQueenSkin);
        EquipPrinceSkinButton.onClick.AddListener(EquipPrinceSkin);
        EquipKnightSkinButton.onClick.AddListener(EquipKnightSkin);
        EquipCivilianSkinButton.onClick.AddListener(EquipCivilianSkin);
        EquipAssassinSkinButton.onClick.AddListener(EquipAssassinSkin);
        
    }

    public void BackToSkinMain(){
        KingPanel.SetActive(false);
        QueenPanel.SetActive(false);
        PrincePanel.SetActive(false);
        KnightPanel.SetActive(false);
        CivilianPanel.SetActive(false);
        AssassinPanel.SetActive(false);
        WarningPanel.SetActive(false);
        ConfirmationPanel.SetActive(false);
    }

    public void ShowKingPanel(){
        BackToSkinMain();
        KingPanel.SetActive(true);
    }
    public void ShowQueenPanel(){
        BackToSkinMain();
        QueenPanel.SetActive(true);
    }
    public void ShowPrincePanel(){
        BackToSkinMain();
        PrincePanel.SetActive(true);
    }
    public void ShowKnightPanel(){
        BackToSkinMain();
        KnightPanel.SetActive(true);
    }
    public void ShowCivilianPanel(){
        BackToSkinMain();
        CivilianPanel.SetActive(true);
    }
    public void ShowAssassinPanel(){
        BackToSkinMain();
        AssassinPanel.SetActive(true);
    }

    IEnumerator SellCardStyle(string token, int card_style_id)
    {
        WWWForm form = new WWWForm();
        form.AddField("Token", token); // 
        form.AddField("SkinId", card_style_id); //

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL_sell_card_style, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning(www.error);

                // Warning Panel
                Warning_Message.SetText("Please check your network connection");
                WarningPanel.SetActive(true);

            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log("Server Response: " + responseText);
                // 解析伺服器回應的 JSON
                // ResponseData responseData = JsonUtility.FromJson<ResponseData>(responseText);
                // 根據狀態碼執行不同的操作
                switch (responseText)
                {
                    case "200002":
                        Debug.Log("Sell success!");

                        // Warning Panel
                        Warning_Message.SetText("Congratulations! You have managed to sell your skin and get 10 coins in exchange!");
                        WarningPanel.SetActive(true);
                        break;
                    case "200022":
                        Debug.Log("User doesn't have this item");
                        // Warning Panel
                        Warning_Message.SetText("It seems like you do not have this item in your inventory");
                        WarningPanel.SetActive(true);
                        break;
                }
            }
        }
    }

    IEnumerator EquipCardStyle(string token, int card_style_id)
    {
        WWWForm form = new WWWForm();
        form.AddField("Token", token); // 
        form.AddField("SkinId", card_style_id); //

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL_equip_card_style, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning(www.error);

                // Warning Panel
                Warning_Message.SetText("Please check your network connection");
                WarningPanel.SetActive(true);

            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log("Server Response: " + responseText);
                switch (responseText)
                {
                    case "200001":
                        Debug.Log("Equip success!");

                        // Warning Panel
                        Warning_Message.SetText("Congratulations! You have managed to change the skin of your card!");
                        WarningPanel.SetActive(true);
                        //請改對應到的卡牌造型~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                        break;
                    case "200021":
                        Debug.Log("User doesn't have this item");
                        // Warning Panel
                        Warning_Message.SetText("It seems like you do not have this item in your inventory");
                        WarningPanel.SetActive(true);
                        break;
                }
            }
        }
    }
    
    private void ConfirmSell()
    {
        Confirmation_Message.SetText("Are you sure you want to sell this item?");
        ConfirmationPanel.SetActive(true);
        Confirmation_ConfirmButton.onClick.AddListener(SellSkin);
        Confirmation_UndoButton.onClick.AddListener(BackToSkinMain);
    }
    private void CloseWarning()
    {

        WarningPanel.SetActive(false);
        Warning_Message.SetText("");

    }

    private void SellSkin()
    {
        ConfirmationPanel.SetActive(false);
        StartCoroutine(SellCardStyle("523654", 546324));
    }
    // private void SellKingSkin()
    // {
    //     StartCoroutine(SellCardStyle("523654", 546324));
    // }
    // private void SellQueenSkin()
    // {
    //     StartCoroutine(SellCardStyle("523654", 546324));
    // }
    // private void SellPrinceSkin()
    // {
    //     // StartCoroutine(SellCardStyle("523654", 546324));
    // }
    // private void SellKnightSkin()
    // {
    //     // StartCoroutine(SellCardStyle("523654", 546324));
    // }
    // private void SellCivilianSkin()
    // {
    //     // StartCoroutine(SellCardStyle("523654", 546324));
    // }
    // private void SellAssassinSkin()
    // {
    //     // StartCoroutine(SellCardStyle("523654", 546324));
    // }

    private void EquipKingSkin()
    {
        StartCoroutine(EquipCardStyle("523654", 546324));
    }
    private void EquipQueenSkin()
    {
        StartCoroutine(EquipCardStyle("523654", 546324));
    }
    private void EquipPrinceSkin()
    {
        StartCoroutine(EquipCardStyle("523654", 546324));
    }
    private void EquipKnightSkin()
    {
        StartCoroutine(EquipCardStyle("523654", 546324));
    }
    private void EquipCivilianSkin()
    {
        StartCoroutine(EquipCardStyle("523654", 546324));
    }
    private void EquipAssassinSkin()
    {
        StartCoroutine(EquipCardStyle("523654", 546324));
    }
}

