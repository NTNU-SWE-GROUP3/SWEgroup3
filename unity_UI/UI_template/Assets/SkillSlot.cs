/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class SkillSlot : MonoBehaviour, IPointerClickHandler
{
    public Image skillSprite;
    public string skillName;
    public string description;
    public float probability;

    private SkillPopup skillPopup;

    void Start()
    {
        skillPopup = FindObjectOfType<SkillPopup>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (skillPopup != null)
        {
            skillPopup.ShowSkillInfo(skillName, description, probability);
        }
    }
} */