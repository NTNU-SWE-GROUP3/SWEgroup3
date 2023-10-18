using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card
{
    public int id;
    public string cardName;
    public string cardSkill;
    public string cardDescription;
    public char set;
    public Sprite cardSprite;
    
    public Card()
    {

    }

    public Card(int _id, string _cardName, string _cardSkill, string _cardDescription, char _set, Sprite _cardSprite)
    {
        id = _id;
        cardName = _cardName;
        cardSkill = _cardSkill;
        cardDescription = _cardDescription;
        set = _set;
        cardSprite = _cardSprite;
    
    }

}



