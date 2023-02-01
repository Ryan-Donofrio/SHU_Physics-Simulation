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
        //public TMP_Text playercount;
        public TMP_Text roomname;
        public TMP_InputField inpt;
        public TMP_Text chat;
        string mxppl;
        //public Transform contentplayerlist;
        private Dictionary<string, Player> Players = new Dictionary<string, Player>();
        //public GameObject Playerlisting;
        public GameObject Fade_out_on_load;
        [PunRPC]
        public void changetext(string textRPC)
        {
            chat.text = textRPC;
        }
        void disablefade()
        {
            Fade_out_on_load.SetActive(false);
        }
        private void Start()
        {
            Fade_out_on_load.SetActive(true);
            Invoke("disablefade", 0.5f);
            roomname.text = PhotonNetwork.CurrentRoom.Name.ToString();
            if (PhotonNetwork.CurrentRoom.MaxPlayers == 0)
            {
                mxppl = "No Limit";
            }
            else
                mxppl = PhotonNetwork.CurrentRoom.MaxPlayers.ToString();
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                //GameObject playerlist = Instantiate(Playerlisting, contentplayerlist) as GameObject;
                //playerlist.transform.Find("Name").GetComponent<Text>().text = player.NickName;
                //playerlist.transform.Find("private ID").GetComponent<Text>().text = player.ActorNumber.ToString();
            }
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
    }

