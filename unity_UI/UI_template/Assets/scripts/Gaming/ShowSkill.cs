using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSkill : MonoBehaviour
{
    public GameObject Skill;
    public static int skillIndex;
    public static int[] PlayerSkillIdList = {2,3,4,5,7,8,9};
    void Start()
    {
        skillIndex = 0;
        for(int i = 0;i<PlayerSkillIdList.Length;i++)
        {
            GameObject playerSkill  = Instantiate(Skill,gameObject.transform.position,gameObject.transform.rotation);
            playerSkill.transform.SetParent(gameObject.transform,false);
            playerSkill.layer = LayerMask.NameToLayer("Skill(Unused)");

        }
    }
   
}
