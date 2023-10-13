using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    // Update is called once per frame
    
    void Update()
    {
        if (Input.GetKey("[0]"))
        {
            LoadSceneZero();
        }
        else if(Input.GetKey("[1]"))
        {
            LoadSceneOne();
        }
        else if(Input.GetKey("[2]"))
        {
            LoadSceneTwo();
        }

        
    }

    public void LoadSceneZero()
    {
        SceneManager.LoadScene(0);
        
    }
    public void LoadSceneOne()
    {
        SceneManager.LoadScene(1);
        
    }
    public void LoadSceneTwo()
    {
        SceneManager.LoadScene(2);
        
    }
}
