using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PurchaseControl
{
    public class PurchaseController : MonoBehaviour
    {
        [SerializeField] GameObject purchasePanel;
        [SerializeField] InputField[] inputFields;
        [SerializeField] Dropdown dropdownRegion;
        [SerializeField] TextMeshProUGUI messageText;
        public Action actionReferences;
        string[] allOptions = { "Japan", "Taiwan", "Korea", "China", "Germany", "Italy", "France", "Spain", "Canada" };
        // Text messageText;

        void Start()
        {
            DropdownInit();
            InputFeildsInit();
            MessageInit();
        }

        public void OpenPurchasePanel()
        {
            purchasePanel.SetActive(true);
            actionReferences.buyClicked = false;
            actionReferences.cancelClicked = false;
            // ClearInputFields();
            ClearMessages();
            HideMessage();
        }

        public bool CardNumberCheck()
        {
            Debug.Log("Check Card Number");
            if (purchasePanel != null)
            {
                InputField cardNumField = purchasePanel.GetComponentInChildren<InputField>();
                if (cardNumField != null)
                {
                    // if (cardNumField.Length < 12)
                    // {
                    //     return false;
                    // }
                    // return true;
                }
                else
                {
                    Debug.Log("Can't find cardNumField");
                    return false;
                }
            }
            Debug.Log("purchasePanel is null.");
            return false;
        }

        void DropdownInit()
        {
            if (dropdownRegion != null)
            {
                dropdownRegion.ClearOptions();
                Debug.Log("Dropdown cleared.");

                foreach (var option in allOptions)
                {
                    dropdownRegion.options.Add(new Dropdown.OptionData(option));
                    Debug.Log("Option added: " + option);
                }

                dropdownRegion.value = 1; // default option
                Debug.Log("Default option set.");
            }
            else
            {
                Debug.LogError("Can't find dropdown.");
            }
        }

        void InputFeildsInit()
        {
            if (inputFields != null)
            {
                ClearInputFields();
                inputFields[0].characterLimit = 12;
                inputFields[0].contentType = InputField.ContentType.IntegerNumber;

                inputFields[1].characterLimit = 4;
                inputFields[1].contentType = InputField.ContentType.IntegerNumber;

                inputFields[2].characterLimit = 3;
                inputFields[2].contentType = InputField.ContentType.IntegerNumber;

                inputFields[4].characterLimit = 5;
                inputFields[4].contentType = InputField.ContentType.IntegerNumber;
            }
            else
            {
                Debug.Log("Failed to get input field.");
            }
        }

        void ClearInputFields()
        {
            foreach (InputField inputField in inputFields)
            {
                if (inputField != null)
                {
                    inputField.text = "";
                }
            }
        }

        void ClearMessages()
        {
            if (messageText != null)
            {
                messageText.text = "";
            }
            else
            {
                Debug.Log("Can't find message text.");
            }
        }

        void MessageInit()
        {
            // messageText = purchasePanel.transform.Find("Message").GetComponent<Text>();
            if (messageText == null)
            {
                Debug.Log("Cannot find Text under purchase panel.");

            }
            else
            {
                HideMessage();
            }

        }

        void HideMessage()
        {
            if (messageText != null)
            {
                messageText.gameObject.SetActive(false);
            }
        }

        public void DisplayMessage(string message)
        {
            messageText.color = Color.red;
            messageText.gameObject.SetActive(true);
            messageText.text = message;
        }

        // public void CardNumSeperator()
        // {
        //     InputField cardNumField = purchasePanel.GetComponentInChildren<InputField>();
        //     if (cardNumField != null)
        //     {
        //         string formatValue = cardNumField.text;
        //         for (int i = 0; i < cardNumField.text.Length; i++)
        //         {
        //             if (i % 4 == 3)
        //             {
        //                 formatValue = formatValue.Insert(i," ");
        //             }
        //         }
        //         cardNumField.text = formatValue;
        //     }
        //     else
        //     {
        //         Debug.Log("Cant find cardNumField");
        //     }
        // }

    }

}