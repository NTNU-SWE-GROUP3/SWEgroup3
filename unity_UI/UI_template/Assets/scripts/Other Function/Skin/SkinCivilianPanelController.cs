using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinCivilianPanelController : MonoBehaviour
{
    public GameObject SkinCivilianTitle;
    public GameObject SkinCivilianImage;
    public GameObject SkinCivilianDescription;
    public GameObject SkinCivilianSellSkinButton;
    public GameObject SkinCivilianEquipSkinButton;

    // Start is called before the first frame update
    void Start()
    {
        BackToSkinCivilianPanel(); 
        ShowCivilianSkinPanel();
    }

    public void BackToSkinCivilianPanel(){
        SkinCivilianTitle.SetActive(false);
        SkinCivilianImage.SetActive(false);
        SkinCivilianDescription.SetActive(false);
        SkinCivilianSellSkinButton.SetActive(false);
        SkinCivilianEquipSkinButton.SetActive(false);
    }

    public void ShowCivilianSkinPanel()
    {
        SkinCivilianTitle.SetActive(true);
        SkinCivilianImage.SetActive(true);
        SkinCivilianDescription.SetActive(true);
        SkinCivilianSellSkinButton.SetActive(true);
        SkinCivilianEquipSkinButton.SetActive(true);
    }
}
