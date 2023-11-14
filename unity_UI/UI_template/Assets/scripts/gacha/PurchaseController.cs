using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseController : MonoBehaviour
{
    public InputField cardNumberInput;
    public Dropdown dropdownRegion;
    string[] allOptions = { "Japan", "Taiwan", "Korea", "China", "Germany", "Italy", "France", "Spain", "Canada" };



    void Start()
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

    void InputChecker()
    {

    }
}
