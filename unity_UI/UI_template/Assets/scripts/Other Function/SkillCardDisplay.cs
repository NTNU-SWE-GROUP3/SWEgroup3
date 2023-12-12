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
    // public SkillPopup skillDescriptionPanel;

    // Replace with your Flask server URL
    private string serverUrl = "http://127.0.0.1:5050";

    //private string token;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("displayingskillllllllllll");
        StartCoroutine(DisplaySkillStyle());
    }

    public IEnumerator DisplaySkillStyle()
    {
        //Button skillButton = null;
        string urlCard = serverUrl + "/skill_style/display_skill_style";
        WWWForm form = new WWWForm();
        string token = "token123";
        //string skillId = "1";
        form.AddField("Token", token);
        //form.AddField("SkillId", skillId);

         // HANDLE CARD DISPLAYED BY TOKEN 
        UnityWebRequest requestCard = UnityWebRequest.Post(urlCard, form);       
        yield return requestCard.SendWebRequest();

        if (requestCard.result == UnityWebRequest.Result.ConnectionError || requestCard.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("requesttttt errrorrr : " + requestCard.error);
        }
        else
        {
            string jsonCardResponse = requestCard.downloadHandler.text;
            Debug.Log("JSON card response = " + jsonCardResponse);
            // SkillStyleResponse response = JsonUtility.FromJson<SkillStyleResponse>(jsonResponse);
        
            SkillStyleResponse responseCard = JsonUtility.FromJson<SkillStyleResponse>(jsonCardResponse);
            //Debug.Log("Notice: " + responseCard);
            //StartCoroutine(DisplaySkillDesc(responseCard));

            if (responseCard != null)
            {
                Debug.Log("Response status: " + responseCard.status);
                Debug.Log("Response message: " + responseCard.msg);

                if (responseCard.status == "400055")
                {
                    Debug.Log("success 400055");
                    Debug.Log ("response card skill oclection : " + responseCard.skill_collection);
                    foreach (int skillStyle in responseCard.skill_collection)
                    {
                        Debug.Log ("skillstyle in skillcardDisplay.cs : " + skillStyle);
                        GameObject Skill_slot = Instantiate(Skill_slotPrefab, Skill_Bar.transform);
                        SkillSlotScript skillSlotScript = Skill_slot.GetComponent<SkillSlotScript>();
                        skillSlotScript.SetSkillStyle(skillStyle);
                        
                        //Button skillButton = Skill_slot.GetComponent<Button>();
                        //skillButton.onClick.AddListener(() => OnSkillSlotClicked(skillSlotScript));
                        //skillButton.onClick.AddListener(() => skillSlotScript.OnSkillSlotClicked());
                    }
                }
                else
                {
                    Debug.LogError("Error: Invalid status or null skill_collection");
                }
            }
            else
            {
                Debug.LogError("Error: Failed to deserialize response");
            }
        }
    }

    // public IEnumerator DisplaySkillDesc(SkillStyleResponse responseStyle)
    // {
    //     string url = serverUrl + "/skill_style/display_skill_desc";
    //     WWWForm form = new WWWForm();
    //     string token = "token123";
    //     form.AddField("Token", token);

    //     UnityWebRequest request = UnityWebRequest.Post(url, form);
    //     yield return request.SendWebRequest();

    //     if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
    //     {
    //         Debug.LogError("Request error: " + request.error);
    //     }
    //     else
    //     {
    //         string jsonResponse = request.downloadHandler.text;
    //         Debug.Log("JSON desc response = " + jsonResponse);
    //         SkillDescResponse response = JsonUtility.FromJson<SkillDescResponse>(jsonResponse);

    //         if (response != null)
    //         {
    //             Debug.Log("Response status: " + response.status);
    //             if (response.status == "400055" && response.skillName != null && response.skillDesc != null && response.skillProb != null)
    //             {
    //                 string[] skillNames = response.skillName;
    //                 string[] skillDescs = response.skillDesc;
    //                 string[] skillProbs = response.skillProb;

    //                 foreach (int skillStyleIndex in responseStyle.skill_collection)
    //                 {
    //                     // Make sure the index is within bounds
    //                     if (skillStyleIndex >= 0 && skillStyleIndex < skillNames.Length)
    //                     {
    //                         string skillName = skillNames[skillStyleIndex-1];
    //                         string skillDesc = skillDescs[skillStyleIndex-1];
    //                         string skillProb = skillProbs[skillStyleIndex-1];

    //                         Debug.Log("Skillname = " + skillName);
    //                         Debug.Log("Skilldesc = " + skillDesc);
    //                         Debug.Log("Skillprob = " + skillProb);

    //                         // Display skill information in the panel
    //                         skillDescriptionPanel.DisplaySkillInfo(skillName, skillDesc, skillProb);
    //                     }
    //                     else
    //                     {
    //                         Debug.LogError("Error: Invalid index");
    //                     }
    //                 }
    //             }
    //             else
    //             {
    //                 Debug.LogError("Error: Empty or null skill arrays");
    //             }
    //         }
    //         else
    //         {
    //             Debug.LogError("Error: Invalid status or null response");
    //         }
    //     }
    // }

    [System.Serializable]
    public class SkillStyleResponse
    {
        public string status;
        public string msg;
        public int[] skill_collection;
    }

    // [System.Serializable]
    // public class SkillDescResponse
    // {
    //     public string status;
    //     public string msg;
    //     public string[] skillName;
    //     public string[] skillDesc;
    //     public string[] skillProb;
    // }
}
/*
[System.Serializable]
public class SkillDescResponse
{
    public string status;
    public string msg;
    public string[][] skillDesc; // Change this to match the structure from the server
} 

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
    private string serverUrl = "http://127.0.0.1:8000";

    void Start()
    {
        StartCoroutine(DisplaySkillStyle());
    }

    void OnSkillSlotClicked(SkillSlotScript skillSlot)
    {
        Debug.Log($"Skill Name: {skillSlot.skillName}, Description: {skillSlot.skillDescription}, Probability: {skillSlot.skillProbability}");
        // Implement logic to show the message panel with the skill information
    }

    IEnumerator DisplaySkillStyle()
    {
        string url = serverUrl + "/skill_style/display_skill_style";
        WWWForm form = new WWWForm();
        string token = "token123";
        form.AddField("Token", token);

        UnityWebRequest request = UnityWebRequest.Post(url, form);
        
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("request error: " + request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            SkillStyleResponse response = JsonUtility.FromJson<SkillStyleResponse>(jsonResponse);

            if (response != null)
            {
                Debug.Log("Response status: " + response.status);
                Debug.Log("Response message: " + response.msg);

                if (response.status == "400055" && response.skill_collection != null)
                {
                    foreach (int skillStyle in response.skill_collection)
                    {
                        GameObject Skill_slot = Instantiate(Skill_slotPrefab, Skill_Bar.transform);
                        SkillSlotScript skillSlotScript = Skill_slot.GetComponent<SkillSlotScript>();
                        skillSlotScript.SetSkillStyle(skillStyle, response);
                        
                        Button skillButton = Skill_slot.GetComponent<Button>();
                        skillButton.onClick.AddListener(() => OnSkillSlotClicked(skillSlotScript));
                    }
                }
                else
                {
                    Debug.LogError("Error: Invalid status or null skill_collection");
                }
            }
            else
            {
                Debug.LogError("Error: Failed to deserialize response");
            }
        }
    }
}

[System.Serializable]
public class SkillStyleResponse
{
    public string status;
    public string msg;
    public int[] skill_collection;
    public string[][] skillDesc; // Add this line for skill descriptions
}
*/