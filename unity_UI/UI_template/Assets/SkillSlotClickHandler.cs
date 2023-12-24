using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlotClickHandler : MonoBehaviour
{
    private string skillName;
    private string description;
    private float probability;
    public GameObject skillPopup;
    public Text SkillName;
    public Text skillDescription;

    public void SetSkillInfo(string name, string desc, float prob)
    {
        skillName = name;
        description = desc;
        probability = prob;
    }
    
    public void OnPointerClick()
    {
        skillPopup.SetActive(true);
        SkillName.text = this.GetComponent<SkillSlotScript>().skillName;
        skillDescription.text = this.GetComponent<SkillSlotScript>().skillDes;
        
    }
}