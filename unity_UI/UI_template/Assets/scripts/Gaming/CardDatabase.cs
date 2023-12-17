using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();
    public static List<int> cardStyleIdList = new List<int>();
    string ImageAddress = "images/Skin/";
    string IconAddress = "images/GameSc/CharacterIcon/";
    string SkinStyle = "";
    string KingSkin = "Poker";
    string QueenSkin = "Poker";
    string PrinceSkin = "Poker";
    string KnightSkin = "Poker";
    string KillerSkin = "Poker";
    string CivilSkin = "Poker";
    public void Create()
    {
        for(int i = 0;i<cardStyleIdList.Count;i++)
        {
            if(cardStyleIdList[i] < 1 || cardStyleIdList[i] >60 )
                Debug.Log("CardStyle Error :" + cardStyleIdList[i]);
            switch((cardStyleIdList[i]-1)/6)
            {
                case 0 :
                    SkinStyle = "Frozen"; 
                    break;
                case 1 :
                    SkinStyle = "Aladin"; 
                    break;
                case 2 :
                    SkinStyle = "Alice in wonderland"; 
                    break;
                case 3 :
                    SkinStyle = "Cinderella"; 
                    break;
                case 4 :
                    SkinStyle = "Romet and Juliette"; 
                    break;
                case 5 :
                    SkinStyle = "Chess"; 
                    break;
                case 6 :
                    SkinStyle = "Chinese chess"; 
                    break;
                case 7 :
                    SkinStyle = "Japanese chess"; 
                    break;
                case 8 :
                    SkinStyle = "Snow white"; 
                    break;
            }

            switch(cardStyleIdList[i]%6)
            {
                case 0 :
                    QueenSkin = SkinStyle;
                    break;
                case 1 :
                    CivilSkin = SkinStyle;
                    break;
                case 2 :
                    KillerSkin = SkinStyle;
                    break;
                case 3 :
                    KingSkin = SkinStyle; 
                    break;
                case 4 :
                    KnightSkin = SkinStyle; 
                    break;
                case 5 :
                    PrinceSkin = SkinStyle;
                    break;
            }
        }




        cardList.Add(new Card(0,"國王","無技能","若對手為暗殺者或皇后落敗，反之則獲勝。",'A',Resources.Load<Sprite>(ImageAddress+ KingSkin + "/King"),Resources.Load<Sprite>(IconAddress + "King")));//路徑可改
        cardList.Add(new Card(1,"皇后","無技能","若對手為暗殺者或王子落敗，反之則獲勝。",'A',Resources.Load<Sprite>(ImageAddress+ QueenSkin + "/Queen"),Resources.Load<Sprite>(IconAddress + "Queen")));
        cardList.Add(new Card(2,"王子","無技能","若對手為暗殺者或國王落敗，反之則獲勝。",'A',Resources.Load<Sprite>(ImageAddress+ PrinceSkin + "/Prince"),Resources.Load<Sprite>(IconAddress + "Prince")));
        cardList.Add(new Card(3,"騎士","無技能","若對手為王家落敗，暗殺者或平民則獲勝。",'A',Resources.Load<Sprite>(ImageAddress+ KnightSkin + "/Knight"),Resources.Load<Sprite>(IconAddress + "Knight")));
        cardList.Add(new Card(4,"騎士","無技能","若對手為王家落敗，暗殺者或平民則獲勝。",'A',Resources.Load<Sprite>(ImageAddress+ KnightSkin + "/Knight"),Resources.Load<Sprite>(IconAddress + "Knight")));
        cardList.Add(new Card(5,"殺手","無技能","若對手為騎士落敗，王家則獲勝，其他則平手。",'A',Resources.Load<Sprite>(ImageAddress+ KillerSkin + "/Killer"),Resources.Load<Sprite>(IconAddress + "Killer")));
        cardList.Add(new Card(6,"殺手","無技能","若對手為騎士落敗，王家則獲勝，其他則平手。",'A',Resources.Load<Sprite>(ImageAddress+ KillerSkin + "/Killer"),Resources.Load<Sprite>(IconAddress + "Killer")));
        cardList.Add(new Card(7,"平民","簡易剔除","觸發條件：<平手>\n從對手手排中選出一張卡牌移出遊戲。",'A',Resources.Load<Sprite>(ImageAddress+ CivilSkin + "/Civil"),Resources.Load<Sprite>(IconAddress + "CivilianA")));
        cardList.Add(new Card(8,"平民","全部重置","觸發條件：<平手>\n將對手贏到的牌全部放到平手區。",'A',Resources.Load<Sprite>(ImageAddress+ CivilSkin + "/Civil"),Resources.Load<Sprite>(IconAddress + "CivilianB")));
        cardList.Add(new Card(9,"平民","不敗的勇者","觸發條件：<不限>\n可以贏過任何一張牌。",'A',Resources.Load<Sprite>(ImageAddress+ CivilSkin + "/Civil"),Resources.Load<Sprite>(IconAddress + "CivilianC")));
        cardList.Add(new Card(10,"國王","無技能","若對手為暗殺者或皇后落敗，反之則獲勝。",'B',Resources.Load<Sprite>(ImageAddress+ KingSkin + "/King"),Resources.Load<Sprite>(IconAddress + "King")));//路徑可改
        cardList.Add(new Card(11,"皇后","無技能","若對手為暗殺者或王子落敗，反之則獲勝。",'B',Resources.Load<Sprite>(ImageAddress+ QueenSkin + "/Queen"),Resources.Load<Sprite>(IconAddress + "Queen")));
        cardList.Add(new Card(12,"王子","無技能","若對手為暗殺者或國王落敗，反之則獲勝。",'B',Resources.Load<Sprite>(ImageAddress+ PrinceSkin + "/Prince"),Resources.Load<Sprite>(IconAddress + "Prince")));
        cardList.Add(new Card(13,"騎士","無技能","若對手為王家落敗，暗殺者或平民則獲勝。",'B',Resources.Load<Sprite>(ImageAddress+ KnightSkin + "/Knight"),Resources.Load<Sprite>(IconAddress + "Knight")));
        cardList.Add(new Card(14,"殺手","無技能","若對手為騎士落敗，王家則獲勝，其他及相同則平手。",'B',Resources.Load<Sprite>(ImageAddress+ KillerSkin + "/Killer"),Resources.Load<Sprite>(IconAddress + "Killer")));
        cardList.Add(new Card(15,"平民","爆發式成長","觸發條件：<平手>\n自身增加X張的贏取卡(X為回合數)。",'B',Resources.Load<Sprite>(ImageAddress+ CivilSkin+ "/Civil"),Resources.Load<Sprite>(IconAddress + "CivilianA")));
        cardList.Add(new Card(16,"平民","大革命","觸發條件：<平手>\n觸發成功後，所有卡牌優劣反轉。",'B',Resources.Load<Sprite>(ImageAddress+ CivilSkin + "/Civil"),Resources.Load<Sprite>(IconAddress + "CivilianB")));
        cardList.Add(new Card(17,"平民","特洛伊木馬","觸發條件：<輸王家或騎士>對手一半贏牌轉為玩家贏牌。",'B',Resources.Load<Sprite>(ImageAddress+ CivilSkin + "/Civil"),Resources.Load<Sprite>(IconAddress + "CivilianC")));
        cardList.Add(new Card(18,"平民","無技能","看似軟弱但淺藏無限力量，尤其在大革命發動以後。",'B',Resources.Load<Sprite>(ImageAddress+ CivilSkin + "/Civil"),Resources.Load<Sprite>(IconAddress + "Civilian")));
        cardList.Add(new Card(19,"平民","無技能","看似軟弱但淺藏無限力量，尤其在大革命發動以後。",'B',Resources.Load<Sprite>(ImageAddress+ CivilSkin + "/Civil"),Resources.Load<Sprite>(IconAddress + "Civilian")));
    
    }


}
