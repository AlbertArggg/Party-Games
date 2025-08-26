using Fusion;
using Other;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiddleSectionPanel : LobbyPanelBase
{
    [Header("Middle Section Panel")]
    [SerializeField] private Button joinRandomGameButton;
    [SerializeField] private Button joinRoomByArgButton;
    [SerializeField] private Button createRoomButton;
    
    [SerializeField] private TMP_InputField joinRoomByArgsInputField;
    [SerializeField] private TMP_InputField createRoomInputField;
    
    private NetworkRunnerController _networkRunnerController;

    public override void InitPanel(LobbyUIManager uiManager)
    {
        base.InitPanel(uiManager);
        _networkRunnerController = GlobalManagers.Instance.NetworkRunnerController;
        joinRandomGameButton.onClick.AddListener(() => JoinOrCreateRoom(GameMode.AutoHostOrClient, string.Empty));
        joinRoomByArgButton.onClick.AddListener(() => JoinOrCreateRoom(GameMode.Client, joinRoomByArgsInputField.text));
        createRoomButton.onClick.AddListener(() => JoinOrCreateRoom(GameMode.Host, createRoomInputField.text));
    }

    private void JoinOrCreateRoom(GameMode gameMode, string roomName)
    {
        Debug.Log($"Join or Create Room: {gameMode}, {roomName}");
        
        if(roomName.Length < 2 && gameMode != GameMode.AutoHostOrClient)
            return;
        
        _networkRunnerController.StartGame(gameMode, roomName);
    }
}
