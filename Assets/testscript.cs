using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using ExitGames.Client.Photon;

public class testscript : MonoBehaviourPun
{

    public const byte codebye = 2;

    public void Update()
    {
        sendPOSRE();
    }

    public void sendPOSRE()
    {
        Debug.Log("SendPoseRe");
        string pos = "123";
        string rot = "123rot";
        object[] data = new object[] { pos, rot };

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(codebye, data, raiseEventOptions, SendOptions.SendReliable);
    }

    public void OnEvent(byte eventCode, object content, int senderId)
    {
        Debug.Log("Recied Self 000000000000000000000000000000000000000000000000000000000");
        while (eventCode == codebye)
        {
            object[] data = (object[])content;
            Vector3 pos = (Vector3)data[0];
            Quaternion rot = (Quaternion)data[1];
            //camView.transform.position = pos;
            //camView.transform.rotation = rot;
        }
    }

}
