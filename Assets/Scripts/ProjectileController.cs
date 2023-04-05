using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    public float bulletSpeed;
    public float releaseAngle;
    public Rigidbody bullet;
    public float startheight;

    public void Fire()
    {
        //reset pos auto resets and updates the position and start height so dont have to click reset button on update 
        ResetPos();
        bullet.constraints = RigidbodyConstraints.None;
        var targetDirn = transform.forward;
        var elevationAxis = transform.right;
        var releaseVector = Quaternion.AngleAxis(releaseAngle, elevationAxis) * targetDirn;
        bullet.velocity = releaseVector * bulletSpeed;

        //Add two equations here in vector 3
        bullet.AddForce(releaseVector);
        Debug.Log("fire in projectilController script");
    }

    public void ResetPos(bool hideObject = false)
    {
        bullet.constraints = RigidbodyConstraints.FreezePosition;
        bullet.velocity = Vector3.zero;
        bullet.freezeRotation = true;
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(0, startheight, 0);

        if (hideObject)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);

        //Debug.Log("reset in projectileController script");
    }


    //not used function
    public void Freeze()
    {
        bullet.constraints = RigidbodyConstraints.FreezePosition;
        bullet.freezeRotation = true;
        bullet.velocity = Vector3.zero;
    }

    public void LaunchForce(float value)
    {
        bulletSpeed = value;
    }

    public void LaunchAngle(float value)
    {
        releaseAngle = value * -1;
    }

    public void StartHeight(float value)
    {
        startheight = value;
    }

    
}





    