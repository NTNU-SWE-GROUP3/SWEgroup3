using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillDisplay : MonoBehaviour
{
    public List<PlayerSkill> displaySkill = new List<PlayerSkill>();

    public int skillId;
    public string skillDescription;
    public Sprite skillSprite;
    
    public Image skillImage;

    void Start()
    {
        displaySkill[0] = SkillDatabase.SkillList[0];
        skillId = displaySkill[0].SkillId;
        skillDescription = displaySkill[0].SkillDescription;
        skillSprite = displaySkill[0].SkillSprite;

        skillImage.sprite = skillSprite;
    }

    
}
