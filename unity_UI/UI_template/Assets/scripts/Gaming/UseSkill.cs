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
    ShowCard SC;
    void Start()
    {
        SC = GameObject.Find("GameController").GetComponent<ShowCard>();
    }
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
        switch (skillId)
        {
            case 1: //時間限縮
                Debug.Log("Player Use Skill 1");
                yield return new WaitForSeconds(1);
                break;
            case 2: //階級流動
                Debug.Log("Player Use Skill 2");
                yield return new WaitForSeconds(1);
                break;
            case 3: //暗影轉職
                Debug.Log("Player Use Skill 3");
                yield return new WaitForSeconds(1);
                break;
            case 4: //技能封印
                Debug.Log("Player Use Skill 4");
                yield return new WaitForSeconds(1);
                break;
            case 5: //力量剝奪
                Debug.Log("Player Use Skill 5");
                yield return new WaitForSeconds(1);
                break;
            case 6: //黃金風暴
                Debug.Log("Player Use Skill 6");
                yield return new WaitForSeconds(1);
                break;
            case 7: //知己知彼
                Debug.Log("Player Use Skill 7");
                yield return new WaitForSeconds(1);
                break;
            case 8: //抉擇束縛
                Debug.Log("Player Use Skill 8");
                yield return new WaitForSeconds(1);
                break;
            case 9: //強制徵收
                Debug.Log("Player Use Skill 9");
                SC.TriumphManipulation();
                yield return new WaitForSeconds(1);
                break;
            case 10: //勝者之堆
                Debug.Log("Player Use Skill 10");
                SC.VictoryBoost();
                yield return new WaitForSeconds(1);
                break;
        }
    }
}
