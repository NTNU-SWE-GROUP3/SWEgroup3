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
    public int player_ranking;
    public string player_nickname;


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
    private TMP_Text TotalGameplay;
    private TMP_Text WinningRate;
    private TMP_Text TotalWin;
    private TMP_Text Ranking;
    private Button ReportBugButton;

    //WarningPanel
    private GameObject WarningPanel;
    private TMP_Text NoticeMessage;
    private Button WarningConfirmButton;


    //URL
    private static string serverUrl = "http://127.0.0.1:80";
    private string serverURL_login = serverUrl + "/account/login";
    private string serverURL_signup = serverUrl + "/account/signup";
    private string serverURL_checkaccount = serverUrl + "/forget_password/checkaccount";
    private string serverURL_changepassword = serverUrl + "/forget_password/changepassword";


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



    }

    private void UpdateUserInformation()
    {

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
        Debug.Log("Log File has been sent, Go debug yourself");
    }







}
