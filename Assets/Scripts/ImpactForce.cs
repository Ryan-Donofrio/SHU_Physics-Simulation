using UnityEngine;
using Game;

public class ImpactForce : MonoBehaviour, IHitable
{
    // Start is called before the first frame update
    public float force = 0f;

    public AudioClip collisionSound;

    private bool firstCollision;

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

    public void Start()
    {
        firstCollision = true;
        InitialPosition = transform.position;
        InitialRotation = transform.rotation;
        GetRigidbody = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject == ObjectLauncher.GetObjectLauncher.GetProjectile)
        {
            force = collision.relativeVelocity.magnitude;
            Impacted();
        }
    }

    public void forcereset()
    {
        firstCollision = true;
        force = 0;
        MenuController.GetMenuController.SetImpactText("0");
    }

    public void Impacted()
    {
        if (force > 1f)
        {
            Debug.Log("Playing impact sound");
            SoundManager.GetSoundManager.InstantiateSound(transform.position, collisionSound, 0.3f);
        }

        if (firstCollision == true)
        {
            MenuController.GetMenuController.SetImpactText(force.ToString());
            firstCollision = false;
        }
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
}
