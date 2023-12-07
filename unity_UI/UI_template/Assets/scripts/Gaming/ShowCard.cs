using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowCard : MonoBehaviour
{
    GameController GC;
    DeleteChange deletChange;
    public GameObject PlayerShow;
    public GameObject OpponentShow;
    public GameObject PlayerEarn;
    public GameObject OpponentEarn;
    public GameObject DrawArea;
    public GameObject PlayerArea;
    public GameObject OpponentArea;
    public Text skillMessage;
    public Text skillDescription;
    public Text WhoWins;
    public GameObject WinImage;
    public GameObject SkillImage;
    public Text PlayerEarnText;
    public Text OpponentEarnText;
    public bool isRevolution;
    public bool isPlayerPeasantImmunity;
    public bool isComPeasantImmunity;
    public int PlayerX;
    public int OpponentX;
    public static int RejectTimer;
    public Text RejectTimerText;
    GameObject PlayerCardObject;
    GameObject OpponentCardObject;
    CardDisplay PlayerCard;
    CardDisplay OpponentCard;
    CardDisplay CardDelete;
    Transform Card;

    public AudioClip WinSound;
    public AudioClip LoseSound;
    public AudioClip DrawSound;
    public AudioClip CardSound;
    public AudioClip SkillSound;
    public AudioClip MoveSound;

    AudioSource audioSource;

    void Start()
    {
        isPlayerPeasantImmunity = false;
        isComPeasantImmunity = false;
        RejectTimer = 8;
        isRevolution = false;
        RejectTimerText.gameObject.SetActive(false);
        GC = GameObject.Find("GameController").GetComponent<GameController>();
        deletChange = GameObject.Find("GameController").GetComponent<DeleteChange>();
    }
    public IEnumerator Show()
    {
        PlayerCardObject = PlayerShow.transform.GetChild(0).gameObject;
        OpponentCardObject = OpponentShow.transform.GetChild(0).gameObject;
        PlayerCardObject.layer = LayerMask.NameToLayer("Show");
        OpponentCardObject.layer = LayerMask.NameToLayer("Show");
        PlaySE(CardSound);
        PlayerCard = PlayerCardObject.GetComponent<CardDisplay>();
        OpponentCard = OpponentCardObject.GetComponent<CardDisplay>();
        // 判斷
        //-------------------------\\
        // 不敗的勇者
        // if ((PlayerCard.id == 9 && isComPeasantImmunity == true) && OpponentCard.id == 9 && isPlayerPeasantImmunity == false)
        //     yield return StartCoroutine(ToDrawArea());
        if (PlayerCard.id == 9 && isComPeasantImmunity == false)
           yield return StartCoroutine( Undefeated(1));
        else if (OpponentCard.id == 9 && isPlayerPeasantImmunity == false)
            yield return StartCoroutine(Undefeated(2));
        else
        {
            if (isComPeasantImmunity == true && PlayerCard.id == 9)
            {
                yield return StartCoroutine(PeasantImmunity());

            }
            else if (isPlayerPeasantImmunity == true && OpponentCard.id == 9)
            {
                yield return StartCoroutine(PeasantImmunity());
            }
            if (PlayerCard.cardName == "國王" && (OpponentCard.cardName == "王子" || OpponentCard.cardName == "騎士" || OpponentCard.cardName == "平民"))
            {
                if (isRevolution == false){
                    yield return StartCoroutine(ToPlayerEarn());// 玩家贏
                }
                else
                {
                    yield return StartCoroutine(ToOpponentEarn());// 對手贏
                }
                if (isRevolution == false && OpponentCard.id == 17)
                {
                    if (isPlayerPeasantImmunity == false)
                        yield return StartCoroutine(Trojan(OpponentEarn,PlayerEarn));
                    else
                         yield return StartCoroutine(PeasantImmunity());
                }
            }
            else if (OpponentCard.cardName == "國王" && (PlayerCard.cardName == "王子" || PlayerCard.cardName == "騎士" || PlayerCard.cardName == "平民"))
            {
                
                if (isRevolution == false){
                    yield return StartCoroutine(ToOpponentEarn());// 對手贏
                }
                else
                {
                    yield return StartCoroutine(ToPlayerEarn());// 玩家贏
                }
                if (isRevolution == false &&PlayerCard.id == 17)
                {
                    if (isComPeasantImmunity == false)
                        yield return StartCoroutine(Trojan(PlayerEarn,OpponentEarn));
                    else 
                         yield return StartCoroutine(PeasantImmunity());
                }
            }
            else if (PlayerCard.cardName == "皇后" && (OpponentCard.cardName == "國王" || OpponentCard.cardName == "騎士" || OpponentCard.cardName == "平民"))
            {
                if (isRevolution == false){
                   yield return StartCoroutine(ToPlayerEarn());// 玩家贏
                }
                else
                {
                    yield return StartCoroutine(ToOpponentEarn());// 對手贏
                }
                if (isRevolution == false &&OpponentCard.id == 17)
                {
                    if (isPlayerPeasantImmunity == false)
                        yield return StartCoroutine(Trojan(OpponentEarn,PlayerEarn));
                    else 
                         yield return StartCoroutine(PeasantImmunity());
                }
            }
            else if (OpponentCard.cardName == "皇后" && (PlayerCard.cardName == "國王" || PlayerCard.cardName == "騎士" || PlayerCard.cardName == "平民"))
            {
                if (isRevolution == false){
                    yield return StartCoroutine(ToOpponentEarn());// 對手贏
                }
                else
                {
                    yield return StartCoroutine(ToPlayerEarn());// 玩家贏
                }
                if (isRevolution == false &&PlayerCard.id == 17)
                {
                    if (isComPeasantImmunity == false)
                        yield return StartCoroutine(Trojan(PlayerEarn,OpponentEarn));
                    else 
                         yield return StartCoroutine(PeasantImmunity());
                }
            }
            else if (PlayerCard.cardName == "王子" && (OpponentCard.cardName == "皇后" || OpponentCard.cardName == "騎士" || OpponentCard.cardName == "平民"))
            {
                if (isRevolution == false){
                    yield return StartCoroutine(ToPlayerEarn());// 玩家贏
                }
                else
                {
                    yield return StartCoroutine(ToOpponentEarn());// 對手贏
                }
                if (isRevolution == false && OpponentCard.id == 17)
                {
                    if (isPlayerPeasantImmunity == false)
                        yield return StartCoroutine(Trojan(OpponentEarn,PlayerEarn));
                    else 
                         yield return StartCoroutine(PeasantImmunity());
                }
            }
            else if (OpponentCard.cardName == "王子" && (PlayerCard.cardName == "皇后" || PlayerCard.cardName == "騎士" || PlayerCard.cardName == "平民"))
            {
                if (isRevolution == false){
                    yield return StartCoroutine(ToOpponentEarn());// 對手贏
                }
                else
                {
                    yield return StartCoroutine(ToPlayerEarn());// 玩家贏
                }
                if (isRevolution == false && PlayerCard.id == 17)
                {
                    if (isComPeasantImmunity == false)
                        yield return StartCoroutine(Trojan(PlayerEarn,OpponentEarn));
                    else 
                         yield return StartCoroutine(PeasantImmunity());
                }
            }
            else if (PlayerCard.cardName == "騎士" && (OpponentCard.cardName == "殺手" || OpponentCard.cardName == "平民"))
            {
                
                if (isRevolution == false){
                    yield return StartCoroutine(ToPlayerEarn());// 玩家贏
                }
                else
                {
                    yield return StartCoroutine(ToOpponentEarn());// 對手贏
                }
                if (isRevolution == false && OpponentCard.id == 17)
                {
                    if (isPlayerPeasantImmunity == false)
                        yield return StartCoroutine(Trojan(OpponentEarn,PlayerEarn));
                    else 
                         yield return StartCoroutine(PeasantImmunity());
                }
            }
            else if (OpponentCard.cardName == "騎士" && (PlayerCard.cardName == "殺手" || PlayerCard.cardName == "平民"))
            {
                if (isRevolution == false){
                    yield return StartCoroutine(ToOpponentEarn()); // 對手贏
                }
                else
                {
                    yield return StartCoroutine(ToPlayerEarn());// 玩家贏
                }
                if (isRevolution == false && PlayerCard.id == 17)
                {
                    if (isComPeasantImmunity == false)
                        yield return StartCoroutine(Trojan(PlayerEarn,OpponentEarn));
                    else 
                         yield return StartCoroutine(PeasantImmunity());
                }
            }
            else if (PlayerCard.cardName == "殺手" && (OpponentCard.cardName == "國王" || OpponentCard.cardName == "王子" || OpponentCard.cardName == "皇后"))
            {
                
                if (isRevolution == false)
                {
                    yield return StartCoroutine(ToPlayerEarn());// 玩家贏
                }
                else
                {
                    yield return StartCoroutine(ToOpponentEarn());// 對手贏
                }
                
            }
            else if (OpponentCard.cardName == "殺手" && (PlayerCard.cardName == "國王" || PlayerCard.cardName == "王子" || PlayerCard.cardName == "皇后"))
            {
                if (isRevolution == false)
                {
                   yield return StartCoroutine(ToOpponentEarn());// 對手贏
                }
                else
                {
                    yield return StartCoroutine(ToPlayerEarn());// 玩家贏
                }
                if (isComPeasantImmunity == true)
                    isComPeasantImmunity = false;
                if (isPlayerPeasantImmunity == true)
                    isPlayerPeasantImmunity = false;
            }
            else
            {
                // 平手
                yield return StartCoroutine(ToDrawArea());
                WinImage.SetActive(false);
                SkillImage.SetActive(true);
                skillMessage.gameObject.SetActive(true);
                skillDescription.gameObject.SetActive(true);
                // 大革命
                if (PlayerCard.id == 16 || OpponentCard.id == 16)
                {
                    if (isPlayerPeasantImmunity == true && OpponentCard.id == 16)
                         yield return StartCoroutine(PeasantImmunity());
                    else if (isComPeasantImmunity == true && PlayerCard.id == 16)
                         yield return StartCoroutine(PeasantImmunity());
                    else
                    {
                        PlaySE(SkillSound);
                        skillMessage.text = "大革命!";
                        skillDescription.text = "從此回合卡牌強弱翻轉";
                        isRevolution = true;
                    }
                    yield return new WaitForSeconds(3f);
                }
                // 爆發式成長
                if (PlayerCard.id == 15)
                {
                    if (isComPeasantImmunity == true)
                         yield return StartCoroutine(PeasantImmunity());
                    else
                    {
                        PlaySE(SkillSound);
                        skillMessage.text = "爆發式成長!";
                        skillDescription.text = "玩家獲得 "+ GameController.Turn.ToString() + " 張牌";
                        PlayerX = GameController.Turn;
                        RefreshEarnText(1);
                        yield return new WaitForSeconds(3f);
                    }
                }
                if (OpponentCard.id == 15)
                {
                    if (isPlayerPeasantImmunity == true)
                         yield return StartCoroutine(PeasantImmunity());
                    else
                    {
                        PlaySE(SkillSound);
                        skillMessage.text = "爆發式成長!";
                        skillDescription.text = "對手獲得 "+ GameController.Turn.ToString() + " 張牌";
                        OpponentX = GameController.Turn;
                        RefreshEarnText(2);
                    }
                    yield return new WaitForSeconds(3f);
                }
                // 全部重製
                if (PlayerCard.id == 8 && OpponentEarn.transform.childCount != 0)
                {
                    if (isComPeasantImmunity == true)
                         yield return StartCoroutine(PeasantImmunity());
                    else
                    {
                        PlaySE(SkillSound);
                        skillMessage.text = "全部重置!";
                        skillDescription.text = "將對手全部贏牌移至平手區";
                        yield return StartCoroutine(ResetAll(OpponentEarn));
                    }
                }
                if (OpponentCard.id == 8 && PlayerEarn.transform.childCount != 0)
                {
                    if (isPlayerPeasantImmunity == true)
                         yield return StartCoroutine(PeasantImmunity());
                    else
                    {
                        PlaySE(SkillSound);
                        skillMessage.text = "全部重置!";
                        skillDescription.text = "玩家全部贏牌移至平手區";
                    }
                    yield return StartCoroutine(ResetAll(PlayerEarn));
                }
                // 簡易剔除
                if (PlayerCard.id == 7)
                {
                    if (isComPeasantImmunity == true)
                         yield return StartCoroutine(PeasantImmunity());
                    else
                    {
                        PlaySE(SkillSound);
                        skillMessage.text = "簡易剔除!";
                        skillDescription.text = "請選擇一張牌剔除";
                        PlayerSimpleRejection();
                        RejectTimerText.gameObject.SetActive(true);
                        while(RejectTimer >= 0)
                        {
                            RejectTimerText.text = RejectTimer.ToString();
                            yield return new WaitForSeconds(1);
                            RejectTimer -- ;
                        }
                        RejectTimerText.gameObject.SetActive(false);
                    }
                }
                if(OpponentCard.id == 7)
                {
                    if (isPlayerPeasantImmunity == true)
                         yield return StartCoroutine(PeasantImmunity());
                    else
                    {
                        PlaySE(SkillSound);
                        skillMessage.text = "簡易剔除!";
                        skillDescription.text = "對手將選擇一張牌剔除";
                        OpponentSimpleRejection();
                    }
                    yield return new WaitForSeconds(3f);
                }
            }
        }
        if (isComPeasantImmunity == true)
            isComPeasantImmunity = false;
        if (isPlayerPeasantImmunity == true)
            isPlayerPeasantImmunity = false;
        //-------------------------\\
        RefreshEarnText(2);
        RefreshEarnText(1);
        GC.FinishCheck(PlayerEarn.transform.childCount + PlayerX, OpponentEarn.transform.childCount + OpponentX, PlayerArea.transform.childCount, OpponentArea.transform.childCount);

    }
    // 玩家贏
    IEnumerator ToPlayerEarn()
    {
        SkillImage.SetActive(false);
        WinImage.SetActive(true);
        WhoWins.gameObject.SetActive(true);

        WhoWins.text = "你贏了!";
        //DrawArea有牌 => 移至PlayerEarn
        yield return new WaitForSeconds(1);
        PlaySE(WinSound);
        for (; DrawArea.transform.childCount > 0;)
        {
            Card = DrawArea.transform.GetChild(DrawArea.transform.childCount - 1);
            Card.SetParent(PlayerEarn.transform, false);
            Card.position = PlayerEarn.transform.position;
            // 玩家贏牌顯示
            RefreshEarnText(1);
            PlaySE(MoveSound);
            yield return new WaitForSeconds(0.5f);
        }

        //兩張卡移至PlayerEarn
        yield return new WaitForSeconds(0.5f);
        PlayerCardObject.transform.SetParent(PlayerEarn.transform, false);
        PlayerCardObject.transform.position = PlayerEarn.transform.position;
        RefreshEarnText(1);
        PlaySE(MoveSound);
        yield return new WaitForSeconds(0.5f);
        OpponentCardObject.transform.SetParent(PlayerEarn.transform, false);
        OpponentCardObject.transform.position = PlayerEarn.transform.position;
        RefreshEarnText(1);
        PlaySE(MoveSound);
        yield return new WaitForSeconds(0.5f);
    }
    // 對手贏
    IEnumerator ToOpponentEarn()
    {
        SkillImage.SetActive(false);
        WinImage.SetActive(true);
        WhoWins.gameObject.SetActive(true);
        WhoWins.text = "你輸了!";
        //DrawArea有牌 => 移至OpponentEarn
        yield return new WaitForSeconds(1);
        PlaySE(LoseSound);
        for (; DrawArea.transform.childCount > 0;)
        {
            Card = DrawArea.transform.GetChild(DrawArea.transform.childCount - 1);
            Card.SetParent(OpponentEarn.transform, false);
            Card.position = OpponentEarn.transform.position;
            // 對手贏牌顯示
            RefreshEarnText(2);
            PlaySE(MoveSound);
            yield return new WaitForSeconds(0.5f);
        }
        //兩張卡移至OpponentEarn
        yield return new WaitForSeconds(0.5f);
        PlayerCardObject.transform.SetParent(OpponentEarn.transform, false);
        PlayerCardObject.transform.position = OpponentEarn.transform.position;
        RefreshEarnText(2);
        PlaySE(MoveSound);
        yield return new WaitForSeconds(0.5f);
        OpponentCardObject.transform.SetParent(OpponentEarn.transform, false);
        OpponentCardObject.transform.position = OpponentEarn.transform.position;
        RefreshEarnText(2);
        PlaySE(MoveSound);
        yield return new WaitForSeconds(0.5f);
    }
    //玩家觸發全部重置
    IEnumerator ResetAll(GameObject WhoLoss)
    {
        for (; WhoLoss.transform.childCount != 0;)
        {
            Card = WhoLoss.transform.GetChild(WhoLoss.transform.childCount - 1);
            Card.SetParent(DrawArea.transform, false);
            Card.position = DrawArea.transform.position;
            RefreshEarnText(2);
            RefreshEarnText(1);
            PlaySE(MoveSound);
            yield return new WaitForSeconds(0.5f);
            
        }
        PlayerCardObject.transform.SetParent(DrawArea.transform, false);
        OpponentCardObject.transform.SetParent(DrawArea.transform, false);

    }
    //玩家簡易剔除
    void PlayerSimpleRejection()
    {
        ToMessagePanel card;
        for(int i = 0;i<OpponentArea.transform.childCount;i++)
        {
            card = OpponentArea.transform.GetChild(i).GetComponent<ToMessagePanel>();
            card.CardShowOnMessagePanel(true);
        }

    }
    void OpponentSimpleRejection()
    {
        
        if(GameController.isCom == true)
        {
            CardDelete = PlayerArea.transform.GetChild(0).gameObject.GetComponent<CardDisplay>();
            deletChange.Delete(PlayerArea,CardDelete.id);
        }
        skillDescription.text = "對方替除了你的"+ CardDelete.cardName;

        
    }
    IEnumerator PeasantImmunity()
    {
        Debug.Log("力量剝奪 不觸發技能");
        WinImage.SetActive(false);
        SkillImage.SetActive(true);
        skillMessage.gameObject.SetActive(true);
        skillDescription.gameObject.SetActive(true);
        skillMessage.text = "力量剝奪!";
        skillDescription.text = "此回合對方平民卡技能無效";
        yield return new WaitForSeconds(2f);
        SkillImage.SetActive(false);
    }
    
    IEnumerator Undefeated(int WhoUse)
    {
        WinImage.SetActive(false);
        SkillImage.SetActive(true);
        skillMessage.gameObject.SetActive(true);
        skillDescription.gameObject.SetActive(true);
        skillMessage.text = "不敗的勇者!";
        skillDescription.text = "可以贏過任何一張牌";
        yield return new WaitForSeconds(0.5f);
        PlaySE(SkillSound);
        yield return new WaitForSeconds(2.5f);
        if(WhoUse == 1 )
            yield return StartCoroutine(ToPlayerEarn());
        else
            yield return StartCoroutine(ToOpponentEarn());
        
    }
    IEnumerator Trojan(GameObject WhoEarn,GameObject WhoLoss)
    {
        WinImage.SetActive(false);
        SkillImage.SetActive(true);
        skillMessage.gameObject.SetActive(true);
        skillDescription.gameObject.SetActive(true);
        skillMessage.text = "特洛伊木馬!";
        skillDescription.text = "贏得對手一半贏牌";
        double halfOfCards = Mathf.Ceil(WhoLoss.transform.childCount / 2);
        Debug.Log(halfOfCards);
        for(;WhoLoss.transform.childCount > halfOfCards;)
        {
            Card = WhoLoss.transform.GetChild(WhoLoss.transform.childCount - 1);
            Card.SetParent(WhoEarn.transform,false);
            Card.position = WhoEarn.transform.position;
            // 玩家贏牌顯示
            RefreshEarnText(2);
            RefreshEarnText(1);
            PlaySE(MoveSound);
            yield return new WaitForSeconds(0.5f);
        }
         yield return new WaitForSeconds(2f);
    }
    // 平手
    IEnumerator ToDrawArea()
    {
        WinImage.SetActive(true);
        WhoWins.gameObject.SetActive(true);
        WhoWins.text = "平手!";
        skillDescription.gameObject.SetActive(false);
        skillMessage.gameObject.SetActive(false);
        //兩張卡移至DrawArea
        yield return new WaitForSeconds(1f);
        
        PlaySE(DrawSound);
        PlayerCardObject.transform.SetParent(DrawArea.transform, false);
        RefreshEarnText(1);
        OpponentCardObject.transform.SetParent(DrawArea.transform, false);
        RefreshEarnText(2);
        PlaySE(MoveSound);
        yield return new WaitForSeconds(1);
    }
    public void PlaySE(AudioClip clip)
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
    }

    public void RefreshEarnText(int who)//刷新EarnText
    {
        if (who == 1) // Player
        {
            PlayerEarnText.text  = (PlayerEarn.transform.childCount + PlayerX).ToString();
        }
        else //Opponent
        {
            OpponentEarnText.text  = (OpponentEarn.transform.childCount + OpponentX).ToString();
        }
    }
}