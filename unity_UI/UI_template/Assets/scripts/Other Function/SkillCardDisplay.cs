using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;


public class SkillCardDisplay : MonoBehaviour
{
    public GameObject Skill_Bar;
    public GameObject Skill_slotPrefab;

    // Replace with your Flask server URL
    private string serverUrl = "http://127.0.0.1:8001";

    //private string token;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplaySkillStyle());
    }

    IEnumerator DisplaySkillStyle()
    {
        string url = serverUrl + "/skill_style/display_skill_style";
        WWWForm form = new WWWForm();
        string token = "token123";
        //string skillId = "1";
        form.AddField("Token", token);
        //form.AddField("SkillId", skillId);

        UnityWebRequest request = UnityWebRequest.Post(url, form);
        //UnityWebRequest request = UnityWebRequest.Post(url);
        //UnityWebRequest request = UnityWebRequest.Get(url);
        
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("requesttttt errrorrr : " + request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            SkillStyleResponse response = JsonUtility.FromJson<SkillStyleResponse>(jsonResponse);
            Debug.LogError("Notice: " + response);

            if (response.status == "400055")
            {
                foreach (int skillStyle in response.skillStyles)
                {
                    // Instantiate skill slot prefab
                    GameObject Skill_slot = Instantiate(Skill_slotPrefab, Skill_Bar.transform);
                    
                    // Attach a script to the instantiated slot to set skill card appearance
                    SkillSlotScript skillSlotScript = Skill_slot.GetComponent<SkillSlotScript>();
                    skillSlotScript.SetSkillStyle(skillStyle);
                }
            }
            else
            {
                Debug.LogError("hsahshesdhdshshshshshs Error: " + response.msg);
            }
        }
    }
}

[System.Serializable]
public class SkillStyleResponse
{
    public string status;
    public string msg;
    public int[] skillStyles;
}