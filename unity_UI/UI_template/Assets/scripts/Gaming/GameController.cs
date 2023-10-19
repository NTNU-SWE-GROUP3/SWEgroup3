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
    
    void Start()
    {
        isCom = true;
        drawCard = GameObject.Find("GameController").GetComponent<DrawCard>();
        if(isCom == true)
        {
            ComPlayer = GameObject.Find("ComputerPlayer").GetComponent<ComputerPlayer>();
        }
    }

    public void GameBegin()
    {
        drawCard.Draw();
        // if(isCom == true)
        // {
        // 
        // }
        ComPlayer.PlayRandomCard();
        // StartCoroutine(StartGame());
    }
    // IEnumerator StartGame()
    // {
        
    // }
}
