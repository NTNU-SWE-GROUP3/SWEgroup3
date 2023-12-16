using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSkill : MonoBehaviour
{
    public GameObject Skill;
    public static int skillIndex;
    public static List<int> PlayerSkillIdList = new List<int>();
    public IEnumerator ShowSkills()
    {
        PlayerSkillIdList.Add(7);
        skillIndex = 0;
        for(int i = 0;i<PlayerSkillIdList.Count;i++)
        {
            GameObject playerSkill  = Instantiate(Skill,gameObject.transform.position,gameObject.transform.rotation);
            playerSkill.transform.SetParent(gameObject.transform,false);
            playerSkill.layer = LayerMask.NameToLayer("Skill(Unused)");

        }
        yield return null;
    }
   
}
