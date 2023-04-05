using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class DataExchange : MonoBehaviourPunCallbacks
{
    public TMP_InputField inputField;
    public TMP_Text sentInformationText;
    public Button sendButton;

    private string dataToSend;

    void Start()
    {
        inputField.onEndEdit.AddListener(delegate { UpdateDataToSend(); });
        sendButton.onClick.AddListener(delegate { SendData(); });
    }

    void UpdateDataToSend()
    {
        dataToSend = inputField.text;
    }
    void SendData()
    {
        if (!PhotonNetwork.IsConnected)
        {
            sentInformationText.text = "You are not connected to the Photon network.";
            return;
        }
        if (string.IsNullOrEmpty(dataToSend))
        {
            sentInformationText.text = "Enter a value in the input field to send.";
            return;
        }

        byte eventCode = 1; // Define a custom event code
        RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        SendOptions sendOptions = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(eventCode, dataToSend, options, sendOptions);
        sentInformationText.text = "Data sent: " + dataToSend;
    }

    public void OnEvent(byte eventCode, object content, int senderId)
    {
        if (eventCode == 1) // Check if the event code matches the custom event code
        {
            string receivedData = content as string;
            sentInformationText.text = sentInformationText + "\n" + "Data received: " + receivedData;
        }
    }
}
