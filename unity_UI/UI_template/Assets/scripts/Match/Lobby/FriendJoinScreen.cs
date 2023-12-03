/* I'm not sure what I gonna use so simply put them all in this script... */
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine.SceneManagement;

public class FriendJoinScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _joinCodeText;
    public static event Action<string> JoinLobbySelected;

    public void JoinClicked()
    {
        JoinLobbySelected?.Invoke( _joinCodeText.text.Replace("\u200B", "") );
    }
}