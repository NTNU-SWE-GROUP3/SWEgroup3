using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayer : MonoBehaviour
{
<<<<<<< HEAD
    CardDatabase cardDatabase;
=======
    // public CardDatabase cardDatabase;
>>>>>>> e2ccb93d1b977a132cc7cc9f91107c424728d35b
    
    // // Start is called before the first frame update
    // void Start()
    // {
    //     PlayRandomCard();
    // }

    // // Update is called once per frame
    // void Update()
    // {
    // }

<<<<<<< HEAD
    public void PlayCard(Card SelectedCard)
    {
        // 在這裡執行出牌的代碼
        // 例如，你可以在這裡使用 SelectedCard 做一些事情
    }

    public void PlayRandomCard()
    {
        if (CardDatabase.cardList != null && CardDatabase.cardList.Count > 0)
        {
            int randomIndex = Random.Range(0, CardDatabase.cardList.Count);
            Card randomCard = CardDatabase.cardList[randomIndex];
=======
    // public void PlayCard(Card SelectedCard)
    // {
    //     //List<Card> playerHand = 
    //     //playerHand.Remove(selectedCard);
        
    // }
    // public void PlayRandomCard()
    // {
    //     if (cardDatabase != null && cardDatabase.cardList.Count > 0)
    //     {
    //         int randomIndex = Random.Range(0, cardDatabase.cardList.Count);
    //         Card randomCard = cardDatabase.cardList[randomIndex];
>>>>>>> e2ccb93d1b977a132cc7cc9f91107c424728d35b

    //         // 出牌
    //         PlayCard(randomCard);
    //     }
    // }
}
