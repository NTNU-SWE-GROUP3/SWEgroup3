using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerHandCard : MonoBehaviour
{
    
    public List<Card> handCardSetA = new List<Card>();
    public List<Card> handCardSetB = new List<Card>();

    public static List<Card> staticHandCardSetA = new List<Card>();
    public static List<Card> staticHandCardSetB = new List<Card>();
    public static int x = 0;

    // public GameObject[] Clones;
    // public GameObject Hands;

    // Start is called before the first frame update
    void Start()
    {
        
        for(int i = 0;i<10;i++)
        {
            handCardSetA[i] = CardDatabase.cardList[i];
            handCardSetB[i] = CardDatabase.cardList[i+10];
        }
        staticHandCardSetA = handCardSetA;
        staticHandCardSetB = handCardSetB;
        
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    
}
