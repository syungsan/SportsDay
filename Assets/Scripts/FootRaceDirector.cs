using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Linq;

public class FootRaceDirector : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject courseParentView = default;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;

        Room room = PhotonNetwork.CurrentRoom;
        Player player = PhotonNetwork.LocalPlayer;

        if (player.GetJoinType() == "Player")
        {
            room.AddPlayerID(player.ActorNumber);
        }
        else if (player.GetJoinType() == "Watcher")
        {
            room.AddWatcherID(player.ActorNumber);
        }

        // ルームを作成したプレイヤーは、現在のサーバー時刻をゲームの開始時刻に設定する
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetStartTime(PhotonNetwork.ServerTimestamp);
        }
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        Player player = PhotonNetwork.LocalPlayer;
        List<int> playerIDList = new List<int>();
        List<int> watcherIDList = new List<int>();

        // 更新されたルームのカスタムプロパティのペアをコンソールに出力する
        foreach (var prop in propertiesThatChanged)
        {
            if (prop.Key.ToString() == "PlayerIDKeys")
            {
                playerIDList = prop.Value.ToString().Split(',').Select(a => int.Parse(a)).ToList();

                if (PhotonNetwork.LocalPlayer.GetJoinType() == "Player")
                {
                    GameObject avatar = PhotonNetwork.Instantiate("Avatars/Punk", new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

                    Transform[] courses = CommonFuncs.GetChildren(this.courseParentView.transform);
                    Transform course = courses[playerIDList.IndexOf(player.ActorNumber)];

                    float avatarRealSizeX = avatar.GetComponent<Renderer>().bounds.size.x * avatar.transform.localScale.x;
                    float positionX = course.transform.position.x - (Camera.main.ScreenToWorldPoint(course.GetComponent<RectTransform>().sizeDelta).x + avatarRealSizeX) * 0.85f;

                    avatar.transform.position = new Vector3(positionX, course.transform.position.y - 0.7f, avatar.transform.position.z);

                    CourseNameDisplay courseNameDisplay = avatar.GetComponent<CourseNameDisplay>();
                    courseNameDisplay.CallSetCourseName(playerIDList.IndexOf(player.ActorNumber));
                }
            }

            if (prop.Key.ToString() == "WatcherIDKeys")
            {
                watcherIDList = prop.Value.ToString().Split(',').Select(a => int.Parse(a)).ToList();

                if (PhotonNetwork.LocalPlayer.GetJoinType() == "Watcher")
                {
                    GameObject avatar = PhotonNetwork.Instantiate("Avatars/DressGirl_1", new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

                    avatar.transform.position = new Vector3(8f, -3.7f, avatar.transform.position.z);

                    Vector3 worldAngle = avatar.transform.eulerAngles;
                    worldAngle.x = 0.0f;
                    worldAngle.y = 180.0f;
                    worldAngle.z = 0.0f;
                    avatar.transform.eulerAngles = worldAngle;
                }
            }
        }
    }
}
