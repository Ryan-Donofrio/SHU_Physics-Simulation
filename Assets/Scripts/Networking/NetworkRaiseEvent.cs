using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using TMPro;


public class NetworkRaiseEvent : MonoBehaviourPun
{
    
    public TMP_Text displayRPC;
    public TMP_Text numOutput;
    private string thisPlayerName;
    public const byte eventCode = 0;
    public object objTextDisplay;

    
    void Start()
    {
        thisPlayerName = photonView.Owner.NickName.ToString();
    }

   
    public void sendDataRE()
    {
        objTextDisplay = displayRPC.text + "\n" + "<color=lightblue>" + thisPlayerName + ": " + "</color>" + numOutput.text;
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(eventCode, objTextDisplay, raiseEventOptions, SendOptions.SendReliable);
    }
   
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == eventCode)
        { 
            string data = (string)obj.CustomData;
            if (this.photonView.IsMine)
            {
                displayRPC.text = data;
                Debug.Log("PhotonView is mine => got: " + data);
            }
            else
            {
                Debug.Log("photon view is not mine => received: " + data);
            }
        }
    }

  
}
