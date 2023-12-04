using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
時間限縮 ：縮短對手選牌時間5秒 (1回合) (可升級) (time warp, time theft)

凡人進化 ：一張平民變騎士 (整場) (peasant's ascension)

暗影轉職：一張平民變暗殺者 (整場) (civillian's dagger)

技能封印：禁止對方使用玩家技能 (1回合) (可升級) (skill nullifier)

平民免疫 ：平民卡效果無效 (1回合) (可升級) (peasant Immunity)

黃金風暴 ：獲勝金幣總數*1.5 (整場) (可升級) (gold Rush)

洞悉對手：看對手剩下的手牌 (1回合) (可升級) (hand insight, deck recon)

抉擇束縛：限制對手二選一出牌 (1回合) (可升級) (dilemma dictator)

對手減一：對手贏牌區張數-1 (整場) (triumph manipulation)

勝者之堆：己方贏牌區+1 (整場) (victory boost)
*/

public class CombineFunc : MonoBehaviour
{
    public Text messageBox;
    public Image highlightImage; // Reference to the UI Image component for highlighting

    private List<CardItem> selectedCards = new List<CardItem>(); // Use a list to store selected cards

    private Color normalColor = Color.white; // Color for non-highlighted state
    private Color highlightColor = Color.yellow; // Color for highlighted state

    public void SelectCard(CardItem card)
    {
        if (selectedCards.Contains(card))
        {
            // Card is already selected, remove it
            selectedCards.Remove(card);
            HighlightCard(card, false); // Remove highlight
        }
        else
        {
            // Card is not selected, add it
            selectedCards.Add(card);
            HighlightCard(card, true); // Highlight the selected card
        }

        if (selectedCards.Count == 2)
        {
            TryCombineCards();
        }
    }

    public void HighlightCard(CardItem card, bool highlight)
    {
        // You might need to modify this based on your UI structure
        // For example, change the color or add a border to signify highlighting
        Image cardImage = card.GetComponent<Image>();

        if (cardImage != null)
        {
            cardImage.color = highlight ? highlightColor : normalColor;
        }
    }

    private void TryCombineCards()
    {
        if (selectedCards[0].cardSkill == selectedCards[1].cardSkill)
        {
            // Cards have the same type, ask for confirmation
            string confirmationMessage = $"Do you want to combine card {selectedCards[0].cardSkill} to card {selectedCards[1].cardSkill}?";
            ShowMessageBox(confirmationMessage);
        }
        else
        {
            // Cards have different types, show an error message
            ShowMessageBox("The cards are not the same type");
        }

        ResetSelectedCards();
    }

    private void ShowMessageBox(string message)
    {
        messageBox.text = message;
    }

    private void ResetSelectedCards()
    {
        foreach (var card in selectedCards)
        {
            HighlightCard(card, false); // Remove highlight from all selected cards
        }

        selectedCards.Clear();
    }
}