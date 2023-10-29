using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CountDown : MonoBehaviour
{
    GameController GC;
    public int countdownTime;
    public static int TurnTime = 3;
    public Text countdownDisplay;
    public Text TimerText;
    public GameObject PlayerArea;
    public GameObject PlayerShow;
    public GameObject ShowDisplay;
    public Transform Card;
    ShowCard showcard;

    // Start is called before the first frame update
    private void Start()
    {
        GC = GameObject.Find("GameController").GetComponent<GameController>();
        showcard = GameObject.Find("GameController").GetComponent<ShowCard>();
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1);
            countdownTime -- ;
        }
        countdownDisplay.text = "START!";
        yield return new WaitForSeconds(1f);
        GC.GameBegin();
        countdownDisplay.gameObject.SetActive(false);
    }

    public IEnumerator TurnCountdown()
    {
        TurnTime = 3;
        TimerText.gameObject.SetActive(true);
        while(TurnTime >= 0)
        {
            TimerText.text = TurnTime.ToString();
            yield return new WaitForSeconds(1);
            TurnTime -- ;
        }
        if(PlayerShow.transform.childCount == 0)
            NoPlayCard();
        yield return new WaitForSeconds(0.5f);
        TimerText.text = "Show!";
        yield return new WaitForSeconds(1f);
        showcard.Show(); 
        TimerText.gameObject.SetActive(false);
    }

    public void NoPlayCard()
    {
        Card = PlayerArea.transform.GetChild(PlayerArea.transform.childCount - 1);
        Card.SetParent(PlayerShow.transform,false);
        Card.position = ShowDisplay.transform.position;
        Card.gameObject.layer = LayerMask.NameToLayer("CardBack");
    }
}
