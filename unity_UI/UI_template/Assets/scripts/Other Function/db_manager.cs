using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class DatabaseManager : MonoBehaviour
{
    private string databaseURL = "http://localhost:5000/getCardCollections";  // Update with your Flask server address

    public IEnumerator GetCardCollections(int account_id, System.Action<List<int>> callback)
    {
        string url = $"{databaseURL}?account_id={account_id}";

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {www.error}");
            }
            else
            {
                // Parse the JSON response
                List<int> cardCollections = new List<int>();
                cardCollections = JsonUtility.FromJson<List<int>>(www.downloadHandler.text);

                // Call the callback with the retrieved data
                callback?.Invoke(cardCollections);
            }
        }
    }
}