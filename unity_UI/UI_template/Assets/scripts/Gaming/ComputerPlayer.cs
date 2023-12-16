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
    int[] ComSkillIdList = {4,5,6};
    int[] skills = {1,2,3,4,5,7,8,9,10};
    public static int ComSkillIndex;
    int randomTime;
    int useSkillOrNot;
    
    void Start()
    {
        
        for (int i = 0; i < 9; i++)
        {
            int temp = skills[i];
            int rand = Random.Range(i, 9);
            skills[i] = skills[rand];
            skills[rand] = temp;
        }
        for (int i = 0; i < 3; i++)
        {
            ComSkillIdList[i] = skills[i];
        }
        Debug.Log("ComSKill" + string.Join(", ", ComSkillIdList));
        Random.InitState((int)System.DateTime.Now.Ticks);
        ComSkillIndex = 0;
    }
    public IEnumerator PlayCard()
    {
        yield return new WaitForSeconds(4);
        if (UseSkill.ComIsdilemmaDictator == false)
        {
            int randomIndex = Random.Range(0,OpponentArea.transform.childCount);
            Card = OpponentArea.transform.GetChild(randomIndex);
            // if(test == 0){
            //     Card = OpponentArea.transform.GetChild(0);
            //     test++;
            // }
            // else
            // {
            //     Card = OpponentArea.transform.GetChild(OpponentArea.transform.childCount-1);
            // }
        }
        else 
        {
            int randomIndex = Random.Range(0,1);
            for(int i = 0;i<OpponentArea.transform.childCount;i++)
            {
                if (OpponentArea.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().id == UseSkill.ComDilemmaDictatorId[randomIndex])
                {
                    Card = OpponentArea.transform.GetChild(i);
                    UseSkill.ComIsdilemmaDictator = false;
                }
            }
        }
        Card.SetParent(OpponentShow.transform,false);
        Card.position = OpponentShow.transform.position;
        Card.gameObject.layer = LayerMask.NameToLayer("CardBack");
    }
    public IEnumerator ToUseSkill()
    {
        
        randomTime = Random.Range(30, 80);
        useSkillOrNot = Random.Range(1,100);
        yield return new WaitForSeconds((float)(randomTime/10));
        if(useSkillOrNot % 2 == 0)
        {
            GameController.OpponentSkillId = ComSkillIdList[ComSkillIndex];
            ComSkillIndex ++;
        }
        // yield return(StartCoroutine(useSkill.Use(ComSkillIdList[ComSkillIndex],false)));
        GameController.OpponentFUS = true;
        Debug.Log("Opponent Finish choosing skill");
    
    }

}
