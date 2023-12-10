using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DontDestroy : MonoBehaviour
{
    public static string serverURL = "http://127.0.0.1:5050";
    public string serverURL_CardInfo = serverURL + "/user_data/getcardinfo";
    public string serverURL_SkillInfo = serverURL + "/user_data/getskillinfo";
    public string token = "";

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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


    public List<CharacterData> characterDataList = new List<CharacterData>();
    public List<SkillData> SkillDataList = new List<SkillData>();




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
                Debug.Log(dataString);

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
                Debug.Log(dataString);

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

}
