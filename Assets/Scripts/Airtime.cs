using UnityEngine;
using Game;

public class Airtime : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rock;
    public float starttime;
    public float endtime;
    public float time;
    public ProjectileController objL;
    public bool firstCollision;


    public void starttimmer()
    {
        starttime += Time.time;
        firstCollision = false;
    }

    public void endtimer()
    {
        endtime += Time.time;
        time = endtime - starttime;
        firstCollision = true;
        MenuController.GetMenuController.SetAirtimeText(time.ToString("F2"));

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (firstCollision == false)
        {
            endtimer();
            //objL.Freeze();
        }
    }

    public void resettimer()
    {
        time = 0;
        starttime = 0;
        endtime = 0;
    }
}