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
    int[] ComSkillIdList = {8,7,9};
    public static int ComSkillIndex;

    void Start()
    {
        ComSkillIndex = 0;
    }
    public IEnumerator PlayCard()
    {
        yield return new WaitForSeconds(4);
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
