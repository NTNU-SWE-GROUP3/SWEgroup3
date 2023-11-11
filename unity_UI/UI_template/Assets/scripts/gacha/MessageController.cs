using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageController : MonoBehaviour
{
    public GameObject textSingleCash;
    public GameObject textMultCash;
    public GameObject textSingleCoin;
    public GameObject textMultCoin;
    [SerializeField] GotchaPanel gotchaPanel;


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        textSingleCoin.SetActive(false);
        textMultCoin.SetActive(false);
        textSingleCash.SetActive(false);
        textMultCash.SetActive(false);
    }

    public void ShowMessage(int textType)
    {
        Debug.Log(gotchaPanel.currentPage);
        if (gotchaPanel.currentPage == 1)
        {
            switch (textType)
            {
                case 1:
                    textSingleCoin.SetActive(true);
                    textMultCoin.SetActive(false);
                    textSingleCash.SetActive(false);
                    textMultCash.SetActive(false);
                    break;
                case 2:
                    textSingleCoin.SetActive(false);
                    textMultCoin.SetActive(true);
                    textSingleCash.SetActive(false);
                    textMultCash.SetActive(false);
                    break;
                default:
                    textSingleCoin.SetActive(false);
                    textMultCoin.SetActive(false);
                    textSingleCash.SetActive(false);
                    textMultCash.SetActive(false);
                    break;
            }
        }
        else if (gotchaPanel.currentPage == 2)
        {
            switch (textType)
            {
                case 1:
                    textSingleCoin.SetActive(false);
                    textMultCoin.SetActive(false);
                    textSingleCash.SetActive(true);
                    textMultCash.SetActive(false);

                    break;
                case 2:
                    textSingleCoin.SetActive(false);
                    textMultCoin.SetActive(false);
                    textSingleCash.SetActive(false);
                    textMultCash.SetActive(true);
                    break;
                default:
                    textSingleCoin.SetActive(false);
                    textMultCoin.SetActive(false);
                    textSingleCash.SetActive(false);
                    textMultCash.SetActive(false);
                    break;
            }
        }

    }
}
