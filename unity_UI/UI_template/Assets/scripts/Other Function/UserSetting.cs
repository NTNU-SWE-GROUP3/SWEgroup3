using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class UserSetting : MonoBehaviour
{

    //Variables
    public string player_token;
    public int player_coins;
    public int player_level;
    public int player_totalwin;
    public int player_totalmatch;
    public float player_winrate;
    public string player_ranking;
    public string player_nickname;
    public string player_email;

    //DEBUG
    public GameObject DEBUG_Page4Panels;
    public GameObject DEBUG_MessagePanels;

    //Avatar button
    public Button AvatarButton;

    //UserDataPanel
    private GameObject UserDataPanel;
    private Button BackButton;
    private Button GeneralSettingButton;
    private Button StatisticsButton;

    //SettingPanel
    private GameObject SettingPanel;
    private TMP_Text UserName;
    private TMP_Text Email;
    private Button ChangeNicknameButton;
    private Button ChangeEmailButton;
    private Button Avator1Button;
    private Button Avator2Button;
    private Button Avator3Button;
    private Button Avator4Button;

    //StatisticPanel
    private GameObject StatisticPanel;
    public Text TotalGameplay;
    public Text WinningRate;
    public Text TotalWin;
    public Text Ranking;
    public Button ReportBugButton;


    //ChangeInfoPanel
    public GameObject ChangeInfoPanel;
    public Text InformInputText;
    public InputField NewInfoInput;
    public Button ChangeCancelButton;
    public Button ChangeConfirmButton;
    public Text InfoPlaceholder;


    //UserSettingWarningPanel
    public GameObject UserSettingWarningPanel;
    public Text UseSettingMessage;
    public Text NoticeTitleText;
    public Button UserWarningButton;

    //UI Text
    public Text UIUserName;
    public Text UIUserLevel;
    public Text UICoins;
    public Text UIChips;

    //WarningPanel
    private GameObject WarningPanel;
    private TMP_Text NoticeMessage;
    private Button WarningConfirmButton;


    //URL
    private static string serverUrl = "http://127.0.0.1:80";
    private string serverURL_playerdata = serverUrl + "/user_information/getplayerdata";
  

    void Start()
    {

        UserDataPanel = GameObject.Find("UserDataPanel");
        SettingPanel = GameObject.Find("SettingPanel");
        StatisticPanel = GameObject.Find("StatisticPanel");
        WarningPanel = GameObject.Find("WarningPanel");

        //UserDataPanel
        BackButton = UserDataPanel.transform.Find("BackButton").GetComponent<Button>(); ;
        GeneralSettingButton = UserDataPanel.transform.Find("GeneralSettingButton").GetComponent<Button>();
        StatisticsButton = UserDataPanel.transform.Find("StatisticsButton").GetComponent<Button>();

        //SettingPanel
        UserName = SettingPanel.transform.Find("UserName").GetComponent<TMP_Text>();
        Email = SettingPanel.transform.Find("Email").GetComponent<TMP_Text>(); 
        ChangeNicknameButton = SettingPanel.transform.Find("ChangeNicknameButton").GetComponent<Button>(); 
        ChangeEmailButton = SettingPanel.transform.Find("ChangeEmailButton").GetComponent<Button>();
        Avator1Button = SettingPanel.transform.Find("Avator1").GetComponent<Button>();
        Avator2Button = SettingPanel.transform.Find("Avator2").GetComponent<Button>();
        Avator3Button = SettingPanel.transform.Find("Avator3").GetComponent<Button>();
        Avator4Button = SettingPanel.transform.Find("Avator4").GetComponent<Button>();

        //StatisticPanel
        TotalGameplay = StatisticPanel.transform.Find("TotalGameplay").GetComponent<TMP_Text>();
        WinningRate = StatisticPanel.transform.Find("WinningRate").GetComponent<TMP_Text>();
        TotalWin = StatisticPanel.transform.Find("TotalWin").GetComponent<TMP_Text>();
        Ranking = StatisticPanel.transform.Find("Ranking").GetComponent<TMP_Text>();
        ReportBugButton = StatisticPanel.transform.Find("ReportBugButton").GetComponent<Button>();

        //WarningPanel
        NoticeMessage = WarningPanel.transform.Find("NoticeMessage").GetComponent<TMP_Text>();
        WarningConfirmButton = WarningPanel.transform.Find("ConfirmButton").GetComponent<Button>();


        //Panels Init
        InitPanel();

        //Update uesr data once login success
        UpdateUserGeneralData();
        UpdateUserInformation();


        AvatarButton.onClick.AddListener(OpenUserDataPanel);
        BackButton.onClick.AddListener(CloseUserDataPanel);
        GeneralSettingButton.onClick.AddListener(TurnGeneralSetting);
        StatisticsButton.onClick.AddListener(TurnStatistics);
        WarningConfirmButton.onClick.AddListener(CloseWarningPanel);

        Avator1Button.onClick.AddListener(ChangeAV1);
        Avator2Button.onClick.AddListener(ChangeAV2);
        Avator3Button.onClick.AddListener(ChangeAV3);
        Avator4Button.onClick.AddListener(ChangeAV4);

        ChangeNicknameButton.onClick.AddListener(UserChangeNickname);
        ChangeEmailButton.onClick.AddListener(UserChangeEmail);
        ReportBugButton.onClick.AddListener(ReportBug);

        ChangeCancelButton.onClick.AddListener(CloseChangeInfoPanel);

    }

    //Scene Operation
    private void InitPanel()
    {
        //DEBUG
        DEBUG_Page4Panels.SetActive(false);
        DEBUG_MessagePanels.SetActive(false);

        UserDataPanel.SetActive(false);
        SettingPanel.SetActive(false);
        StatisticPanel.SetActive(false);
        WarningPanel.SetActive(false);
    }

    private void OpenUserDataPanel()
    {
        UserDataPanel.SetActive(true);
        SettingPanel.SetActive(true);
        StatisticPanel.SetActive(false);
        UpdateUserGeneralData();
        UpdateUserInformation();
    }

    private void CloseUserDataPanel()
    {
        UserDataPanel.SetActive(false);
        SettingPanel.SetActive(false);
        StatisticPanel.SetActive(false);
    }

    private void CloseWarningPanel()
    {
        WarningPanel.SetActive(false);
        NoticeMessage.SetText("");
    }

    private void TurnGeneralSetting()
    {
        UserDataPanel.SetActive(true);
        SettingPanel.SetActive(true);
        StatisticPanel.SetActive(false);
    }

    private void TurnStatistics()
    {
        UserDataPanel.SetActive(true);
        SettingPanel.SetActive(false);
        StatisticPanel.SetActive(true);
    }

    //Change avator
    private void ChangeAV1()
    {
        // >>>>>>>>>>>>>>>Change the Avator to Avator 1 !! <<<<<<<<<<<<<<<< #Design Group
        WarningPanel.SetActive(true);
        NoticeMessage.SetText("Avator Changed!");
        Debug.Log("Avator Changed > 1");
    }

    private void ChangeAV2()
    {
        // >>>>>>>>>>>>>>>Change the Avator to Avator 2 !! <<<<<<<<<<<<<<<< #Design Group
        WarningPanel.SetActive(true);
        NoticeMessage.SetText("Avator Changed!");
        Debug.Log("Avator Changed > 2");

    }

    private void ChangeAV3()
    {
        // >>>>>>>>>>>>>>>Change the Avator to Avator 3 !! <<<<<<<<<<<<<<<< #Design Group
        WarningPanel.SetActive(true);
        NoticeMessage.SetText("Avator Changed!");
        Debug.Log("Avator Changed > 3");
    }

    private void ChangeAV4()
    {
        // >>>>>>>>>>>>>>>Change the Avator to Avator 4 !! <<<<<<<<<<<<<<<< #Design Group
        WarningPanel.SetActive(true);
        NoticeMessage.SetText("Avator Changed!");
        Debug.Log("Avator Changed > 4");
    }


    //Code

    private void UpdateUserGeneralData()  
    {
        // 執行UpdateUserGeneralData操作
        StartCoroutine(PlayerDataRequest(player_token));
        Debug.Log("Try to Update User General Data");
    }


    private IEnumerator PlayerDataRequest(string player_token)
    {
        WWWForm form = new WWWForm();
        form.AddField("Token", player_token); // 
        
        using (UnityWebRequest www = UnityWebRequest.Post(serverURL_playerdata, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning(www.error);

                // Warning Panel
                WarningPanel.SetActive(true);
                NoticeMessage.SetText("Please check your internet connection");
                Debug.Log("Internet error");

            }
            else
            {

                string responseText = www.downloadHandler.text;
                Debug.Log("Server Response: " + responseText);
                // 解析伺服器回應的 JSON
                UserInformationResponseData responseData = JsonUtility.FromJson<UserInformationResponseData>(responseText);
                // 根據狀態碼執行不同的操作
                switch (responseData.status)
                {
                    case "400004":
                        Debug.Log("Get player data successfully");
                        player_coins = responseData.coin;
                        player_level = responseData.level;
                        player_totalwin = responseData.totalwin ;
                        player_totalmatch = responseData.totalgame;
                        player_ranking = responseData.ranking;
                        player_nickname = responseData.nickname;
                        player_email = responseData.email;
                        player_winrate = responseData.winrate;
                        break;

                    case "403011":
                        //>>>>>>>>>>>>>>>>>>>>> return to login
                        break;

                }
            }
        }
    }

    private void UpdateUserInformation()
    {
        //SettingPanel
        UserName.SetText(player_nickname);
        Email.SetText(player_email);

        //StatisticPanel
        TotalGameplay.SetText(player_totalmatch.ToString());
        WinningRate.SetText(player_winrate.ToString());
        TotalWin.SetText(player_totalwin.ToString());
        Ranking.SetText(player_ranking);


    }

    private void UserChangeNickname()
    {

    }

    private void UserChangeEmail()
    {

    }

    private void ReportBug()
    {
        WarningPanel.SetActive(true);
        NoticeMessage.SetText("Log File has been sent!\nContact us via sweonlinegame@gmail.com");
        Debug.Log("Log File has not been sent, Go debug yourself");
    }







}


public class UserInformationResponseData
{
    public string status;
    public string msg;
    public string nickname;
    public string email;
    public int totalgame;
    public float winrate;
    public int totalwin;
    public string ranking;
    public int coin;
    public int level;

}

