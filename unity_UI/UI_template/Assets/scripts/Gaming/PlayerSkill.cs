using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class PlayerSkill
{
    public int SkillId;
    public string SkillName;
    public string SkillDescription;
    public Sprite SkillSprite;
    
    public PlayerSkill(int _skillId,string _skillName,string _skillDescription, Sprite _skillSprite)
    {
        SkillId = _skillId;
        SkillName = _skillName;
        SkillDescription = _skillDescription;
        SkillSprite =  _skillSprite;
    
    }

}