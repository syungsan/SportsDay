using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;

// MonoBehaviourPunCallbacks���p�����āAphotonView�v���p�e�B���g����悤�ɂ���
public class AvatarController : MonoBehaviourPunCallbacks, IPunObservable
{
    private const float MaxStamina = 6f;

    [SerializeField]
    private Image staminaBar = default;

    private float currentStamina = MaxStamina;

    private float footStep = 30.0f;
    private float stepDistance;
    private float stepTime = 0.5f;

    // Start is called before the first frame update
    private void Start()
    {
        GameObject course = GameObject.Find($"Course{photonView.OwnerActorNr}Panel");

        float avatarRealSizeX = this.GetComponent<Renderer>().bounds.size.x * this.transform.localScale.x;
        float courseLength = (course.transform.position.x - (Camera.main.ScreenToWorldPoint(course.GetComponent<RectTransform>().sizeDelta).x + avatarRealSizeX) * 0.85f) * 2.0f;

        this.stepDistance = -1.0f * (courseLength / footStep);
    }

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

        if (photonView.IsMine)
        {
            if ((Mouse.current != null && Mouse.current.leftButton.wasReleasedThisFrame) ||
                            (Joystick.current != null && Joystick.current.trigger.wasReleasedThisFrame) ||
                            (Gamepad.current != null && Gamepad.current.rightTrigger.wasReleasedThisFrame))
            {
                float movedChartX = this.transform.position.x + this.stepDistance;
                this.transform.DOMoveX(movedChartX, this.stepTime);
            }
        }
            
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
}
