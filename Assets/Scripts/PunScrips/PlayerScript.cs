using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PlayerScript : MonoBehaviourPun
{
    PhotonView PV;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    

    public void Start()
    {
        GameObject playerobject = GameObject.FindGameObjectWithTag("Player");
        //PV = playerobject.GetComponentInChildren<PhotonView>();
    }
    public void Update()
    {
        if (PV.GetComponentInChildren<PhotonView>().IsMine)
        {
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
            float z = Input.GetAxis("Verticle") * Time.deltaTime * 3.0f;
            transform.Rotate(0, x, 0);
            transform.Translate(0, 0, z);
            photonView.RPC("SyncPositionRotation", RpcTarget.Others, transform.position, transform.rotation);
        
        }
    else
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5);
        }
    }
    [PunRPC]
    void SyncPositionRotation(Vector3 newPosition, Quaternion newRotation)
    {

        targetRotation = newRotation;
        targetPosition = newPosition;

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            targetPosition = (Vector3)stream.ReceiveNext();
            targetRotation = (Quaternion)stream.ReceiveNext();
        }
      
    }
}
