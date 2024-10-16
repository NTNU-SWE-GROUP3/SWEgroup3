﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine;
using Object = UnityEngine.Object;

public static class Authentication
{
    public static string PlayerId { get; private set; }

    public static async Task Login()
    {
        if (UnityServices.State == ServicesInitializationState.Uninitialized)
        {
            var options = new InitializationOptions();
            await UnityServices.InitializeAsync(options);
        }

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            PlayerId = AuthenticationService.Instance.PlayerId;
        }
    }
}

public static class MatchmakingService
{
    private const int HeartbeatInterval = 15;
    private const int LobbyRefreshRate = 2; // Rate limits at 2

    private static UnityTransport _transport;

    private static Lobby _currentLobby;
    private static CancellationTokenSource _heartbeatSource, _updateLobbySource;

    private static UnityTransport Transport
    {
        get => _transport != null ? _transport : _transport = Object.FindObjectOfType<UnityTransport>();
        set => _transport = value;
    }

    public static event Action<Lobby> CurrentLobbyRefreshed;

    public static void ResetStatics()
    {
        if (Transport != null)
        {
            Transport.Shutdown();
            Transport = null;
        }

        _currentLobby = null;
    }

    public static async Task<List<Lobby>> GatherLobbies(LobbyData data)
    {
        var options = new QueryLobbiesOptions
        {
            Count = 15,
            Filters = new List<QueryFilter> {
                new(QueryFilter.FieldOptions.AvailableSlots, "0", QueryFilter.OpOptions.GT),
                new(QueryFilter.FieldOptions.IsLocked, "0", QueryFilter.OpOptions.EQ),
                new(QueryFilter.FieldOptions.N1, data.Type.ToString(), QueryFilter.OpOptions.EQ)
            }
        };

        var allLobbies = await Lobbies.Instance.QueryLobbiesAsync(options);
        return allLobbies.Results;
    }

    public static async Task CreateLobbyWithAllocation(LobbyData data)
    {
        // Create a relay allocation and generate a join code to share with the lobby
        var a = await RelayService.Instance.CreateAllocationAsync(data.MaxPlayers);
        var joinCode = await RelayService.Instance.GetJoinCodeAsync(a.AllocationId);
        // Create a lobby, adding the relay join code to the lobby data
        var options = new CreateLobbyOptions
        {
            Data = new Dictionary<string, DataObject> {
                { Constants.JoinKey, new DataObject(DataObject.VisibilityOptions.Member, joinCode) },
                { Constants.GameTypeKey, new DataObject(DataObject.VisibilityOptions.Public, data.Type.ToString(), DataObject.IndexOptions.N1) }, {
                    Constants.DifficultyKey,
                    new DataObject(DataObject.VisibilityOptions.Public, data.Difficulty.ToString(), DataObject.IndexOptions.N2)
                }
            }
        };

        //The name of the lobby will be same as the host player.
        _currentLobby = await Lobbies.Instance.CreateLobbyAsync("New", data.MaxPlayers, options);
        Debug.Log($"Lobby created: {_currentLobby.Id}");

        //Transport.SetHostRelayData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData);

        Heartbeat();
        PeriodicallyRefreshLobby();
    }

    public static async Task LockLobby()
    {
        try
        {
            await Lobbies.Instance.UpdateLobbyAsync(_currentLobby.Id, new UpdateLobbyOptions { IsLocked = true });
        }
        catch (Exception e)
        {
            Debug.Log($"Failed closing lobby: {e}");
        }
    }

    private static async void Heartbeat()
    {
        _heartbeatSource = new CancellationTokenSource();
        while (!_heartbeatSource.IsCancellationRequested && _currentLobby != null)
        {
            await Lobbies.Instance.SendHeartbeatPingAsync(_currentLobby.Id);
            await Task.Delay(HeartbeatInterval * 1000);
        }
    }

    public static async Task JoinLobbyWithAllocation(string lobbyId)
    {
        _currentLobby = await Lobbies.Instance.JoinLobbyByIdAsync(lobbyId);
        var a = await RelayService.Instance.JoinAllocationAsync(_currentLobby.Data[Constants.JoinKey].Value);
        
        Transport.SetClientRelayData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData, a.HostConnectionData);
        PeriodicallyRefreshLobby();
    }

    // This function is used for Friend game X join, but with problem....
    public static async Task JoinLobbyWithAllocationCode(string lobbyCode)
    {
        _currentLobby = await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode);
        var a = await RelayService.Instance.JoinAllocationAsync(_currentLobby.Data[Constants.JoinKey].Value);

        Transport.SetClientRelayData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData, a.HostConnectionData);

        PeriodicallyRefreshLobby();
    }

    private static async void PeriodicallyRefreshLobby()
    {
        _updateLobbySource = new CancellationTokenSource();
        await Task.Delay(LobbyRefreshRate * 1000);
        while (!_updateLobbySource.IsCancellationRequested && _currentLobby != null)
        {
            _currentLobby = await Lobbies.Instance.GetLobbyAsync(_currentLobby.Id);
            CurrentLobbyRefreshed?.Invoke(_currentLobby);
            await Task.Delay(LobbyRefreshRate * 1000);
        }
    }

    public static async Task LeaveLobby()
    {
        _heartbeatSource?.Cancel();
        _updateLobbySource?.Cancel();

        if (_currentLobby != null)
            try
            {
                if (_currentLobby.HostId == Authentication.PlayerId) await Lobbies.Instance.DeleteLobbyAsync(_currentLobby.Id);
                else await Lobbies.Instance.RemovePlayerAsync(_currentLobby.Id, Authentication.PlayerId);
                _currentLobby = null;
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
    }
}

public class Constants
{
    public const string JoinKey = "j";
    public const string DifficultyKey = "d";
    public const string GameTypeKey = "t";

    public const int MAX_PLAYER = 2;

    public static readonly List<string> GameTypes = new() { "Normal", "Friends", "Rank" };

    public static readonly List<string> Difficulties = new() { "None", "Basic", "Medium", "Hard", "Extreme", "Nightmare" };
    // None: for Friends, Normal
    // Basic: for Rank 1 - 2
    // Medium: for Rank 3 - 4
    // Hard: for Rank 5
    // Extreme: for Rank 6
    // Nightmare: for Rank 7
}

public struct LobbyData
{
    public int MaxPlayers;
    public int Difficulty;
    public int Type;
}