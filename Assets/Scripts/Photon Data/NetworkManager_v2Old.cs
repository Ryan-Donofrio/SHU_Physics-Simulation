using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManagerv2_Old: MonoBehaviourPunCallbacks
{
    public bool connected { get; private set; }


    void Start()
    {
        Debug.Log("Connecting...");
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("connected to server");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("disconnected from server for reaseon " + cause.ToString());
        
    }



}
