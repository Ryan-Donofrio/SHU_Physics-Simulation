using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;


public class DataManager : MonoBehaviourPun
{

    private PhotonView PV;
    public InputField inputFeild;
    public string data;



    void Awake() { PhotonNetwork.ConnectUsingSettings(); }
    
        
    
    void Start()
    {
        //data.text = "testcompleate";
    }

    [PunRPC]
    public void changetext()
    {
        data = inputFeild.text;
        inputFeild.text = data;
        
        Debug.Log(data + " = changetext");
    }

    [PunRPC]
    public void callrpc()

    {
        if (!string.IsNullOrEmpty(inputFeild.text))

        {

            PhotonView photonview = PhotonView.Get(this);
            photonView.RPC("changetext", RpcTarget.All, inputFeild.text);



            Debug.Log(data + " = Data.text");
            Debug.Log(inputFeild.text + " = input.text");
            //Debug.Log(textRPC);
            //inputFeild.text = null;


        }
    }
}
