using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkRunnerController : MonoBehaviour, INetworkRunnerCallbacks
{
    public event Action OnStartRunnerConnection;
    public event Action OnPlayerJoinedSuccessfully;
    public event Action OnStoppedRunnerConnection;
    
    [SerializeField] private NetworkRunner networkRunnerPrefab;
    [SerializeField] private NetworkRunner networkRunnerInstance;
    
    public async void StartGame(GameMode gameMode, string roomName)
    {
        try
        {
            OnStartRunnerConnection?.Invoke();

            if (networkRunnerInstance == null)
                networkRunnerInstance = Instantiate(networkRunnerPrefab);

            networkRunnerInstance.AddCallbacks(this);
            networkRunnerInstance.ProvideInput = true;

            var StartGameArgs = new StartGameArgs()
            {
                GameMode = gameMode,
                SessionName = roomName,
                PlayerCount = 4,
                SceneManager = networkRunnerInstance.GetComponent<INetworkSceneManager>()
            };

            var result = await networkRunnerInstance.StartGame(StartGameArgs);

            if (result.Ok)
            {
                const string sceneName = "MainGame";
                networkRunnerInstance.SetActiveScene(sceneName);
            }
            else
            {
                Debug.LogError($"Failed to start {result.ShutdownReason}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        finally
        {
            OnStoppedRunnerConnection?.Invoke();
        }
    }

    public void Shutdown()
    {
        networkRunnerInstance.Shutdown();
    }
    
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("Player joined");
        OnPlayerJoinedSuccessfully?.Invoke();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("Player Left");
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        Debug.Log("On Input");
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        Debug.Log("OnInputMissing");
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        Debug.Log("OnShutdown");
        SceneManager.LoadScene("Lobby");
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("On Connected To Server");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        Debug.Log("On Disconnected From Server");
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        Debug.Log("On Connect Request");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.Log("On Connect Failed");
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        Debug.Log("On User Simulation Message");
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        Debug.Log("On Session List Updated");
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        Debug.Log("On Custom Authentication Response");
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        Debug.Log("On Host Migration");
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        Debug.Log("On Reliable Data Received");
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        Debug.Log("On Scene Load Done");
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        Debug.Log("On Scene Load Start");
    }
}
