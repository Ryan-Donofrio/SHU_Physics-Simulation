using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;




public class UserSpawn : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public Transform[] spawnPoints;

    private void Start()
    {
        int randomNumber = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomNumber];
        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerHolderUpdated"]];
        PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
    }

}
