using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class FootRaceDirector : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject courseParentView = default;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;

        GameObject avatar = PhotonNetwork.Instantiate("Avatars/Punk", new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

        Transform[] courses = CommonFuncs.GetChildren(this.courseParentView.transform);

        // ローカルプレイヤーオブジェクトを取得する
        Player player = PhotonNetwork.LocalPlayer;

        Transform course = courses[player.ActorNumber - 1];

        float avatarRealSizeX = avatar.GetComponent<Renderer>().bounds.size.x * avatar.transform.localScale.x;
        float positionX = course.transform.position.x - (Camera.main.ScreenToWorldPoint(course.GetComponent<RectTransform>().sizeDelta).x + avatarRealSizeX) * 0.85f;
        
        avatar.transform.position = new Vector3(positionX, course.transform.position.y - 0.7f, avatar.transform.position.z);

        // ルームを作成したプレイヤーは、現在のサーバー時刻をゲームの開始時刻に設定する
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetStartTime(PhotonNetwork.ServerTimestamp);
        }
    }
}
