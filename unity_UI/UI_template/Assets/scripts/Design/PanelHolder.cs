using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{

    private Vector3 panelLocation;
    public float percentThreshold = 0.2f;
    public float easing = 0.5f;
    public int totalPages = 5;
    private int currentPage = 3;
    public MainAudioManager audioManager;

    void Start()
    {
        panelLocation = transform.position;
        audioManager = GameObject.Find("AudioBox").GetComponent<MainAudioManager>();
    }
    public void ToPage1()
    {
        if(currentPage == 5){
            audioManager.Page5jump();
        }
        int pageDiff = currentPage - 1;
        Vector3 newLocation = panelLocation;
        newLocation += new Vector3(Screen.width * pageDiff, 0, 0);
        StartCoroutine(SmoothMove(panelLocation, newLocation, easing));
        currentPage = 1;
        panelLocation = newLocation;
    }
    public void ToPage2()
    {
        if(currentPage == 5){
            audioManager.Page5jump();
        }
        int pageDiff = currentPage - 2;
        Vector3 newLocation = panelLocation;
        newLocation += new Vector3(Screen.width * pageDiff, 0, 0);
        StartCoroutine(SmoothMove(panelLocation, newLocation, easing));
        currentPage = 2;
        panelLocation = newLocation;
    }

    public void ToPage3()
    {
        if(currentPage == 5){
            audioManager.Page5jump();
        }
        int pageDiff = currentPage - 3;
        Vector3 newLocation = panelLocation;
        newLocation += new Vector3(Screen.width * pageDiff, 0, 0);
        StartCoroutine(SmoothMove(panelLocation, newLocation, easing));
        currentPage = 3;
        panelLocation = newLocation;
    }

    public void ToPage4()
    {
        audioManager.Page4jump();
        int pageDiff = currentPage - 4;
        Vector3 newLocation = panelLocation;
        newLocation += new Vector3(Screen.width * pageDiff, 0, 0);
        StartCoroutine(SmoothMove(panelLocation, newLocation, easing));
        currentPage = 4;
        panelLocation = newLocation;
    }
    public void ToPage5()
    {
        if(currentPage <= 3){
            audioManager.Page5jump();
        }
        int pageDiff = currentPage - 5;
        Vector3 newLocation = panelLocation;
        newLocation += new Vector3(Screen.width * pageDiff, 0, 0);
        StartCoroutine(SmoothMove(panelLocation, newLocation, easing));
        currentPage = 5;
        panelLocation = newLocation;
    }



    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.x - data.position.x;
        transform.position = panelLocation - new Vector3(difference, 0, 0);
    }

    public void OnEndDrag(PointerEventData data)
    {
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
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }
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
