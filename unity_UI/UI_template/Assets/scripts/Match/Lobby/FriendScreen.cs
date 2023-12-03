/* I'm not sure what I gonna use so simply put them all in this script... */
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine.SceneManagement;

public class FriendScreen : MonoBehaviour
{
    private void Start()
    {
        /* It seems that nothing is going to be done... */
    }

    public static event Action CreateSelected;
    public static event Action JoinSelected;

    public void CreateClicked()
    {
        CreateSelected?.Invoke();
    }

    public void JoinClicked()
    {
        JoinSelected?.Invoke();
    }
}