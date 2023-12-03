/* I'm not sure what I gonna use so simply put them all in this script... */
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine.SceneManagement;

public class MainMatchScreen : MonoBehaviour
{
    private void Start()
    {
        /* It seems that nothing is going to be done... */
    }

    public static event Action NormalSelected;
    public static event Action RankSelected;
    public static event Action FriendSelected;

    public void NormalButtonClicked()
    {
        NormalSelected?.Invoke();
    }

    public void RankButtonClicked()
    {
        RankSelected?.Invoke();
    }

    public void FriendButtonClicked()
    {
        FriendSelected?.Invoke();
    }
}