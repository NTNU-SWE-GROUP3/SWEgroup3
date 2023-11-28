using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ErrorMessage : MonoBehaviour
{
    [SerializeField] GameObject errorPanel;
    [SerializeField] TextMeshProUGUI errorMessage;

    void Awake()
    {
        ClearMessages();
        errorPanel.SetActive(false);
    }

    void ClearMessages()
    {
        errorMessage.text = "";
    }

    public void ShowErrorMessage(string message)
    {
        errorMessage.text = message;
        errorPanel.SetActive(true);
    }

}
