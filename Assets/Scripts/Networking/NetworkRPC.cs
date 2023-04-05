using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;


public class NetworkRPC : MonoBehaviourPun
{
    public TMP_Text displayRPC;
    public TMP_Text numOutput;
    private string thisPlayerName;

    private void Start()
    {
        
        thisPlayerName = photonView.Owner.NickName.ToString();
    }
    public void callrpc()
    {
        if (this.photonView.IsMine)
        {
            Debug.Log("Started CallRpc Func");
            this.photonView.RPC("changetext", RpcTarget.AllBufferedViaServer, displayRPC.text + "\n" + "<color=lightblue>" + thisPlayerName + ": " + "</color>" + numOutput.text);
        }
            
       
    }

    [PunRPC]
    void changetext(string textRPC, PhotonMessageInfo info)
    {

        if (this.photonView.IsMine)
        {

            displayRPC.text = textRPC;
            Debug.Log("Player that the RPC was sent from: " + info);
            Debug.Log("This is the rpc transfer data....." + textRPC);
        }
        else
        { 
            Debug.Log("Called changetext but it wasnt photon view mine" + info);
        }
        
    }


}
