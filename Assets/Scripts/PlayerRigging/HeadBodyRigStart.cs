using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class VRMap0
{
    public Transform VRTarget0;
    public Transform rigTarget0;
    public Vector3 positionOffset0;
    public Vector3 rotationOffset0;

    public void Map0()
    {
        rigTarget0.position = VRTarget0.TransformPoint(positionOffset0);
        rigTarget0.rotation = VRTarget0.rotation * Quaternion.Euler(rotationOffset0);
    }
}

public class HeadBodyRigStart : MonoBehaviour
{
    public VRMap0 head;
    public VRMap0 rightHand;
    public VRMap0 leftHand;

    public Transform headConstraint;
    Vector3 offset;

    public float turnFactor = 1f;
    public ForwardAxis forwardAxis;

    public enum ForwardAxis
    {
        blue,
        green,
        red
    }

 

    void Start()
    {
       
        head.VRTarget0 = GameObject.Find("Main Camera").GetComponent<Transform>();
        rightHand.VRTarget0 = GameObject.Find("RightHand Controller").GetComponent<Transform>();
        leftHand.VRTarget0 = GameObject.Find("LeftHand Controller").GetComponent<Transform>();

        offset = transform.position - headConstraint.position;
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

        head.Map0();
        rightHand.Map0();
        leftHand.Map0();
    }
}
