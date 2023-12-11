using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using UnityEngine.Networking;
using MiniJSON;
using UnityEngine.SceneManagement;

public class PvE : MonoBehaviour
{
    private DontDestroy userdata;
    string tokenId;
    string plyerInfoUrl = "http://140.122.185.169:5050/gaming/get_player_info";
    string equipUrl = "http://140.122.185.169:5050/gaming/get_skills_card_styles";
    void Start()
    {
        userdata = FindObjectOfType<DontDestroy>();
        tokenId = userdata.token;
        Debug.Log("In PvE: " + userdata.token);

    }

    public void PvEButton()
    {
        // get token id from login first
        Debug.Log("In PvE: " + userdata.token);

        StartCoroutine(SendRequestInfo(tokenId));
        StartCoroutine(SendRequestEquip(tokenId));
    }

    IEnumerator SendRequestInfo(string tokenId)
    {
        Debug.Log("SendRequestInfo");
        WWWForm form = new WWWForm();

        form.AddField("token_id", tokenId);

        UnityWebRequest www = UnityWebRequest.Post(plyerInfoUrl, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            string response = www.downloadHandler.text;
            Debug.Log(response);
            Dictionary<string, object> jsonDict = Json.Deserialize(response) as Dictionary<string, object>;
            if (jsonDict != null)
            {
                int id = int.Parse(jsonDict["account_id"].ToString());
                int coin = int.Parse(jsonDict["coin"].ToString());
                int exp = int.Parse(jsonDict["experience"].ToString());
                int level = int.Parse(jsonDict["level"].ToString());
                string nick_name = jsonDict["nickname"].ToString();
                string rank = jsonDict["rank"].ToString();
                int ranked_XP = int.Parse(jsonDict["ranked_XP"].ToString());
                int win_streak = int.Parse(jsonDict["ranked_winning_streak"].ToString());
                int total_match = int.Parse(jsonDict["total_match"].ToString());
                int total_win = int.Parse(jsonDict["total_win"].ToString());
                PlayerPrefs.SetInt("id", id);
                PlayerPrefs.SetInt("coin", coin);
                PlayerPrefs.SetInt("exp", exp);
                PlayerPrefs.SetInt("level", level);
                PlayerPrefs.SetString("nickname", nick_name);
                PlayerPrefs.SetString("rank", rank);
                PlayerPrefs.SetInt("ranked_XP", ranked_XP);
                PlayerPrefs.SetInt("win_streak", win_streak);
                PlayerPrefs.SetInt("total_match", total_match);
                PlayerPrefs.SetInt("total_win", total_win);

                PlayerPrefs.Save();
                // SceneManager.LoadScene(2);
            }
            else
            {
                Debug.LogError("Failed to parse JSON data.");
            }
        }
    }
    IEnumerator SendRequestEquip(string tokenId)
    {
        Debug.Log("SendRequestEquip");
        WWWForm form = new WWWForm();

        form.AddField("token_id", tokenId);

        UnityWebRequest www = UnityWebRequest.Post(equipUrl, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            string response = www.downloadHandler.text;
            Debug.Log(response);
            Dictionary<string, object> jsonDict = Json.Deserialize(response) as Dictionary<string, object>;
            if (jsonDict != null)
            {
                List<object> skills = jsonDict["skills"] as List<object>;
                Debug.Log("Skills Count: " + skills.Count);

                List<int> skillIds = new List<int>();
                foreach (object skillId in skills)
                {
                    int id = System.Convert.ToInt32(skillId);
                    // Debug.Log("Skill ID: " + id);
                    skillIds.Add(id);
                }
                SaveSkills(skillIds);

                List<object> cardStyles = jsonDict["card_styles"] as List<object>;
                Debug.Log("Card Styles Count: " + cardStyles.Count);

                List<int> styleIds = new List<int>();
                foreach (object styleId in cardStyles)
                {
                    int id = System.Convert.ToInt32(styleId);
                    // Debug.Log("Style ID: " + id);
                    styleIds.Add(id);
                }
                SaveStyles(styleIds);
            }
            else
            {
                Debug.LogError("Failed to parse JSON data.");
            }
        }
        SceneManager.LoadScene(2);
    }
    void SaveSkills(List<int> skills)
    {
        string skillsString = string.Join(",", skills.ConvertAll(i => i.ToString()).ToArray());

        PlayerPrefs.SetString("skills", skillsString);
        PlayerPrefs.Save();
    }
    void SaveStyles(List<int> styles)
    {
        string stylesString = string.Join(",", styles.ConvertAll(i => i.ToString()).ToArray());

        PlayerPrefs.SetString("card_styles", stylesString);
        PlayerPrefs.Save();
    }
}
