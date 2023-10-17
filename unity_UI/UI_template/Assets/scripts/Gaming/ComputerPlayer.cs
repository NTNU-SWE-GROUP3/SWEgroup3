using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayer : MonoBehaviour
{
    public CardDatabase cardDatabase;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayRandomCard();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayCard(Card SelectedCard)
    {
        //List<Card> playerHand = 
        //playerHand.Remove(selectedCard);
        
    }
    public void PlayRandomCard()
    {
        if (cardDatabase != null && cardDatabase.cardList.Count > 0)
        {
            int randomIndex = Random.Range(0, cardDatabase.cardList.Count);
            Card randomCard = cardDatabase.cardList[randomIndex];

            // 出牌
            PlayCard(randomCard);
        }
    }
}
