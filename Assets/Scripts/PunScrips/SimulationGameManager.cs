using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;

public class SimulationGameManager : MonoBehaviourPunCallbacks
{
    PhotonView PV;
    public TMP_Text roomname;
    public TMP_Text playerName;
    public TMP_Text inpt;
    public TMP_Text chat;
    public Transform[] spawnPoints;

    [PunRPC]
    public void changetext(string textRPC)
    {
        chat.text = textRPC;
    }
    
    private void Start()
    {
        PhotonNetwork.Instantiate("PlayerHolderUpdated", spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].position, Quaternion.identity);
        roomname.text = PhotonNetwork.CurrentRoom.Name.ToString();
        playerName.text = PhotonNetwork.LocalPlayer.NickName.ToString();
    }
    private void LateUpdate()
    {
        //playercount.text = PhotonNetwork.PlayerList.Length.ToString() + "/" + mxppl;
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //foreach (Transform a in contentplayerlist)
            //Destroy(a.gameObject);
        //foreach (Player player in PhotonNetwork.PlayerList)
        //{
            //GameObject playerlist = Instantiate(Playerlisting, contentplayerlist) as GameObject;
            //playerlist.transform.Find("Name").GetComponent<Text>().text = player.NickName;
            //playerlist.transform.Find("private ID").GetComponent<Text>().text = player.ActorNumber.ToString();
        //}
        //base.OnPlayerLeftRoom(otherPlayer);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //foreach (Transform a in contentplayerlist)
            // Destroy(a.gameObject);
        //foreach (Player player in PhotonNetwork.PlayerList)
        //{
            //GameObject playerlist = Instantiate(Playerlisting, contentplayerlist) as GameObject;
            //playerlist.transform.Find("private ID").GetComponent<Text>().text = player.ActorNumber.ToString();
            //playerlist.transform.Find("Name").GetComponent<Text>().text = player.NickName;
        //}
        //base.OnPlayerEnteredRoom(newPlayer);
    }

    public void callrpc()
    {
        if (!string.IsNullOrEmpty(inpt.text))
        {
            GameObject playerobject = GameObject.FindGameObjectWithTag("Player");
            PV = playerobject.GetComponentInChildren<PhotonView>();
            photonView.RPC("changetext", RpcTarget.All, chat.text + "\n" + "<color=lightblue>" + PV.Owner.NickName + ": " + "</color>" + inpt.text);
            inpt.text = null;
        }
    }

    public void OutputNetworkingInfo()
    {
        Debug.Log("Current room: " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Player count: " + PhotonNetwork.CountOfPlayers);
        Debug.Log("Player name: " + PhotonNetwork.LocalPlayer.NickName);
        Debug.Log("Current lobby: " + PhotonNetwork.CurrentLobby);
        

    }
}

