using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    public DrawCard drawCard;
    
    void Start()
    {
        drawCard = GameObject.Find("GameController").GetComponent<DrawCard>();
    }

    public void GameBegin()
    {
        drawCard.Draw();
        // StartCoroutine(StartGame());
    }
    // IEnumerator StartGame()
    // {
        
    // }
}
