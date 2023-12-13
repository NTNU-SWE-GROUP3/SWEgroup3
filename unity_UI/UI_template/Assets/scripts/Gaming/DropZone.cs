using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour,IDropHandler
{
    public GameObject showDisplay;
    public static bool backToHand = true;
    public static bool haveCard = false;
    
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.GetComponent<CardDisplay>().cardBack == false  && DragCard.canDrag)
        {
            if(haveCard)
            {
                Debug.Log("There's a card already.");
            }
            else
            {
                Debug.Log(eventData.pointerDrag.name + "was dropped on " + gameObject.name);
            
                if(eventData.pointerDrag != null)
                {
                    
                    eventData.pointerDrag.transform.SetParent(gameObject.transform,true);
                    eventData.pointerDrag.GetComponent<RectTransform>().position = showDisplay.transform.position;
                    eventData.pointerDrag.layer = LayerMask.NameToLayer("CardBack");
                    backToHand = false;
                    haveCard = true;
                }
            }
        }
        
        
        
        
    }
}
