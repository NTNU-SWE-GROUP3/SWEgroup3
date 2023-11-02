using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ComputerPlayer : MonoBehaviour
{
    public GameObject OpponentArea;
    public GameObject OpponentShow;
    public Transform Card;
    public int test = 0;
    
    public void PlayCard()
    {
        //int randomIndex = Random.Range(0,OpponentArea.transform.childCount);
        //Card = OpponentArea.transform.GetChild(randomIndex);
        if(test == 0){
        Card = OpponentArea.transform.GetChild(0);
        test++;
        }
        else
            Card = OpponentArea.transform.GetChild(OpponentArea.transform.childCount-1);
        Card.SetParent(OpponentShow.transform,false);
        Card.position = OpponentShow.transform.position;
        Card.gameObject.layer = LayerMask.NameToLayer("CardBack");
    }

    public void PlayRandomCard()
    {
        Invoke("PlayCard",3);
    }
}
