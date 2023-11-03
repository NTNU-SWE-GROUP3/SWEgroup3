using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDetector : MonoBehaviour
{
    void Start()
    {
       
    }
    public void OnPointerClick()
    {
        if(this.gameObject.layer == 12)
        Debug.Log("Clicked: " + this.gameObject.GetComponent<CardDisplay>().id);
    }
}

