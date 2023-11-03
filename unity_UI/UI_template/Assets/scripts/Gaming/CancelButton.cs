using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButton : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject ConfirmButton;
    void Start()
    {
        gameObject.SetActive(false);
    }
    public void ClickCancel()
    {
        gameObject.SetActive(false);
        ConfirmButton.SetActive(false);
        ClickDetector.cardId = -1;
        
    }
}
