using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSkill : MonoBehaviour
{
    public GameObject Skill;
    public static int skillIndex;
    public static int[] PlayerSkillIdList = {7,2,3};
    void Start()
    {
        skillIndex = 0;
        for(int i = 0;i<3;i++)
        {
            GameObject playerSkill  = Instantiate(Skill,gameObject.transform.position,gameObject.transform.rotation);
            playerSkill.transform.SetParent(gameObject.transform,false);
            playerSkill.layer = LayerMask.NameToLayer("Skill(Unused)");

        }
    }
   
}
