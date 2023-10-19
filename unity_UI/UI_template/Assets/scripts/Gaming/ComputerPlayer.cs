using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ComputerPlayer : MonoBehaviour
{
    public GameObject OpponentArea;
    public GameObject OpponentShow;
    
    public void PlayCard()
    {
        int randomIndex = Random.Range(0,OpponentArea.transform.childCount);
        OpponentArea.transform.GetChild(randomIndex).SetParent(OpponentShow.transform,false);
        OpponentShow.transform.GetChild(0).position = OpponentShow.transform.position;
    }

    public void PlayRandomCard()
    {
        Invoke("PlayCard",3);
    }
}
