using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class ImageManager : MonoBehaviour
{
    public List<GachaItem> skillImages;
    public List<GachaItem> cardStyleImages;

    void Start()
    {
        InitializeSkill();
        InitializeCardStyle();
    }

    void InitializeSkill()
    {
        skillImages = new List<GachaItem>();
        string SkillImagesPath = "images/MainSc/Skill/";


        skillImages.Add(new GachaItem(1, "時間限縮", Resources.Load<Sprite>(SkillImagesPath+"SKillTime")));
        skillImages.Add(new GachaItem(2, "階級流動", Resources.Load<Sprite>(SkillImagesPath+"SkillKnight")));
        skillImages.Add(new GachaItem(3, "暗影轉職", Resources.Load<Sprite>(SkillImagesPath+"SkillAssassin")));
        skillImages.Add(new GachaItem(4, "技能封印", Resources.Load<Sprite>(SkillImagesPath+"SkillStopSkill")));
        skillImages.Add(new GachaItem(5, "力量剝奪", Resources.Load<Sprite>(SkillImagesPath+"SkillCivilian")));
        skillImages.Add(new GachaItem(6, "黃金風暴", Resources.Load<Sprite>(SkillImagesPath + "SkillCoin")));
        skillImages.Add(new GachaItem(7, "知己知彼", Resources.Load<Sprite>(SkillImagesPath + "SkillShowOpponent")));
        skillImages.Add(new GachaItem(8, "抉擇束縛", Resources.Load<Sprite>(SkillImagesPath+ "SkillCardChoose")));
        skillImages.Add(new GachaItem(9, "強制徵收", Resources.Load<Sprite>(SkillImagesPath + "SkillEarn1")));
        skillImages.Add(new GachaItem(10, "勝者之堆", Resources.Load<Sprite>(SkillImagesPath+ "SkillMin1")));
    }
    void InitializeCardStyle()
    {
        cardStyleImages = new List<GachaItem>();

        // Frozen
        cardStyleImages.Add(new GachaItem(1, "Frozen\n(平民)", Resources.Load<Sprite>("images/Skin/Frozen/Civil")));
        cardStyleImages.Add(new GachaItem(2, "Frozen\n(殺手)", Resources.Load<Sprite>("images/Skin/Frozen/Killer")));
        cardStyleImages.Add(new GachaItem(3, "Frozen\n(國王)", Resources.Load<Sprite>("images/Skin/Frozen/King")));
        cardStyleImages.Add(new GachaItem(4, "Frozen\n(騎士)", Resources.Load<Sprite>("images/Skin/Frozen/Knight")));
        cardStyleImages.Add(new GachaItem(5, "Frozen\n(王子)", Resources.Load<Sprite>("images/Skin/Frozen/Prince")));
        cardStyleImages.Add(new GachaItem(6, "Frozen\n(皇后)", Resources.Load<Sprite>("images/Skin/Frozen/Queen")));
        // Aladin
        cardStyleImages.Add(new GachaItem(7, "Aladdin\n(平民)", Resources.Load<Sprite>("images/Skin/Aladin/Civil")));
        cardStyleImages.Add(new GachaItem(8, "Aladdin\n(殺手)", Resources.Load<Sprite>("images/Skin/Aladin/Killer")));
        cardStyleImages.Add(new GachaItem(9, "Aladdin\n(國王)", Resources.Load<Sprite>("images/Skin/Aladin/King")));
        cardStyleImages.Add(new GachaItem(10, "Aladdin\n(騎士)", Resources.Load<Sprite>("images/Skin/Aladin/Knight")));
        cardStyleImages.Add(new GachaItem(11, "Aladdin\n(王子)", Resources.Load<Sprite>("images/Skin/Aladin/Prince")));
        cardStyleImages.Add(new GachaItem(12, "Aladdin\n(皇后)", Resources.Load<Sprite>("images/Skin/Aladin/Queen")));
        // Alice in wonderland
        cardStyleImages.Add(new GachaItem(13, "Wonderland\n(平民)", Resources.Load<Sprite>("images/Skin/Alice in wonderland/Civil")));
        cardStyleImages.Add(new GachaItem(14, "Wonderland\n(殺手)", Resources.Load<Sprite>("images/Skin/Alice in wonderland/Killer")));
        cardStyleImages.Add(new GachaItem(15, "Wonderland\n(國王)", Resources.Load<Sprite>("images/Skin/Alice in wonderland/King")));
        cardStyleImages.Add(new GachaItem(16, "Wonderland\n(騎士)", Resources.Load<Sprite>("images/Skin/Alice in wonderland/Knight")));
        cardStyleImages.Add(new GachaItem(17, "Wonderland\n(王子)", Resources.Load<Sprite>("images/Skin/Alice in wonderland/Prince")));
        cardStyleImages.Add(new GachaItem(18, "Wonderland\n(皇后)", Resources.Load<Sprite>("images/Skin/Alice in wonderland/Queen")));
        // Cinderella
        cardStyleImages.Add(new GachaItem(19, "Cinderella\n(平民)", Resources.Load<Sprite>("images/Skin/Cinderella/Civil")));
        cardStyleImages.Add(new GachaItem(20, "Cinderella\n(殺手)", Resources.Load<Sprite>("images/Skin/Cinderella/Killer")));
        cardStyleImages.Add(new GachaItem(21, "Cinderella\n(國王)", Resources.Load<Sprite>("images/Skin/Cinderella/King")));
        cardStyleImages.Add(new GachaItem(22, "Cinderella\n(騎士)", Resources.Load<Sprite>("images/Skin/Cinderella/Knight")));
        cardStyleImages.Add(new GachaItem(23, "Cinderella\n(王子)", Resources.Load<Sprite>("images/Skin/Cinderella/Prince")));
        cardStyleImages.Add(new GachaItem(24, "Cinderella\n(皇后)", Resources.Load<Sprite>("images/Skin/Cinderella/Queen")));
        // Romet and Julliette
        cardStyleImages.Add(new GachaItem(25, "Romeo\n(平民)", Resources.Load<Sprite>("images/Skin/Romet and Juliette/Civil")));
        cardStyleImages.Add(new GachaItem(26, "Romeo\n(殺手)", Resources.Load<Sprite>("images/Skin/Romet and Juliette/Killer")));
        cardStyleImages.Add(new GachaItem(27, "Romeo\n(國王)", Resources.Load<Sprite>("images/Skin/Romet and Juliette/King")));
        cardStyleImages.Add(new GachaItem(28, "Romeo\n(騎士)", Resources.Load<Sprite>("images/Skin/Romet and Juliette/Knight")));
        cardStyleImages.Add(new GachaItem(29, "Romeo\n(王子)", Resources.Load<Sprite>("images/Skin/Romet and Juliette/Prince")));
        cardStyleImages.Add(new GachaItem(30, "Romeo\n(皇后)", Resources.Load<Sprite>("images/Skin/Romet and Juliette/Queen")));
        // Chess
        cardStyleImages.Add(new GachaItem(31, "Chess\n(平民)", Resources.Load<Sprite>("images/Skin/chess/Civil")));
        cardStyleImages.Add(new GachaItem(32, "Chess\n(殺手)", Resources.Load<Sprite>("images/Skin/chess/Killer")));
        cardStyleImages.Add(new GachaItem(33, "Chess\n(國王)", Resources.Load<Sprite>("images/Skin/chess/King")));
        cardStyleImages.Add(new GachaItem(34, "Chess\n(騎士)", Resources.Load<Sprite>("images/Skin/chess/Knight")));
        cardStyleImages.Add(new GachaItem(35, "Chess\n(王子)", Resources.Load<Sprite>("images/Skin/chess/Prince")));
        cardStyleImages.Add(new GachaItem(36, "Chess\n(皇后)", Resources.Load<Sprite>("images/Skin/chess/Queen")));
        // Chinese chess
        cardStyleImages.Add(new GachaItem(37, "ChineseChess\n(平民)", Resources.Load<Sprite>("images/Skin/Chinese chess/Civil")));
        cardStyleImages.Add(new GachaItem(38, "ChineseChess\n(殺手)", Resources.Load<Sprite>("images/Skin/Chinese chess/Killer")));
        cardStyleImages.Add(new GachaItem(39, "ChineseChess\n(國王)", Resources.Load<Sprite>("images/Skin/Chinese chess/King")));
        cardStyleImages.Add(new GachaItem(40, "ChineseChess\n(騎士)", Resources.Load<Sprite>("images/Skin/Chinese chess/Knight")));
        cardStyleImages.Add(new GachaItem(41, "ChineseChess\n(王子)", Resources.Load<Sprite>("images/Skin/Chinese chess/Prince")));
        cardStyleImages.Add(new GachaItem(42, "ChineseChess\n(皇后)", Resources.Load<Sprite>("images/Skin/Chinese chess/Queen")));
        // Japanese chess
        cardStyleImages.Add(new GachaItem(43, "JapaneseChess\n(平民)", Resources.Load<Sprite>("images/Skin/Japanese chess/Civil")));
        cardStyleImages.Add(new GachaItem(44, "JapaneseChess\n(殺手)", Resources.Load<Sprite>("images/Skin/Japanese chess/Killer")));
        cardStyleImages.Add(new GachaItem(45, "JapaneseChess\n(國王)", Resources.Load<Sprite>("images/Skin/Japanese chess/King")));
        cardStyleImages.Add(new GachaItem(46, "JapaneseChess\n(騎士)", Resources.Load<Sprite>("images/Skin/Japanese chess/Knight")));
        cardStyleImages.Add(new GachaItem(47, "JapaneseChess\n(王子)", Resources.Load<Sprite>("images/Skin/Japanese chess/Prince")));
        cardStyleImages.Add(new GachaItem(48, "JapaneseChess\n(皇后)", Resources.Load<Sprite>("images/Skin/Japanese chess/Queen")));
        // Snow whit
        cardStyleImages.Add(new GachaItem(49, "SnowWhite\n(平民)", Resources.Load<Sprite>("images/Skin/Snow White/Civil")));
        cardStyleImages.Add(new GachaItem(50, "SnowWhite\n(殺手)", Resources.Load<Sprite>("images/Skin/Snow White/Killer")));
        cardStyleImages.Add(new GachaItem(51, "SnowWhite\n(國王)", Resources.Load<Sprite>("images/Skin/Snow White/King")));
        cardStyleImages.Add(new GachaItem(52, "SnowWhite\n(騎士)", Resources.Load<Sprite>("images/Skin/Snow White/Knight")));
        cardStyleImages.Add(new GachaItem(53, "SnowWhite\n(王子)", Resources.Load<Sprite>("images/Skin/Snow White/Prince")));
        cardStyleImages.Add(new GachaItem(54, "SnowWhite\n(皇后)", Resources.Load<Sprite>("images/Skin/Snow White/Queen")));
        
    }

    public Sprite GetSkillImage(int skill_id)
    {
        if (skillImages[skill_id - 1] != null)
        {
            return skillImages[skill_id - 1].itemSprite;
        }
        else
        {
            Debug.LogWarning($"SkillImageMapper: No image found for skill with id {skill_id}.");
            return null;
        }
    }
    public string GetSkillName(int skill_id)
    {
        if (skillImages[skill_id - 1] != null)
        {
            return skillImages[skill_id - 1].itemName;
        }
        else
        {
            Debug.LogWarning($"SkillImageMapper: No image found for skill with id {skill_id}.");
            return null;
        }
    }
    public Sprite GetCardStyleImage(int style_id)
    {
        if (cardStyleImages[style_id - 1] != null)
        {
            return cardStyleImages[style_id - 1].itemSprite;
        }
        else
        {
            Debug.LogWarning($"CardStyleImages: No image found for style with id {style_id}.");
            // foreach (var key in cardStyleImages.Keys)
            // {
            //     Debug.Log($"Key in dictionary: {key}");
            // }
            return null;
        }
    }
    public string GetCardStyleName(int style_id)
    {
        if (cardStyleImages[style_id - 1] != null)
        {
            return cardStyleImages[style_id - 1].itemName;
        }
        else
        {
            Debug.LogWarning($"CardStyleImages: No image found for style with id {style_id}.");
            // foreach (var key in cardStyleImages.Keys)
            // {
            //     Debug.Log($"Key in dictionary: {key}");
            // }
            return null;
        }
    }
}
