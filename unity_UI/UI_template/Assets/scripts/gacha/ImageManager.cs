using UnityEngine;
using System.Collections.Generic;

public class ImageManager : MonoBehaviour
{
    public Dictionary<int, Sprite> skillImages;

    void Start()
    {
        InitializeSkillImages();
    }

    void InitializeSkillImages()
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
}
