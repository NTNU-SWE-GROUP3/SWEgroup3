using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeButton : MonoBehaviour
{
    public HorizontalLayoutGroup cardLayoutGroup;
    public float swipeSpeed = 10f;

    public void NextPage()
    {
        Vector3 currentPosition = cardLayoutGroup.transform.position;
        currentPosition.x -= swipeSpeed;
        cardLayoutGroup.transform.position = currentPosition;
    }

    public void PrevPage()
    {
        Vector3 currentPosition = cardLayoutGroup.transform.position;
        currentPosition.x += swipeSpeed;
        cardLayoutGroup.transform.position = currentPosition;
    }
}