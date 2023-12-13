using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPanel : MonoBehaviour
{
    GridLayoutGroup GLG;
    void Start()
    {
        GLG = gameObject.GetComponent<GridLayoutGroup>();
    }
    void Update()
    {
        if(gameObject.transform.childCount > 9)
        {
            GLG.spacing = new Vector2(85,-80);
        }
        else
        {
            GLG.spacing = new Vector2(85,5);
        }
    }
}
