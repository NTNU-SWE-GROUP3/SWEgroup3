using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUIController : MonoBehaviour
{
    public GameObject LoginPanel;
    public GameObject SignUpPanel;
    public GameObject PasswordPanel1;
    public GameObject PasswordPanel2;
    void Awake()
    {
        LoginPanel.SetActive(true);
        SignUpPanel.SetActive(false);
        PasswordPanel1.SetActive(false);
        PasswordPanel2.SetActive(false);
        
    }
    
    public void ChangetoPanel(){
        

    }
}
