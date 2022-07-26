using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;

// MonoBehaviourPunCallbacksを継承して、photonViewプロパティを使えるようにする
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
                // 入力があったら、スタミナを減少させる
                currentStamina = Mathf.Max(0f, currentStamina - Time.deltaTime);
                
                
                transform.Translate(6f * Time.deltaTime * input.normalized);
            }
            else
            {
                // 入力がなかったら、スタミナを回復させる
                currentStamina = Mathf.Min(currentStamina + Time.deltaTime * 2, MaxStamina);
            }
        }

        // スタミナをゲージに反映する
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
            // 自身のアバターのスタミナを送信する
            stream.SendNext(currentStamina);
        }
        else
        {
            // 他プレイヤーのアバターのスタミナを受信する
            currentStamina = (float)stream.ReceiveNext();
        }
    }
}
