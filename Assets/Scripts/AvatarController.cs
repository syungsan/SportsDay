using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.InputSystem;

// MonoBehaviourPunCallbacksを継承して、photonViewプロパティを使えるようにする
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

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Fire");
        }
    }
}
