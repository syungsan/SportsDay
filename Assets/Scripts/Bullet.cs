using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    private Vector3 origin; // �e�𔭎˂��������̍��W

    private Vector3 velocity;

    private int timestamp; // �e�𔭎˂�������

    // �e��ID��Ԃ��v���p�e�B
    public int Id { get; private set; }

    // �e�𔭎˂����v���C���[��ID��Ԃ��v���p�e�B
    public int OwnerId { get; private set; }

    // �����e���ǂ�����ID�Ŕ��肷�郁�\�b�h
    public bool Equals(int id, int ownerId) => id == Id && ownerId == OwnerId;

    public void Init(int id, int ownerId, Vector3 origin, float angle, int timestamp)
    {
        Id = id;
        OwnerId = ownerId;

        this.origin = origin;

        velocity = 9f * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));

        this.timestamp = timestamp;

        // ��x��������Update()���Ă�ŁAtransform.position�̏����l�����߂�
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        // �e�𔭎˂����������猻�ݎ����܂ł̌o�ߎ��Ԃ����߂�
        float elapsedTime = Mathf.Max(0f, unchecked(PhotonNetwork.ServerTimestamp - timestamp) / 1000f);

        // �e�𔭎˂��������ł̍��W�E���x�E�o�ߎ��Ԃ��猻�݂̍��W�����߂�
        transform.position = origin + velocity * elapsedTime;
    }

    // ��ʊO�Ɉړ�������폜����
    // �iUnity�̃G�f�B�^�[��ł̓V�[���r���[�̉�ʂ��e������̂Œ��Ӂj
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
