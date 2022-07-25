using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class AvatarFireBullet : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Bullet bulletPrefab = default;

    private int nextBulletId = 0;

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            // 左クリックでカーソルの方向に弾を発射する
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 objectPoint = Camera.main.WorldToScreenPoint(transform.position);

                // マウス位置をpointScreenに格納
                Vector3 pointScreen = new Vector3(Input.mousePosition.x, Input.mousePosition.y, objectPoint.z);

                var mousePosition = Camera.main.ScreenToWorldPoint(pointScreen);
                mousePosition.z = transform.position.z;

                Debug.Log(mousePosition.x + ";" + mousePosition.y + ";" + mousePosition.z);

                var direction = mousePosition - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x);

                // 弾を発射するたびに弾のIDを1ずつ増やしていく
                photonView.RPC(nameof(FireBullet), RpcTarget.All, nextBulletId++, angle);
            }
        }
    }

    // 弾を発射するメソッド
    [PunRPC]
    private void FireBullet(int id, float angle, PhotonMessageInfo info)
    {
        var bullet = Instantiate(bulletPrefab);

        // 弾を発射した時刻に50msのディレイをかける
        int timestamp = unchecked(info.SentServerTimestamp + 50);
        bullet.Init(id, photonView.OwnerActorNr, transform.position, angle, timestamp);
    }
}
