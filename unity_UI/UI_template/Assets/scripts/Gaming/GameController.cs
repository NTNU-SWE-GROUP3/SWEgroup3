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
    public int Trun;
    public Text TrunText;
    
    void Start()
    {
        Trun = 1;
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
        Trun = 1;
        drawCard.Draw();
        // Game Start
        TrunStart();
    }
    public void TrunStart()
    {
            TrunText.text = "回合:" + Trun.ToString();
            StartCoroutine(Timer.TurnCountdown());
            if(isCom == true)
            {
                ComPlayer.PlayRandomCard();
            }
            Trun++;
    }
    
}
