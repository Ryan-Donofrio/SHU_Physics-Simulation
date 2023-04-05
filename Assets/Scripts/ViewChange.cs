using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class ViewChange : MonoBehaviourPun
{
    public int parentViewID = 1001;
    public string[] childNames;
    public int playerCount;
    public TMP_Text textCount;
    public int startPlayer = 1001;
    public int endPlayer;
    public GameObject adminOverlay;
    public Camera camView;


    public void Start()
    {
        if (photonView.IsMine)
        {
            adminOverlay.SetActive(true);
        }
        else
        {
            adminOverlay.SetActive(false);
        }
    }
    void Update()
    {
        endPlayer = PhotonNetwork.CurrentRoom.PlayerCount + 1000;
        if (photonView.IsMine)
        {
            PhotonView parentView = PhotonView.Find(parentViewID);
            if (parentView != null)
            {
                Transform childTransform = parentView.transform;
                foreach (string childName in childNames)
                {
                    childTransform = childTransform.Find(childName);
                    if (childTransform == null)
                    {
                        Debug.LogWarning("Child " + childName + " not found on GameObject with ViewID " + parentViewID + ".");
                        return;
                    }
                }
                PhotonTransformView childTransformView = childTransform.GetComponent<PhotonTransformView>();
                if (childTransformView != null)
                {
                    Vector3 childPosition = childTransformView.transform.position;
                    Quaternion childRotation = childTransformView.transform.rotation;
                    camView.transform.position = childPosition;
                    camView.transform.rotation = childRotation;
                    Debug.Log("ROTATION AND POSITION:   ROT => " + childRotation + "  POS =>  " + childPosition);
                    Debug.Log("Position of child " + childNames[childNames.Length - 1] + " of PhotonView with ViewID " + parentViewID + " is " + childPosition);
                }
                else
                {
                    Debug.LogWarning("Photon Transform View not found on child " + childNames[childNames.Length - 1] + ".");
                }
            }
            else
            {
                Debug.LogWarning("PhotonView with ViewID " + parentViewID + " not found.");
            }
        }
    }

    
    public void nextPlayer()
    {
       
        parentViewID += 1;
        
        textCount.text = "Current Player: " + parentViewID;

    }

    public void prevPlayer()
    {

       
        parentViewID -= 1;
        
        textCount.text = "Current Player: " + parentViewID;
    }
}