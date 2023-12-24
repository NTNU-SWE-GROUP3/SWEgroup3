using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EquipButtonController : MonoBehaviour
{
    private int skillStyleID;
    public Button equipButton;
    public Text equipButtonText;
    public GameObject equipPanel;
    public Text equipPanelText; 
    private DontDestroy userdata;

    private void Start()
    {
        StartCoroutine(StartCheckEquipStatusAndUpdateUI());
        userdata = FindObjectOfType<DontDestroy>();
    }

    // Call this method to set the current skillStyleID
    public void SetSkillStyleID(int newSkillStyleID)
    {
        this.skillStyleID = newSkillStyleID;
        Debug.Log("NEWWWWSkillStyleID set to: " + newSkillStyleID);
        StartCoroutine(CheckEquipStatusAndUpdateUI());
    }

    public void OnEquipButtonClick()
    {
        StartCoroutine(ToggleEquipStatus());
    }

    IEnumerator StartCheckEquipStatusAndUpdateUI()
    {
        // string url = "http://127.0.0.1:5050/skill_style/check_equip_status";
        string url = "http://140.122.185.169:5050/skill_style/check_equip_status";

        WWWForm form = new WWWForm();
        // int targetSkillId = skillStyleID;
        int targetSkillId = 1;
        string token = userdata.token;
        // string token = "token123";
        
        form.AddField("tokenId", token);
        form.AddField("targetSkillId", targetSkillId);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error checking equip status: " + www.error);
            }
            else
            {
                string equipStatus = www.downloadHandler.text;
                UpdateButtonUI(equipStatus == "1");

                // Show the panel only after the button text has been updated
                ShowEquipStatusPanel(equipStatus == "1");
            }
        }
    }

    IEnumerator CheckEquipStatusAndUpdateUI()
    {
        // string url = "http://127.0.0.1:5050/skill_style/check_equip_status";
        string url = "http://140.122.185.169:5050/skill_style/check_equip_status";

        WWWForm form = new WWWForm();
        // int targetSkillId = skillStyleID;
        int targetSkillId = 1;
        string token = userdata.token;
        // string token = "token123";
        
        form.AddField("tokenId", token);
        form.AddField("targetSkillId", targetSkillId);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error checking equip status: " + www.error);
            }
            else
            {
                string equipStatus = www.downloadHandler.text;
                UpdateButtonUI(equipStatus == "1");

                // Show the panel only after the button text has been updated
                ShowEquipStatusPanel(equipStatus == "1");
            }
        }
    }

    IEnumerator ToggleEquipStatus()
    {
        // string url = "http://127.0.0.1:5050/skill_style/toggle_equip_status";
        string url = "http://140.122.185.169:5050/skill_style/toggle_equip_status";

        WWWForm form = new WWWForm();
        string token = userdata.token;
        // string token = "token123";
        Debug.Log("SkillstyleId inside toggle : " + skillStyleID);
        
        // int targetSkillId = skillStyleID;
        int targetSkillId = 1; //test
        Debug.Log("targetskillId = " + targetSkillId);

        form.AddField("tokenId", token);
        form.AddField("targetSkillId", targetSkillId);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error toggling equip status: " + www.error);
            }
            else
            {
                string equipStatus = www.downloadHandler.text;
                Debug.Log("Equip status after toggle: " + equipStatus);
                UpdateButtonUI(equipStatus == "1");
                ShowEquipStatusPanel(equipStatus == "1");
            }
        }
    }

    void UpdateButtonUI(bool isEquipped)
    {
        if (isEquipped)
        {
            equipButtonText.text = "Unequip";
        }
        else
        {
            equipButtonText.text = "Equip";
        }
    }

    void ShowEquipStatusPanel(bool isEquipped)
    {
        Debug.Log("bool isequipped : " + isEquipped);
        string message = isEquipped ? "Equipped successfully!" : "Unequipped successfully!";
        equipPanelText.text = message;
        equipPanel.SetActive(true);

        // Delay for 2 seconds and then hide the panel
        StartCoroutine(HidePanelAfterDelay());
    }

    IEnumerator HidePanelAfterDelay()
    {
        yield return new WaitForSeconds(2f); 
        equipPanel.SetActive(false);
    }
}
