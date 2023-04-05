using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using TMPro;


public class ViewChangeV2 : MonoBehaviourPun
{
    public int parentViewID = 1001;
    public string[] childNames;
    public int playerCount;
    public TMP_Text textCount;
    public int startPlayer = 1001;
    public int endPlayer;
    public GameObject adminOverlay;
    
    //Position Send
    public const byte eventCode1 = 1;
    public Camera camView;


    public void Start()
    {
        if (this.photonView.IsMine)
        {
            adminOverlay.SetActive(true);
        }
        else
        {
            adminOverlay.SetActive(false);
        }
    }
    public void OnEvent(byte eventCode, object content, int senderId)
    {
        Debug.Log("Networkraise event sending received  on self");
        while (eventCode == eventCode1)
        {
            object[] data = (object[])content;
            Vector3 pos = (Vector3)data[0];
            Quaternion rot = (Quaternion)data[1];
            camView.transform.position = pos;
            camView.transform.rotation = rot;
        }
    }

    public void nextPlayer()
    {
        textCount.text = "Current Player: +1";
    }

    public void prevPlayer()
    {
        textCount.text = "Current Player: -1";
    }
}