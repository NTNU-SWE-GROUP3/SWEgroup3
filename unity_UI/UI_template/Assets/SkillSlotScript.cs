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

    // Function to set the skill style based on the skill ID or other parameters
    public void SetSkillStyle(int skillStyleID)
    {
        // You might want to replace this with your actual logic to determine the sprite
        Sprite skillSprite = GetSkillSprite(skillStyleID);

        // Set the sprite to the Image component
        if (skillSprite != null)
        {
            skillImage.sprite = skillSprite;
        }
        else
        {
            Debug.LogError("Skill sprite not found for ID: " + skillStyleID);
        }
    }

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