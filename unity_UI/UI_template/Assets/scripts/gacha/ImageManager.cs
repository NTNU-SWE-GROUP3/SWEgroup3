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
        cardStyleImages.Add(new GachaItem(2, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Frozen/Killer")));
        cardStyleImages.Add(new GachaItem(3, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Frozen/King")));
        cardStyleImages.Add(new GachaItem(4, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Frozen/Knight")));
        cardStyleImages.Add(new GachaItem(5, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Frozen/Prince")));
        cardStyleImages.Add(new GachaItem(6, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Frozen/Queen")));
        // Aladin
        cardStyleImages.Add(new GachaItem(7, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Aladin/Civil")));
        cardStyleImages.Add(new GachaItem(8, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Aladin/Killer")));
        cardStyleImages.Add(new GachaItem(9, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Aladin/King")));
        cardStyleImages.Add(new GachaItem(10, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Aladin/Knight")));
        cardStyleImages.Add(new GachaItem(11, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Aladin/Prince")));
        cardStyleImages.Add(new GachaItem(12, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Aladin/Queen")));
        // Alice in wonderland
        cardStyleImages.Add(new GachaItem(13, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Alice in wonderland/Civil")));
        cardStyleImages.Add(new GachaItem(14, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Alice in wonderland/Killer")));
        cardStyleImages.Add(new GachaItem(15, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Alice in wonderland/King")));
        cardStyleImages.Add(new GachaItem(16, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Alice in wonderland/Knight")));
        cardStyleImages.Add(new GachaItem(17, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Alice in wonderland/Prince")));
        cardStyleImages.Add(new GachaItem(18, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Alice in wonderland/Queen")));
        // Cinderella
        cardStyleImages.Add(new GachaItem(19, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Cinderella/Civil")));
        cardStyleImages.Add(new GachaItem(20, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Cinderella/Killer")));
        cardStyleImages.Add(new GachaItem(21, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Cinderella/King")));
        cardStyleImages.Add(new GachaItem(22, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Cinderella/Knight")));
        cardStyleImages.Add(new GachaItem(23, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Cinderella/Prince")));
        cardStyleImages.Add(new GachaItem(24, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Cinderella/Queen")));
        // Romet and Julliette
        cardStyleImages.Add(new GachaItem(25, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Romet and Juliette/Civil")));
        cardStyleImages.Add(new GachaItem(26, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Romet and Juliette/Killer")));
        cardStyleImages.Add(new GachaItem(27, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Romet and Juliette/King")));
        cardStyleImages.Add(new GachaItem(28, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Romet and Juliette/Knight")));
        cardStyleImages.Add(new GachaItem(29, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Romet and Juliette/Prince")));
        cardStyleImages.Add(new GachaItem(30, "Frozen(平民)", Resources.Load<Sprite>("images/Skin/Romet and Juliette/Queen")));
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
