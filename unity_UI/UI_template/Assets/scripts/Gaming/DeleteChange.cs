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
   public void Change(GameObject WhoUse, int cardId, string skillName)
   {
        for (int i = 0; i < WhoUse.transform.childCount; i++)
        {
            if(WhoUse.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().id == cardId)
            {
                //刪除平民
                Destroy(WhoUse.transform.GetChild(i).gameObject);
                //新增卡片
                Debug.Log("新增");
                break;
            }
        }
   }
}
