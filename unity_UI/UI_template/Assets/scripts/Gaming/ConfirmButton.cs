using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmButton : MonoBehaviour
{
    GameController GC;
    DeleteChange deleteChange;
    GameObject PlayerCardObject;
    Transform Card;
    public Text skillName;
    public GameObject OpponentArea;
    public GameObject PlayerArea;
    public GameObject PlayerShow;
    public GameObject MessagePanel;
    public GameObject Panel;
    public GameObject CancelButton;
    public UseSkill useSkill;

    public GameObject SkillPanel;
    GameObject SkillObject;
    SkillDisplay Skill;


    public static bool CardSelected;
    void Start()
    {
        GC = GameObject.Find("GameController").GetComponent<GameController>();
        deleteChange = GameObject.Find("GameController").GetComponent<DeleteChange>();
        gameObject.SetActive(false);
        CardSelected = false;
    }
    public void ClickConfirm()
    {
        if (skillName.text == "簡易剔除!")
        {
            deleteChange.Delete(OpponentArea,ClickDetector.cardId);
            ShowCard.RejectTimer = 1;
            MessagePanel.SetActive(false);
        }
        else if (skillName.text == "階級流動!")
        {
            deleteChange.Change(PlayerArea,ClickDetector.cardId, "階級流動");
            UseSkill.Clock= 1;
            MessagePanel.SetActive(false);
            GC.DestoryCardOnPanel();
        }
        else if(skillName.text == "暗影轉職!")
        {
            deleteChange.Change(PlayerArea,ClickDetector.cardId, "暗影轉職");
            UseSkill.Clock = 1;
            MessagePanel.SetActive(false);
            GC.DestoryCardOnPanel();
        }
        else if (skillName.text == "抉擇束縛!")
        {
            
            Debug.Log("抉擇束縛!");
            for (int i = 0; i < PlayerArea.transform.childCount; i++)
            {
                Card = PlayerArea.transform.GetChild(i);
                if(Card.gameObject.GetComponent<CardDisplay>().id == ClickDetector.cardId)
                {
                    Card.SetParent(PlayerShow.transform, false);
                    Card.position = PlayerShow.transform.position;
                    Card.gameObject.layer = LayerMask.NameToLayer("CardBack");
                    CardSelected = true;
                    break;
                }
            }
            UseSkill.Clock = 0;
        }
        else
        {
            Debug.Log("useSkill");
            GameController.PlayerSkillId = ClickDetector.skillId;
            for(int i = 0; i < SkillPanel.transform.childCount;i++)
            {
               SkillObject = SkillPanel.transform.GetChild(i).gameObject;
               Skill = SkillObject.GetComponent<SkillDisplay>();
               if(SkillObject.layer == 13 && Skill.id == ClickDetector.skillId)
               {
                    SkillObject.layer = LayerMask.NameToLayer("Skill(Used)");
                    break;
               }
            }
            UseSkill.Clock = 1;
            // StartCoroutine(useSkill.Use(ClickDetector.skillId,true));
            ClickDetector.skillId = -1;
        }
        
        
    }
}
