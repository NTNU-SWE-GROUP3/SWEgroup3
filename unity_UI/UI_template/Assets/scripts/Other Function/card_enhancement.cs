using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class card_enhancement : MonoBehaviour
{
    //first, you need to collect from db
    //second, you do the card enhancement algortihm
    //third, update the database
    //fourth, show the card inventory

    // Define a Card class to represent a card

    private string apiUrl = "http://localhost:3306/card_collections/";
    
    // Retrieve card style for a specific account
    // Account ID for the account whose cards you want to retrieve
    public int accountId;

    // Call this method to retrieve card collections for a specific account
    public void GetCardCollectionsForAccount()
    {
        StartCoroutine(GetCardCollectionsRequest());
    }

    private IEnumerator GetCardCollectionsRequest()
    {
        string url = apiUrl + accountId;
        UnityWebRequest webRequest = UnityWebRequest.Get(url);

        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + webRequest.error);
        }
        else
        {
            string response = webRequest.downloadHandler.text;
            Debug.Log("Card Collections for Account " + accountId + ": " + response);
            // Parse the response and use the data to display the card collections in your Unity game.
        }
    }

    // combine two cards into a more powerful card
    public void CombineCards(int cardId1, int cardId2)
    {
        //use the retrieved card data to determine the result of the combination
        if (cardId1 == cardId2)
        {
            // Calculate the new power for the combined card (e.g., doubling the power)
            int combinedPower = GetCombinedPower(cardId1);

            // Update the UI or game logic with the combined card information
            Debug.Log("Combined Card - ID: " + cardId1 + ", Power: " + combinedPower);
        }
        else
        {
            Debug.Log("Cannot combine cards of different types.");
        }
    }

    private int GetCombinedPower(int cardId)
    {
        // WARNING : implement the combination result based on the group's design
        return 2; //* GetPowerFromDatabase(cardId); //this is just example
    }

    /*
    //starts
    public class Card
    {
        public string cardName;
        public int attackPower;

        public Card(string name, int power)
        {
            cardName = name;
            attackPower = power;
        }

        // Combine two cards to enhance their attack power
        public Card Combine(Card otherCard)
        {
            if (cardName == otherCard.cardName)
            {
                return new Card(cardName, attackPower + otherCard.attackPower);
            }
            else
            {
                Debug.Log("Cannot combine different type of cards.");
                return null;
            }
        }
    }

    // Initialize the inventory with some cards
    List<Card> inventory = new List<Card>();

    void Start()
    {
        // Adding example cards to the inventory
        inventory.Add(new Card("A", 30));
        inventory.Add(new Card("A", 30));
        inventory.Add(new Card("B", 25));
        inventory.Add(new Card("C", 40));

        // Combine two cards of the same type
        CombineCards("A");
    }

    // Combine two cards with the given name
    public void CombineCards(string cardName)
    {
        List<Card> cardsToCombine = new List<Card>();

        // Find all cards with the specified name in the inventory
        foreach (Card card in inventory)
        {
            if (card.cardName == cardName)
            {
                cardsToCombine.Add(card);
            }
        }

        // Check if there are at least 2 cards to combine
        if (cardsToCombine.Count >= 2)
        {
            // Combine the first two cards
            Card combinedCard = cardsToCombine[0].Combine(cardsToCombine[1]);

            // Remove the combined cards from the inventory
            inventory.Remove(cardsToCombine[0]);
            inventory.Remove(cardsToCombine[1]);

            // Add the combined card to the inventory
            inventory.Add(combinedCard);

            // Print the updated inventory
            PrintInventory();
        }
        else
        {
            Debug.Log("Not enough cards to combine.");
        }
    }

    // Print the current inventory
    void PrintInventory()
    {
        Debug.Log("Inventory:");
        foreach (Card card in inventory)
        {
            Debug.Log(card.cardName + " - Attack Power: " + card.attackPower);
        }
    }
    */
}
