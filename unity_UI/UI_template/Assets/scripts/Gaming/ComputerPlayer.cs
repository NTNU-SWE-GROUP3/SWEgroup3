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
        // 出牌
    }

    public void PlayRandomCard()
    {
        if (CardDatabase.cardList != null && CardDatabase.cardList.Count > 0)
        {
            int randomIndex = Random.Range(0, CardDatabase.cardList.Count);
            Card randomCard = CardDatabase.cardList[randomIndex];

            PlayCard(randomCard);
        }
    }
}
