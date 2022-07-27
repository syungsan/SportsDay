using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class CourseNameDisplay : MonoBehaviourPunCallbacks
{
    public void CallSetCourseName(int tentativePlayerID)
    {
        if (photonView.IsMine)
        {
            photonView.RPC(nameof(SetCourseName), RpcTarget.AllBuffered, tentativePlayerID);
        }  
    }

    [PunRPC]
    private void SetCourseName(int tentativePlayerID)
    {
        GameObject courseParentView = GameObject.Find("CourseParentPanel");
        Transform[] courses = CommonFuncs.GetChildren(courseParentView.transform);
        Transform course = courses[tentativePlayerID];

        TextMeshProUGUI label = course.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        label.text = $"{photonView.Owner.NickName}({tentativePlayerID + 1})";
    }
}
