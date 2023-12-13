using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class GotchaPanel : MonoBehaviour, IDragHandler, IEndDragHandler
{

    private Vector3 panelLocation;
    public float percentThreshold = 0.2f;
    public float easing = 0.5f;
    public int panelWidth = 1080;
    public int totalPages = 2;
    // private int currentPage = 1;
    public int currentPage = 1;


    void Start()
    {
        panelLocation = transform.localPosition;
        // Debug.Log(panelLocation);
    }

    void Awake()
    {
        // ToPage1();
    }
    public void ToPage1()
    {
        int pageDiff = currentPage - 1;
        Vector3 newLocation = panelLocation;
        newLocation += new Vector3(panelWidth * pageDiff, 0, 0);
        StartCoroutine(SmoothMove(panelLocation, newLocation, easing));
        currentPage = 1;
        panelLocation = newLocation;
    }

    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.x - data.position.x;
        // Debug.Log(difference);
        
        if(!(((currentPage==1)&&(difference<0))||((currentPage==totalPages)&&(difference>0)))){
        transform.localPosition = panelLocation - new Vector3(difference, 0, 0);
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        
        float percentage = (data.pressPosition.x - data.position.x) / panelWidth;
        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = panelLocation;
            if (percentage > 0 && currentPage < totalPages)
            {
                currentPage++;
                newLocation += new Vector3(-panelWidth, 0, 0);
            }
            else if (percentage < 0 && currentPage > 1)
            {
                currentPage--;
                newLocation += new Vector3(panelWidth, 0, 0);
            }
            StartCoroutine(SmoothMove(transform.localPosition, newLocation, easing));
            panelLocation = newLocation;
        }
        else
        {
            StartCoroutine(SmoothMove(transform.localPosition, panelLocation, easing));
        }
    }

    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.localPosition = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
}
