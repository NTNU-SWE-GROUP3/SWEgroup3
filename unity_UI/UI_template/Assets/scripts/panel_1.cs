using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class PanelSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{

    private Vector3 panelOneLocation;
    private Vector3 currentLocation;
    public float percentThreshold = 0.2f;
    public float easing = 0.5f;
   // public int totalPages = 5;
   // private int currentPage = 3;


    void Start()
    {
        panelOneLocation = transform.localPosition;
        currentLocation = transform.localPosition;
    }
   


    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.y - data.position.y;
        transform.localPosition = currentLocation - new Vector3(0, difference, 0);
    }

    public void OnEndDrag(PointerEventData data)
    {

        currentLocation = transform.localPosition;
        /*
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = panelLocation;
            if (percentage > 0 && currentPage < totalPages)
            {
                currentPage++;
                newLocation += new Vector3(-Screen.width, 0, 0);
            }
            else if (percentage < 0 && currentPage > 1)
            {
                currentPage--;
                newLocation += new Vector3(Screen.width, 0, 0);
            }
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
        }
        else
        {*/
        //StartCoroutine(SmoothMove(transform.position, panelOneLocation, easing));
        // }
    }

    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
}
