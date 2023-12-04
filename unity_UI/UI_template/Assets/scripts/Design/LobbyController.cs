using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour
{

    public GameObject UIsys;
    public GameObject Lobby;
    void Start()
    {
        LeaveLobby();
    }
    public void EnterLobby(){
        Lobby.SetActive(true);
        UIsys.SetActive(false);
    }

    public void LeaveLobby(){
        UIsys.SetActive(true);
        Lobby.SetActive(false);
    }
}
