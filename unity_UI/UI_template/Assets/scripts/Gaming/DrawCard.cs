using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DrawCard : MonoBehaviour
{
    public GameObject Card;
    public GameObject PlayerArea;
    public GameObject OpponentArea;
    public static bool CpGetSetA;

    public static int x = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Draw()
    {
       StartCoroutine(Drawing());
    }
    IEnumerator Drawing()
    {
        var n = Random.Range(0, 2);
        GameObject First ;
        GameObject Second;
        if(n == 0)
        {
            First = OpponentArea;
            Second = PlayerArea;
            CpGetSetA = true;
        }
        else
        {
            First = PlayerArea;
            Second = OpponentArea;
            CpGetSetA = false;
        }
        x = 0;
        for(int i = 0;i<10;i++)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject handCard = Instantiate(Card,transform.position,transform.rotation);
            handCard.transform.SetParent(First.transform,true);
        }  
        for(int i = 0;i<10;i++)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject handCard = Instantiate(Card,transform.position,transform.rotation);
            handCard.transform.SetParent(Second.transform,true);
        }  
    }


}
