using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Animator Transition; 
    public float transitionTime = 1f;
    
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
        else if(Input.GetKey("[3]"))
        {
            StartCoroutine(LoadSceneThree());
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

    IEnumerator LoadSceneThree()
    {

        Transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(3);

    }

    public void ToScene0(){
        StartCoroutine(LoadSceneZero());
    }

    public void ToScene1(){
        StartCoroutine(LoadSceneOne());
    }

    public void ToScene2(){
        StartCoroutine(LoadSceneTwo());
    }

    public void ToScene3(){
        StartCoroutine(LoadSceneThree());
    }
}
