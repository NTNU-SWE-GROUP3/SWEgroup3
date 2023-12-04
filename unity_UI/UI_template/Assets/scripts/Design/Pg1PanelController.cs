using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pg1PanelController : MonoBehaviour
{
    public GameObject KingPanel;
    public GameObject QueenPanel;
    public GameObject PrincePanel;
    public GameObject KnightPanel;
    public GameObject CivilianPanel;
    public GameObject AssassinPanel;
    public GameObject MaskPanel;
    // Start is called before the first frame update
    void Start()
    {
        BackToSkinMain(); 
    }

    public void BackToSkinMain(){
        KingPanel.SetActive(false);
        QueenPanel.SetActive(false);
        PrincePanel.SetActive(false);
        KnightPanel.SetActive(false);
        CivilianPanel.SetActive(false);
        AssassinPanel.SetActive(false);
        MaskPanel.SetActive(false);
    }

    public void ShowKingPanel(){
        BackToSkinMain();
        MaskPanel.SetActive(true);
        KingPanel.SetActive(true);
    }
    public void ShowQueenPanel(){
        BackToSkinMain();
        MaskPanel.SetActive(true);
        QueenPanel.SetActive(true);
    }
    public void ShowPrincePanel(){
        BackToSkinMain();
        MaskPanel.SetActive(true);
        PrincePanel.SetActive(true);
    }
    public void ShowKnightPanel(){
        BackToSkinMain();
        MaskPanel.SetActive(true);
        KnightPanel.SetActive(true);
    }
    public void ShowCivilianPanel(){
        BackToSkinMain();
        MaskPanel.SetActive(true);
        CivilianPanel.SetActive(true);
    }
    public void ShowAssassinPanel(){
        BackToSkinMain();
        MaskPanel.SetActive(true);
        AssassinPanel.SetActive(true);
    }
}
