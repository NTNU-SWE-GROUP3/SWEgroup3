using System;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.Serialization;
using UnityEngine.Networking;
using MiniJSON;

#pragma warning disable CS4014

/// <summary>
///     Lobby orchestrator. I put as much UI logic within the three sub screens,
///     but the transport and RPC logic remains here. It's possible we could pull
/// </summary>
public class LobbyOrchestrator : NetworkBehaviour {

    private DontDestroy userdata;
    static int userRank;
    static string userID;

    void start()
    {
        userdata = FindObjectOfType<DontDestroy>();
        userRank = int.Parse(userdata.rank);
        userID = userdata.token;
    }
    public static async void FriendCreate()
    {
        await Authentication.Login();
        //using (new Load("creating room...")) {
            try {
                
                // Create a lobby
                var data = new LobbyData
                {
                    MaxPlayers = 2,
                    Difficulty = 0,
                    Type = 1,
                    P1ID = userID,
                };

                await MatchmakingService.CreateLobbyWithAllocation(data);

                // Starting the host immediately will keep the relay server alive
                // NetworkManager.Singleton.StartHost();
            }
            catch (Exception e) {
                Debug.LogError(e);
                //CanvasUtilities.Instance.ShowError("Failed create room");
            }
        //}
    }

    //public static async void FriendJoin( string code )
    public async void FriendJoin( string code )
    {
        await Authentication.Login();
        //using (new Load("Joining Lobby...")) {
            try {
                await MatchmakingService.JoinLobbyWithAllocationCode( code, userID );
                
                StartCoroutine( SendRequestStartGame( userID ) );
                //NetworkManager.Singleton.StartClient();
            }
            catch (Exception e) {
                Debug.LogError(e);
                //CanvasUtilities.Instance.ShowError("Failed joining lobby");
            }
        //}
    }

    public static async void LobbyRank()
    {
        await Authentication.Login();
        try{
            if( userRank.Equals("1") || userRank.Equals("2") )         await MatchmakingService.CreateOrJoinLobby( 2, 1, userID );
            else if( userRank.Equals("3") || userRank.Equals("4") )    await MatchmakingService.CreateOrJoinLobby( 2, 2, userID );
            else if( userRank.Equals("5") )                            await MatchmakingService.CreateOrJoinLobby( 2, 3, userID );
            else if( userRank.Equals("6") )                            await MatchmakingService.CreateOrJoinLobby( 2, 4, userID );
            else if( userRank.Equals("7") )                            await MatchmakingService.CreateOrJoinLobby( 2, 5, userID );
        }
        catch ( Exception e ){
            Debug.LogError(e);
        }
    }

    public static async void LobbyNormal()
    {
        await Authentication.Login();
        try{
            await MatchmakingService.CreateOrJoinLobby( 1, 0, userID );
        }
        catch ( Exception e ){
            Debug.LogError(e);
        }
    }

    static string StartGameURL = "http://140.122.185.169:5050/api/gameStart";
    IEnumerator SendRequestStartGame( string P2ID )
    {
        yield return new WaitForSeconds(5);
        Debug.Log("SendRequestStartGame");
        WWWForm form = new WWWForm();

        form.AddField( "gameType", MatchmakingService._currentLobby.Data["type"].ToString() );
        form.AddField( "roomId", MatchmakingService._currentLobby.Id.ToString() );
        form.AddField( "Player1Token", MatchmakingService._currentLobby.Data["p1ID"].ToString() );
        form.AddField( "Player2Token", P2ID );

        UnityWebRequest www = UnityWebRequest.Post( StartGameURL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success )
        {
            StoreData.store(0, MatchmakingService._currentLobby.Id);
            SceneManager.LoadScene(2);
        }
    }
}