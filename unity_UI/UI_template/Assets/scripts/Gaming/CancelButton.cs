using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ConfirmButton;
    public GameObject SkipButton;
    ShowCard SC;
        

    void Start()
    {
        gameObject.SetActive(false);
        SC = GameObject.Find("GameController").GetComponent<ShowCard>();
    }
    public void ClickCancel()
    {
        gameObject.SetActive(false);
        ConfirmButton.SetActive(false);
        if(SC.skillMessage.text != "抉擇束縛!" || SC.skillDescription.text != "請從以下兩張牌中擇一出牌")
            SkipButton.gameObject.SetActive(true);
        ClickDetector.cardId = -1;
        ClickDetector.skillId = -1;
    }
}
