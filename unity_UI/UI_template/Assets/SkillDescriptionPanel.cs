using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class SkillDescriptionPanel : MonoBehaviour
{
    public TextMeshPro skillNameText;
    public TextMeshPro skillDescriptionText;
    public TextMeshPro skillProbabilityText;

    public void DisplaySkillInfo(string skillName, string skillDescription, string skillProbability)
    {
        Debug.Log("Displaying Skill Info!");
        Debug.Log("Skillname = " + skillName);
        Debug.Log("Skilldesc = " + skillDescription);
        Debug.Log("Skillprob = " + skillProbability);
        skillNameText.text = "Skill Name: " + skillName;
        skillDescriptionText.text = "Description: " + skillDescription;
        skillProbabilityText.text = "Probability: " + skillProbability;

        // You can add more logic to update other UI elements or perform additional actions
    }
}