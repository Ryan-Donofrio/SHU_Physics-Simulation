using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

namespace ryandonofrio
{
    public class SaveNameScript : MonoBehaviourPunCallbacks
    {
        public TMP_InputField nameInpunField;
        //public TMP_InputField RoomnameInpunField;
        public Button playbtn;
        //public Button roomsbtn;
        //public Button createbtn;
        void Start()
        {
            if (!PlayerPrefs.HasKey("PlayerName"))
            {
                //playbtn.interactable = false;
                //roomsbtn.interactable = false;
            }
            else
            {
                string PlayerName = PlayerPrefs.GetString("PlayerName");
                nameInpunField.text = PlayerName;
                playbtn.interactable = true;
                //roomsbtn.interactable = true;
            }
            //createbtn.interactable = false;
        }
        public void updatebtninter()
        {
            if (!string.IsNullOrEmpty(nameInpunField.text))
            {
                playbtn.interactable = true;
                //roomsbtn.interactable = true;
            }
            else
            {
                playbtn.interactable = false;
                //roomsbtn.interactable = false;
                //createbtn.interactable = false;
            }
        }
        public void updatebtnRoomName()
        {
            if (!string.IsNullOrEmpty(nameInpunField.text))
            {
                //createbtn.interactable = true;
            }
            else
            {
                //createbtn.interactable = false;
            }
        }
        public void PlacePlayerName()
        {
            string PlayerNickname = nameInpunField.text;
            PhotonNetwork.NickName = PlayerNickname;
            PlayerPrefs.SetString("PlayerName", PlayerNickname);
            Debug.Log("Ranname");
        }

    }
}
