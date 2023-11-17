using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinAssassinPanelController : MonoBehaviour
{
    public GameObject SkinAssassinTitle;
    public GameObject SkinAssassinImage;
    public GameObject SkinAssassinDescription;
    public GameObject SkinAssassinSellSkinButton;
    public GameObject SkinAssassinEquipSkinButton;

    // Start is called before the first frame update
    void Start()
    {
        BackToSkinAssassinPanel(); 
        ShowAssassinSkinPanel();
    }

    public void BackToSkinAssassinPanel(){
        SkinAssassinTitle.SetActive(false);
        SkinAssassinImage.SetActive(false);
        SkinAssassinDescription.SetActive(false);
        SkinAssassinSellSkinButton.SetActive(false);
        SkinAssassinEquipSkinButton.SetActive(false);
    }

    public void ShowAssassinSkinPanel()
    {
        SkinAssassinTitle.SetActive(true);
        SkinAssassinImage.SetActive(true);
        SkinAssassinDescription.SetActive(true);
        SkinAssassinSellSkinButton.SetActive(true);
        SkinAssassinEquipSkinButton.SetActive(true);
    }
}
