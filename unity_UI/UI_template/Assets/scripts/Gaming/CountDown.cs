using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CountDown : MonoBehaviour
{
    GameController instance;
    public int countdownTime;
    public Text countdownDisplay;
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
}
