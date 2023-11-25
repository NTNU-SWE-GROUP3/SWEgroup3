using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseSkill : MonoBehaviour
{
    GameController GC;
    GameObject SkillObject;
    public static bool PlayerSkillForbidden;
    public static bool ComSkillForbidden;
    public GameObject PlayerArea;
    public GameObject OpponentArea;
    public GameObject SkillPanel;
    public GameObject SkipButton;
    public GameObject ConfirmButton;
    public GameObject CancelButton;
    SkillDisplay Skill;
    public static int Clock = 8;
    public Text TimerText;
    ShowCard SC;
    ToMessagePanel card;
    DeleteChange deleteChange;
    private int cardId = 0;
    void Start()
    {
        PlayerSkillForbidden = false;
        ComSkillForbidden = false;
        SC = GameObject.Find("GameController").GetComponent<ShowCard>();
        GC = GameObject.Find("GameController").GetComponent<GameController>();
        deleteChange = GameObject.Find("GameController").GetComponent<DeleteChange>();
    }
    public IEnumerator Timer()
    {
        Clock = 8;
        TimerText.gameObject.SetActive(true);
        SkipButton.gameObject.SetActive(true);
        while(Clock >= 0 )
        {
            TimerText.text = Clock.ToString();
            yield return new WaitForSeconds(1);
            Clock -- ;
        }
        TimerText.gameObject.SetActive(false);
        GC.DestoryCardOnPanel();
    }

    public IEnumerator Use(int skillId,bool isPlayer)
    {
        if( isPlayer == true)
        {
            for(int i = 0; i < 3;i++)
            {
               SkillObject = SkillPanel.transform.GetChild(i).gameObject;
               Skill = SkillObject.GetComponent<SkillDisplay>();
               if(SkillObject.layer == 13 && Skill.id == skillId)
               {
                    SkillObject.layer = LayerMask.NameToLayer("Skill(Used)");
                    break;
               }
            }
        //判斷技能的使用
            yield return new WaitForSeconds(0.5f);
            SkillPanel.gameObject.SetActive(false);

                switch (skillId)
            {
                case 1: //時間限縮
                    Debug.Log("Player Use Skill 1");
                    yield return new WaitForSeconds(1);
                    break;
                case 2: //階級流動
                    Debug.Log("Player Use Skill 2");
                    Clock = 8;
                    SC.skillMessage.text = "階級流動!";
                    SC.skillDescription.text = "請選擇一張要轉換的平民卡";
                    
                    for(int i = 0;i<PlayerArea.transform.childCount;i++)
                    {
                        card = PlayerArea.transform.GetChild(i).GetComponent<ToMessagePanel>();
                        if (PlayerArea.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().cardName == "平民")
                        {
                            card.CardShowOnMessagePanel(true);
                        }
                    }
                    break;
                case 3: //暗影轉職
                    Debug.Log("Player Use Skill 3");
                    Clock = 8;
                    SC.skillMessage.text = "暗影轉職!";
                    SC.skillDescription.text = "請選擇一張要轉換的平民卡";
                    
                    for(int i = 0;i<PlayerArea.transform.childCount;i++)
                    {
                        card = PlayerArea.transform.GetChild(i).GetComponent<ToMessagePanel>();
                        if (PlayerArea.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().cardName == "平民")
                        {
                            card.CardShowOnMessagePanel(true);
                        }
                    }
                    break;
                case 4: //技能封印
                    Debug.Log("Player Use Skill 4");
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    SC.skillMessage.text = "技能封印!";
                    SC.skillDescription.text = "下回合對手技能將被封印";
                    ComSkillForbidden = true;
                    yield return new WaitForSeconds(1);
                    break;
                case 5: //力量剝奪
                    Debug.Log("Player Use Skill 5");
                    Clock = 3;
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    SC.skillMessage.text = "力量剝奪!";
                    SC.skillDescription.text = "此回合對手民卡技能無效";
                    SC.isPlayerPeasantImmunity = true;
                    // yield return new WaitForSeconds(3);
                    break;
                case 6: //黃金風暴
                    Debug.Log("Player Use Skill 6");
                    yield return new WaitForSeconds(1);
                    break;
                case 7: //知己知彼
                    Debug.Log("Player Use Skill 7");
                    Clock = 8;
                    deckRecon();
                    break;
                case 8: //抉擇束縛
                    Debug.Log("Player Use Skill 8");
                    yield return new WaitForSeconds(1);
                    break;
                case 9: //強制徵收
                    Debug.Log("Player Use Skill 9");
                    Clock = 0;
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    SC.skillMessage.text = "強制徵收!";
                    SC.skillDescription.text = "對手贏牌區張數-1";
                    SC.OpponentX -= 1;
                    SC.RefreshEarnText(2);
                    yield return new WaitForSeconds(1);
                    break;
                case 10: //勝者之堆
                    Debug.Log("Player Use Skill 10");
                    Clock = 0;
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    SC.skillMessage.text = "勝者之堆!";
                    SC.skillDescription.text = "我方贏牌區張數+1";
                    SC.PlayerX += 1;
                    SC.RefreshEarnText(1);
                    break;
            }
            ConfirmButton.SetActive(false);
            CancelButton.SetActive(false);
        }
        else 
        {
                switch (skillId)
            {
                case 1: //時間限縮
                    Debug.Log("Opponent Use Skill 1");
                    yield return new WaitForSeconds(1);
                    break;
                case 2: //階級流動
                    Debug.Log("Opponent  Use Skill 2");
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    SC.skillMessage.text = "階級流動!";
                    SC.skillDescription.text = "請選擇一張要轉換的平民卡";
                    cardId = 0;
                    for(int i = OpponentArea.transform.childCount-1;i>=0;i--)
                    {
                        card = OpponentArea.transform.GetChild(i).GetComponent<ToMessagePanel>();
                        if (OpponentArea.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().cardName == "平民")
                        {
                            cardId = i;
                            deleteChange.Change(OpponentArea,cardId, "階級流動");
                            break;
                        }
                    }     
                    break;
                case 3: //暗影轉職
                    Debug.Log("Opponent  Use Skill 3");
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    SC.skillMessage.text = "暗影轉職!";
                    SC.skillDescription.text = "請選擇一張要轉換的平民卡";
                    for(int i = OpponentArea.transform.childCount-1;i>=0;i--)
                    {
                        card = OpponentArea.transform.GetChild(i).GetComponent<ToMessagePanel>();
                        if (OpponentArea.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().cardName == "平民")
                        {
                            cardId = i;
                            deleteChange.Change(OpponentArea,cardId, "暗影轉職");
                            break;
                        }
                    }     
                    cardId = 0;
                    break;
                case 4: //技能封印
                    Debug.Log("Opponent  Use Skill 4");
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    SC.skillMessage.text = "技能封印!";
                    SC.skillDescription.text = "下回合玩家技能將被封印";
                    PlayerSkillForbidden = true;
                    yield return new WaitForSeconds(3);
                    break;
                case 5: //力量剝奪
                    Debug.Log("Opponent  Use Skill 5");
                    Clock = 3;
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    SC.skillMessage.text = "力量剝奪!";
                    SC.skillDescription.text = "此回合對手民卡技能無效";
                    SC.isComPeasantImmunity = true;
                    // yield return new WaitForSeconds(3);
                    break;
                case 6: //黃金風暴
                    Debug.Log("Opponent  Use Skill 6");
                    yield return new WaitForSeconds(1);
                    break;
                case 7: //知己知彼
                    Debug.Log("Opponent  Use Skill 7");
                    Clock = 8;
                    deckRecon();
                    break;
                case 8: //抉擇束縛
                    Debug.Log("Opponent  Use Skill 8");
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    SC.skillMessage.text = "抉擇束縛!";
                    SC.skillDescription.text = "請從以下兩張牌中擇一出牌";
                    yield return StartCoroutine(Timer());
                    break;
                case 9: //強制徵收
                    Debug.Log("Opponent  Use Skill 9");
                    Clock = 0;
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    SC.skillMessage.text = "強制徵收!";
                    SC.skillDescription.text = "玩家贏牌區張數-1";
                    SC.PlayerX -= 1;
                    SC.RefreshEarnText(1);
                    yield return new WaitForSeconds(1);
                    break;
                case 10: //勝者之堆
                    Debug.Log("Opponent  Use Skill 10");
                    Clock = 0;
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    SC.skillMessage.text = "勝者之堆!";
                    SC.skillDescription.text = "對手贏牌區張數+1";
                    SC.OpponentX += 1;
                    SC.RefreshEarnText(2);
                    break;
            }
        }
    
    }
    void deckRecon()
    {
        SkipButton.SetActive(true);
        ToMessagePanel card;
        for(int i = 0;i<OpponentArea.transform.childCount;i++)
        {
            card = OpponentArea.transform.GetChild(i).GetComponent<ToMessagePanel>();
            card.CardShowOnMessagePanel(false);
        }
    }
}
