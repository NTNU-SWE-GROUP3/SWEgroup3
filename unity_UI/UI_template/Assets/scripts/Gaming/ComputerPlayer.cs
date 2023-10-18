using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayer : MonoBehaviour
{
    CardDatabase cardDatabase;
    
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
        // 在這裡執行出牌的代碼
        // 例如，你可以在這裡使用 SelectedCard 做一些事情
    }

    public void PlayRandomCard()
    {
        if (CardDatabase.cardList != null && CardDatabase.cardList.Count > 0)
        {
            int randomIndex = Random.Range(0, CardDatabase.cardList.Count);
            Card randomCard = CardDatabase.cardList[randomIndex];

            // 出牌
            PlayCard(randomCard);
        }
    }
}
