using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MatchmakingView : MonoBehaviourPunCallbacks
{
    private RoomList roomList = new RoomList();
    private List<RoomButton> roomButtonList = new List<RoomButton>();
    private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        this.canvasGroup = GetComponent<CanvasGroup>();

        // ���r�[�ɎQ������܂ł́A�S�Ẵ��[���Q���{�^���������Ȃ��悤�ɂ���
        this.canvasGroup.interactable = false;

        // �S�Ẵ��[���Q���{�^��������������
        int roomId = 1;
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<RoomButton>(out var roomButton))
            {
                roomButton.Init(this, roomId++);
                this.roomButtonList.Add(roomButton);
            }
        }
    }

    public override void OnJoinedLobby()
    {
        // ���r�[�ɎQ��������A���[���Q���{�^����������悤�ɂ���
        this.canvasGroup.interactable = true;
    }

    public override void OnRoomListUpdate(List<RoomInfo> changedRoomList)
    {
        this.roomList.Update(changedRoomList);

        // �S�Ẵ��[���Q���{�^���̕\�����X�V����
        foreach (var roomButton in this.roomButtonList)
        {
            if (this.roomList.TryGetRoomInfo(roomButton.RoomName, out var roomInfo))
            {
                roomButton.SetPlayerCount(roomInfo.PlayerCount);
            }
            else
            {
                roomButton.SetPlayerCount(0);
            }
        }
    }

    public void OnJoiningRoom()
    {
        // ���[���Q���������́A�S�Ẵ��[���Q���{�^���������Ȃ��悤�ɂ���
        this.canvasGroup.interactable = false;
    }

    public override void OnJoinedRoom()
    {
        // ���[���ւ̎Q��������������AUI���\���ɂ���
        this.transform.parent.gameObject.SetActive(false);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        // ���[���ւ̎Q�������s������A�Ăу��[���Q���{�^����������悤�ɂ���
        this.canvasGroup.interactable = true;
    }
}
