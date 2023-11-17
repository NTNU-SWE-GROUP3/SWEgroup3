using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSkill : MonoBehaviour
{
    public GameObject SkillPanel;
    GameObject SkillObject;
    SkillDisplay Skill;
    public IEnumerator Use(int skillId)
    {
        for(int i = 0; i < 3;i++)
        {
           SkillObject = SkillPanel.transform.GetChild(i).gameObject;
           Skill = SkillObject.GetComponent<SkillDisplay>();
           if(Skill.id == skillId)
           {
                SkillObject.layer = LayerMask.NameToLayer("Skill(Used)");
                break;
           }
        }
        if (skillId == 3)
        Debug.Log("Player Use Skill 3");
        yield return new WaitForSeconds(1);
    }
}
