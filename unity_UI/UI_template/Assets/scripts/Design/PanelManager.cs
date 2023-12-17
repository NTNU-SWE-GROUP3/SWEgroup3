using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] GameObject userinfo;
    // Start is called before the first frame update
    void Start()
    {
       closeAll();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void closeAll(){
        userinfo.SetActive(false);
    }

    public void O_userinfo(){
        userinfo.SetActive(true);
    }
}
