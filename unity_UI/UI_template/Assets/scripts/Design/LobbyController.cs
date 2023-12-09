using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class LobbyController : MonoBehaviour
{

    public GameObject UIsys;
    public GameObject Lobby;
    void Start()
    {
        LeaveLobby();
    }
    public void EnterLobbyNormal(){
        Lobby.SetActive(true);
        UIsys.SetActive(false);
    }
    public void EnterLobbyRank(){
        
    }
    public void CreateLobbyFriend(){

        LobbyOrchestrator.FriendCreate();
    }
    public void JoinLobbyFriend(){
        Lobby.SetActive(true);
        UIsys.SetActive(false);
    }

    public void LeaveLobby(){
        UIsys.SetActive(true);
        Lobby.SetActive(false);
    }
}