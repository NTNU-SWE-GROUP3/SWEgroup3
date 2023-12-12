using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DontDestroy : MonoBehaviour
{
    public static string serverURL = "http://140.122.185.169:5050";
    public string serverURL_CardInfo = serverURL + "/user_data/getcardinfo";
    public string serverURL_SkillInfo = serverURL + "/user_data/getskillinfo";
    public string serverURL_UserSkillData = serverURL + "/user_data/getuserskilldata";
    public string serverURL_UserCardData = serverURL + "/user_data/getuserstyledata";
    public string serverURL_AccountData = serverURL + "/user_data/get_accountdata_table";


    public string token = "";
    public int uid;
    public string nickname = "";
    public int level;
    public int experience;
    public string rank = "";
    public int total_match;
    public int total_win;
    public int ranked_winning_streak;
    public int rank_xp;
    public int coin;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(InitData());
    }

    IEnumerator InitData()
    {
        yield return StartCoroutine(CardInfoRequest());
        yield return StartCoroutine(SkillInfoRequest());

    }


    // 定義資料結構
    [System.Serializable]
    public class CharacterData
    {
        public string CardID;
        public string CardName;
        public string CardDescription;
        public string CardProbability;
    }

    [System.Serializable]
    public class SkillData
    {
        public string SkillID;
        public string SkillName;
        public string SkillDescription;
        public string SkillProbability;
    }

    [System.Serializable]
    public class UserCardData
    {
        public string CardID;
        public string EquipStatus;
    }

    [System.Serializable]
    public class UserSkillData
    {
        public string SkillID;
        public string EquipStatus;
    }


    public List<CharacterData> characterDataList = new List<CharacterData>();
    public List<SkillData> SkillDataList = new List<SkillData>();
    public List<UserCardData> UserCardDataList = new List<UserCardData>();
    public List<UserSkillData> UserSkillDataList = new List<UserSkillData>();



    public IEnumerator CardInfoRequest()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL_CardInfo, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning(www.error);
                Debug.Log("Internet error @ CardInfoRequest");
            }
            else
            {
                string dataString = www.downloadHandler.text;
                //Debug.Log(dataString);

                string[] dataParts = dataString.Split(';');

                // 確保有足夠的元素來初始化變數
                if (dataParts.Length % 4 == 0)
                {
                    // 迭代處理每四個元素
                    for (int i = 0; i < dataParts.Length; i += 4)
                    {
                        string id = (dataParts[i]);
                        string cardname = dataParts[i + 1];
                        string description = dataParts[i + 2];
                        string probability = (dataParts[i + 3]);

                        // 使用取得的數據初始化變數
                        InitializeCardStyleVariables(id, cardname, description, probability);
                        //Debug.Log("id:" + id + "\ntype:" + cardname + "\ndescription:" + description + "\nvalue:" + probability);
                    }
                }
                else
                {
                    Debug.Log("Invalid data format from the server:"+ dataParts.Length);
                }
            }
        }
    }



    // 初始化變數
    private void InitializeCardStyleVariables(string id, string type, string description, string value)
    {
        CharacterData character = new CharacterData
        {
            CardID = id,
            CardName = type,
            CardDescription = description,
            CardProbability = value
        };

        // 將角色資料添加到列表中
        characterDataList.Add(character);
    }





    public IEnumerator SkillInfoRequest()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL_SkillInfo, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning(www.error);
                Debug.Log("Internet error @ SkillInfoRequest");
            }
            else
            {
                string dataString = www.downloadHandler.text;
                //Debug.Log(dataString);

                string[] dataParts = dataString.Split(';');

                // 確保有足夠的元素來初始化變數
                if (dataParts.Length % 4 == 0)
                {
                    // 迭代處理每四個元素
                    for (int i = 0; i < dataParts.Length; i += 4)
                    {
                        string id = (dataParts[i]);
                        string cardname = dataParts[i + 1];
                        string description = dataParts[i + 2];
                        string probability = (dataParts[i + 3]);

                        // 使用取得的數據初始化變數
                        InitializeSkillVariables(id, cardname, description, probability);
                        //Debug.Log("id:" + id + "\ntype:" + cardname + "\ndescription:" + description + "\nvalue:" + probability);
                    }
                }
                else
                {
                    Debug.Log("Invalid data format from the server:" + dataParts.Length);
                }
            }
        }
    }


    private void InitializeSkillVariables(string id, string name, string description, string probability)
    {
        SkillData skill = new SkillData
        {
            SkillID = id,
            SkillName = name,
            SkillDescription = description,
            SkillProbability = probability
        };

        // 將skill資料添加到列表中
        SkillDataList.Add(skill);
    }


    public IEnumerator UserCardDataRequest(string player_token)
    {
        WWWForm form = new WWWForm();
        form.AddField("Token", player_token); // 

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL_UserCardData, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning(www.error);
                Debug.Log("Internet error @ UserCardDataRequest");
            }
            else
            {
                string dataString = www.downloadHandler.text;
                //Debug.Log(dataString);

                string[] dataParts = dataString.Split(';');

                // 確保有足夠的元素來初始化變數
                if (dataParts.Length % 2 == 0)
                {
                    // 迭代處理每四個元素
                    for (int i = 0; i < dataParts.Length; i += 2)
                    {
                        string CardID = (dataParts[i]);
                        string EquipStatus = dataParts[i + 1];


                        // 使用取得的數據初始化變數
                        InitializeUserCardDataVariables(CardID, EquipStatus);
                        //Debug.Log("CardID:" + CardID + "\nEquipStatus:" + EquipStatus);
                    }
                }
                else
                {
                    Debug.Log("No Card data from the server:");
                }
            }
        }
    }



    // 初始化變數
    private void InitializeUserCardDataVariables(string CardID, string EquipStatus)
    {
        UserCardData usercarddata = new UserCardData
        {
            CardID = CardID,
            EquipStatus = EquipStatus,
        };

        // 將角色資料添加到列表中
        UserCardDataList.Add(usercarddata);
    }



    public IEnumerator UserSkillDataRequest(string player_token)
    {
        WWWForm form = new WWWForm();
        form.AddField("Token", player_token); // 

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL_UserSkillData, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning(www.error);
                Debug.Log("Internet error @ UserSkillDataRequest");
            }
            else
            {
                string dataString = www.downloadHandler.text;
                //Debug.Log(dataString);

                string[] dataParts = dataString.Split(';');

                // 確保有足夠的元素來初始化變數
                if (dataParts.Length % 2 == 0)
                {
                    // 迭代處理每四個元素
                    for (int i = 0; i < dataParts.Length; i += 2)
                    {
                        string SkillID = (dataParts[i]);
                        string EquipStatus = dataParts[i + 1];


                        // 使用取得的數據初始化變數
                        InitializeUserSkillDataVariables(SkillID, EquipStatus);
                        //Debug.Log("SkillID:" + SkillID + "\nEquipStatus:" + EquipStatus);
                    }
                }
                else
                {
                    Debug.Log("Invalid data format from the server:" + dataParts.Length);
                }
            }
        }
    }



    // 初始化變數
    private void InitializeUserSkillDataVariables(string SkillID, string EquipStatus)
    {
        UserSkillData userskilldata = new UserSkillData
        {
            SkillID = SkillID,
            EquipStatus = EquipStatus,
        };

        // 將角色資料添加到列表中
        UserSkillDataList.Add(userskilldata);
    }


    public IEnumerator AccountTableRequest(string player_token)
    {
        WWWForm form = new WWWForm();
        form.AddField("Token", player_token); // 

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL_AccountData, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning(www.error);
                Debug.Log("Internet error @ AccountTableRequest");
            }
            else
            {
                string dataString = www.downloadHandler.text;
                //Debug.Log(dataString);

                string[] dataParts = dataString.Split(';');

                // 確保有足夠的元素來初始化變數
                if (dataParts.Length == 10)
                {
                    uid = int.Parse(dataParts[0]);
                    nickname = dataParts[1];
                    level = int.Parse(dataParts[2]);
                    experience = int.Parse(dataParts[3]);
                    rank = dataParts[4];
                    total_match = int.Parse(dataParts[5]);
                    total_win = int.Parse(dataParts[6]);
                    ranked_winning_streak = int.Parse(dataParts[7]);
                    rank_xp = int.Parse(dataParts[8]);
                    coin = int.Parse(dataParts[9]);

                    /*
                        public int uid;
                        public string nickname = "";
                        public int level;
                        public int experience;
                        public string rank = "";
                        public int total_match;
                        public int total_win;
                        public int ranked_winning_streak;
                        public int rank_xp;
                        public int coin;
                     */
                }
                else
                {
                    Debug.Log("Invalid data format from the server:" + dataParts.Length);
                }
            }
        }
    }



    public IEnumerator Init_Card_Skill_Account_Data(string token)
    {
        yield return StartCoroutine(UserCardDataRequest(token));
        yield return StartCoroutine(UserSkillDataRequest(token));
        yield return StartCoroutine(AccountTableRequest(token));

    }





}
