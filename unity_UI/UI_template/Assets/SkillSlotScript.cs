using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class SkillSlotScript : MonoBehaviour
{
    //public GameObject SkillPopupPrefab;
    public Image skillImage;
    private int skillStyleID;

    // Function to set the skill style based on the skill ID or other parameters
    public void SetSkillStyle(int skillStyleID)
    {
        this.skillStyleID = skillStyleID;
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

    // public void OnSkillSlotClicked()
    // {
    //     // Replace with the actual popup panel prefab
    //     GameObject SkillPopupObject = Instantiate(SkillPopupPrefab);
    //     SkillPopup popupScript = SkillPopupObject.GetComponent<SkillPopup>();

    //     // Set the content based on the skillStyleID
    //     popupScript.ShowSkillInfo(GetSkillName(skillStyleID), GetSkillDesc(skillStyleID), GetSkillProbability(skillStyleID));

    //     // Attach a close button listener to destroy the popup when closed
    //     Button closeButton = SkillPopupObject.GetComponentInChildren<Button>();
    //     closeButton.onClick.AddListener(() => Destroy(SkillPopupObject));
    // }

    private Sprite GetSkillSprite(int skillStyleID)
    {
        // // Load the sprites from the specified folder
        // Object[] skillSprites = Resources.LoadAll("images/MainSc/Skill", typeof(Sprite));

        // // Check if the skillStyleID is within the array bounds
        // if (skillStyleID >= 0 && skillStyleID <= skillSprites.Length)
        // {
        //     return (Sprite)skillSprites[skillStyleID-1];
        // }
        string SkillImagesPath = "images/MainSc/Skill/";
        if (skillStyleID == 1)
        {
            return Resources.Load<Sprite>(SkillImagesPath+"SKillTime");
        }
        else if (skillStyleID == 2)
        {
            return Resources.Load<Sprite>(SkillImagesPath+"SkillKnight");
        }
        else if (skillStyleID == 3)
        {
            return Resources.Load<Sprite>(SkillImagesPath+"SkillAssassin");
        }
        else if (skillStyleID == 4)
        {
            return Resources.Load<Sprite>(SkillImagesPath+"SkillStopSkill");
        }
         else if (skillStyleID == 5)
        {
            return Resources.Load<Sprite>(SkillImagesPath+"SkillCivilian" );
        }
         else if (skillStyleID == 6)
        {
            return Resources.Load<Sprite>(SkillImagesPath+"SkillCoin");
        }
         else if (skillStyleID == 7)
        {
            return Resources.Load<Sprite>(SkillImagesPath+"SkillShowOpponent");
        }
         else if (skillStyleID == 8)
        {
            return Resources.Load<Sprite>(SkillImagesPath+"SkillCardChoose");
        }
         else if (skillStyleID == 9)
        {
            return Resources.Load<Sprite>(SkillImagesPath+"SkillMin1");
        }
         else if (skillStyleID == 10)
        {
            return Resources.Load<Sprite>(SkillImagesPath+"SkillEarn1");
        }

        // Return null if the skill ID is out of bounds
        return null;
    }

    private string GetSkillName(int skillStyleID)
    {
        // Replace with your actual skill names array
        string[] skillNames = { "Skill 1", "Skill 2", "Skill 3", "Skill 4", "Skill 5", "Skill 6", "Skill 7", "Skill 8", "Skill 9", "Skill 10" };
        return skillNames[skillStyleID];
    }

    private string GetSkillDesc(int skillStyleID)
    {
        // Replace with your actual skill descriptions array
        string[] skillDescs = { "Description 1", "Description 2", "Description 3", "Description 4", "Description 5", "Description 6", "Description 7", "Description 8", "Description 9", "Description 10" };
        return skillDescs[skillStyleID];
    }

    private float GetSkillProbability(int skillStyleID)
    {
        // Replace with your actual skill probabilities array
        float[] skillProbs = { 0.05f, 0.15f, 0.1f, 0.08f, 0.12f, 0.07f, 0.1f, 0.09f, 0.1f, 0.14f };
        return skillProbs[skillStyleID];
    }
}
