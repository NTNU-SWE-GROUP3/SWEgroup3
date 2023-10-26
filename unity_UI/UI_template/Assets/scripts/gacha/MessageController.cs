using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageController : MonoBehaviour
{
    public GameObject textSingle;
    public GameObject textMult;
    

    // Start is called before the first frame update
    void Start()
    {
        textSingle.SetActive(false);
        textMult.SetActive(false);
    }

    public void ShowMessage(int textType)
    {
        switch (textType)
        {
            case 1:
                textSingle.SetActive(true);
                textMult.SetActive(false);
                break;
            case 2:
                textSingle.SetActive(false);
                textMult.SetActive(true);
                break;
            default:
                textSingle.SetActive(false);
                textMult.SetActive(false);
                break;
        }
    }
}
