using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EquipButtonController : MonoBehaviour
{
    private int skillStyleID;
    public GameObject skillSprite;
    public Button equipButton;
    public Text equipButtonText;
    public GameObject equipPanel;
    public Text equipPanelText; 
    private DontDestroy userdata;
    private GameObject lastClickedSprite;

    private void Start()
    {
        userdata = FindObjectOfType<DontDestroy>();
        equipButton.onClick.AddListener(OnEquipButtonClick);
    }

    // Call this method to set the current skillStyleID
    public void SetSkillStyleID()
    {
        skillStyleID = skillSprite.GetComponent<SkillSlotScript>().skillStyleID;
        Debug.Log("NEWWWW SkillStyleID : " + skillStyleID);
        StartCoroutine(CheckEquipStatusAndUpdateUI());
    }

    public void OnPointerClick(GameObject clickedSprite)
    {
        lastClickedSprite = clickedSprite;
        skillSprite = lastClickedSprite;
        skillStyleID = skillSprite.GetComponent<SkillSlotScript>().skillStyleID;
        Debug.Log("CURR CLICKED SPRITE ID : " + skillStyleID);
        StartCoroutine(CheckEquipStatusAndUpdateUI());
    }

    public void OnEquipButtonClick()
    {
        SetSkillStyleID();
        StartCoroutine(ToggleEquipStatus());
    }

    IEnumerator CheckEquipStatusAndUpdateUI()
    {
        string url = "http://127.0.0.1:5050/skill_style/check_equip_status";
        // string url = "http://140.122.185.169:5050/skill_style/check_equip_status";

        WWWForm form = new WWWForm();
        int targetSkillId = skillStyleID;
        // Debug.Log("targetskillId in check equip status = " + targetSkillId);
        // int targetSkillId = 1;
        // string token = userdata.token;
        string token = "token123";
        
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
            }
        }
    }

    IEnumerator ToggleEquipStatus()
    {
        string url = "http://127.0.0.1:5050/skill_style/toggle_equip_status";
        // string url = "http://140.122.185.169:5050/skill_style/toggle_equip_status";

        if (lastClickedSprite != null)
        {
            skillSprite = lastClickedSprite;
            int targetSkillId = skillSprite.GetComponent<SkillSlotScript>().skillStyleID;
            Debug.Log("ToggleEquipStatus: targetskillId on toggelequipstatus = " + targetSkillId);
            WWWForm form = new WWWForm();
            // string token = userdata.token;
            string token = "token123";

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
                    if(equipStatus == "2")
                    {
                        ShowMaxSkillsPanel();
                    }
                    else
                    {
                        Debug.Log("Equip status after toggle: " + equipStatus);
                        UpdateButtonUI(equipStatus == "1");
                        ShowEquipStatusPanel(equipStatus == "1");
                    }
                }
            }
        }
        else
        {
            Debug.LogError("ToggleEquipStatus: No sprite clicked.");
        }
        
    }
    void UpdateButtonUI(bool isEquipped)
    {
        if (isEquipped)
        {
            equipButtonText.text = "取消裝備";
        }
        else
        {
            equipButtonText.text = "裝備";
        }
    }

    void ShowEquipStatusPanel(bool isEquipped)
    {
        Debug.Log("bool isequipped : " + isEquipped);
        string message = isEquipped ? "裝備成功!" : "解除裝備成功!";
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

    void ShowMaxSkillsPanel()
    {
        // Display a panel message indicating that the maximum number of skills is reached
        equipPanelText.text = "最多可裝備3個技能!";
        equipPanel.SetActive(true);

        // Delay for 2 seconds and then hide the panel
        StartCoroutine(HidePanelAfterDelay());
    }
}
