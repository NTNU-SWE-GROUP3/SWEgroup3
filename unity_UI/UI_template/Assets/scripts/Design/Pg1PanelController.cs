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
    }

    public void ShowKingPanel(){
        BackToSkinMain();
        KingPanel.SetActive(true);
    }
    public void ShowQueenPanel(){
        BackToSkinMain();
        QueenPanel.SetActive(true);
    }
    public void ShowPrincePanel(){
        BackToSkinMain();
        KnightPanel.SetActive(true);
    }
    public void ShowKnightPanel(){
        BackToSkinMain();
        KnightPanel.SetActive(true);
    }
    public void ShowCivilianPanel(){
        BackToSkinMain();
        CivilianPanel.SetActive(true);
    }
    public void ShowAssassinPanel(){
        BackToSkinMain();
        AssassinPanel.SetActive(true);
    }
}
