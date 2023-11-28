using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pg5PanelController : MonoBehaviour
{


    public GameObject FriendlyPanel;
    public GameObject FriendlyPanelCreate;
    public GameObject FriendlyPanelJoin;
    public GameObject MaskPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        BackToMain();
    }

    public void BackToMain(){
        FriendlyPanel.SetActive(false);
        FriendlyPanelCreate.SetActive(false);
        FriendlyPanelJoin.SetActive(false);
        MaskPanel.SetActive(false);
    }

    public void ShowFriendlyPanel(){
        BackToMain();
        MaskPanel.SetActive(true);
        FriendlyPanel.SetActive(true);
    }
    
    public void ShowFriendlyPanelCreate(){
        BackToMain();
        MaskPanel.SetActive(true);
        FriendlyPanelCreate.SetActive(true);
    }

    public void ShowFriendlyPanelJoin(){
        BackToMain();
        MaskPanel.SetActive(true);
        FriendlyPanelJoin.SetActive(true);
    }

}
