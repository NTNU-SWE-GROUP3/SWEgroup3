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
    int[] ComSkillIdList = {8,6,9};
    public static int ComSkillIndex;
    int randomTime;
    int useSkillOrNot;
    
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        ComSkillIndex = 0;
    }
    public IEnumerator PlayCard()
    {
        yield return new WaitForSeconds(4);
        if (UseSkill.ComIsdilemmaDictator == false)
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
            UseSkill.PlayerIsdilemmaDictator = false;
        }
        Card.SetParent(OpponentShow.transform,false);
        Card.position = OpponentShow.transform.position;
        Card.gameObject.layer = LayerMask.NameToLayer("CardBack");
    }
    public IEnumerator ToUseSkill()
    {
        
        randomTime = Random.Range(30, 80);
        // useSkillOrNot = Random.Range(1,100);
        useSkillOrNot = 2;
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
