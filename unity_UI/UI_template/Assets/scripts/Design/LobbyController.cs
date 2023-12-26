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

            yield return new WaitForSeconds(7f);

            if( type == 1 )
            {
                Debug.Log("PVP -> PVE");
                LeaveLobby();
                pve.PvEButton();
            }

            if( UIsys != null ) UIsys.SetActive(false);
            if( Lobby != null ) Lobby.SetActive(true);
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
        LobbyCodeShow.text = $"Room Code: ";
    }

    // static string StartGameURL = "http://140.122.185.169:5050/api/gameStart";
    // public static IEnumerator SendRequestStartGame( string P2ID )
    // {
    //     yield return new WaitForSeconds(5);
    //     Debug.Log("SendRequestStartGame");
    //     WWWForm form = new WWWForm();

    //     form.AddField( "gameType", MatchmakingService._currentLobby.Data["type"].ToString() );
    //     form.AddField( "roomId", MatchmakingService._currentLobby.Id.ToString() );
    //     form.AddField( "Player1Token", MatchmakingService._currentLobby.Data["p1ID"].ToString() );
    //     form.AddField( "Player2Token", P2ID );

    //     UnityWebRequest www = UnityWebRequest.Post( StartGameURL, form);
    //     yield return www.SendWebRequest();

    //     if (www.result == UnityWebRequest.Result.Success )
    //     {
    //         StoreData.store(0, MatchmakingService._currentLobby.Id);
    //         SceneManager.LoadScene(2);
    //     }
    // }
}