using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowCard : MonoBehaviour
{
    GameController GC;
    public GameObject PlayerShow;
    public GameObject OpponentShow;
    public GameObject PlayerEarn;
    public GameObject OpponentEarn;
    public GameObject DrawArea;
    public Text WhoWins;
    public Text PlayerEarnText;
    public Text OpponentEarnText;
    
    GameObject PlayerCardObject;
    GameObject OpponentCardObject;
    CardDisplay PlayerCard;
    CardDisplay OpponentCard;
    Transform Card;

    
    void Start()
    {
        WhoWins.gameObject.SetActive(false);
        GC = GameObject.Find("GameController").GetComponent<GameController>();
    }
    public void Show()
    {
        PlayerCardObject = PlayerShow.transform.GetChild(0).gameObject;
        OpponentCardObject = OpponentShow.transform.GetChild(0).gameObject;
        PlayerCardObject.layer = LayerMask.NameToLayer("Show");
        OpponentCardObject.layer = LayerMask.NameToLayer("Show");
        PlayerCard = PlayerCardObject.GetComponent<CardDisplay>();
        OpponentCard = OpponentCardObject.GetComponent<CardDisplay>();

        // 判斷(可以用PlayerCard.cardName & OpponentCard.cardName，如果是平民有技能可以比PlayerCard.id & OpponentCard.id)

        //-------------------------\\
        if(PlayerCard.cardName == "國王" && (OpponentCard.cardName == "王子" || OpponentCard.cardName == "騎士" || OpponentCard.cardName == "平民"))
        {
            // 玩家贏
            StartCoroutine(ToPlayerEarn());
        }
        else if(OpponentCard.cardName == "國王" && (PlayerCard.cardName == "王子" || PlayerCard.cardName == "騎士" || PlayerCard.cardName == "平民"))
        {
            // 對手贏
            StartCoroutine(ToOpponentEarn());
        }
        else if(PlayerCard.cardName == "皇后" && (OpponentCard.cardName == "國王" || OpponentCard.cardName == "騎士" || OpponentCard.cardName == "平民"))
        {
            // 玩家贏
            StartCoroutine(ToPlayerEarn());
        }
        else if(OpponentCard.cardName == "皇后" && (PlayerCard.cardName == "國王" || PlayerCard.cardName == "騎士" || PlayerCard.cardName == "平民"))
        {
            // 對手贏
            StartCoroutine(ToOpponentEarn());
        }
        else if(PlayerCard.cardName == "王子" && (OpponentCard.cardName == "皇后" || OpponentCard.cardName == "騎士" || OpponentCard.cardName == "平民"))
        {
            // 玩家贏
            StartCoroutine(ToPlayerEarn());
        }
        else if(OpponentCard.cardName == "王子" && (PlayerCard.cardName == "皇后" || PlayerCard.cardName == "騎士" || PlayerCard.cardName == "平民"))
        {
            // 對手贏
            StartCoroutine(ToOpponentEarn());
        }
        else if(PlayerCard.cardName == "騎士" && (OpponentCard.cardName == "王子" || OpponentCard.cardName == "殺手" || OpponentCard.cardName == "平民"))
        {
            // 玩家贏
            StartCoroutine(ToPlayerEarn());
        }
        else if(OpponentCard.cardName == "騎士" && (PlayerCard.cardName == "王子" || PlayerCard.cardName == "殺手" || PlayerCard.cardName == "平民"))
        {
            // 對手贏
            StartCoroutine(ToOpponentEarn());
        }
        else if (PlayerCard.cardName == "殺手" && (OpponentCard.cardName == "國王" || OpponentCard.cardName == "王子" || OpponentCard.cardName == "皇后"))
        {
            // 玩家贏
            StartCoroutine(ToPlayerEarn());
        }
        else if (OpponentCard.cardName == "殺手" && (PlayerCard.cardName == "國王" || PlayerCard.cardName == "王子" || PlayerCard.cardName == "皇后")) 
        {
            // 對手贏
            StartCoroutine(ToOpponentEarn());
        }
        else
        {
            // 平手
            StartCoroutine(ToDrawArea());
        }
        DropZone.haveCard = false;
        DropZone.backToHand = true;
        //-------------------------\\

    }
    // 玩家贏
    IEnumerator ToPlayerEarn()
    {
        yield return new WaitForSeconds(1);
        WhoWins.gameObject.SetActive(true);
        WhoWins.text = "你贏了!";
        //DrawArea有牌 => 移至PlayerEarn
        yield return new WaitForSeconds(1);
        for(;DrawArea.transform.childCount > 0;)
        {
            Card =DrawArea.transform.GetChild(DrawArea.transform.childCount - 1);
            Card.SetParent(PlayerEarn.transform,false);
            Card.position = PlayerEarn.transform.position;
            // 玩家贏牌顯示
            PlayerEarnText.text  = PlayerEarn.transform.childCount.ToString();
        }
        
        //兩張卡移至PlayerEarn
        yield return new WaitForSeconds(1);
        PlayerCardObject.transform.SetParent(PlayerEarn.transform,false);
        PlayerCardObject.transform.position =PlayerEarn.transform.position;
        OpponentCardObject.transform.SetParent(PlayerEarn.transform,false);
        OpponentCardObject.transform.position =PlayerEarn.transform.position;
        // 玩家贏牌顯示
        PlayerEarnText.text = PlayerEarn.transform.childCount.ToString();

        //下回合Start    
        yield return new WaitForSeconds(1);
        if(PlayerEarn.transform.childCount < 10)
        {
            GC.TurnStart();
        }
        WhoWins.gameObject.SetActive(false);
    }
    // 對手贏
    IEnumerator ToOpponentEarn()
    {
        yield return new WaitForSeconds(1);
        WhoWins.gameObject.SetActive(true);
        WhoWins.text = "你輸了!";
        //DrawArea有牌 => 移至OpponentEarn
        yield return new WaitForSeconds(1);
        for(;DrawArea.transform.childCount > 0;)
        {
            Card =DrawArea.transform.GetChild(DrawArea.transform.childCount - 1);
            Card.SetParent(OpponentEarn.transform,false);
            Card.position = OpponentEarn.transform.position;
            // 對手贏牌顯示
            OpponentEarnText.text  = OpponentEarn.transform.childCount.ToString();
        }
        //兩張卡移至OpponentEarn
        yield return new WaitForSeconds(1);
        PlayerCardObject.transform.SetParent(OpponentEarn.transform,false);
        PlayerCardObject.transform.position =OpponentEarn.transform.position;
        OpponentCardObject.transform.SetParent(OpponentEarn.transform,false);
        OpponentCardObject.transform.position =OpponentEarn.transform.position;
        // 對手贏牌顯示
        OpponentEarnText.text  = OpponentEarn.transform.childCount.ToString();
        //下回合Start   
        yield return new WaitForSeconds(1);
        if(OpponentEarn.transform.childCount < 10)
        {
            GC.TurnStart();
        }
        WhoWins.gameObject.SetActive(false);
    }
    // 平手
    IEnumerator ToDrawArea()
    {
        yield return new WaitForSeconds(1);
        WhoWins.gameObject.SetActive(true);
        WhoWins.text = "平手!";
        //兩張卡移至DrawArea
        yield return new WaitForSeconds(1);
        PlayerCardObject.transform.SetParent(DrawArea.transform,false);
        OpponentCardObject.transform.SetParent(DrawArea.transform,false);
        //下回合Start
        yield return new WaitForSeconds(1);
        GC.TurnStart();
        WhoWins.gameObject.SetActive(false);
    }

}
