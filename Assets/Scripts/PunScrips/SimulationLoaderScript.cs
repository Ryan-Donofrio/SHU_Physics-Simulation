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

        void Awake()
        {
            Debug.Log("Trying to Connect to Server...");
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = "0.4";
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = "0.4";
            loadingscreen.gameObject.SetActive(true);
        }
        public void Start()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected To Master-Server...");
            Debug.Log("Trying to Connect to Lobby...");
            PhotonNetwork.JoinLobby();
            base.OnConnectedToMaster();
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("Player Joined Lobby Successfully...");
            loadingscreen.SetBool("Fadeloading", true);
            Invoke("disableloading", 0.9f);
            base.OnJoinedLobby();
        }

        public void disableloading()
        {
            loadingscreen.gameObject.SetActive(false);
        }

        //JOIN ROOM
        public void joinnamedroom()
        {
            string roomName = RoomNameinput.text;
            PhotonNetwork.NickName = UserNameInput.text;
            PhotonNetwork.JoinRoom(roomName);
            Debug.Log("Trying to Join Room... " + RoomNameinput.text);
        }

        //CREATE ROOM
        public void CreateRoom()
        {
            Debug.Log("Tryign to Create Room... " + RoomNameinput.text);
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 20;
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;
            PhotonNetwork.NickName = UserNameInput.text;
            PhotonNetwork.JoinOrCreateRoom(RoomNameinput.text, roomOptions, TypedLobby.Default);
            
        }

        //CREATE ROOM STEP 2
        public override void OnCreatedRoom()
        {
            Debug.Log("Created Room Successfully...");
            base.OnCreatedRoom();
        }

        //FINAL
        public override void OnJoinedRoom()
        {
            Debug.Log("Client succesfully joined/created room: " + PhotonNetwork.CurrentRoom);
            PhotonNetwork.LoadLevel("MainScene2");
            base.OnJoinedRoom();
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
            Debug.Log("Failed to Join Room For these Resons...           Return code " + returnCode + " Message:  " + message);
            Debug.Log(RoomNameinput.text);
        }
    }
}