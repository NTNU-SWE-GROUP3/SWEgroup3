using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResultWindowController : MonoBehaviour
{
    [SerializeField] Button okButton;
    [SerializeField] GameObject resultPanel;
    
    void Start()
    {
        Init();
    }   

    void Init()
    {
        resultPanel.SetActive(false);
    }

    void ButtonClick()
    {
        resultPanel.SetActive(true);
    }
    
}
