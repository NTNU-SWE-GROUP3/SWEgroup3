using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinQueenPanelController : MonoBehaviour
{
    public GameObject SkinQueenTitle;
    public GameObject SkinQueenImage;
    public GameObject SkinQueenDescription;
    public GameObject SkinQueenSellSkinButton;
    public GameObject SkinQueenEquipSkinButton;

    // Start is called before the first frame update
    void Start()
    {
        BackToSkinQueenPanel(); 
        ShowQueenSkinPanel();
    }

    public void BackToSkinQueenPanel(){
        SkinQueenTitle.SetActive(false);
        SkinQueenImage.SetActive(false);
        SkinQueenDescription.SetActive(false);
        SkinQueenSellSkinButton.SetActive(false);
        SkinQueenEquipSkinButton.SetActive(false);
    }

    public void ShowQueenSkinPanel()
    {
        SkinQueenTitle.SetActive(true);
        SkinQueenImage.SetActive(true);
        SkinQueenDescription.SetActive(true);
        SkinQueenSellSkinButton.SetActive(true);
        SkinQueenEquipSkinButton.SetActive(true);
    }
}
