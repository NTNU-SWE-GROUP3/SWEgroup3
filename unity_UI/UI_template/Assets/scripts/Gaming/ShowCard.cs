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
    public int PlayerX;
    public int OpponentX;
    public static int RejectTimer;
    public Text RejectTimerText;
    GameObject PlayerCardObject;
    GameObject OpponentCardObject;
    CardDisplay PlayerCard;
    CardDisplay OpponentCard;
    Transform Card;

    void Start()
    {
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
        PlayerCard = PlayerCardObject.GetComponent<CardDisplay>();
        OpponentCard = OpponentCardObject.GetComponent<CardDisplay>();
        // 判斷
        //-------------------------\\
        // 不敗的勇者
        if (PlayerCard.id == 9 || OpponentCard.id == 9)
        {
            WinImage.SetActive(false);
            SkillImage.SetActive(true);
            skillMessage.gameObject.SetActive(true);
            skillDescription.gameObject.SetActive(true);
            skillMessage.text = "不敗的勇者!";
            skillDescription.text = "可以贏過任何一張牌";
            yield return new WaitForSeconds(3f);
            if(PlayerCard.id == 9 )
                StartCoroutine(ToPlayerEarn());
            else
                StartCoroutine(ToOpponentEarn());
            yield return new WaitForSeconds(3f);
        }
        else
        {

            if (PlayerCard.cardName == "國王" && (OpponentCard.cardName == "王子" || OpponentCard.cardName == "騎士" || OpponentCard.cardName == "平民"))
            {
                if (isRevolution == false){
                    StartCoroutine(ToPlayerEarn());// 玩家贏
                }
                else
                {
                    StartCoroutine(ToOpponentEarn());// 對手贏
                }
                yield return new WaitForSeconds(3f);
                if (OpponentCard.id == 17)
                {
                    StartCoroutine(Trojan(OpponentEarn,PlayerEarn));
                    yield return new WaitForSeconds(3f);
                }
            }
            else if (OpponentCard.cardName == "國王" && (PlayerCard.cardName == "王子" || PlayerCard.cardName == "騎士" || PlayerCard.cardName == "平民"))
            {
                
                if (isRevolution == false){
                    StartCoroutine(ToOpponentEarn());// 對手贏
                }
                else
                {
                    StartCoroutine(ToPlayerEarn());// 玩家贏
                }
                yield return new WaitForSeconds(3f);
                if (PlayerCard.id == 17)
                {
                    StartCoroutine(Trojan(PlayerEarn,OpponentEarn));
                    yield return new WaitForSeconds(3f);
                }
            }
            else if (PlayerCard.cardName == "皇后" && (OpponentCard.cardName == "國王" || OpponentCard.cardName == "騎士" || OpponentCard.cardName == "平民"))
            {
                if (isRevolution == false){
                    StartCoroutine(ToPlayerEarn());// 玩家贏
                }
                else
                {
                    StartCoroutine(ToOpponentEarn());// 對手贏
                }
                yield return new WaitForSeconds(3f);
                if (OpponentCard.id == 17)
                {
                    StartCoroutine(Trojan(OpponentEarn,PlayerEarn));
                    yield return new WaitForSeconds(3f);// 特洛伊木馬
                }
            }
            else if (OpponentCard.cardName == "皇后" && (PlayerCard.cardName == "國王" || PlayerCard.cardName == "騎士" || PlayerCard.cardName == "平民"))
            {
                if (isRevolution == false){
                    StartCoroutine(ToOpponentEarn());// 對手贏
                }
                else
                {
                    StartCoroutine(ToPlayerEarn());// 玩家贏
                }
                yield return new WaitForSeconds(3f);
                if (PlayerCard.id == 17)
                {
                    StartCoroutine(Trojan(PlayerEarn,OpponentEarn));
                    yield return new WaitForSeconds(3f);
                }
            }
            else if (PlayerCard.cardName == "王子" && (OpponentCard.cardName == "皇后" || OpponentCard.cardName == "騎士" || OpponentCard.cardName == "平民"))
            {
                if (isRevolution == false){
                    StartCoroutine(ToPlayerEarn());// 玩家贏
                }
                else
                {
                    StartCoroutine(ToOpponentEarn());// 對手贏
                }
                yield return new WaitForSeconds(3f);// 特洛伊木馬
                if (OpponentCard.id == 17)
                {
                    StartCoroutine(Trojan(OpponentEarn,PlayerEarn));
                    yield return new WaitForSeconds(3f);
                }
            }
            else if (OpponentCard.cardName == "王子" && (PlayerCard.cardName == "皇后" || PlayerCard.cardName == "騎士" || PlayerCard.cardName == "平民"))
            {
                if (isRevolution == false){
                    StartCoroutine(ToOpponentEarn());// 對手贏
                }
                else
                {
                    StartCoroutine(ToPlayerEarn());// 玩家贏
                }
                yield return new WaitForSeconds(3f);// 特洛伊木馬
                if (PlayerCard.id == 17)
                {
                    StartCoroutine(Trojan(PlayerEarn,OpponentEarn));
                    yield return new WaitForSeconds(3f);
                }
            }
            else if (PlayerCard.cardName == "騎士" && (OpponentCard.cardName == "殺手" || OpponentCard.cardName == "平民"))
            {
                
                if (isRevolution == false){
                    StartCoroutine(ToPlayerEarn());// 玩家贏
                }
                else
                {
                    StartCoroutine(ToOpponentEarn());// 對手贏
                }
                yield return new WaitForSeconds(3f);
                if (OpponentCard.id == 17)
                {
                    StartCoroutine(Trojan(OpponentEarn,PlayerEarn));
                    yield return new WaitForSeconds(3f);
                }
            }
            else if (OpponentCard.cardName == "騎士" && (PlayerCard.cardName == "殺手" || PlayerCard.cardName == "平民"))
            {
                if (isRevolution == false){
                    StartCoroutine(ToOpponentEarn());
                    // 對手贏
                }
                else
                {
                    StartCoroutine(ToPlayerEarn());// 玩家贏
                }
                yield return new WaitForSeconds(3f);
                if (PlayerCard.id == 17)
                {
                    StartCoroutine(Trojan(PlayerEarn,OpponentEarn));
                    yield return new WaitForSeconds(3f);
                }
            }
            else if (PlayerCard.cardName == "殺手" && (OpponentCard.cardName == "國王" || OpponentCard.cardName == "王子" || OpponentCard.cardName == "皇后"))
            {
                
                if (isRevolution == false)
                {
                    StartCoroutine(ToPlayerEarn());// 玩家贏
                }
                else
                {
                    StartCoroutine(ToOpponentEarn());// 對手贏
                }
                 yield return new WaitForSeconds(3f);
                
            }
            else if (OpponentCard.cardName == "殺手" && (PlayerCard.cardName == "國王" || PlayerCard.cardName == "王子" || PlayerCard.cardName == "皇后"))
            {
                if (isRevolution == false)
                {
                    StartCoroutine(ToOpponentEarn());// 對手贏
                }
                else
                {
                    StartCoroutine(ToPlayerEarn());// 玩家贏
                }
                yield return new WaitForSeconds(3f);
            }
            else
            {
                // 平手
                StartCoroutine(ToDrawArea());
                yield return new WaitForSeconds(3f);
                WinImage.SetActive(false);
                SkillImage.SetActive(true);
                skillMessage.gameObject.SetActive(true);
                skillDescription.gameObject.SetActive(true);
                // 大革命
                if (PlayerCard.id == 16 || OpponentCard.id == 16)
                {
                    skillMessage.text = "大革命!";
                    skillDescription.text = "從此回合卡牌強弱翻轉";
                    isRevolution = true;
                    yield return new WaitForSeconds(3f);
                }
                // 爆發式成長
                if (PlayerCard.id == 15)
                {
                    skillMessage.text = "爆發式成長!";
                    skillDescription.text = "玩家獲得 "+ GameController.Turn.ToString() + " 張牌";
                    PlayerX = GameController.Turn;
                    yield return new WaitForSeconds(3f);
                }
                else if (OpponentCard.id == 15)
                {
                    skillMessage.text = "爆發式成長!";
                    skillDescription.text = "對手獲得 "+ GameController.Turn.ToString() + " 張牌";
                    OpponentX = GameController.Turn;
                    yield return new WaitForSeconds(3f);
                }
                // 全部重製
                if (PlayerCard.id == 8)
                {
                    skillMessage.text = "全部重置!";
                    skillDescription.text = "將對手全部贏牌移至平手區";
                    Debug.Log("玩家發動全部重置");
                    ResetAll(OpponentEarn);
                    yield return new WaitForSeconds(3f);
                }
                else if (OpponentCard.id == 8)
                {
                    skillMessage.text = "全部重置!";
                    skillDescription.text = "玩家全部贏牌移至平手區";
                    ResetAll(PlayerEarn);
                    yield return new WaitForSeconds(3f);
                }
                // 簡易剔除
                if (PlayerCard.id == 7)
                {
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
                else if(OpponentCard.id == 7)
                {
                    skillMessage.text = "簡易剔除!";
                    skillDescription.text = "對手將選擇一張牌剔除";
                    OpponentSimpleRejection();
                    yield return new WaitForSeconds(3f);
                }

                
            }
            
        }
        //-------------------------\\
        OpponentEarnText.text  = (OpponentEarn.transform.childCount + OpponentX).ToString();
        PlayerEarnText.text  = (PlayerEarn.transform.childCount + PlayerX).ToString();
        if(OpponentEarn.transform.childCount + OpponentX < 10 && PlayerEarn.transform.childCount + PlayerX < 10)
            StartCoroutine(GC.TurnStart());
        else
        {
            WinImage.SetActive(true);
            WhoWins.gameObject.SetActive(true);
            if(PlayerEarn.transform.childCount + PlayerX >= 10 )
            {
                 WhoWins.text = "VICTORY";
            }
            else
            {
                WhoWins.text = "DEFEAT";
            }
        }

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
        for (; DrawArea.transform.childCount > 0;)
        {
            Card = DrawArea.transform.GetChild(DrawArea.transform.childCount - 1);
            Card.SetParent(PlayerEarn.transform, false);
            Card.position = PlayerEarn.transform.position;
            // 玩家贏牌顯示
            PlayerEarnText.text = (PlayerEarn.transform.childCount + PlayerX).ToString();
            yield return new WaitForSeconds(0.5f);
        }

        //兩張卡移至PlayerEarn
        yield return new WaitForSeconds(1);
        PlayerCardObject.transform.SetParent(PlayerEarn.transform, false);
        PlayerCardObject.transform.position = PlayerEarn.transform.position;
        OpponentCardObject.transform.SetParent(PlayerEarn.transform, false);
        OpponentCardObject.transform.position = PlayerEarn.transform.position;
        // 玩家贏牌顯示
        PlayerEarnText.text = (PlayerEarn.transform.childCount + PlayerX).ToString();


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
        for (; DrawArea.transform.childCount > 0;)
        {
            Card = DrawArea.transform.GetChild(DrawArea.transform.childCount - 1);
            Card.SetParent(OpponentEarn.transform, false);
            Card.position = OpponentEarn.transform.position;
            // 對手贏牌顯示
            OpponentEarnText.text = (OpponentEarn.transform.childCount + OpponentX).ToString();
            yield return new WaitForSeconds(0.5f);
        }
        //兩張卡移至OpponentEarn
        yield return new WaitForSeconds(1);
        PlayerCardObject.transform.SetParent(OpponentEarn.transform, false);
        PlayerCardObject.transform.position = OpponentEarn.transform.position;
        OpponentCardObject.transform.SetParent(OpponentEarn.transform, false);
        OpponentCardObject.transform.position = OpponentEarn.transform.position;
        // 對手贏牌顯示
        OpponentEarnText.text = (OpponentEarn.transform.childCount + OpponentX).ToString();
    }
    //玩家觸發全部重置
    void ResetAll(GameObject WhoLoss)
    {
        for (; WhoLoss.transform.childCount != 0;)
        {
            Card = WhoLoss.transform.GetChild(WhoLoss.transform.childCount - 1);
            Card.SetParent(DrawArea.transform, false);
            Card.position = DrawArea.transform.position;
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
            card.ShowOnMessagePanel();
        }

    }
    void OpponentSimpleRejection()
    {
        
        if(GC.isCom == true)
        {
            deletChange.Delete(PlayerArea,PlayerArea.transform.GetChild(0).gameObject.GetComponent<CardDisplay>().id);
        }
        
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
            OpponentEarnText.text  = (OpponentEarn.transform.childCount + OpponentX).ToString();
            PlayerEarnText.text  = (PlayerEarn.transform.childCount + PlayerX).ToString();
            yield return new WaitForSeconds(0.5f);
        }
        
    }
    // 平手
    IEnumerator ToDrawArea()
    {
        WinImage.SetActive(true);
        WhoWins.gameObject.SetActive(true);
        skillDescription.gameObject.SetActive(false);
        skillMessage.gameObject.SetActive(false);
        WhoWins.text = "平手!";
        //兩張卡移至DrawArea
        yield return new WaitForSeconds(1);
        PlayerCardObject.transform.SetParent(DrawArea.transform, false);
        OpponentCardObject.transform.SetParent(DrawArea.transform, false);
        PlayerEarnText.text = (PlayerEarn.transform.childCount + PlayerX).ToString();
        OpponentEarnText.text = (OpponentEarn.transform.childCount + OpponentX).ToString();
    }
    
}