using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDetector : MonoBehaviour
{
    public static int cardId;
    public static int skillId;
    public Transform ConfirmButton;
    public Transform CancelButton;

    public void Awake()
    {
        cardId = -1;
        skillId = -1;
        ConfirmButton = GameObject.Find("Canvas").GetComponentInChildren<Transform>().Find("MessagePanel/ConfirmButton");
        CancelButton = GameObject.Find("Canvas").GetComponentInChildren<Transform>().Find("MessagePanel/CancelButton");
    }
    void Update()
    {
        if(this.gameObject.layer == 12)
        {
            if(ClickDetector.cardId != -1)
            {
                ConfirmButton.gameObject.SetActive(true);
                CancelButton.gameObject.SetActive(true);
            }
        }
        if(this.gameObject.name == "PlayerSkill(Clone)")
        {
            if(ClickDetector.skillId != -1)
            {
                ConfirmButton.gameObject.SetActive(true);
                CancelButton.gameObject.SetActive(true);
            }
        }
    }
    public void OnPointerClick()
    {
        if(this.gameObject.layer == 12)
        cardId = this.gameObject.GetComponent<CardDisplay>().id;
        if(this.gameObject.name == "PlayerSkill(Clone)")
        skillId = this.gameObject.GetComponent<SkillDisplay>().id;
    }
    
}

