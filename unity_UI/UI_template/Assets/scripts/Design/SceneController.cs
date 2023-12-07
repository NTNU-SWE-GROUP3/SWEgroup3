using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Animator Transition; 
    public float transitionTime = 1f;

    void Start(){
        // StartCoroutine(LoadSceneZero());
    }
    
    void Update()
    {
        if (Input.GetKey("[0]"))
        {
            StartCoroutine(LoadSceneZero());
        }
        else if(Input.GetKey("[1]"))
        {
            StartCoroutine(LoadSceneOne());
        }
        else if(Input.GetKey("[2]"))
        {
            StartCoroutine(LoadSceneTwo());
        }

        
    }


    IEnumerator LoadSceneZero()
    {

        Transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(0);

    }

        IEnumerator LoadSceneOne()
    {

        Transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(1);

    }

        IEnumerator LoadSceneTwo()
    {

        Transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(2);
    }

    public void SceneZero(){
        StartCoroutine(LoadSceneZero());
    }

    public void SceneOne(){
        StartCoroutine(LoadSceneOne());
    }

    public void SceneTwo(){
        StartCoroutine(LoadSceneTwo());
    }

}
