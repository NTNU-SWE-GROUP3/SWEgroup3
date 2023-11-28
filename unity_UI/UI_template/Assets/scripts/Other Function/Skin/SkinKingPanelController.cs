using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinKingPanelController : MonoBehaviour
{
    public GameObject SkinKingTitle;
    public GameObject SkinKingImage;
    public GameObject SkinKingDescription;
    public GameObject SkinKingSellSkinButton;
    public GameObject SkinKingEquipSkinButton;

    // Start is called before the first frame update
    void Start()
    {
        BackToSkinKingPanel(); 
        ShowKingSkinPanel();
    }

    public void BackToSkinKingPanel(){
        SkinKingTitle.SetActive(false);
        SkinKingImage.SetActive(false);
        SkinKingDescription.SetActive(false);
        SkinKingSellSkinButton.SetActive(false);
        SkinKingEquipSkinButton.SetActive(false);
    }

    public void ShowKingSkinPanel()
    {
        SkinKingTitle.SetActive(true);
        SkinKingImage.SetActive(true);
        SkinKingDescription.SetActive(true);
        SkinKingSellSkinButton.SetActive(true);
        SkinKingEquipSkinButton.SetActive(true);
    }
}
