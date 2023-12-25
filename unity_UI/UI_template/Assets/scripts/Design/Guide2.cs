using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide2 : MonoBehaviour
{
    [SerializeField] GameObject Guidepanel0;
    [SerializeField] GameObject Guidepanel1;
    [SerializeField] GameObject Guidepanel2;
    [SerializeField] GameObject Guidepanel3;
    [SerializeField] GameObject Guidepanel4;
    [SerializeField] GameObject Guidepanel5;
    void Start()
    {
        ClearGuide();
    }

    public void ClearGuide(){
        Guidepanel0.SetActive(false);
        Guidepanel1.SetActive(false);
        Guidepanel2.SetActive(false);
        Guidepanel3.SetActive(false);
        Guidepanel4.SetActive(false);
        Guidepanel5.SetActive(false);
    }
}
