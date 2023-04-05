using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

[System.Serializable]
public class VRMap
{
    public Transform VRTarget;
    public Transform rigTarget;
    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    public void Map()
    {
        rigTarget.position = VRTarget.TransformPoint(positionOffset);
        rigTarget.rotation = VRTarget.rotation * Quaternion.Euler(rotationOffset);
    }
}

public class HeadBodyRig : MonoBehaviourPun
{
    public VRMap head;
    public VRMap rightHand;
    public VRMap leftHand;

    public Transform headConstraint;
    Vector3 offset;

    public float turnFactor = 1f;
    public ForwardAxis forwardAxis;


    //Position Send
    public const byte eventCode1 = 1;
    


    public enum ForwardAxis
    {
        blue,
        green,
        red
    }

    public GameObject photonViewGameObjectHolder;
    private PhotonView photonView;

    public const byte eventCode = 1;

    void Start()
    {
        photonView = photonViewGameObjectHolder.GetComponent<PhotonView>();
        head.VRTarget = GameObject.Find("Main Camera").GetComponent<Transform>();
        rightHand.VRTarget = GameObject.Find("RightHand Controller").GetComponent<Transform>();
        leftHand.VRTarget = GameObject.Find("LeftHand Controller").GetComponent<Transform>();

        offset = transform.position - headConstraint.position;
    }

    public void Update()
    {
        if(photonView.IsMine)
        {
            sendPOSRE();
        }
    }

    public void sendPOSRE()
    {
        Debug.Log("SendPoseRe");
        Vector3 pos = head.VRTarget.transform.position;
        Quaternion rot = head.VRTarget.transform.rotation;
        object[] data = new object[] { pos, rot };

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(eventCode1, data, raiseEventOptions, SendOptions.SendReliable);
    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    private void NetworkingClient_EventReceived(EventData eventData)
    {
        
        while (eventCode == eventCode1)
        {
            object[] data = (object[])eventData.CustomData;
            Vector3 pos = (Vector3)data[0];
            Quaternion rot = (Quaternion)data[1];
            //camView.transform.position = pos;
            //camView.transform.rotation = rot;
            Debug.Log("Recied Self" + pos + "+++++++" + rot);
        }
    }



    void FixedUpdate()
    {
        transform.position = headConstraint.position + offset;
        Vector3 projectionVector = headConstraint.up;
        switch (forwardAxis)
        {
            case ForwardAxis.green:
                projectionVector = headConstraint.up;
                break;
            case ForwardAxis.blue:
                projectionVector = headConstraint.forward;
                break;
            case ForwardAxis.red:
                projectionVector = headConstraint.right;
                break;
        }
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(projectionVector, Vector3.up).normalized, Time.deltaTime * turnFactor);

        head.Map();
        rightHand.Map();
        leftHand.Map();
    }
}
