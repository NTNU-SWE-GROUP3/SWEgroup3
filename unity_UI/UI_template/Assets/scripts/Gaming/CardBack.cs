using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBack : MonoBehaviour
{
    public GameObject cardBack;
    // Start is called before the first frame update
    void Start()
    {
        if(CardDisplay.staticCardBack == true)
        {
            cardBack.SetActive(true);
        }
        else
        {
            cardBack.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(this.gameObject.layer)
        {
            case 10 :
                cardBack.SetActive(false);
                break;
            case 11 :
                cardBack.SetActive(true);
                break;
        }
    }
}
