using System;
using UnityEngine;
using UnityEngine.UI;

public class FifteenTimer : MonoBehaviour
{
    public static float CountDownTime;
    public Text TextCountDown;

    void Start()
    {
        CountDownTime = 15.0F;
    }

    // Update is called once per frame
    void Update()
    {
        TextCountDown.text = String.Format("{0:00.00}", CountDownTime);
        CountDownTime -= Time.deltaTime;
        if (CountDownTime <= 0.0F)
        {
            CountDownTime = 0.0F;
        }
    }
}