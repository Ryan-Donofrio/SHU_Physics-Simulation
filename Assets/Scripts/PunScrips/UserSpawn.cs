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
        createNewPlayer();
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        createNewPlayer();
    }




    public void createNewPlayer()
    {
        PhotonNetwork.Instantiate(playerPrefabs[selectedPrefab].name, spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].position, Quaternion.identity);
    }
}
