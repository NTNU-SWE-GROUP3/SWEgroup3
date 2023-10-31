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
    
    void Start()
    {
        Turn = 1;
        isCom = true;
        drawCard = GameObject.Find("GameController").GetComponent<DrawCard>();
        Timer = GameObject.Find("GameController").GetComponent<CountDown>();
        if(isCom == true)
        {
            ComPlayer = GameObject.Find("ComputerPlayer").GetComponent<ComputerPlayer>();
        }
    }

    public void GameBegin()
    {
        Turn = 1;
        drawCard.Draw();
        // Game Start
        TurnStart();
    }
    public void TurnStart()
    {
            TurnText.text = "回合:" + Turn.ToString();
            StartCoroutine(Timer.TurnCountdown());
            if(isCom == true)
            {
                ComPlayer.PlayRandomCard();
            }
            Turn++;
    }
    
}
