using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class SkillSlotScript : MonoBehaviour
{
    public Image skillImage;
    //private int skillStyle;

    // Function to set the skill style based on the skill ID or other parameters
    public void SetSkillStyle(int skillStyleID)
    {
        //skillStyle = skillStyleID;
        Sprite skillSprite = GetSkillSprite(skillStyleID);

        if (skillSprite != null)
        {
            skillImage.sprite = skillSprite;
        }
        else
        {
            Debug.LogError("Skill sprite not found for ID: " + skillStyleID);
        }
    }
    
    // private void OnClick()
    // {
    //     // Call a method in SkillCardDisplay or elsewhere to handle the click event
    //     SkillCardDisplay.Instance.OnSkillSlotClicked(skillStyle);
    // }

    // Function to get the skill sprite based on the skill ID
    private Sprite GetSkillSprite(int skillStyleID)
    {
        // Load the sprites from the specified folder
        Object[] skillSprites = Resources.LoadAll("images/MainSc/Skill", typeof(Sprite));
        
        // Check if the skillStyleID is within the array bounds
        if (skillStyleID >= 0 && skillStyleID < skillSprites.Length)
        {
            return (Sprite)skillSprites[skillStyleID];
        }

        // Return null if the skill ID is out of bounds
        return null;
    }
}