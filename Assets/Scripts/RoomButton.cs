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
        // ルーム参加処理中は、全ての参加ボタンを押せないようにする
        this.matchmakingView.OnJoiningRoom();

        // ボタンに対応したルーム名のルームに参加する（ルームが存在しなければ作成してから参加する）
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = MAX_PLAYERS;
        PhotonNetwork.JoinOrCreateRoom(this.RoomName, roomOptions, TypedLobby.Default);
    }

    public void SetPlayerCount(int playerCount)
    {
        this.label.text = $"{this.RoomName}\n{playerCount} / {MAX_PLAYERS}";

        // ルームが満員でない時のみ、ルーム参加ボタンを押せるようにする
        this.button.interactable = (playerCount < MAX_PLAYERS);
    }
}
