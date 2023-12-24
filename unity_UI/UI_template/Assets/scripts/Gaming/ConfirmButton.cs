using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfirmButton : MonoBehaviour
{
    GameController GC;
    DeleteChange deleteChange;
    GameObject PlayerCardObject;
    Transform Card;
    public Text skillName;
    public GameObject SkipButton;
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

    private DontDestroy userdata;

    public static bool CardSelected;
    void Start()
    {
        GC = GameObject.Find("GameController").GetComponent<GameController>();
        deleteChange = GameObject.Find("GameController").GetComponent<DeleteChange>();
        gameObject.SetActive(false);
        CardSelected = false;
        userdata = FindObjectOfType<DontDestroy>();
    }

    public IEnumerator SendSkillCard(int skillId)
    {
        SkillSelection gs = gameObject.AddComponent<SkillSelection>();
        gs.gameType = userdata.gameType;
        gs.roomId = userdata.roomId;
        gs.playerToken = userdata.token;
        gs.playerSkillID = skillId;
        gs.cardId = ClickDetector.cardId;

        Debug.Log("useSkill");
        CoroutineWithData cd = new CoroutineWithData(this, Flask.SendRequest(gs.SaveToString(),"useSkill"));
        yield return cd.coroutine;
        Debug.Log("return : " + cd.result);

        string retString = cd.result.ToString();
        SkillMsgBack ret = new SkillMsgBack();
        if (retString == "ConnectionError" || retString == "ProtocolError" || retString == "InProgress" || retString == "DataProcessingError")
        {
            Debug.Log("ConfirmButton:" + retString);
            SceneManager.LoadScene(0);
        }
        else
        {
            ret = SkillMsgBack.CreateFromJSON(cd.result.ToString());
        }

        if(ret.OpponentSkillId == -1)
        {
            Debug.Log("ConfirmButton:" + ret.errMessage);
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("ConfirmButton:" + ret.errMessage);
        }
    }

    public void ClickConfirm()
    {
        
        int gameType = CountDown.gameType;
        if (skillName.text == "簡易剔除!")
        {
            if(gameType == 0)
            {
                StartCoroutine(SendSkillCard(11));
            }

            ShowCard.isEasyDelete = false;
            deleteChange.Delete(OpponentArea,ClickDetector.cardId);
            ShowCard.RejectTimer = 0;
            MessagePanel.SetActive(false);
        }
        else if (skillName.text == "階級流動!")
        {
            if(gameType == 0)
            {
                StartCoroutine(SendSkillCard(2));
            }
            

            StartCoroutine(deleteChange.Change(PlayerArea,ClickDetector.cardId, "階級流動"));
            UseSkill.Clock= 0;
            // MessagePanel.SetActive(false);
            GC.DestoryCardOnPanel();
        }
        else if(skillName.text == "暗影轉職!")
        {
            if(gameType == 0)
            {
                StartCoroutine(SendSkillCard(3));
            }

            StartCoroutine(deleteChange.Change(PlayerArea,ClickDetector.cardId, "暗影轉職"));
            UseSkill.Clock = 0;
            // MessagePanel.SetActive(false);
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

            /*if(gameType == 0)
            {
                StartCoroutine(SendSkillCard(8));
            }*/
            UseSkill.Clock = 0;
        }
        else
        {
            Debug.Log("useSkill");
            GameController.PlayerSkillId = ClickDetector.skillId;
            UseSkill.Clock = 0;
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
            
            // StartCoroutine(useSkill.Use(ClickDetector.skillId,true));
            ClickDetector.skillId = -1;
        }
        
        gameObject.GetComponent<Button>().interactable = false;
    }
}
