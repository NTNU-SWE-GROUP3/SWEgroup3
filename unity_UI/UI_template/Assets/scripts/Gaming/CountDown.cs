using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CountDown : MonoBehaviour
{
    GameController instance;
    public int countdownTime;
    public static int TurnTime = 10;
    public Text countdownDisplay;
    public Text TimerText;
    // Start is called before the first frame update
    private void Start()
    {
        instance = GameObject.Find("GameController").GetComponent<GameController>();
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
        instance.GameBegin();
        countdownDisplay.gameObject.SetActive(false);
    }

    public IEnumerator TurnCountdown()
    {
        TurnTime = 10;
        TimerText.gameObject.SetActive(true);
        while(TurnTime > 0)
        {
             TimerText.text = TurnTime.ToString();
            yield return new WaitForSeconds(1);
            TurnTime -- ;
        }
         TimerText.text = "Show!";
        yield return new WaitForSeconds(1f);
        // instance.GameBegin(); call show
        TimerText.gameObject.SetActive(false);
    }
}
