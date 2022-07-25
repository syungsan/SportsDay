using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour
{
    private const int MAX_PLAYERS = 20;

    [SerializeField] private TextMeshProUGUI label = default;

    private MatchmakingView matchmakingView;
    private Button button;

    public string RoomName { get; private set; }

    public void Init(MatchmakingView parentView, int roomId)
    {
        this.matchmakingView = parentView;
        this.RoomName = $"Room{roomId}";

        this.button = GetComponent<Button>();
        this.button.interactable = false;
        this.button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // ���[���Q���������́A�S�Ă̎Q���{�^���������Ȃ��悤�ɂ���
        this.matchmakingView.OnJoiningRoom();

        // �{�^���ɑΉ��������[�����̃��[���ɎQ������i���[�������݂��Ȃ���΍쐬���Ă���Q������j
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = MAX_PLAYERS;
        PhotonNetwork.JoinOrCreateRoom(this.RoomName, roomOptions, TypedLobby.Default);
    }

    public void SetPlayerCount(int playerCount)
    {
        this.label.text = $"{this.RoomName}\n{playerCount} / {MAX_PLAYERS}";

        // ���[���������łȂ����̂݁A���[���Q���{�^����������悤�ɂ���
        this.button.interactable = (playerCount < MAX_PLAYERS);
    }
}
