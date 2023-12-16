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
    private DontDestroy userdata;
    // public SkillPopup skillDescriptionPanel;

    // Replace with your Flask server URL
    private string serverUrl = "http://140.122.185.169:5050";

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
        Debug.Log("displayingskillllllllllll");
        userdata = FindObjectOfType<DontDestroy>();
        StartCoroutine(DisplaySkillStyle());
    }

    public IEnumerator DisplaySkillStyle()
    {
        //Button skillButton = null;
        string urlCard = serverUrl + "/skill_style/display_skill_style";
        WWWForm form = new WWWForm();
        string token = userdata.token;
        Debug.Log("Display token: " + token);
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
                    Debug.Log("response card skill oclection : " + responseCard.skill_collection);
                    foreach (Transform child in Skill_Bar.transform)
                    {
                        Destroy(child.gameObject);
                    }
                    foreach (int skillStyle in responseCard.skill_collection)
                    {
                        Debug.Log("skillstyle in skillcardDisplay.cs : " + skillStyle);
                        GameObject Skill_slot = Instantiate(Skill_slotPrefab, Skill_Bar.transform);
                        SkillSlotScript skillSlotScript = Skill_slot.GetComponent<SkillSlotScript>();
                        skillSlotScript.SetSkillStyle(skillStyle);

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


    [System.Serializable]
    public class SkillStyleResponse
    {
        public string status;
        public string msg;
        public int[] skill_collection;
    }


}
