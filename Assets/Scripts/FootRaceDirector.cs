using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FootRaceDirector : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject courseParentView = default;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;

        var position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        GameObject avatar = PhotonNetwork.Instantiate("Avatars/Avatar", position, Quaternion.identity);

        // Transform[] courses = CommonFuncs.GetChildren(this.courseParentView.transform);

        // ローカルプレイヤーオブジェクトを取得する
        // var player = PhotonNetwork.LocalPlayer;

        // ルームを作成したプレイヤーは、現在のサーバー時刻をゲームの開始時刻に設定する
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetStartTime(PhotonNetwork.ServerTimestamp);
        }
    }
}
