using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteChange : MonoBehaviour
{
   public void Delete(GameObject WhoLoss,int cardId)
   {
        for (int i = 0; i < WhoLoss.transform.childCount; i++)
        {
            if(WhoLoss.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().id == cardId)
            {
                Destroy(WhoLoss.transform.GetChild(i).gameObject);
                break;
            }
            
        }
   }
}
