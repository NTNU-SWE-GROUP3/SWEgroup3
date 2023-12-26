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
    public IEnumerator Drawing(int gameType,int n)
    {
        GameObject First ;
        GameObject Second;
        First = PlayerArea;
        Second = OpponentArea;
        CpGetSetA = false;
        tmp = DrawSound1;
        DrawSound1 = DrawSound2;
        DrawSound2 = tmp;
        x =0;
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
