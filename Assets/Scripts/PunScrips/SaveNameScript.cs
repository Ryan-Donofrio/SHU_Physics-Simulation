using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

    public class SaveNameScript : MonoBehaviourPunCallbacks
    {
        public TMP_InputField nameField;
        public TMP_InputField roomNameField;
        public Button joinBtn;
        public Button createBtn;

        void Start()
        {
            joinBtn.interactable = false;
            createBtn.interactable = false;

            if (PlayerPrefs.HasKey("PlayerName"))
            {
                string PlayerName = PlayerPrefs.GetString("PlayerName");
                nameField.text = PlayerName;
                joinBtn.interactable = false;
                createBtn.interactable = false;
            }
        }

        void Update()
        {
            if (!string.IsNullOrEmpty(nameField.text) && !string.IsNullOrEmpty(roomNameField.text))
            {
                joinBtn.interactable = true;
                createBtn.interactable = true;
            }
        }

        public void PlacePlayerName()
        {
           // PhotonNetwork.NickName = nameField.text;
           // PlayerPrefs.SetString("PlayerName", nameField.text);
        }
    }

