using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pg1PanelController : MonoBehaviour
{
    public GameObject SkinPanel;
    public GameObject MaskPanel; 
    // Start is called before the first frame update
    void Start()
    {
        BackToSkinMain(); 
    }

    public void BackToSkinMain(){
        SkinPanel.SetActive(false);
        MaskPanel.SetActive(false);
    }

    public void ShowKingPanel(){
        BackToSkinMain();
        MaskPanel.SetActive(true);
        SkinPanel.SetActive(true);
    }
    public void ShowQueenPanel(){
        BackToSkinMain();
        MaskPanel.SetActive(true);
        SkinPanel.SetActive(true);
    }
    public void ShowPrincePanel(){
        BackToSkinMain();
        MaskPanel.SetActive(true);
        SkinPanel.SetActive(true);
    }
    public void ShowKnightPanel(){
        BackToSkinMain();
        MaskPanel.SetActive(true);
        SkinPanel.SetActive(true);
    }
    public void ShowCivilianPanel(){
        BackToSkinMain();
        MaskPanel.SetActive(true);
        SkinPanel.SetActive(true);
    }
    public void ShowAssassinPanel(){
        BackToSkinMain();
        MaskPanel.SetActive(true);
        SkinPanel.SetActive(true);
    }
    //我寫在CardStyleScriptㄌ
}
