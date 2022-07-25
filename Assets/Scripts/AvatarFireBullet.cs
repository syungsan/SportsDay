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
            // ���N���b�N�ŃJ�[�\���̕����ɒe�𔭎˂���
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 objectPoint = Camera.main.WorldToScreenPoint(transform.position);

                // �}�E�X�ʒu��pointScreen�Ɋi�[
                Vector3 pointScreen = new Vector3(Input.mousePosition.x, Input.mousePosition.y, objectPoint.z);

                var mousePosition = Camera.main.ScreenToWorldPoint(pointScreen);
                mousePosition.z = transform.position.z;

                Debug.Log(mousePosition.x + ";" + mousePosition.y + ";" + mousePosition.z);

                var direction = mousePosition - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x);

                // �e�𔭎˂��邽�тɒe��ID��1�����₵�Ă���
                photonView.RPC(nameof(FireBullet), RpcTarget.All, nextBulletId++, angle);
            }
        }
    }

    // �e�𔭎˂��郁�\�b�h
    [PunRPC]
    private void FireBullet(int id, float angle, PhotonMessageInfo info)
    {
        var bullet = Instantiate(bulletPrefab);

        // �e�𔭎˂���������50ms�̃f�B���C��������
        int timestamp = unchecked(info.SentServerTimestamp + 50);
        bullet.Init(id, photonView.OwnerActorNr, transform.position, angle, timestamp);
    }
}
