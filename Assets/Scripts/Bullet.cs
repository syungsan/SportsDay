using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    private Vector3 origin; // 弾を発射した時刻の座標

    private Vector3 velocity;

    private int timestamp; // 弾を発射した時刻

    // 弾のIDを返すプロパティ
    public int Id { get; private set; }

    // 弾を発射したプレイヤーのIDを返すプロパティ
    public int OwnerId { get; private set; }

    // 同じ弾かどうかをIDで判定するメソッド
    public bool Equals(int id, int ownerId) => id == Id && ownerId == OwnerId;

    public void Init(int id, int ownerId, Vector3 origin, float angle, int timestamp)
    {
        Id = id;
        OwnerId = ownerId;

        this.origin = origin;

        velocity = 9f * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));

        this.timestamp = timestamp;

        // 一度だけ直接Update()を呼んで、transform.positionの初期値を決める
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        // 弾を発射した時刻から現在時刻までの経過時間を求める
        float elapsedTime = Mathf.Max(0f, unchecked(PhotonNetwork.ServerTimestamp - timestamp) / 1000f);

        // 弾を発射した時刻での座標・速度・経過時間から現在の座標を求める
        transform.position = origin + velocity * elapsedTime;
    }

    // 画面外に移動したら削除する
    // （Unityのエディター上ではシーンビューの画面も影響するので注意）
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
