using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDisplay : MonoBehaviour
{
    public List<Card> displayCard = new List<Card>();

    public int displayId;

    public int id;
    public string cardName;
    public string cardSkill;
    public string cardDescription;
    public char set;
    public Sprite cardSprite;

    public Text nameText;
    public Text cardSkillText;
    public Text descriptionText;
    public Image cardImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        displayCard[0] = CardDatabase.cardList[displayId];
        id = displayCard[0].id;
        cardName = displayCard[0].cardName;
        cardSkill = displayCard[0].cardSkill;
        cardDescription = displayCard[0].cardDescription;
        set = displayCard[0].set;
        cardSprite = displayCard[0].cardSprite;

        nameText.text = cardName;
        cardSkillText.text = cardSkill;
        descriptionText.text = cardDescription;
        cardImage.sprite = cardSprite;
    }
}
