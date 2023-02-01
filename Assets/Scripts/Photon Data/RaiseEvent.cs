using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaiseEvent : MonoBehaviourPun 
{
    private Text _TextDisplay;
    public InputField _TextInput;
    public string _TextString;
    private static byte TEXT_CHANGE_EVENT = 0;

    public enum PhotonEventCodes
    { 
        textchange = 0
    }
    private void Awake()
    {
        _TextDisplay = GetComponent<Text>();
        //PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
        Debug.Log("is assigned");
    }

    void Update()
    {
        _TextString = _TextInput.text;
        if (!PhotonNetwork.NetworkingClient.IsConnected)
            Debug.Log("disconnected from networking client");
        else if (!PhotonNetwork.IsConnected)
            Debug.Log("disconnected from Photon network");
    }

    public void NetworkingClient_EventReceived(EventData photonEvent)
    {
        Debug.Log("calling");
        byte eventCode = photonEvent.Code;
        if (photonEvent.Code == TEXT_CHANGE_EVENT)
        {
            string T = (string)photonEvent[0];
            _TextDisplay.text = T;
        }

        Debug.Log("Completed " + (string)photonEvent[0]);
    }


    public void ChangeText()
    {
        if (base.photonView.IsMine && !string.IsNullOrEmpty(_TextInput.text))
        {

            string datas = _TextString;



            PhotonNetwork.RaiseEvent(TEXT_CHANGE_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
        }

    }

}
