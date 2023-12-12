using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToLogin : MonoBehaviour
{
    public int check_id = 0;
    public void ReLogin()
    {
        Debug.Log(check_id);
        if (check_id == -3)
            SceneManager.LoadScene(0);
    }
}
