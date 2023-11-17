using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseSkill : MonoBehaviour
{
    public GameObject SkillPanel;
    GameObject SkillObject;
    SkillDisplay Skill;
    int Clock = 8;
    public Text TimerText;

    public IEnumerator Timer()
    {
        Clock = 10;
        TimerText.gameObject.SetActive(true);
        while(Clock >= 0)
        {
            TimerText.text = Clock.ToString();
            yield return new WaitForSeconds(1);
            Clock -- ;
        }
        TimerText.gameObject.SetActive(false);
    }

    public IEnumerator Use(int skillId)
    {
        for(int i = 0; i < 3;i++)
        {
           SkillObject = SkillPanel.transform.GetChild(i).gameObject;
           Skill = SkillObject.GetComponent<SkillDisplay>();
           if(SkillObject.layer == 13 && Skill.id == skillId)
           {
                SkillObject.layer = LayerMask.NameToLayer("Skill(Used)");
                break;
           }
        }
        //判斷技能的使用
        if (skillId == 3)
        {
            Debug.Log("Player Use Skill 3");
            yield return new WaitForSeconds(1);
        }
        
    }
}
