using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace ryandonofrio
{

    public class SimulationLoaderScript : MonoBehaviourPunCallbacks
    {
        public TMP_InputField UserNameInput;
        public TMP_InputField RoomNameinput;
        public GameObject fade;
        public Animator loadingscreen;
        public SaveNameScript nicknamesave;
        public GameObject playerPrefab;

        private string lobbyName;
        private const string GameVersion = "0.4";
        private const int MaxPlayersPerRoom = 2;

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.SendRate = 15;
            PhotonNetwork.SerializationRate = 15;
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = "0.4";
            PhotonNetwork.GameVersion = GameVersion;
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = GameVersion;
            loadingscreen.gameObject.SetActive(true);
        }

        private void Start()
        {
            //DontDestroyOnLoad(playerPrefab);
            PhotonNetwork.AutomaticallySyncScene = true;

        }

        public void SearchForGame()
        {
            fade.SetActive(true);
            Invoke("joinrandomroom", 0.25f);
        }
        public void searchthroughrooms()
        {

        }
        public void joinrandomroom()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }
        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
            base.OnConnectedToMaster();
        }
        public override void OnJoinedLobby()
        {
            loadingscreen.SetBool("Fadeloading", true);
            Invoke("disableloading", 0.9f);
            base.OnJoinedLobby();
        }
        void disableloading()
        {
            loadingscreen.gameObject.SetActive(false);
        }

        void JoinRoom(Transform roomname)
        {
            nicknamesave.PlacePlayerName();
            fade.SetActive(true);
            lobbyName = roomname.Find("Name").GetComponent<Text>().text;
            Invoke("joinnamedroom", 0.25f);
            Debug.Log("JoinRoom called for " + PhotonNetwork.CurrentRoom);
        }
        public void joinnamedroom()
        {
            PhotonNetwork.JoinRoom(RoomNameinput.text);
            Debug.Log("Joining room " + RoomNameinput.text);
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log($"Disconected due to: {cause}");
        }
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("No clients have made any rooms");
            PhotonNetwork.CreateRoom("Random Room " + UnityEngine.Random.Range(1, 1000), new RoomOptions { MaxPlayers = MaxPlayersPerRoom });
        }
        public override void OnJoinedRoom()
        {
            Debug.Log("Client succesfully joined room: " + PhotonNetwork.CurrentRoom);
            PhotonNetwork.LoadLevel("MainScene2");
            if (PhotonNetwork.InRoom)
            {
                Debug.Log("Player " + PhotonNetwork.NickName + " is in room " + PhotonNetwork.CurrentRoom);
            }
            else
                Debug.Log("Not in a lobby");
        }
        public void changevaluemaxppl()
        {

        }
        public void CreateRoom()
        {
            fade.SetActive(true);
            Invoke("createit", 0.25f);
        }
        void createit()
        {
            int max = (int)MaxPlayersPerRoom;
            byte maxppl = Convert.ToByte(max);
            PhotonNetwork.CreateRoom(RoomNameinput.text, new RoomOptions { MaxPlayers = maxppl });
            PhotonNetwork.NickName = UserNameInput.text;
        }

    }
}