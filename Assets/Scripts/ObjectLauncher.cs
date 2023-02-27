using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class ObjectLauncher : MonoBehaviour
{

    [SerializeField] private GameObject projectile;

    [Header("Launch variables")]
    public float bulletSpeed;
    public float releaseAngle;
    public Rigidbody projectileRB;
    public Vector3 startheight;
    public float projectileHeight;
    public float projectileMaxHeight;

    //Private variables
    private bool projectileLaunched = false;

    //Properties
    private static ObjectLauncher objectLauncher;
    public static ObjectLauncher GetObjectLauncher
    {
        get { return objectLauncher; }
    }
    public GameObject GetProjectile
    {
        get { return projectile; }
    }


    private void Start()
    {
        startheight = projectileRB.transform.position;
        ResetPos();
    }
    private void Awake()
    {
        if (objectLauncher != null)
            Destroy(gameObject);
        else
            objectLauncher = this;

        
    }

    private void Update()
    {
        MaxHeight();
    }

    public void Fire()
    {
        //reset pos auto resets and updates the position and start height so dont have to click reset button on update 
        ResetPos();
        projectileRB.constraints = RigidbodyConstraints.None;
        var targetDirn = projectile.transform.forward;
        var elevationAxis = projectile.transform.right;
        var releaseVector = Quaternion.AngleAxis(releaseAngle, elevationAxis) * targetDirn;
        projectileRB.velocity = releaseVector * bulletSpeed;

        //Add two equations here in vector 3
        projectileRB.AddForce(releaseVector);
        projectileLaunched = true;
        Debug.Log("fire in projectile Controller script");
    }

    public void ResetPos(bool hideObject = false)
    {
        projectileRB.constraints = RigidbodyConstraints.FreezePosition;
        projectileRB.velocity = Vector3.zero;
        projectileRB.freezeRotation = true;
        projectile.transform.rotation = Quaternion.identity;
        projectile.transform.position = startheight;
        projectileLaunched = false;

        if (hideObject)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);

        //Debug.Log("reset in projectileController script");

        //ResetHorizonalHeight
        projectileHeight = 0;
        projectileMaxHeight = 0;
    }


    //not used function
    public void Freeze()
    {
        projectileRB.constraints = RigidbodyConstraints.FreezePosition;
        projectileRB.freezeRotation = true;
        projectileRB.velocity = Vector3.zero;
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
        startheight.y = value;

        if (projectileLaunched)
            return;

        projectile.transform.position = startheight;
    }

    public void UpdateProjectileMass(float value)
    {
        if (projectileLaunched)
            return;

        projectileRB.mass = value;
    }

    public void UpdateProjectileGravity(float value)
    {
        if (projectileLaunched)
            return;

        //projectileRB = value;
    }

    public void MaxHeight()
    {
        projectileHeight = projectile.transform.position.y;
        if (projectileHeight > projectileMaxHeight)
        {
            projectileMaxHeight = projectileHeight;
        }
        MenuController.GetMenuController.SetMaxHeightText(projectileMaxHeight - startheight.y);
    }
}

