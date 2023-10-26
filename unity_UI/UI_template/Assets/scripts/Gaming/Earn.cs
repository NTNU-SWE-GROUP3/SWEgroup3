using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Earn : MonoBehaviour
{
    public Text EarnText;
    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.transform.childCount);
        EarnText.text = gameObject.transform.childCount.ToString();
    }
}
