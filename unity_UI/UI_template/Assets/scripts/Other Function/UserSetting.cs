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
    public int player_equip_avatar;

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

    //Avator Image
    public Sprite AV1_Image;
    public Sprite AV2_Image;
    public Sprite AV3_Image;
    public Sprite AV4_Image;


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

       
        //Panels Init
        InitPanel();

        //Update uesr data 
        UpdateUserGeneralData();


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

        ChangeCancelButton.onClick.AddListener(CloseChangeInfoPanel);

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

    private void ChangeName()
    {
        string new_nickname = NewInfoInput.text;
        Debug.Log("Ready to change nickname by token =" + player_token);
        Debug.Log("Ready to change nickname = " + new_nickname);
        StartCoroutine(ChangeNicknameRequest(player_token, new_nickname));
        ChangeInfoPanel.SetActive(false);
        //UpdateUserGeneralData();
        UpdateUserInformation();
        InformInputText.text = ("");
        NewInfoInput.text = "";
    }

    private void ChangeEmail()
    {
        string new_email = NewInfoInput.text;
        //StartCoroutine(ChangeEmailRequest(player_token, new_email));
        ChangeInfoPanel.SetActive(false);
        //UpdateUserGeneralData();
        //UpdateUserInformation();
        InformInputText.text = ("");
        NewInfoInput.text = "";
        UserSettingWarningPanel.SetActive(true);
        NoticeTitleText.text = ("功能調整中！");
        UseSettingMessage.text = ("這個功能正在修改中QQ");
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
        UseSettingMessage.text = ("已成功更換造型!");
        Debug.Log("Avator Changed > 1");
        //AvatarButton
        Image buttonImage = AvatarButton.image;

        // 修改Image组件的sprite属性
        buttonImage.sprite = AV1_Image;


    }

    private void ChangeAV2()
    {
        // >>>>>>>>>>>>>>>Change the Avator to Avator 2 !! <<<<<<<<<<<<<<<< #Design Group
        UserSettingWarningPanel.SetActive(true);
        NoticeTitleText.text = ("頭像已變更！");
        UseSettingMessage.text = ("已成功更換造型!");
        Debug.Log("Avator Changed > 2");
        //AvatarButton
        Image buttonImage = AvatarButton.image;

        // 修改Image组件的sprite属性
        buttonImage.sprite = AV2_Image;
    }

    private void ChangeAV3()
    {
        // >>>>>>>>>>>>>>>Change the Avator to Avator 3 !! <<<<<<<<<<<<<<<< #Design Group
        UserSettingWarningPanel.SetActive(true);
        NoticeTitleText.text = ("頭像已變更！");
        UseSettingMessage.text = ("已成功更換造型!");
        Debug.Log("Avator Changed > 3");
        //AvatarButton
        Image buttonImage = AvatarButton.image;

        // 修改Image组件的sprite属性
        buttonImage.sprite = AV3_Image;
    }

    private void ChangeAV4()
    {
        // >>>>>>>>>>>>>>>Change the Avator to Avator 4 !! <<<<<<<<<<<<<<<< #Design Group
        UserSettingWarningPanel.SetActive(true);
        NoticeTitleText.text = ("頭像已變更！");
        UseSettingMessage.text = ("已成功更換造型!");
        Debug.Log("Avator Changed > 4");
        //AvatarButton
        Image buttonImage = AvatarButton.image;

        // 修改Image组件的sprite属性
        buttonImage.sprite = AV4_Image;
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
                        player_equip_avatar = responseData.equip_avatar;

                        UserName.text = player_nickname;
                        UIUserName.text = player_nickname;

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
        //User Info
        UIUserLevel.text = "LV." + player_level.ToString();
        UICoins.text = player_coins.ToString();
        UIChips.text = "0";

        //SettingPanel
        UserName.text = (player_nickname);
        Email.text = (player_email);

        //StatisticPanel
        TotalGameplay.text = (player_totalmatch.ToString());
        WinningRate.text = (player_winrate * 100f).ToString("0.00") + "%";
        TotalWin.text = (player_totalwin.ToString());
        Ranking.text = (player_ranking);

        Image buttonImage = AvatarButton.image;
        if (player_equip_avatar == 0)
        {
            Debug.Log("Defalt Avatar");
        }
        else if (player_equip_avatar == 1)
        {
            // 修改Image组件的sprite属性
            buttonImage.sprite = AV1_Image;
        }
        else if (player_equip_avatar == 2)
        {
            // 修改Image组件的sprite属性
            buttonImage.sprite = AV2_Image;
        }
        else if (player_equip_avatar == 3)
        {
            // 修改Image组件的sprite属性
            buttonImage.sprite = AV3_Image;
        }
        else if (player_equip_avatar == 4)
        {
            // 修改Image组件的sprite属性
            buttonImage.sprite = AV4_Image;
        }

    }

    private void UserChangeNickname()
    {

        //string new_nickname = NewInfoInput.text;
        ChangeInfoPanel.SetActive(true);
        InformInputText.text = ("請輸入新的暱稱");
        InfoPlaceholder.text = ("新的暱稱");
        ChangeConfirmButton.onClick.RemoveAllListeners();
        ChangeConfirmButton.onClick.AddListener(ChangeName);

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
                        UserSettingWarningPanel.SetActive(true);
                        NoticeTitleText.text = ("成功");
                        UseSettingMessage.text = "成功更改玩家暱稱：" + new_nickname;
                        UpdateUserGeneralData();
                        break;

                    case "403001":
                        Debug.Log("User Name Existed");
                        UserSettingWarningPanel.SetActive(true);
                        NoticeTitleText.text = ("改名失敗");
                        UseSettingMessage.text = "已存在的玩家暱稱：" + new_nickname;
                        break;

                    case "403002":
                        Debug.Log("User Name Too Long");
                        UserSettingWarningPanel.SetActive(true);
                        NoticeTitleText.text = ("改名失敗");
                        UseSettingMessage.text = "玩家暱稱長度過長";
                        break;

                    case "403003":
                        Debug.Log("User Name is illigal");
                        UserSettingWarningPanel.SetActive(true);
                        NoticeTitleText.text = ("改名失敗");
                        UseSettingMessage.text = "玩家暱稱長度過短";
                        break;

                    case "403004":
                        Debug.Log("User Name is illigal");
                        UserSettingWarningPanel.SetActive(true);
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
        //string new_email = NewInfoInput.text;
        ChangeInfoPanel.SetActive(true);
        InformInputText.text = ("請輸入新的Email");
        InfoPlaceholder.text = ("新的Email地址");


        //to be continue...
        //StartCoroutine(ChangeEmailRequest(player_token, new_email));
        Debug.Log("Try to Change Email...");

        ChangeConfirmButton.onClick.RemoveAllListeners();
        ChangeConfirmButton.onClick.AddListener(ChangeEmail);


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
        NoticeTitleText.text = ("日誌已上傳");
        UseSettingMessage.text = ("日誌資料已傳送給工程師！\n回報更多錯誤資訊或建議請聯繫\nsweonlinegame@gmail.com");
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
    public int equip_avatar;

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