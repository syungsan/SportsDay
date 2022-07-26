using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.InputSystem;

// MonoBehaviourPunCallbacks���p�����āAphotonView�v���p�e�B���g����悤�ɂ���
public class AvatarController : MonoBehaviourPunCallbacks, IPunObservable
{
    private const float MaxStamina = 6f;

    [SerializeField]
    private Image staminaBar = default;

    private float currentStamina = MaxStamina;

    // Update is called once per frame
    private void Update()
    {
        if (photonView.IsMine)
        {
            var input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
            if (input.sqrMagnitude > 0f)
            {
                // ���͂���������A�X�^�~�i������������
                currentStamina = Mathf.Max(0f, currentStamina - Time.deltaTime);
                
                
                transform.Translate(6f * Time.deltaTime * input.normalized);
            }
            else
            {
                // ���͂��Ȃ�������A�X�^�~�i���񕜂�����
                currentStamina = Mathf.Min(currentStamina + Time.deltaTime * 2, MaxStamina);
            }
        }

        // �X�^�~�i���Q�[�W�ɔ��f����
        staminaBar.fillAmount = currentStamina / MaxStamina;
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // ���g�̃A�o�^�[�̃X�^�~�i�𑗐M����
            stream.SendNext(currentStamina);
        }
        else
        {
            // ���v���C���[�̃A�o�^�[�̃X�^�~�i����M����
            currentStamina = (float)stream.ReceiveNext();
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Fire");
        }
    }
}
