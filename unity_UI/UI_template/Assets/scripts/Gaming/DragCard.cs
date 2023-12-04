using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragCard : MonoBehaviour,IDragHandler,IEndDragHandler,IBeginDragHandler
{
    public GameObject PlayerShow;
    public RectTransform rectTransform;
    public Vector2 originalRectPosition;
    private CanvasGroup canvasGroup;
    public AudioClip DragSound;
    AudioSource audioSource;
    public static bool canDrag = false;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        if(eventData.pointerDrag.GetComponent<CardDisplay>().cardBack == false && this.transform.parent.name == "PlayerArea"&& !DropZone.haveCard && canDrag)
        {
            originalRectPosition = rectTransform.position;
            canvasGroup.blocksRaycasts = false;
            
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        // Debug.Log(this.transform.parent.name);
        if(eventData.pointerDrag.GetComponent<CardDisplay>().cardBack == false && this.transform.parent.name == "PlayerArea"&& !DropZone.haveCard && canDrag)
        {
            if(CountDown.timeUp == false)
            {
                rectTransform.position = eventData.position;
            }
            else
            {
                rectTransform.position = originalRectPosition;
                //canvasGroup.blocksRaycasts = true;
                return;
            }
            
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        //audioSource.PlayOneShot(DragSound);
        if (eventData.pointerDrag.GetComponent<CardDisplay>().cardBack == false && this.transform.parent.name == "PlayerArea" && !DropZone.haveCard && canDrag)
        {
            if(DropZone.backToHand)
            {
                rectTransform.position = originalRectPosition;
            }

            DropZone.backToHand = true;

            
            
        }
        
        canvasGroup.blocksRaycasts = true;
    }

}
