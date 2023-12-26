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
    [SerializeField] private TMP_Text LobbyCodeShow;
    
    void Start()
    {
        LeaveLobby();
    }
    public void EnterLobbyNormal(){
        LobbyOrchestrator.LobbyNormal();

        if( UIsys != null ) UIsys.SetActive(false);
        if( Lobby != null ) Lobby.SetActive(true);
    }
    public void EnterLobbyRank(){
        LobbyOrchestrator.LobbyRank();

        if( UIsys != null ) UIsys.SetActive(false);
        if( Lobby != null ) Lobby.SetActive(true);
    }
    public void CreateLobbyFriend(){
        LobbyOrchestrator.FriendCreate();

        if( UIsys != null ) UIsys.SetActive(false);
        if( Lobby != null ) Lobby.SetActive(true);
    }
    public void JoinLobbyFriend(){
        LobbyOrchestrator.FriendJoin( _joinCodeText.text.Replace("\u200B", "") );
    }

    public async void LeaveLobby(){
        await MatchmakingService.LeaveLobby();
        UIsys.SetActive(true);
        Lobby.SetActive(false);
        LobbyCodeShow.text = $"Room Code: {Constants._LobbyCodeForOut}";
    }
}