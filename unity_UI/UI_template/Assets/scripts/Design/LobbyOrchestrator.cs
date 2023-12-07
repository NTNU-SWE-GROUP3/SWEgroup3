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
}