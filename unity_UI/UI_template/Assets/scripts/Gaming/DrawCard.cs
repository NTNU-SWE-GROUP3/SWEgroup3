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
         x = 0;
        GameObject First = PlayerArea;
        GameObject Second = OpponentArea;
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
