using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PopupManager : MonoBehaviour
{
    public GameObject CombinePanel;
    public Text messageText;
    public Button YesButton;
    public Button NoButton;

    private bool combineConfirmation = false;

    private void Start()
    {
        // Assign click events to the buttons
        YesButton.onClick.AddListener(OnYesButtonClick);
        NoButton.onClick.AddListener(OnNoButtonClick);

        // Hide the popup initially
        CombinePanel.SetActive(false);
    }

    public void ShowConfirmationPopup(string message)
    {
        // Set the message text
        messageText.text = message;

        // Show the popup
        CombinePanel.SetActive(true);
    }

    private void OnYesButtonClick()
    {
        combineConfirmation = true;
        CombinePanel.SetActive(false);
    }

    private void OnNoButtonClick()
    {
        combineConfirmation = false;
        CombinePanel.SetActive(false);
    }

    public bool GetCombineConfirmation()
    {
        return combineConfirmation;
    }
}
/*

public class PopupManager : MonoBehaviour
{
    public GameObject CombinePanel;

    void Start()
    {
        // Ensure the popup panel is initially inactive
        CombinePanel.SetActive(false);
    }

    public void ShowPopup()
    {
        // Show the popup panel
        CombinePanel.SetActive(true);
    }

    public void ClosePopup()
    {
        // Close the popup panel
        CombinePanel.SetActive(false);
    }

    public void ConfirmYes()
    {
        // Implement the logic when "Yes" is clicked
        Debug.Log("Yes clicked");
        ClosePopup();
    }

    public void ConfirmNo()
    {
        // Implement the logic when "No" is clicked
        Debug.Log("No clicked");
        ClosePopup();
    }
}
*/