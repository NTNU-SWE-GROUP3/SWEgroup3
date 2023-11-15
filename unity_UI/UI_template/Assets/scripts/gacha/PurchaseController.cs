using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PurchaseControl
{
    public class PurchaseController : MonoBehaviour
    {
        [SerializeField] GameObject purchasePanel;
        [SerializeField] InputField[] inputFields;
        public Dropdown dropdownRegion;
        string[] allOptions = { "Japan", "Taiwan", "Korea", "China", "Germany", "Italy", "France", "Spain", "Canada" };
        public Action actionReferences;

        void Start()
        {
            DropdownInit();
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

        public void OpenPurchasePanel()
        {
            purchasePanel.SetActive(true);
            actionReferences.buyClicked = false;
            actionReferences.cancelClicked = false;
            ClearInputFields();
        }

    }

}