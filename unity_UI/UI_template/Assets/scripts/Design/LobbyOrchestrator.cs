using System;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

#pragma warning disable CS4014

/// <summary>
///     Lobby orchestrator. I put as much UI logic within the three sub screens,
///     but the transport and RPC logic remains here. It's possible we could pull
/// </summary>
public class LobbyOrchestrator : NetworkBehaviour {

    private DontDestroy userdata;
    static int userRank;

    void start()
    {
        userdata = FindObjectOfType<DontDestroy>();
        userRank = int.Parse(userdata.rank);
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
                    Type = 1
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

    public static async void FriendJoin( string code )
    {
        await Authentication.Login();
        //using (new Load("Joining Lobby...")) {
            try {
                await MatchmakingService.JoinLobbyWithAllocationCode( code );
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
            if( userRank.Equals("1") || userRank.Equals("2") )         await MatchmakingService.CreateOrJoinLobby( 2, 1 );
            else if( userRank.Equals("3") || userRank.Equals("4") )    await MatchmakingService.CreateOrJoinLobby( 2, 2 );
            else if( userRank.Equals("5") )                            await MatchmakingService.CreateOrJoinLobby( 2, 3 );
            else if( userRank.Equals("6") )                            await MatchmakingService.CreateOrJoinLobby( 2, 4 );
            else if( userRank.Equals("7") )                            await MatchmakingService.CreateOrJoinLobby( 2, 5 );
        }
        catch ( Exception e ){
            Debug.LogError(e);
        }
    }

    public static async void LobbyNormal()
    {
        await Authentication.Login();
        try{
            await MatchmakingService.CreateOrJoinLobby( 1, 0 ); // Level is editing...
        }
        catch ( Exception e ){
            Debug.LogError(e);
        }
    }
}