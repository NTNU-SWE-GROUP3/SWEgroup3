using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

using TMPro;
using UnityEngine.UI;
using UnityEngine.Serialization;
using UnityEngine.Networking;
using MiniJSON;

public class LobbyController : MonoBehaviour
{
    LobbyOrchestrator lobbyOrchestrator;

    PvE pve;
    public GameObject UIsys;
    public GameObject Lobby;
    [SerializeField] private TMP_Text _joinCodeText;
    [SerializeField] private Text LobbyCodeShow;
    
    void Start()
    {
        lobbyOrchestrator = GameObject.Find("LobbyController").GetComponent<LobbyOrchestrator>();
        pve = GameObject.Find("LobbyController").GetComponent<PvE>();
        
        LeaveLobby();
    }
    IEnumerator Enter( int type )
    {
        yield return new WaitForSeconds(3f);

        if( MatchmakingService._currentLobby.LobbyCode != null )
        {
            Debug.Log($"Room Code: {MatchmakingService._currentLobby.LobbyCode}");
            LobbyCodeShow.text = $"Room Code: {MatchmakingService._currentLobby.LobbyCode}";

            if( UIsys != null ) UIsys.SetActive(false);
            if( Lobby != null ) Lobby.SetActive(true);

            yield return new WaitForSeconds(7f);

            if( type == 1 && MatchmakingService.IamJoin == false && MatchmakingService.SecondPlayerJoinflag == 0 )
            {
                Debug.Log("PVP -> PVE");
                LeaveLobby();
                pve.PvEButton();
            }

            //if( UIsys != null ) UIsys.SetActive(false);
            //if( Lobby != null ) Lobby.SetActive(true);
        }
    }
    public void EnterLobbyNormal(){
        lobbyOrchestrator.LobbyNormal();

        StartCoroutine( Enter(1) );
    }
    public void EnterLobbyRank(){
        lobbyOrchestrator.LobbyRank();

        StartCoroutine( Enter(0) );
    }
    public void CreateLobbyFriend(){
        lobbyOrchestrator.FriendCreate();

        StartCoroutine( Enter(0) );
    }
    public void JoinLobbyFriend(){
       lobbyOrchestrator.FriendJoin( _joinCodeText.text.Replace("\u200B", "") );
    }

    public async void LeaveLobby(){
        await MatchmakingService.LeaveLobby();
        if( UIsys != null ) UIsys.SetActive(true);
        if( Lobby != null ) Lobby.SetActive(false);
        if( LobbyCodeShow != null ) LobbyCodeShow.text = $"Room Code: ";
    }
}