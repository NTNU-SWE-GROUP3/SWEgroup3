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

using System.Threading;

#pragma warning disable CS4014

/// <summary>
///     Lobby orchestrator. I put as much UI logic within the three sub screens,
///     but the transport and RPC logic remains here. It's possible we could pull
/// </summary>
public class LobbyOrchestrator : NetworkBehaviour {

    private DontDestroy userdata;
    static int userRank;
    static string userID;

    public static bool isJoin;

    public async void FriendCreate()
    {
        userdata = FindObjectOfType<DontDestroy>();
        if( userdata != null )
        {
            userID = userdata.token;
            Debug.Log($"LobbyOrchestrator: userID: {userID}");
            //Debug.Log($"userRank: {userdata.rank}");
            //userRank = int.Parse(userdata.rank);
            isJoin = false;
        }
        else
        {
            Debug.Log("Lobby Orchestrator: Cannot found userdata");
        }

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
        userdata = FindObjectOfType<DontDestroy>();
        if( userdata != null )
        {
            userID = userdata.token;
            Debug.Log($"LobbyOrchestrator: userID: {userID}");
            //Debug.Log($"userRank: {userdata.rank}");
            //userRank = int.Parse(userdata.rank);
            isJoin = true;
        }
        else
        {
            Debug.Log("Lobby Orchestrator: Cannot found userdata");
        }

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

    public async void LobbyRank()
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

        Thread.Sleep(5000);
        if( isJoin )    StartCoroutine( SendRequestStartGame( userID ) );
    }

    public async void LobbyNormal()
    {
        userdata = FindObjectOfType<DontDestroy>();
        if( userdata != null )
        {
            userID = userdata.token;
            Debug.Log($"LobbyOrchestrator: userID: {userID}");
            //Debug.Log($"userRank: {userdata.rank}");
            //userRank = int.Parse(userdata.rank);
            isJoin = false;
        }
        else
        {
            Debug.Log("Lobby Orchestrator: Cannot found userdata");
        }

        await Authentication.Login();
        try{
            await MatchmakingService.CreateOrJoinLobby( 1, 0, userID );
        }
        catch ( Exception e ){
            Debug.LogError(e);
        }

        Thread.Sleep(5000);
        if( isJoin )    StartCoroutine( SendRequestStartGame( userID ) );
    }

    static string StartGameURL = "http://140.122.185.169:5050/api/gameStart";
    IEnumerator SendRequestStartGame( string P2ID )
    {
        yield return new WaitForSeconds(5);

        if( MatchmakingService._currentLobby == null )
        {
            Debug.Log("_currentLobby is null");
        }
        else
        {
            Debug.Log("SendRequestStartGame");

            // Construct the JSON payload
            string jsonData = $"{{\"gameType\":\"{MatchmakingService._currentLobby.Data["t"].Value}\"," +
                              $"\"roomId\":\"{MatchmakingService._currentLobby.Id}\"," +
                              $"\"player1Token\":\"{MatchmakingService._currentLobby.Data["p"].Value}\"," +
                              $"\"player2Token\":\"{P2ID}\"}}";

            // Convert JSON string to bytes
            byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

            // Create a UnityWebRequest with raw JSON data
            UnityWebRequest www = UnityWebRequest.PostWwwForm(StartGameURL, "POST");
            www.uploadHandler = new UploadHandlerRaw(jsonBytes);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success )
            {
                StoreData.store(0, MatchmakingService._currentLobby.Id);
                Debug.Log("Load Scene: SendRequestStartGame()");
                SceneManager.LoadScene(2);
            }
        }
    }
}