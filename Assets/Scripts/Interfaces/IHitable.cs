using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitable
{
    public Vector3 InitialPosition { get; set; }
    public Quaternion InitialRotation { get; set; }

    public Rigidbody GetRigidbody { get; set; }

    public void Impacted();
    public void ResetSelf();
    public void HideSelf();

    //Make sure the object sets its initial position here
    public void Start();

    //This following is an example of how a script might look
    /*
    
    private Vector3 initialPos;
    private Quaternion initialRot;
    private Rigidbody rb;

    public Vector3 InitialPosition
    {
        get { return initialPos; }
        set { initialPos = value; }
    }

    public Quaternion InitialRotation
    {
        get { return initialRot; }
        set { initialRot = value; }
    }

    public Rigidbody GetRigidbody
    {
        get { return rb; }
        set { rb = value; }
    }

    //make sure the Start function is public in your script
    public void Start()
    {
        InitialPosition = transform.position;
        InitialRotation = transform.rotation;

        GetRigidbody = GetComponent<Rigidbody>();
    }

    public void Impacted()
    {

    }

    public void ResetSelf()
    {
        transform.position = InitialPosition;
        transform.rotation = InitialRotation;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void HideSelf()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    */

}
