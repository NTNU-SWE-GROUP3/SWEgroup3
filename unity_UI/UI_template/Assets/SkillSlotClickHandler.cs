using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSlotClickHandler : MonoBehaviour, IPointerClickHandler
{
    private string skillName;
    private string description;
    private float probability;
    private SkillPopup skillPopup;

    public void SetSkillInfo(string name, string desc, float prob)
    {
        skillName = name;
        description = desc;
        probability = prob;
    }

    public void SetSkillPopup(SkillPopup popup)
    {
        skillPopup = popup;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (skillPopup != null)
        {
            skillPopup.ShowSkillInfo(skillName, description, probability);
        }
    }
}