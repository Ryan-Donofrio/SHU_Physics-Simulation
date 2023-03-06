using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;



public class UserSpawn : MonoBehaviourPunCallbacks
{
    public GameObject[] playerPrefabs;
    public Transform[] spawnPoints;
    public int selectedPrefab;



    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            createNewPlayer();
        }
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        createNewPlayer();
    }




    public void createNewPlayer()
    {
        Debug.Log("Players In Room: " + PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.Instantiate(playerPrefabs[selectedPrefab].name, spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].position, Quaternion.identity);
    }
}
