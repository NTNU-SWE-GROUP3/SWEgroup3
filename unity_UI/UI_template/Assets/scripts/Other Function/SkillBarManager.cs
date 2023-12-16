// // using UnityEngine;
// // using UnityEngine.UI;

// // public class SkillBarManager : MonoBehaviour
// // {
// //     public Transform skillBarPanel;
// //     public Sprite[] skillSprites;
// //     public GameObject skillSlotPrefab;
// //     public SkillPopup skillPopupPrefab;

// //     void Start()
// //     {
// //         LoadSkillSprites();
// //         InstantiateSkillSlots();
// //     }

// //     void LoadSkillSprites()
// //     {
// //         skillSprites = Resources.LoadAll<Sprite>("images/MainSc/Skill");
// //     }

// //     void InstantiateSkillSlots()
// //     {
// //         string[] skillNames = { "Skill 1", "Skill 2", "Skill 3", "Skill 4", "Skill 5", "Skill 6", "Skill 7", "Skill 8", "Skill 9", "Skill 10" };
// //         string[] skillDescs = { "Description 1", "Description 2", "Description 3", "Description 4", "Description 5", "Description 6", "Description 7", "Description 8", "Description 9", "Description 10" };
// //         string[] skillProbs = { "0.05", "0.15", "0.1", "0.08", "0.12", "0.07", "0.1", "0.09", "0.1", "0.14"};
// //         float[] skillProbabilities = new float[skillProbs.Length];

// //         int index = 0;
// //         foreach (Sprite skillSprite in skillSprites)
// //         {
// //             GameObject skillSlot = Instantiate(skillSlotPrefab, skillBarPanel);
// //             Image slotImage = skillSlot.GetComponent<Image>();
// //             SkillSlot skillSlotScript = skillSlot.GetComponent<SkillSlot>();
// //             float.TryParse(skillProbs[index], out float parsedValue);
// //             skillProbabilities[index] = parsedValue;

// //             if (slotImage != null && skillSlotScript != null)
// //             {
// //                 slotImage.sprite = skillSprite;
// //                 skillSlotScript.skillSprite = skillSprite; 
// //                 skillSlotScript.skillName = skillNames[index]; // Assign a sample name
// //                 skillSlotScript.description = skillDescs[index];
// //                 skillSlotScript.probability = skillProbabilities[index]; // Assign a sample probability
// //             }
// //             else
// //             {
// //                 Debug.LogError("Image or SkillSlot component not found on skill slot prefab.");
// //             }
// //             index++;
// //         }
// //     }
// // }

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillBarManager : MonoBehaviour
{
    public GameObject skillSlotPrefab;
    public Transform skillBarPanel;
    public Sprite[] skillSprites;
    private SkillPopup skillPopupPrefab;
    private GameObject SkillPopupObject;  // Declare as a class-level variable

    // Assign these in the Unity Editor by dragging the TextMeshProUGUI components
    public TextMeshProUGUI skillNameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI probabilityText;

    // void Start()
    // {
    //     LoadSkillSprites();
    //     InstantiateSkillSlots();
    //     GameObject SkillPopupObject = GameObject.Find("SkillPopupObject");

    //     // Use the skillpopupobject transform as the parent for skill popups
    //     if (SkillPopupObject != null)
    //     {
    //         canvasTransform = SkillPopupObject.transform;
    //     }
    //     else
    //     {
    //         Debug.LogError("SkillPopupObject not found in the scene.");
    //     }
    // }
    void Start()
    {
        LoadSkillSprites();
        InstantiateSkillSlots();
    }

    void LoadSkillSprites()
    {
        // Load all sprites from the "images/MainSc/Skill" directory
        skillSprites = Resources.LoadAll<Sprite>("images/MainSc/Skill");
    }

    void InstantiateSkillSlots()
    {
        string[] skillNames = { "Skill 1", "Skill 2", "Skill 3", "Skill 4", "Skill 5", "Skill 6", "Skill 7", "Skill 8", "Skill 9", "Skill 10" };
        string[] skillDescs = { "Description 1", "Description 2", "Description 3", "Description 4", "Description 5", "Description 6", "Description 7", "Description 8", "Description 9", "Description 10" };
        string[] skillProbs = { "0.05", "0.15", "0.1", "0.08", "0.12", "0.07", "0.1", "0.09", "0.1", "0.14" };
        float[] skillProbabilities = new float[skillProbs.Length];

        int i = 0;
        foreach (Sprite skillSprite in skillSprites)
        {
            GameObject skillSlot = Instantiate(skillSlotPrefab, skillBarPanel);
            Image slotImage = skillSlot.GetComponent<Image>();
            float.TryParse(skillProbs[i], out float parsedValue);
            skillProbabilities[i] = parsedValue;

            if (slotImage != null)
            {
                slotImage.sprite = skillSprite;

                // Attach a click event handler to each skill slot
                Button slotButton = skillSlot.AddComponent<Button>();
                slotButton.onClick.AddListener(() => ShowSkillPopup(skillNames[i], skillDescs[i], skillProbabilities[i]));
                i++;
            }
            else
            {
                Debug.LogError("Image component not found on skill slot prefab.");
            }
        }
    }

    void ShowSkillPopup(string skillName, string description, float probability)
    {
        SkillPopup skillPopupInstance = Instantiate(skillPopupPrefab); // Instantiate the SkillPopup
        skillPopupInstance.transform.SetParent(skillBarPanel); // Set parent to skillBarPanel
        skillPopupInstance.ShowSkillInfo(skillName, description, probability);
    }
}




// using UnityEngine;
// using UnityEngine.UI;

// public class SkillBarManager : MonoBehaviour
// {
//     public GameObject skillSlotPrefab; // Reference to your skill slot prefab
//     public Transform skillBarPanel; // Reference to your skill bar panel
//     public Sprite[] skillSprites; // Loaded skill sprites from Resources
//     public SkillPopup skillPopup; // Reference to the SkillPopup script

//     void Start()
//     {
//         LoadSkillSprites();
//         InstantiateSkillSlots();
//     }

//     void LoadSkillSprites()
//     {
//         // Load all sprites from the "images/MainSc/Skill" directory
//         skillSprites = Resources.LoadAll<Sprite>("images/MainSc/Skill");
//     }

//     void InstantiateSkillSlots()
//     {
//         // Assuming skillSprites array is loaded
//         string[] skillNames = { "Skill 1", "Skill 2", "Skill 3", "Skill 4", "Skill 5", "Skill 6", "Skill 7", "Skill 8", "Skill 9", "Skill 10" };
//         string[] skillDescs = { "Description 1", "Description 2", "Description 3", "Description 4", "Description 5", "Description 6", "Description 7", "Description 8", "Description 9", "Description 10" };
//         string[] skillProbs = { "0.05", "0.15", "0.1", "0.08", "0.12", "0.07", "0.1", "0.09", "0.1", "0.14"};
//         float[] skillProbabilities = new float[skillProbs.Length];
//         int i = 0;
//         // Loop through each sprite and instantiate a skill slot
//         foreach (Sprite skillSprite in skillSprites)
//         {
//             GameObject skillSlot = Instantiate(skillSlotPrefab, skillBarPanel);
//             Image slotImage = skillSlot.GetComponent<Image>();
//             float.TryParse(skillProbs[i], out float parsedValue);
//             skillProbabilities[i] = parsedValue;

//             if (slotImage != null)
//             {
//                 // Set the sprite of the Image component in the skill slot
//                 slotImage.sprite = skillSprite;

//                 // Attach the SkillSlotClickHandler to each skill slot
//                 SkillSlotClickHandler clickHandler = skillSlot.AddComponent<SkillSlotClickHandler>();
//                 string skill_name = skillNames[i];
//                 string skill_desc = skillDescs[i];
//                 float skill_prob = skillProbabilities[i];
//                 clickHandler.SetSkillInfo(skill_name, skill_desc, skill_prob);
//                 clickHandler.SetSkillPopup(skillPopup);
//             }
//             else
//             {
//                 Debug.LogError("Image component not found on skill slot prefab.");
//             }
//             i++;
//         }
//     }
// }