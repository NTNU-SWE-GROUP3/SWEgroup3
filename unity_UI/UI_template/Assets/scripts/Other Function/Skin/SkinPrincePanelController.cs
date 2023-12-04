using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinPrincePanelController : MonoBehaviour
{
    public GameObject SkinPrinceTitle;
    public GameObject SkinPrinceImage;
    public GameObject SkinPrinceDescription;
    public GameObject SkinPrinceSellSkinButton;
    public GameObject SkinPrinceEquipSkinButton;

    // Start is called before the first frame update
    void Start()
    {
        BackToSkinPrincePanel(); 
        ShowPrinceSkinPanel();
    }

    public void BackToSkinPrincePanel(){
        SkinPrinceTitle.SetActive(false);
        SkinPrinceImage.SetActive(false);
        SkinPrinceDescription.SetActive(false);
        SkinPrinceSellSkinButton.SetActive(false);
        SkinPrinceEquipSkinButton.SetActive(false);
    }

    public void ShowPrinceSkinPanel()
    {
        SkinPrinceTitle.SetActive(true);
        SkinPrinceImage.SetActive(true);
        SkinPrinceDescription.SetActive(true);
        SkinPrinceSellSkinButton.SetActive(true);
        SkinPrinceEquipSkinButton.SetActive(true);
    }
}
