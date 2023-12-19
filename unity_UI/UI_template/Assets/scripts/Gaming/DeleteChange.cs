using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteChange : MonoBehaviour
{
    public GameObject Card;
    public static int ChangedCardId;
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
   public IEnumerator Change(GameObject WhoUse, int cardId, string skillName)
   {
        Debug.Log("Delete.Change");
        for (int i = 0; i < WhoUse.transform.childCount; i++)
        {
            if(WhoUse.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().id == cardId)
            {
                Debug.Log("Delete.Change:" + cardId);
                //刪除平民
                Destroy(WhoUse.transform.GetChild(i).gameObject);
                //新增卡片
                GameObject ChangeCard = Instantiate(Card,WhoUse.transform.position,WhoUse.transform.rotation);
                ChangeCard.transform.SetParent(WhoUse.transform,true);
                ChangeCard.layer = LayerMask.NameToLayer("Change");
                if(skillName == "階級流動")
                {
                    if(cardId < 10)
                    {
                        ChangedCardId = 3;
                        Debug.Log(cardId + " -> " + ChangedCardId);
                    }
                    else
                    {
                        ChangedCardId = 13;
                        Debug.Log(cardId + " -> " + ChangedCardId);
                    }
                    
                }
                else if (skillName == "暗影轉職")
                {
                    if(cardId < 10)
                    {
                        ChangedCardId = 5;
                        Debug.Log(cardId + " -> " + ChangedCardId);
                    }
                    else
                    {
                        ChangedCardId = 14;
                        Debug.Log(cardId + " -> " + ChangedCardId);
                    }
                }
                break;
            }
        }
        yield return null;
   }
}
