using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : MonoBehaviour
{
    public GameObject photonViewGameObjectHolder;
    private PhotonView photonView;

    void Start()
    {
        photonView = photonViewGameObjectHolder.GetComponent<PhotonView>();
        if(!photonView.IsMine)
        {
            Destroy(GetComponent<HeadBodyRig>());
            GetComponent<Animator>().enabled = false;
            GetComponent<WalkingController>().enabled = false;
        }
    }
}
