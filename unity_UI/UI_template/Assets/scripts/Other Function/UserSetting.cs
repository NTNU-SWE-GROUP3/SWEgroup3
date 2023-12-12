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
    private string player_token;
    public int player_coins;
    public int player_level;
    public int player_totalwin;
    public int player_totalmatch;
    public float player_winrate;
    public string player_ranking;
    public string player_nickname;
    public string player_email;

    //Avatar button
    public Button AvatarButton;

    //UserDataPanel
    private GameObject UserDataPanel;
    public Button BackButton;
    public Button GeneralSettingButton;
    public Button StatisticsButton;

    //SettingPanel
    private GameObject SettingPanel;
    public Text UserName;
    public Text Email;
    public Button ChangeNicknameButton;
    public Button ChangeEmailButton;
    public Button Avator1Button;
    public Button Avator2Button;
    public Button Avator3Button;
    public Button Avator4Button;

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
    public Button ChangeConfirmButton;
    public Text InfoPlaceholder;


    //UserSettingWarningPanel
    public GameObject UserSettingWarningPanel;
    public Text UseSettingMessage;
    public Text NoticeTitleText;
    public Button UserWarningButton;



    //URL
    public static string serverUrl = "http://140.122.185.169:5050";

    private string serverURL_playerdata = serverUrl + "/user_information/getplayerdata";
    private string serverURL_changeNickname = serverUrl + "/user_information/changenickname";
    private string serverURL_changeEmail = serverUrl + "/user_information/changeemail";


    void Start()
    {
        DontDestroy userdata = FindObjectOfType<DontDestroy>();

        if (userdata != null)
        {
            // 访问token变量
            player_token = userdata.token;
            Debug.Log("Token value: " + player_token);
        }
        else
        {
            Debug.LogError("DontDestroy script not found!");
        }

        UserDataPanel = GameObject.Find("UserDataPanel");
        SettingPanel = GameObject.Find("SettingPanel");
        StatisticPanel = GameObject.Find("StatisticPanel");

        /*
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
        */

        //Panels Init
        InitPanel();

        //Update uesr data once login success
        UpdateUserGeneralData();
        //UpdateUserInformation();


        AvatarButton.onClick.AddListener(OpenUserDataPanel);
        BackButton.onClick.AddListener(CloseUserDataPanel);
        GeneralSettingButton.onClick.AddListener(TurnGeneralSetting);
        StatisticsButton.onClick.AddListener(TurnStatistics);
        UserWarningButton.onClick.AddListener(CloseWarningPanel);

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
        UserDataPanel.SetActive(false);
        SettingPanel.SetActive(false);
        StatisticPanel.SetActive(false);
        ChangeInfoPanel.SetActive(false);
        UserSettingWarningPanel.SetActive(false);
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
        UserSettingWarningPanel.SetActive(false);
        UseSettingMessage.text = ("");
        NoticeTitleText.text = ("");
    }

    private void CloseChangeInfoPanel()
    {
        ChangeInfoPanel.SetActive(false);
        InformInputText.text = ("");
        NewInfoInput.text = "";
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
        UserSettingWarningPanel.SetActive(true);
        NoticeTitleText.text = ("頭像已變更！");
        UseSettingMessage.text = ("已成功更換xxx1造型!");
        Debug.Log("Avator Changed > 1");
    }

    private void ChangeAV2()
    {
        // >>>>>>>>>>>>>>>Change the Avator to Avator 2 !! <<<<<<<<<<<<<<<< #Design Group
        UserSettingWarningPanel.SetActive(true);
        NoticeTitleText.text = ("頭像已變更！");
        UseSettingMessage.text = ("已成功更換xxx2造型!");
        Debug.Log("Avator Changed > 2");

    }

    private void ChangeAV3()
    {
        // >>>>>>>>>>>>>>>Change the Avator to Avator 3 !! <<<<<<<<<<<<<<<< #Design Group
        UserSettingWarningPanel.SetActive(true);
        NoticeTitleText.text = ("頭像已變更！");
        UseSettingMessage.text = ("已成功更換xxx3造型!");
        Debug.Log("Avator Changed > 3");
    }

    private void ChangeAV4()
    {
        // >>>>>>>>>>>>>>>Change the Avator to Avator 4 !! <<<<<<<<<<<<<<<< #Design Group
        UserSettingWarningPanel.SetActive(true);
        NoticeTitleText.text = ("頭像已變更！");
        UseSettingMessage.text = ("已成功更換xxx4造型!");
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
                UserSettingWarningPanel.SetActive(true);
                NoticeTitleText.text = ("網路連接錯誤");
                UseSettingMessage.text = "Please check your internet connection";
                Debug.Log("Internet error");

            }
            else
            {

                string responseText = www.downloadHandler.text;
                //Debug.Log("Server Response: " + responseText);
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
        UserName.text = (player_nickname);
        Email.text = (player_email);

        //StatisticPanel
        TotalGameplay.text = (player_totalmatch.ToString());
        WinningRate.text = (player_winrate.ToString());
        TotalWin.text = (player_totalwin.ToString());
        Ranking.text = (player_ranking);


    }

    private void UserChangeNickname()
    {

        string new_nickname = NewInfoInput.text;
        ChangeInfoPanel.SetActive(true);
        InformInputText.text = ("請輸入新的暱稱");
        InfoPlaceholder.text = ("Enter new Nickname");

        ChangeConfirmButton.onClick.AddListener(CloseChangeInfoPanel);
        //to be continue...
        StartCoroutine(ChangeNicknameRequest(player_token, new_nickname));
        Debug.Log("Try to Change User Nickname...");

    }

    private IEnumerator ChangeNicknameRequest(string player_token, string new_nickname)
    {
        WWWForm form = new WWWForm();
        form.AddField("Token", player_token); //
        form.AddField("NewNickname", new_nickname); //

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL_changeNickname, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning(www.error);

                // Warning Panel
                UserSettingWarningPanel.SetActive(true);
                NoticeTitleText.text = ("網路連接錯誤");
                UseSettingMessage.text = "Please check your internet connection";
                Debug.Log("Internet error");

            }
            else
            {

                string responseText = www.downloadHandler.text;
                Debug.Log("Server Response: " + responseText);
                // 解析伺服器回應的 JSON
                NicknameResponseData responseData = JsonUtility.FromJson<NicknameResponseData>(responseText);
                // 根據狀態碼執行不同的操作
                switch (responseData.status)
                {
                    case "400000":
                        Debug.Log("Nickname change sucessfully");
                        NoticeTitleText.text = ("成功");
                        UseSettingMessage.text = "成功更改玩家暱稱：" + new_nickname;
                        break;

                    case "403001":
                        Debug.Log("User Name Existed");
                        NoticeTitleText.text = ("改名失敗");
                        UseSettingMessage.text = "已存在的玩家暱稱：" + new_nickname;
                        break;

                    case "403002":
                        Debug.Log("User Name Too Long");
                        NoticeTitleText.text = ("改名失敗");
                        UseSettingMessage.text = "玩家暱稱長度過長";
                        break;

                    case "403003":
                        Debug.Log("User Name is illigal");
                        NoticeTitleText.text = ("改名失敗");
                        UseSettingMessage.text = "玩家暱稱不符合規定";
                        break;

                    case "403011":
                        Debug.Log("Token Expired");
                        //>>>>>>>>>>>>>>>>>>>>> return to login
                        break;

                }
            }
        }
    }

    private void UserChangeEmail()
    {
        string new_email = NewInfoInput.text;
        ChangeInfoPanel.SetActive(true);
        InformInputText.text = ("請輸入新的Email");
        InfoPlaceholder.text = ("Enter new Email");


        ChangeConfirmButton.onClick.AddListener(CloseChangeInfoPanel);
        //to be continue...
        StartCoroutine(ChangeEmailRequest(player_token, new_email));
        Debug.Log("Try to Change Email...");

    }

    private IEnumerator ChangeEmailRequest(string player_token, string new_email)
    {
        WWWForm form = new WWWForm();
        form.AddField("Token", player_token); //
        form.AddField("Email", new_email); //

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL_changeEmail, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning(www.error);

                // Warning Panel
                UserSettingWarningPanel.SetActive(true);
                NoticeTitleText.text = ("網路連接錯誤");
                UseSettingMessage.text = "Please check your internet connection";
                Debug.Log("Internet error");

            }
            else
            {

                string responseText = www.downloadHandler.text;
                Debug.Log("Server Response: " + responseText);
                // 解析伺服器回應的 JSON
                ChangeEmailResponseData responseData = JsonUtility.FromJson<ChangeEmailResponseData>(responseText);
                // 根據狀態碼執行不同的操作
                switch (responseData.status)
                {
                    case "400000":
                        Debug.Log("Email check sucessfully, Email will be Sent!");
                        NoticeTitleText.text = ("驗證信已發送");
                        UseSettingMessage.text = "請到新的電子信箱點擊確認";
                        break;

                    case "403005":
                        Debug.Log("Email has been uesd!");
                        NoticeTitleText.text = ("錯誤");
                        UseSettingMessage.text = "此Email已被註冊";
                        break;

                    case "403011":
                        Debug.Log("Token Expired");
                        //>>>>>>>>>>>>>>>>>>>>> return to login
                        break;

                }
            }
        }
    }

    private void ReportBug()
    {
        UserSettingWarningPanel.SetActive(true);
        NoticeTitleText.text = ("錯誤訊息已回報！");
        UseSettingMessage.text = ("Log File has been sent!\nContact us via sweonlinegame@gmail.com");
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

public class NicknameResponseData
{
    public string status;
    public string msg;
}

public class ChangeEmailResponseData
{
    public string status;
    public string msg;
}