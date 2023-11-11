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

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        if(eventData.pointerDrag.GetComponent<CardDisplay>().cardBack == false && this.transform.parent.name == "PlayerArea"&& !DropZone.haveCard)
        {
            
            originalRectPosition = rectTransform.position;
            canvasGroup.blocksRaycasts = false;
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        // Debug.Log(this.transform.parent.name);
        if(eventData.pointerDrag.GetComponent<CardDisplay>().cardBack == false && this.transform.parent.name == "PlayerArea"&& !DropZone.haveCard)
        {
            rectTransform.position = eventData.position;
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        if(eventData.pointerDrag.GetComponent<CardDisplay>().cardBack == false && this.transform.parent.name == "PlayerArea" && !DropZone.haveCard)
        {
            if(DropZone.backToHand)
            {
                rectTransform.position = originalRectPosition;
            }

            DropZone.backToHand = true;

            
            canvasGroup.blocksRaycasts = true;
        }
        

    }

}
