using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;

public class SimulationGameManager : MonoBehaviourPun
{
    public TMP_Text roomname;
    public TMP_Text playerName;
    public TMP_Text numOutput;
    public TMP_Text displayRPC;
    public PhotonView photonView;

   
    
    private void Start()
    {
        roomname.text = PhotonNetwork.CurrentRoom.Name.ToString();
        //playerName.text = PhotonNetwork.LocalPlayer.NickName.ToString();

        playerName.text = photonView.Owner.NickName.ToString();
    }
   




    public void OutputNetworkingInfo()
    {

        Debug.Log("Player name from photonview script: " + photonView.Owner.NickName);

        Debug.Log("Current room: " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Player count: " + PhotonNetwork.CountOfPlayers);
        Debug.Log("Player name: " + PhotonNetwork.LocalPlayer.NickName);
        Debug.Log("Current lobby: " + PhotonNetwork.CurrentLobby);


    }
}

