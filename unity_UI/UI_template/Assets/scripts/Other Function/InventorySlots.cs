using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlots : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0) {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;
        }
        
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InventorySlots : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;

        if (dropped != null)
        {
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            CardItem droppedCard = dropped.GetComponent<CardItem>();
            CardItem slotCard = GetComponentInChildren<CardItem>();

            if (draggableItem != null && droppedCard != null && slotCard != null)
            {
                if (droppedCard.SkillId == slotCard.SkillId)
                {
                    // Cards have the same skill, ask for confirmation
                    string confirmationMessage = $"Do you want to combine cards with SkillId {droppedCard.SkillId}?";
                    bool combineConfirmed = true/* Implement your own confirmation logic here ;

                    if (combineConfirmed)
                    {
                        // Combine the cards
                        CombineCards(draggableItem, droppedCard, slotCard);
                    }
                }
                else
                {
                    // Cards have different skills, show an error message
                    Debug.LogError("The cards have different skills");
                }
            }

            draggableItem.parentAfterDrag = transform;
        }
    }

    private void CombineCards(DraggableItem draggableItem, CardItem droppedCard, CardItem slotCard)
    {
        // Implement your logic for combining cards here

        // For example, you can destroy the dropped card and update the slot card's properties
        Destroy(droppedCard.gameObject);

        // Update the slot card's properties based on the combination logic
        //slotCard.LevelUp();

        // You may want to update UI or other game logic based on the combination
    }
}*/