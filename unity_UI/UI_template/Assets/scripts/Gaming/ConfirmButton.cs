using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmButton : MonoBehaviour
{
    DeleteChange deletChange;
    public Text skillName;
    public GameObject OpponentArea;
    public GameObject PlayerArea;
    public GameObject MessagePanel;
    public GameObject Panel;
    public GameObject CancelButton;
    public UseSkill useSkill;
    void Start()
    {
        deletChange = GameObject.Find("GameController").GetComponent<DeleteChange>();
        gameObject.SetActive(false);
    }
    public void ClickConfirm()
    {
        if (skillName.text == "簡易剔除!")
        {
            deletChange.Delete(OpponentArea,ClickDetector.cardId);
            ShowCard.RejectTimer = 1;
            MessagePanel.SetActive(false);
        }
        else if (skillName.text == "階級流動!")
        {
            deletChange.Change(PlayerArea,ClickDetector.cardId, "階級流動");
            ShowCard.RejectTimer = 1;
            MessagePanel.SetActive(false);
        }

        else
        {
            Debug.Log("useSkill");
            StartCoroutine(useSkill.Use(ClickDetector.skillId));
        }
        
        
    }
}
