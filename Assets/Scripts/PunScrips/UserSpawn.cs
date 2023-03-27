using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class UserSpawn : MonoBehaviourPunCallbacks
{
    public GameObject[] playerPrefabs;
    //public GameObject playerMenu;
    public Transform[] spawnPoints;
    public GameObject vRRig;
    public int selectedPrefab;
    private GameObject spawnedPlayerPref;
    //private GameObject spawnedMenuPref;


    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            createNewPlayer();
            //createNewMenu();
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        createNewPlayer();
    }
    public void createNewPlayer()
    {
        Debug.Log("Spawning Player " + PhotonNetwork.CurrentRoom.PlayerCount);
        spawnedPlayerPref = PhotonNetwork.Instantiate(playerPrefabs[selectedPrefab].name, spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].position, spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].rotation);
        vRRig.transform.position = spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].position;
        vRRig.transform.rotation = spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].rotation;
    }

    public void createNewMenu()
    {
        Debug.Log("Spawning Menu ");
        //spawnedMenuPref = PhotonNetwork.Instantiate(playerMenu.name, spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].position, spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].rotation);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPref);
        //PhotonNetwork.Destroy(spawnedMenuPref);
    }
}
