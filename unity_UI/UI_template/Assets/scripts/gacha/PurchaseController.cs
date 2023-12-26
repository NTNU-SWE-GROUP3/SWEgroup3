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
        string[] allOptions = { "Afghanistan","Algeria","Argentina","Australia","Bangladesh","Brazil","Canada","China",
                                "Colombia","Democratic Republic of the Congo","Egypt","Ethiopia","France","Germany",
                                "India","Indonesia","Iran","Iraq","Italy","Japan","Kenya","Korea","Mexico","Morocco",
                                "Myanmar (Burma)","Nigeria","Pakistan","Peru","Philippines","Poland","Russia",
                                "Saudi Arabia","South Africa","South Korea","Spain","Sudan","Taiwan","Tanzania","Thailand",
                                "Turkey","Uganda","Ukraine","United Kingdom","United States","Uzbekistan","Venezuela",
                                "Vietnam","Yemen","Zambia","Zimbabwe"};
        // Text messageText;

        void Start()
        {
            DropdownInit();
            InputFeildsInit();
            MessageInit();
        }

        public void OpenPurchasePanel()
        {
            ClearInputFields();
            ClearMessages();
            HideMessage();
            purchasePanel.SetActive(true);
            actionReferences.buyClicked = false;
            actionReferences.cancelClicked = false;

        }

        public bool CCVCheck()
        {
            Debug.Log("Check CCV");
            if (purchasePanel != null)
            {
                InputField ccvField = GameObject.Find("VerifyCode")?.GetComponentInChildren<InputField>();
                if (ccvField != null && ccvField.text.Length == 3)
                {
                    return true;
                }
            }
            return false;
        }

        public bool DateCheck()
        {
            Debug.Log("Check Date");
            if (purchasePanel != null)
            {
                InputField dateField = GameObject.Find("Date")?.GetComponentInChildren<InputField>();
                if (dateField != null && dateField.text.Length == 5)
                {
                    char separator = dateField.text[2];
                    Debug.Log(separator);

                    if (separator == '/')
                    {
                        string[] dateParts = dateField.text.Split(separator);
                        if (dateParts.Length == 2)
                        {
                            if (int.TryParse(dateParts[0], out int month))
                            {
                                // Check if the month is within the valid range (1 to 12)
                                if (month >= 1 && month <= 12)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool PostCheck()
        {
            Debug.Log("Check Date");
            if (purchasePanel != null)
            {
                InputField postField = GameObject.Find("PostNumber")?.GetComponentInChildren<InputField>();
                if (postField != null && postField.text.Length == 5 || postField.text.Length == 3)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CardNumberCheck()
        {
            Debug.Log("Check Card Number");
            if (purchasePanel != null)
            {
                InputField cardNumField = purchasePanel.GetComponentInChildren<InputField>();
                if (cardNumField != null)
                {
                    if (cardNumField.text.Length < 12)
                    {
                        return false;
                    }
                    return true;
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
                    // Debug.Log("Option added: " + option);
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

                inputFields[1].characterLimit = 5;
                // inputFields[1].contentType = InputField.ContentType.IntegerNumber;

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



    }
}