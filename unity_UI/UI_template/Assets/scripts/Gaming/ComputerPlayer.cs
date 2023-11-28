using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ComputerPlayer : MonoBehaviour
{
    public GameObject OpponentArea;
    public GameObject OpponentShow;
    public Transform Card;
    public UseSkill useSkill;
    public int test = 0;
    int[] ComSkillIdList = {3,8,9};
    public static int ComSkillIndex;
    public static bool isdilemmaDictator;
    void Start()
    {
        ComSkillIndex = 0;
        isdilemmaDictator = false;
    }
    public IEnumerator PlayCard()
    {
        yield return new WaitForSeconds(4);
        if (isdilemmaDictator == false)
        {
            //int randomIndex = Random.Range(0,OpponentArea.transform.childCount);
            //Card = OpponentArea.transform.GetChild(randomIndex);
            if(test == 0){
                Card = OpponentArea.transform.GetChild(0);
                test++;
            }
            else
            {
                Card = OpponentArea.transform.GetChild(OpponentArea.transform.childCount-1);
            }
        }
        else 
        {
            int randomIndex = Random.Range(0,1);
            Card = OpponentArea.transform.GetChild(UseSkill.dilemmaDictatorIndex[randomIndex]);
        }
        Card.SetParent(OpponentShow.transform,false);
        Card.position = OpponentShow.transform.position;
        Card.gameObject.layer = LayerMask.NameToLayer("CardBack");
    }
    public IEnumerator ToUseSkill()
    {
        yield return new WaitForSeconds(2f);
        yield return(StartCoroutine(useSkill.Use(ComSkillIdList[ComSkillIndex],false)));
        ComSkillIndex ++;
    
    }

}
