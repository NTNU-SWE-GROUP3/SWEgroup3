using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

using TMPro;

public class LobbyController : MonoBehaviour
{

    public GameObject UIsys;
    public GameObject Lobby;
    [SerializeField] private TMP_Text _joinCodeText;
    
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
        Debug.Log( $"Lobby Code input: {_joinCodeText.text.Replace("\u200B", "")}" );
        LobbyOrchestrator.FriendJoin( _joinCodeText.text.Replace("\u200B", "") );
    }

    public void LeaveLobby(){
        UIsys.SetActive(true);
        Lobby.SetActive(false);
    }
}