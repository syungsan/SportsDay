using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class CourseNameDisplay : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject coursePanel = GameObject.Find($"Course{photonView.OwnerActorNr}Panel");
        TextMeshProUGUI label = coursePanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        label.text = $"{photonView.Owner.NickName}({photonView.OwnerActorNr})";
    }
}
