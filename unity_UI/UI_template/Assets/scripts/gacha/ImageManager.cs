using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class ImageManager : MonoBehaviour
{
    public Dictionary<int, Sprite> skillImages;
    public Dictionary<int, Sprite> cardStyleImages;

    void Start()
    {
        InitializeSkill();
        InitializeCardStyle();
    }

    void InitializeSkill()
    {
        skillImages = new Dictionary<int, Sprite>();


        skillImages.Add(1, Resources.Load<Sprite>("images/Skin/Poker/Civil"));
        skillImages.Add(2, Resources.Load<Sprite>("images/Skin/Poker/Killer"));
        skillImages.Add(3, Resources.Load<Sprite>("images/Skin/Poker/King"));
        skillImages.Add(4, Resources.Load<Sprite>("images/Skin/Poker/Knight"));
        skillImages.Add(5, Resources.Load<Sprite>("images/Skin/Poker/Prince"));
        skillImages.Add(6, Resources.Load<Sprite>("images/Skin/Poker/Queen"));
        skillImages.Add(7, Resources.Load<Sprite>("images/Skin/Poker/Civil"));
        skillImages.Add(8, Resources.Load<Sprite>("images/Skin/Poker/Killer"));
        skillImages.Add(9, Resources.Load<Sprite>("images/Skin/Poker/King"));
        skillImages.Add(10, Resources.Load<Sprite>("images/Skin/Poker/Knight"));
    }
    void InitializeCardStyle()
    {
        cardStyleImages = new Dictionary<int, Sprite>();

        cardStyleImages.Add(1, Resources.Load<Sprite>("images/Skin/Frozen/Civil"));
        cardStyleImages.Add(2, Resources.Load<Sprite>("images/Skin/Frozen/Killer"));
        cardStyleImages.Add(3, Resources.Load<Sprite>("images/Skin/Frozen/King"));
        cardStyleImages.Add(4, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
        cardStyleImages.Add(5, Resources.Load<Sprite>("images/Skin/Frozen/Prince"));
        cardStyleImages.Add(6, Resources.Load<Sprite>("images/Skin/Frozen/Queen"));
        cardStyleImages.Add(7, Resources.Load<Sprite>("images/Skin/Frozen/Civil"));
        cardStyleImages.Add(8, Resources.Load<Sprite>("images/Skin/Frozen/Killer"));
        cardStyleImages.Add(9, Resources.Load<Sprite>("images/Skin/Frozen/King"));
        cardStyleImages.Add(10, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
        cardStyleImages.Add(11, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
        cardStyleImages.Add(12, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
        cardStyleImages.Add(13, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
        cardStyleImages.Add(14, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
        cardStyleImages.Add(15, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
        cardStyleImages.Add(16, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
        cardStyleImages.Add(17, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
        cardStyleImages.Add(18, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
        cardStyleImages.Add(19, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
        cardStyleImages.Add(20, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
        cardStyleImages.Add(21, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
        cardStyleImages.Add(22, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
        cardStyleImages.Add(23, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
        cardStyleImages.Add(24, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
        cardStyleImages.Add(25, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
        cardStyleImages.Add(26, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
        cardStyleImages.Add(27, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
        cardStyleImages.Add(28, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
        cardStyleImages.Add(29, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
        cardStyleImages.Add(30, Resources.Load<Sprite>("images/Skin/Frozen/Knight"));
    }

    public Sprite GetSkillImage(int skill_id)
    {
        if (skillImages.ContainsKey(skill_id))
        {
            return skillImages[skill_id];
        }
        else
        {
            Debug.LogWarning($"SkillImageMapper: No image found for skill with id {skill_id}.");
            return null;
        }
    }
    public Sprite GetCardStyleImage(int style_id)
    {
        if (cardStyleImages.ContainsKey(style_id))
        {
            return cardStyleImages[style_id];
        }
        else
        {
            Debug.LogWarning($"CardStyleImages: No image found for style with id {style_id}.");
            foreach (var key in cardStyleImages.Keys)
            {
                Debug.Log($"Key in dictionary: {key}");
            }
            return null;
        }
    }
}
