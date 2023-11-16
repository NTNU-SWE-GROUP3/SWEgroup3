using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSkill : MonoBehaviour
{
    public GameObject Skill;
    public static int skillIndex;
    public static int[] PlayerSkillIdList = {3,4,5};
    void Start()
    {
        skillIndex = 0;
        for(int i = 0;i<3;i++)
        {
            Instantiate(Skill,gameObject.transform.position,gameObject.transform.rotation).transform.SetParent(gameObject.transform,false);
        }
    }
   
}
