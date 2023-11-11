using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();
    
    void Awake()
    {
        cardList.Add(new Card(0,"國王","無技能","就是國王",'A',Resources.Load<Sprite>("images/king")));//路徑可改
        cardList.Add(new Card(1,"皇后","無技能","就是皇后",'A',Resources.Load<Sprite>("images/king")));
        cardList.Add(new Card(2,"王子","無技能","就是王子",'A',Resources.Load<Sprite>("images/king")));
        cardList.Add(new Card(3,"騎士","無技能","就是騎士",'A',Resources.Load<Sprite>("images/king")));
        cardList.Add(new Card(4,"騎士","無技能","就是騎士",'A',Resources.Load<Sprite>("images/king")));
        cardList.Add(new Card(5,"殺手","無技能","就是殺手",'A',Resources.Load<Sprite>("images/king")));
        cardList.Add(new Card(6,"殺手","無技能","就是殺手",'A',Resources.Load<Sprite>("images/king")));
        cardList.Add(new Card(7,"平民","簡易剔除","觸發條件:<平手>\n從對手手排中選出一張卡牌移出遊戲",'A',Resources.Load<Sprite>("images/king")));
        cardList.Add(new Card(8,"平民","全部重置","觸發條件:<平手>\n將對手贏到的牌全部放到平手區",'A',Resources.Load<Sprite>("images/king")));
        cardList.Add(new Card(9,"平民","不敗的勇者","觸發條件:<不限>\n可以贏過任何一張牌",'A',Resources.Load<Sprite>("images/king")));
        cardList.Add(new Card(10,"國王","無技能","就是國王",'B',Resources.Load<Sprite>("images/king")));//路徑可改
        cardList.Add(new Card(11,"皇后","無技能","就是皇后",'B',Resources.Load<Sprite>("images/king")));
        cardList.Add(new Card(12,"王子","無技能","就是王子",'B',Resources.Load<Sprite>("images/king")));
        cardList.Add(new Card(13,"騎士","無技能","就是騎士",'B',Resources.Load<Sprite>("images/king")));
        cardList.Add(new Card(14,"殺手","無技能","就是殺手",'B',Resources.Load<Sprite>("images/king")));
        cardList.Add(new Card(15,"平民","爆發式成長","觸發條件:<平手>\n自身增加X張的贏取卡(X為回合數)",'B',Resources.Load<Sprite>("images/king")));
        cardList.Add(new Card(16,"平民","大革命","觸發條件:<平手>\n觸發成功後,所有卡牌優劣反轉",'B',Resources.Load<Sprite>("images/king")));
        cardList.Add(new Card(17,"平民","特洛伊木馬","觸發條件:<輸王家或騎士>對手一半贏牌轉為玩家贏牌",'B',Resources.Load<Sprite>("images/king")));
        cardList.Add(new Card(18,"平民","無技能","就是平民",'B',Resources.Load<Sprite>("images/king")));
        cardList.Add(new Card(19,"平民","無技能","就是平民",'B',Resources.Load<Sprite>("images/king")));
    
    }


}
