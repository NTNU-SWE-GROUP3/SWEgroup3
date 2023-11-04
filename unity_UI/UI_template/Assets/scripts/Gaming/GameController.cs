using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    public DrawCard drawCard;
    public bool isCom;
    public ComputerPlayer ComPlayer;
    public CountDown Timer;
    public static int Turn;
    public Text TurnText;
    public GameObject ConfirmButton;
    public GameObject CancelButton;
    public GameObject Panel;
    public GameObject MessagePanel;
    public GameObject SkillName;
    public GameObject WinImage;
    public Text NextRoundText;
    
    void Start()
    {
        isCom = true;
        SkillName.SetActive(false);
        ConfirmButton.SetActive(false);
        CancelButton.SetActive(false);
        drawCard = GameObject.Find("GameController").GetComponent<DrawCard>();
        Timer = GameObject.Find("GameController").GetComponent<CountDown>();
        if(isCom == true)
        {
            ComPlayer = GameObject.Find("ComputerPlayer").GetComponent<ComputerPlayer>();
        }
    }

    public void GameBegin()
    {
        Turn = 0;
        drawCard.Draw();
        // Game Start
        StartCoroutine(TurnStart());
    }
    public IEnumerator TurnStart()
    {
       
        Turn++;
        MessagePanel.SetActive(true);
        ClickDetector.cardId = -1;
        for(int i = 0 ; i < Panel.transform.childCount;i++)
        {
            Destroy(Panel.transform.GetChild(i).gameObject);
        }
        ConfirmButton.SetActive(false);
        CancelButton.SetActive(false);
        SkillName.gameObject.SetActive(false);
        WinImage.SetActive(true);
        NextRoundText.gameObject.SetActive(true);
        NextRoundText.text = "Round" + Turn.ToString();
        yield return new WaitForSeconds(1);
        MessagePanel.SetActive(false);
        DropZone.haveCard = false;
        DropZone.backToHand = true;
        TurnText.text = "回合:" + Turn.ToString();
        StartCoroutine(Timer.TurnCountdown());
        if(isCom == true)
        {
            ComPlayer.PlayRandomCard();
        }
            
    }
    
}
