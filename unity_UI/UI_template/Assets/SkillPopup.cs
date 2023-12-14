using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillPopup : MonoBehaviour
{
    public TextMeshProUGUI skillNameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI probabilityText;

    public void ShowSkillInfo(string skillName, string description, float probability)
    {
        Debug.Log("Showing skill info: " + skillName + ", " + description + ", " + probability);

        skillNameText.text = skillName;
        descriptionText.text = description;
        probabilityText.text = "Probability: " + probability.ToString("P2");

        //gameObject.SetActive(true);
    }

    // public void HideSkillInfo()
    // {
    //     gameObject.SetActive(false);
    // }
}