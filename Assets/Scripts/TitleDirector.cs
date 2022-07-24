using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class TitleDirector : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject matchmakingParentView = default;
    [SerializeField] private GameObject startButton = default;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += this.SceneLoaded;

        this.matchmakingParentView.gameObject.SetActive(false);
    }

    public void OnClickedButton(GameObject sender)
    {
        switch (sender.name) {

            case "StartButton":

                this.matchmakingParentView.gameObject.SetActive(true);
                this.startButton.gameObject.SetActive(false);

                // プレイヤー自身の名前を"Player"に設定する
                PhotonNetwork.NickName = "Player";

                PhotonNetwork.ConnectUsingSettings();

                break;

            case "MatchmakingCloseButton":

                this.matchmakingParentView.gameObject.SetActive(false);
                this.startButton.gameObject.SetActive(true);

                break;

            default:
                break;
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.IsMessageQueueRunning = false;
        FadeManager.Instance.LoadScene("FootRaceScene", 1.0f);
    }

    private void SceneLoaded(Scene next, LoadSceneMode mode)
    {
        if (next.name == "FootRaceScene")
        {
            // シーン切り替え後のスクリプトを取得
            FootRaceDirector footRaceDirector = GameObject.Find("FootRaceDirector").GetComponent<FootRaceDirector>();

            // データを渡す処理
            // footRaceDirector.userName = this.userName;
        }

        // イベントから削除
        SceneManager.sceneLoaded -= SceneLoaded;
    }
}
