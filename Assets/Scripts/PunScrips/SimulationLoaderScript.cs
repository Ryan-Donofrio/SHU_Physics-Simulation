using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ryandonofrio
{

    public class SimulationLoaderScript : MonoBehaviourPunCallbacks
    {
        private string RoomName;
        public TMP_InputField RoomNameinput;
        //public Text maxplayercounttext;
        //public Slider maxplayercountvalue;
        private bool isConnecting = false;
        private const string GameVersion = "0.4";
        private const int MaxPlayersPerRoom = 2;
        public GameObject fade;
        //public Transform contentscrollview;
        //public GameObject RoomPrefab;
        private Dictionary<string, RoomInfo> Rooms = new Dictionary<string, RoomInfo>();
        //public Text roomscount;
        //public Text playersinrooms;
        //public Text playersinlobby;
        //public Text allplayers;
        //public InputField roomsearch;
        public Animator loadingscreen;
        public SaveNameScript nicknamesave;
        //public GameObject character_selection;
        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.SendRate = 15;
            PhotonNetwork.SerializationRate = 15;
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = "0.4";
            PhotonNetwork.GameVersion = "0.1";
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = GameVersion;
            loadingscreen.gameObject.SetActive(true);
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
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {

        }
        public void UpdateRoomsScrowView()
        {

        }
        void JoinRoom(Transform roomname)
        {
            nicknamesave.PlacePlayerName();
            fade.SetActive(true);
            RoomName = roomname.Find("Name").GetComponent<Text>().text;
            Invoke("joinnamedroom", 0.25f);
        }
        void joinnamedroom()
        {
            PhotonNetwork.JoinRoom(RoomName);
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
            Debug.Log("Client succesfully joined room");
            PhotonNetwork.LoadLevel("MainScene");
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
        }

    }
}