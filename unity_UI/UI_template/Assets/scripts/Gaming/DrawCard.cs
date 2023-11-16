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
    public AudioClip DrawSound1;
    public AudioClip DrawSound2;
    public AudioClip tmp = null;
    AudioSource audioSource;

    public static int x = 0;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Draw()
    {
       StartCoroutine(Drawing());
    }
    IEnumerator Drawing()
    {
        var n = Random.Range(1, 100);
        GameObject First ;
        GameObject Second;
        if(n % 2 == 0)
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
            tmp = DrawSound1;
            DrawSound1 = DrawSound2;
            DrawSound2 = tmp;
        }
        x = 0;
        for(int i = 0;i<10;i++)
        {
            audioSource.PlayOneShot(DrawSound1);
            yield return new WaitForSeconds(0.1f);
            GameObject handCard = Instantiate(Card,transform.position,transform.rotation);
            handCard.transform.SetParent(First.transform,true);
        }  
        for(int i = 0;i<10;i++)
        {
            audioSource.PlayOneShot(DrawSound2);
            yield return new WaitForSeconds(0.1f);
            GameObject handCard = Instantiate(Card,transform.position,transform.rotation);
            handCard.transform.SetParent(Second.transform,true);
        }  
    }


}
