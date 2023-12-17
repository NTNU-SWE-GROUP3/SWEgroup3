using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

using TMPro;


public class strat : MonoBehaviour
{

    private GameObject LoginPanel;
    private GameObject SignUpPanel;
    private GameObject PasswordPanel1;
    private GameObject PasswordPanel2;
    private GameObject WarningPanel;

    //LoginPanel
    private Button Login_LoginButton;
    private Button Login_ForgetPasswordButton;
    private Button Login_SignUpButton;
    private TMP_InputField Login_AccountInput;
    private TMP_InputField Login_PasswordInput;

    //SignUpPanel

    private TMP_InputField SignUp_AccountInput;
    private TMP_InputField SignUp_EmailInput;
    private TMP_InputField SignUp_PasswordInput;
    private Button SignUp_SignUpButton;
    private Button SignUp_BackButton;

    //PasswordPanel1

    private TMP_InputField PP1_AccountInput;
    private TMP_InputField PP1_EmailInput;
    private Button PP1_ValidButton;
    private Button PP1_BackButton;

    //PasswordPanel2

    private TMP_InputField PP2_VarifyCode;
    private TMP_InputField PP2_PasswordInput;
    private TMP_InputField PP2_PasswordCheckInput;
    private Button PP2_ConfirmButton;
    private Button PP2_BackButton;

    //WarningPanel 

    private TMP_Text Warning_Message;
    private Button Warning_ConfirmButton;



    //URL

    private static string serverUrl = "http://140.122.185.169:5050";

    private string serverURL_login = serverUrl + "/account/login";
    private string serverURL_signup = serverUrl + "/account/signup";
    private string serverURL_checkaccount = serverUrl + "/forget_password/checkaccount";
    private string serverURL_changepassword = serverUrl + "/forget_password/changepassword";




    void Start()
    {
        

        LoginPanel = GameObject.Find("LoginPanel");
        SignUpPanel = GameObject.Find("SignUpPanel");
        PasswordPanel1 = GameObject.Find("PasswordPanel1");
        PasswordPanel2 = GameObject.Find("PasswordPanel2");
        WarningPanel = GameObject.Find("WarningPanel");

        //LoginPanel

        Login_LoginButton = LoginPanel.transform.Find("LoginButton").GetComponent<Button>();
        Login_ForgetPasswordButton = LoginPanel.transform.Find("ForgetPasswordButton").GetComponent<Button>();
        Login_SignUpButton = LoginPanel.transform.Find("SignUpButton").GetComponent<Button>();
        Login_AccountInput = LoginPanel.transform.Find("EnterAccount").GetComponent<TMP_InputField>();
        Login_PasswordInput = LoginPanel.transform.Find("EnterPassword").GetComponent<TMP_InputField>();

        //SignUpPanel

        SignUp_AccountInput = SignUpPanel.transform.Find("EnterAccount").GetComponent<TMP_InputField>();
        SignUp_EmailInput = SignUpPanel.transform.Find("EnterEmail").GetComponent<TMP_InputField>();
        SignUp_PasswordInput = SignUpPanel.transform.Find("EnterPassword").GetComponent<TMP_InputField>();
        SignUp_SignUpButton = SignUpPanel.transform.Find("SignUpButton").GetComponent<Button>();
        SignUp_BackButton = SignUpPanel.transform.Find("BackButton").GetComponent<Button>();

        //PasswordPanel1

        PP1_AccountInput = PasswordPanel1.transform.Find("EnterAccount").GetComponent<TMP_InputField>();
        PP1_EmailInput = PasswordPanel1.transform.Find("EnterEmail").GetComponent<TMP_InputField>();
        PP1_ValidButton = PasswordPanel1.transform.Find("GetValidButton").GetComponent<Button>();
        PP1_BackButton = PasswordPanel1.transform.Find("BackButton").GetComponent<Button>();

        //PasswordPanel2

        PP2_VarifyCode = PasswordPanel2.transform.Find("EnterVarifyCode").GetComponent<TMP_InputField>();
        PP2_PasswordInput = PasswordPanel2.transform.Find("EnterPassword").GetComponent<TMP_InputField>();
        PP2_PasswordCheckInput = PasswordPanel2.transform.Find("ReEnterPassword").GetComponent<TMP_InputField>();
        PP2_ConfirmButton = PasswordPanel2.transform.Find("ConfirmButton").GetComponent<Button>();
        PP2_BackButton = PasswordPanel2.transform.Find("BackButton").GetComponent<Button>();

        //WarningPanel 

        Warning_Message = WarningPanel.transform.Find("WarningMessage").GetComponent<TMP_Text>();
        Warning_ConfirmButton = WarningPanel.transform.Find("ConfirmButton").GetComponent<Button>();


        LoginPanel.SetActive(true);
        SignUpPanel.SetActive(false);
        PasswordPanel1.SetActive(false);
        PasswordPanel2.SetActive(false);
        WarningPanel.SetActive(false);

        Login_LoginButton.onClick.AddListener(LoginRequest);
        Login_ForgetPasswordButton.onClick.AddListener(GoForgetPassword);
        Login_SignUpButton.onClick.AddListener(GoSignUp);
        SignUp_SignUpButton.onClick.AddListener(SignUpRequest);
        PP1_ValidButton.onClick.AddListener(CheckAccountRequest);
        PP2_ConfirmButton.onClick.AddListener(ChangePasswordRequest);
        Warning_ConfirmButton.onClick.AddListener(CloseWarning);
        SignUp_BackButton.onClick.AddListener(BackToLogin);
        PP1_BackButton.onClick.AddListener(BackToLogin);
        PP2_BackButton.onClick.AddListener(BackToLogin);

    }


    private void BackToLogin()
    {

        LoginPanel.SetActive(true);
        SignUpPanel.SetActive(false);
        PasswordPanel1.SetActive(false);
        PasswordPanel2.SetActive(false);


        Login_AccountInput.text = "";
        Login_PasswordInput.text = "";
        SignUp_AccountInput.text = "";
        SignUp_EmailInput.text = "";
        SignUp_PasswordInput.text = "";
        PP1_AccountInput.text = "";
        PP1_EmailInput.text = "";
        PP2_PasswordInput.text = "";
        PP2_PasswordCheckInput.text = "";

        Debug.Log("Back to login page");

    }

    private void CloseWarning()
    {

        WarningPanel.SetActive(false);
        Warning_Message.SetText("");

    }

    private void LoginRequest()
    {
        string account = Login_AccountInput.text;
        string password = Login_PasswordInput.text;
        if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
        {
            // 如果帳號、密碼為空，顯示錯誤消息
            Debug.Log("Not fill in all required fields.");
            // Warning Panel
            Warning_Message.SetText("Please fill in all required fields");
            WarningPanel.SetActive(true);
        }
        else {
            Debug.Log("Try to login...");
            StartCoroutine(SendLoginRequest(account, password));
        }
    }

    private IEnumerator SendLoginRequest(string account, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("Account", account); // 
        form.AddField("Password", password); // 

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL_login, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning(www.error);

                // Warning Panel
                Warning_Message.SetText("Please check your network connection");
                WarningPanel.SetActive(true);

            }
            else // Login Success
            {
    
                string responseText = www.downloadHandler.text;
                Debug.Log("Server Response: " + responseText);
                // 解析伺服器回應的 JSON
                ResponseData responseData = JsonUtility.FromJson<ResponseData>(responseText);
                // 根據狀態碼執行不同的操作
                switch (responseData.status)
                {
                    case "400000":
                        Debug.Log("Login success!");
                        //Warning_Message.SetText("Login success!");
                        //WarningPanel.SetActive(true);
                        // 找到具有DontDestroy脚本的游戏对象
                        DontDestroy dontDestroyScript = FindObjectOfType<DontDestroy>();

                        // 检查是否找到了对象
                        if (dontDestroyScript != null)
                        {
                            // 访问token变量
                            string tokenValue = responseData.tokenId;
                            Debug.Log("Token value: " + tokenValue);
                            dontDestroyScript.token = responseData.tokenId;
                            // 執行登入成功的操作
                            StartCoroutine(LoadSceneAsync("MainSc"));
                            /////////////////////////////////////////////////////////////////////////////
                            //                  *                                                       /
                            //                   ***                                                    /
                            //                    *****                                                 /
                            //                     *******                                              /
                            //                      *********                                           /
                            //                       ***********                                        /
                            //                        *************                                     /
                            //                         ***************                                  /
                            //                          *****************    登入成功，請在這裡切換場景       
                            //                         ***************                                  /
                            //                        *************                                     /
                            //                       ***********                                        /
                            //                      *********                                           /
                            //                     *******                                              /
                            //                    *****                                                 /
                            //                   ***                                                    /
                            //                  *                                                       /
                            /////////////////////////////////////////////////////////////////////////////

                        }
                        else
                        {
                            Debug.LogError("DontDestroy script not found!");
                        }
                        break;
                    case "403001":
                        Debug.Log("No such account");
                        // Warning Panel
                        Warning_Message.SetText("No such account");
                        WarningPanel.SetActive(true);
                        break;
                    case "403002":
                        Debug.Log("Wrong password");
                        // Warning Panel
                        Warning_Message.SetText("Wrong password");
                        WarningPanel.SetActive(true);
                        break;
                }
            }
        }
    }

    private IEnumerator LoadSceneAsync(string targetSceneName)
    {
        // 异步加载目标场景
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneName);

        // 等待场景加载完成
        while (!asyncLoad.isDone)
        {
            // 这里可以加入加载过程中的其他逻辑，比如更新UI显示加载进度等
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            yield return null;
        }

        // 场景加载完成后的逻辑
        Debug.Log("Scene loaded!");
    }

    private void GoForgetPassword()
    {
        LoginPanel.SetActive(false);
        SignUpPanel.SetActive(false);
        PasswordPanel1.SetActive(true);
        PasswordPanel2.SetActive(false);
        Debug.Log("Turn to PasswordPanel1");

    }

    private void GoSignUp()
    {

        LoginPanel.SetActive(false);
        SignUpPanel.SetActive(true);
        PasswordPanel1.SetActive(false);
        PasswordPanel2.SetActive(false);
        Debug.Log("Turn to SignUpPanel");

    }

    private void SignUpRequest() // On SignUpPanel
    {
        
        string account = SignUp_AccountInput.text;
        string password = SignUp_PasswordInput.text;
        string email = SignUp_EmailInput.text;

        if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
        {
            // 如果帳號、密碼或郵件為空，顯示錯誤消息或執行相應操作
            Debug.Log("Not fill in all required fields.");
            // Warning Panel
            Warning_Message.SetText("Please fill in all required fields");
            WarningPanel.SetActive(true);
        }
        else
        {
            // 執行註冊操作
            StartCoroutine(SendSignupRequest(account, password, email));
            Debug.Log("Try to Register...");
            
        }

    }

    private IEnumerator SendSignupRequest(
        string account, string password, string email)
    {
        WWWForm form = new WWWForm();
        form.AddField("Account", account); // 
        form.AddField("Password", password); //
        form.AddField("Email", email); // 

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL_signup, form))
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
                ResponseData responseData = JsonUtility.FromJson<ResponseData>(responseText);
                // 根據狀態碼執行不同的操作
                switch (responseData.status)
                {
                    case "400001":
                        Debug.Log("Signup success!");

                        // Warning Panel
                        Warning_Message.SetText("Signup success!");
                        WarningPanel.SetActive(true);

                        LoginPanel.SetActive(true);
                        SignUpPanel.SetActive(false);
                        PasswordPanel1.SetActive(false);
                        PasswordPanel2.SetActive(false);

                        break;
                    case "403003":
                        Debug.Log("Username has been used");
                        // Warning Panel
                        Warning_Message.SetText("Usernane has been used");
                        WarningPanel.SetActive(true);
                        break;
                    case "403004":
                        Debug.Log("Email already registered");
                        // Warning Panel
                        Warning_Message.SetText("Email already registered");
                        WarningPanel.SetActive(true);
                        break;
                    case "403005":
                        Debug.Log("Password too short");
                        // Warning Panel
                        Warning_Message.SetText("Password too short");
                        WarningPanel.SetActive(true);
                        break;
                }
            }
        }
    }

    private void CheckAccountRequest() //On PasswordPanel1
    {

        string account = PP1_AccountInput.text;
        string email = PP1_EmailInput.text;

        if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(email))
        {
            // 如果帳號或郵件為空，顯示錯誤消息或執行相應操作
            Debug.Log("Not fill in all required fields.");
            // Warning Panel
            Warning_Message.SetText("Please fill in all required fields");
            WarningPanel.SetActive(true);
        }
        else
        {
            // 執行註冊操作
            StartCoroutine(SendAccountRequest(account, email));
            Debug.Log("Try to find whether the account belongs to user...");
        }
    }

    private IEnumerator SendAccountRequest(string account, string email)
    {
        WWWForm form = new WWWForm();
        form.AddField("Account", account); // 
        form.AddField("Email", email); // 

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL_checkaccount, form))
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
                ResponseData responseData = JsonUtility.FromJson<ResponseData>(responseText);
                // 根據狀態碼執行不同的操作
                switch (responseData.status)
                {
                    case "400002":
                        Debug.Log("Email & Username match");

                        // Warning Panel
                        Warning_Message.SetText("Veryfy code has sent\n to your Email address");
                        WarningPanel.SetActive(true);

                        Debug.Log("Turn to PasswordPanel2");
                        LoginPanel.SetActive(false);
                        SignUpPanel.SetActive(false);
                        PasswordPanel1.SetActive(false);
                        PasswordPanel2.SetActive(true);
                        break;
                    case "403001":
                        Debug.Log("No such account");
                        // Warning Panel
                        Warning_Message.SetText("No such account");
                        WarningPanel.SetActive(true);
                        break;
                    case "403006":
                        Debug.Log("Email & account NOT match");
                        // Warning Panel
                        Warning_Message.SetText("Wrong Email");
                        WarningPanel.SetActive(true);
                        break;
                }
            }
        }
    }

    private void ChangePasswordRequest() // On PasswordPanel2
    {
        string account = PP1_AccountInput.text;
        string VarifyCode = PP2_VarifyCode.text;
        string Password = PP2_PasswordInput.text;
        string CheckPassword = PP2_PasswordCheckInput.text;

        if (string.IsNullOrEmpty(VarifyCode) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(CheckPassword))
        {
            // 如果為空，顯示錯誤消息或執行相應操作
            Debug.Log("Not fill in all required fields.");
            // Warning Panel
            Warning_Message.SetText("Please fill in all required fields");
            WarningPanel.SetActive(true);
        }
        else if(Password != CheckPassword)
        {
            Debug.Log("Input passwords must be the same");
            // Warning Panel
            Warning_Message.SetText("Input passwords must be the same");
            WarningPanel.SetActive(true);
        }
        else
        {
            // 執行change password操作
            StartCoroutine(SendpasswordRequest(account, VarifyCode, Password));
            Debug.Log("Try to find whether the account belongs to user...");
        }
    }

    private IEnumerator SendpasswordRequest(string account, string VarifyCode, string Password)
    {
        WWWForm form = new WWWForm();
        form.AddField("Account", account); // 
        form.AddField("VarifyCode", VarifyCode); // 
        form.AddField("Password", Password); // 

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL_changepassword, form))
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
                ResponseData responseData = JsonUtility.FromJson<ResponseData>(responseText);
                // 根據狀態碼執行不同的操作
                switch (responseData.status)
                {
                    case "400003":
                        Debug.Log("Password changed successfully");

                        // Warning Panel
                        Warning_Message.SetText("Password changed successfully");
                        WarningPanel.SetActive(true);

                        LoginPanel.SetActive(true);
                        SignUpPanel.SetActive(false);
                        PasswordPanel1.SetActive(false);
                        PasswordPanel2.SetActive(false);
                        Debug.Log("Return to LoginPanel");
                        break;
                    case "403007":
                        Debug.Log("Password too short");
                        // Warning Panel
                        Warning_Message.SetText("Password too short");
                        WarningPanel.SetActive(true);
                        break;
                    case "403008":
                        Debug.Log("Password too long");
                        // Warning Panel
                        Warning_Message.SetText("Password too long");
                        WarningPanel.SetActive(true);
                        break;
                    case "403009":
                        Debug.Log("Wrong VarifyCode");
                        // Warning Panel
                        Warning_Message.SetText("Wrong VarifyCode");
                        WarningPanel.SetActive(true);
                        break;
                    case "403010":
                        Debug.Log("VarifyCode Expired");
                        // Warning Panel
                        Warning_Message.SetText("VarifyCode Expired");
                        WarningPanel.SetActive(true);
                        BackToLogin();
                        Debug.Log("Return to LoginPanel");
                        break;
                }
            }
        }
    }


    


}


public class ResponseData
{
    public string status;
    public string tokenId;
}
