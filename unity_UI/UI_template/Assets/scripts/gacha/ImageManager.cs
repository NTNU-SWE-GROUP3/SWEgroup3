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


        skillImages.Add(new GachaItem(1, "暗影轉職", Resources.Load<Sprite>("images/GameSc/Skill/SkillAssassin")));
        skillImages.Add(new GachaItem(2, "階級流動", Resources.Load<Sprite>("images/GameSc/Skill/SkillKnight")));
        skillImages.Add(new GachaItem(3, "知己知彼", Resources.Load<Sprite>("images/GameSc/Skill/SkillShowOpponentCard")));
        skillImages.Add(new GachaItem(4, "暗影轉職", Resources.Load<Sprite>("images/GameSc/Skill/SkillAssassin")));
        skillImages.Add(new GachaItem(5, "階級流動", Resources.Load<Sprite>("images/GameSc/Skill/SkillKnight")));
        skillImages.Add(new GachaItem(6, "知己知彼", Resources.Load<Sprite>("images/GameSc/Skill/SkillShowOpponentCard")));
        skillImages.Add(new GachaItem(7, "暗影轉職", Resources.Load<Sprite>("images/GameSc/Skill/SkillAssassin")));
        skillImages.Add(new GachaItem(8, "階級流動", Resources.Load<Sprite>("images/GameSc/Skill/SkillKnight")));
        skillImages.Add(new GachaItem(9, "知己知彼", Resources.Load<Sprite>("images/GameSc/Skill/SkillShowOpponentCard")));
        skillImages.Add(new GachaItem(10, "暗影轉職", Resources.Load<Sprite>("images/GameSc/Skill/SkillAssassin")));
    }
    void InitializeCardStyle()
    {
        cardStyleImages = new List<GachaItem>();

        // Frozen
        cardStyleImages.Add(new GachaItem(1, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Frozen/Civil")));
        cardStyleImages.Add(new GachaItem(2, "Frozen(殺手)", Resources.Load<Sprite>("images/Skin/Frozen/Killer")));
        cardStyleImages.Add(new GachaItem(3, "Frozen(國王)", Resources.Load<Sprite>("images/Skin/Frozen/King")));
        cardStyleImages.Add(new GachaItem(4, "Frozen(騎士)", Resources.Load<Sprite>("images/Skin/Frozen/Knight")));
        cardStyleImages.Add(new GachaItem(5, "Frozen(王子)", Resources.Load<Sprite>("images/Skin/Frozen/Prince")));
        cardStyleImages.Add(new GachaItem(6, "Frozen(皇后)", Resources.Load<Sprite>("images/Skin/Frozen/Queen")));
        // Aladin
        cardStyleImages.Add(new GachaItem(7, "Aladin(平民)", Resources.Load<Sprite>("images/Skin/Aladin/Civil")));
        cardStyleImages.Add(new GachaItem(8, "Aladin(殺手)", Resources.Load<Sprite>("images/Skin/Aladin/Killer")));
        cardStyleImages.Add(new GachaItem(9, "Aladin(國王)", Resources.Load<Sprite>("images/Skin/Aladin/King")));
        cardStyleImages.Add(new GachaItem(10, "Aladin(騎士)", Resources.Load<Sprite>("images/Skin/Aladin/Knight")));
        cardStyleImages.Add(new GachaItem(11, "Aladin(王子)", Resources.Load<Sprite>("images/Skin/Aladin/Prince")));
        cardStyleImages.Add(new GachaItem(12, "Aladin(皇后)", Resources.Load<Sprite>("images/Skin/Aladin/Queen")));
        // Alice in wonderland
        cardStyleImages.Add(new GachaItem(13, "Wonderland(平民)", Resources.Load<Sprite>("images/Skin/Alice in wonderland/Civil")));
        cardStyleImages.Add(new GachaItem(14, "Wonderland(殺手)", Resources.Load<Sprite>("images/Skin/Alice in wonderland/Killer")));
        cardStyleImages.Add(new GachaItem(15, "Wonderland(國王)", Resources.Load<Sprite>("images/Skin/Alice in wonderland/King")));
        cardStyleImages.Add(new GachaItem(16, "Wonderland(騎士)", Resources.Load<Sprite>("images/Skin/Alice in wonderland/Knight")));
        cardStyleImages.Add(new GachaItem(17, "Wonderland(王子)", Resources.Load<Sprite>("images/Skin/Alice in wonderland/Prince")));
        cardStyleImages.Add(new GachaItem(18, "Wonderland(皇后)", Resources.Load<Sprite>("images/Skin/Alice in wonderland/Queen")));
        // Cinderella
        cardStyleImages.Add(new GachaItem(19, "Cinderella(平民)", Resources.Load<Sprite>("images/Skin/Cinderella/Civil")));
        cardStyleImages.Add(new GachaItem(20, "Cinderella(殺手)", Resources.Load<Sprite>("images/Skin/Cinderella/Killer")));
        cardStyleImages.Add(new GachaItem(21, "Cinderella(國王)", Resources.Load<Sprite>("images/Skin/Cinderella/King")));
        cardStyleImages.Add(new GachaItem(22, "Cinderella(騎士)", Resources.Load<Sprite>("images/Skin/Cinderella/Knight")));
        cardStyleImages.Add(new GachaItem(23, "Cinderella(王子)", Resources.Load<Sprite>("images/Skin/Cinderella/Prince")));
        cardStyleImages.Add(new GachaItem(24, "Cinderella(皇后)", Resources.Load<Sprite>("images/Skin/Cinderella/Queen")));
        // Romet and Julliette
        cardStyleImages.Add(new GachaItem(25, "Romet(平民)", Resources.Load<Sprite>("images/Skin/Romet and Juliette/Civil")));
        cardStyleImages.Add(new GachaItem(26, "Romet(殺手)", Resources.Load<Sprite>("images/Skin/Romet and Juliette/Killer")));
        cardStyleImages.Add(new GachaItem(27, "Romet(國王)", Resources.Load<Sprite>("images/Skin/Romet and Juliette/King")));
        cardStyleImages.Add(new GachaItem(28, "Romet(騎士)", Resources.Load<Sprite>("images/Skin/Romet and Juliette/Knight")));
        cardStyleImages.Add(new GachaItem(29, "Romet(王子)", Resources.Load<Sprite>("images/Skin/Romet and Juliette/Prince")));
        cardStyleImages.Add(new GachaItem(30, "Romet(皇后)", Resources.Load<Sprite>("images/Skin/Romet and Juliette/Queen")));
        // Chess
        cardStyleImages.Add(new GachaItem(31, "Chess(平民)", Resources.Load<Sprite>("images/Skin/chess/Civil")));
        cardStyleImages.Add(new GachaItem(32, "Chess(殺手)", Resources.Load<Sprite>("images/Skin/chess/Killer")));
        cardStyleImages.Add(new GachaItem(33, "Chess(國王)", Resources.Load<Sprite>("images/Skin/chess/King")));
        cardStyleImages.Add(new GachaItem(34, "Chess(騎士)", Resources.Load<Sprite>("images/Skin/chess/Knight")));
        cardStyleImages.Add(new GachaItem(35, "Chess(王子)", Resources.Load<Sprite>("images/Skin/chess/Prince")));
        cardStyleImages.Add(new GachaItem(36, "Chess(皇后)", Resources.Load<Sprite>("images/Skin/chess/Queen")));
        // Chinese chess
        cardStyleImages.Add(new GachaItem(37, "ChineseChess(平民)", Resources.Load<Sprite>("images/Skin/Chinese chess/Civil")));
        cardStyleImages.Add(new GachaItem(38, "ChineseChess(殺手)", Resources.Load<Sprite>("images/Skin/Chinese chess/Killer")));
        cardStyleImages.Add(new GachaItem(39, "ChineseChess(國王)", Resources.Load<Sprite>("images/Skin/Chinese chess/King")));
        cardStyleImages.Add(new GachaItem(40, "ChineseChess(騎士)", Resources.Load<Sprite>("images/Skin/Chinese chess/Knight")));
        cardStyleImages.Add(new GachaItem(41, "ChineseChess(王子)", Resources.Load<Sprite>("images/Skin/Chinese chess/Prince")));
        cardStyleImages.Add(new GachaItem(42, "ChineseChess(皇后)", Resources.Load<Sprite>("images/Skin/Chinese chess/Queen")));
        // Japanese chess
        cardStyleImages.Add(new GachaItem(43, "JanpaneseChess(平民)", Resources.Load<Sprite>("images/Skin/Janpanese chess/Civil")));
        cardStyleImages.Add(new GachaItem(44, "JanpaneseChess(殺手)", Resources.Load<Sprite>("images/Skin/Janpanese chess/Killer")));
        cardStyleImages.Add(new GachaItem(45, "JanpaneseChess(國王)", Resources.Load<Sprite>("images/Skin/Janpanese chess/King")));
        cardStyleImages.Add(new GachaItem(46, "JanpaneseChess(騎士)", Resources.Load<Sprite>("images/Skin/Janpanese chess/Knight")));
        cardStyleImages.Add(new GachaItem(47, "JanpaneseChess(王子)", Resources.Load<Sprite>("images/Skin/Janpanese chess/Prince")));
        cardStyleImages.Add(new GachaItem(48, "JanpaneseChess(皇后)", Resources.Load<Sprite>("images/Skin/Janpanese chess/Queen")));
        // Snow white
        cardStyleImages.Add(new GachaItem(49, "SnowWhite(平民)", Resources.Load<Sprite>("images/Skin/Snow White/Civil")));
        cardStyleImages.Add(new GachaItem(50, "SnowWhite(殺手)", Resources.Load<Sprite>("images/Skin/Snow White/Killer")));
        cardStyleImages.Add(new GachaItem(51, "SnowWhite(國王)", Resources.Load<Sprite>("images/Skin/Snow White/King")));
        cardStyleImages.Add(new GachaItem(52, "SnowWhite(騎士)", Resources.Load<Sprite>("images/Skin/Snow White/Knight")));
        cardStyleImages.Add(new GachaItem(53, "SnowWhite(王子)", Resources.Load<Sprite>("images/Skin/Snow White/Prince")));
        cardStyleImages.Add(new GachaItem(54, "SnowWhite(皇后)", Resources.Load<Sprite>("images/Skin/Snow White/Queen")));
        // 
        cardStyleImages.Add(new GachaItem(45, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Frozen/Civil")));
        cardStyleImages.Add(new GachaItem(56, "Frozen(殺手)", Resources.Load<Sprite>("images/Skin/Frozen/Killer")));
        cardStyleImages.Add(new GachaItem(57, "Frozen(國王)", Resources.Load<Sprite>("images/Skin/Frozen/King")));
        cardStyleImages.Add(new GachaItem(58, "Frozen(騎士)", Resources.Load<Sprite>("images/Skin/Frozen/Knight")));
        cardStyleImages.Add(new GachaItem(59, "Frozen(王子)", Resources.Load<Sprite>("images/Skin/Frozen/Prince")));
        cardStyleImages.Add(new GachaItem(60, "Frozen(皇后)", Resources.Load<Sprite>("images/Skin/Frozen/Queen")));
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
