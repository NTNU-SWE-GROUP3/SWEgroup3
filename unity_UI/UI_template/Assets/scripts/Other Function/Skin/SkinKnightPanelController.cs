using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinKnightPanelController : MonoBehaviour
{
    public GameObject SkinKnightTitle;
    public GameObject SkinKnightImage;
    public GameObject SkinKnightDescription;
    public GameObject SkinKnightSellSkinButton;
    public GameObject SkinKnightEquipSkinButton;

    // Start is called before the first frame update
    void Start()
    {
        BackToSkinKnightPanel(); 
        ShowKnightSkinPanel();
    }

    public void BackToSkinKnightPanel(){
        SkinKnightTitle.SetActive(false);
        SkinKnightImage.SetActive(false);
        SkinKnightDescription.SetActive(false);
        SkinKnightSellSkinButton.SetActive(false);
        SkinKnightEquipSkinButton.SetActive(false);
    }

    public void ShowKnightSkinPanel()
    {
        SkinKnightTitle.SetActive(true);
        SkinKnightImage.SetActive(true);
        SkinKnightDescription.SetActive(true);
        SkinKnightSellSkinButton.SetActive(true);
        SkinKnightEquipSkinButton.SetActive(true);
    }
}
