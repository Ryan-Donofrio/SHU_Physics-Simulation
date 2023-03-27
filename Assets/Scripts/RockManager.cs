using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class RockManager : MonoBehaviour
{
    private float starttime;
    private float endtime;
    private float time;
    private bool firstCollision;
    private float startzpos;
    private float endzpos;
    private float zdistance;
    public MenuController menuController;


    [Header("Object Launch Script Var")]
    [Header("Launch variables")]
    public float bulletSpeed;
    public float releaseAngle;
    private Vector3 startheight;
    public float projectileHeight;
    public float projectileMaxHeight;
    private bool projectileLaunched = false;
    private Rigidbody rock;

   

    void Start()
    {
        startzpos = transform.position.z;
        startheight = transform.position;
        rock = GetComponent<Rigidbody>();
        ResetPos();

    }

    private void Update()
    {
        MaxHeight();
    }

    public void StartTimer()
    {
        starttime += Time.time;
        Debug.Log("Start Timer " + starttime);
        firstCollision = false;
    }

    public void endtimer()
    {
        endtime += Time.time;
        time = endtime - starttime;
        firstCollision = true;
        menuController.SetAirtimeText(time.ToString());

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (firstCollision == false)
        {
            endtimer();
            HorizontalDistance();
        }
    }

    public void HorizontalDistance()
    {
        endzpos = transform.position.z;
        zdistance = endzpos - startzpos;
        menuController.SetHorisontalDistance(zdistance);
    }

    public void resettimer()
    {
        time = 0;
        starttime = 0;
        endtime = 0;
    }


    // OBJECT LAUNCHER SCIPT

    public void Fire()
    {
        ResetPos();
        
        rock.constraints = RigidbodyConstraints.None;
        var targetDirn = transform.forward;
        var elevationAxis = transform.right;
        var releaseVector = Quaternion.AngleAxis(releaseAngle, elevationAxis) * targetDirn;
        rock.velocity = releaseVector * bulletSpeed;

        rock.AddForce(releaseVector);
        projectileLaunched = true;
        Debug.Log("fire in projectile Controller script");
    }

    public void ResetPos(bool hideObject = false)
    {
        rock.constraints = RigidbodyConstraints.FreezePosition;
        rock.velocity = Vector3.zero;
        rock.freezeRotation = true;
        transform.rotation = Quaternion.identity;
        transform.position = startheight;
        projectileLaunched = false;


        projectileHeight = 0;
        projectileMaxHeight = 0;
    }


    //not used function
    public void Freeze()
    {
        rock.constraints = RigidbodyConstraints.FreezePosition;
        rock.freezeRotation = true;
        rock.velocity = Vector3.zero;
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

        transform.position = startheight;
    }

    public void UpdateProjectileMass(float value)
    {
        if (projectileLaunched)
            return;

        rock.mass = value;
    }

    public void UpdateProjectileGravity(float value)
    {
        if (projectileLaunched)
            return;

        //projectileRB = value;
    }

    public void MaxHeight()
    {
        projectileHeight = transform.position.y;
        if (projectileHeight > projectileMaxHeight)
        {
            projectileMaxHeight = projectileHeight;
        }
        menuController.SetMaxHeightText(projectileMaxHeight - startheight.y);
    }
}
