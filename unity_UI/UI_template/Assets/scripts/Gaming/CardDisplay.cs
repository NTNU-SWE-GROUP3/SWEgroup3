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
    
    public bool cardBack;
    public static bool staticCardBack;

    public Text nameText;
    public Text cardSkillText;
    public Text descriptionText;
    public Image cardImage;
    
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerHandCard.x < 10)
        {
            displayCard[0] = PlayerHandCard.staticHandCardSetA[PlayerHandCard.x];
            cardBack =false;
        }
        else
        {
            displayCard[0] = PlayerHandCard.staticHandCardSetB[PlayerHandCard.x-10];
            cardBack = true;
        }

        PlayerHandCard.x++;

        
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
        staticCardBack = cardBack;
       
    }

    // Update is called once per frame
    void Update()
    {
        

        
        
        // if(this.tag == "Clone")
        // {
        //     displayCard[0] = PlayerHandCard.staticHandCardSetA[PlayerHandCard.x + 1];
        //     PlayerHandCard.x ++;
        //     cardBack =false;
        //     this.tag = "Untagged";
        // }
        
    }

    void drawCardA(int index)
    {
        
    }
}
