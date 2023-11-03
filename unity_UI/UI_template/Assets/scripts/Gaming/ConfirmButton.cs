using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmButton : MonoBehaviour
{
    DeleteChange deletChange;
    public Text skillName;
    public GameObject OpponentArea;
    public GameObject MessagePanel;
    public GameObject Panel;
    public GameObject CancelButton;
    void Start()
    {
        deletChange = GameObject.Find("GameController").GetComponent<DeleteChange>();
        gameObject.SetActive(false);
    }
    public void ClickConfirm()
    {
        if(skillName.text == "簡易剔除!")
        {
            deletChange.Delete(OpponentArea,ClickDetector.cardId);
        }
        ClickDetector.cardId = -1;
        for(int i = 0 ; i < Panel.transform.childCount;i++)
        {
            Destroy(Panel.transform.GetChild(i).gameObject);
        }
        gameObject.SetActive(false);
        CancelButton.SetActive(false);
        MessagePanel.SetActive(false);
    }
}
