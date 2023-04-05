using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;


    public class InitiateUser : MonoBehaviour
    {
        [SerializeField] private GameObject PlayerPrefab_Male = null;
        [SerializeField] private GameObject PlayerPrefab_Female = null;
        public Vector3 Player_Spawn_Position;
        public bool Use_Custom_Spawn_Position = false;
        public float Reset_Height;
        GameObject player;
        int selection;
        void Start()
        {
            if (PlayerPrefs.HasKey("Character"))
                selection = PlayerPrefs.GetInt("Character");
            else
                selection = 1;
            if (Use_Custom_Spawn_Position)
            {
                if (selection == 1)
                    player = PhotonNetwork.Instantiate(PlayerPrefab_Male.name, Player_Spawn_Position, Quaternion.identity);
                else
                    player = PhotonNetwork.Instantiate(PlayerPrefab_Female.name, Player_Spawn_Position, Quaternion.identity);
            }
            else
                if (selection == 1)
                player = PhotonNetwork.Instantiate(PlayerPrefab_Male.name, Player_Spawn_Position, Quaternion.identity);
            else
                player = PhotonNetwork.Instantiate(PlayerPrefab_Female.name, Player_Spawn_Position, Quaternion.identity);
        }
        private void Update()
        {
            if (player.transform.position.y < Reset_Height)
            {
                if (Use_Custom_Spawn_Position)
                    player.transform.position = Player_Spawn_Position;
                else
                    player.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 2f, 0);
            }
        }
    }

