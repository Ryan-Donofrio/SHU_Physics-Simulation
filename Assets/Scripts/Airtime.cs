using UnityEngine;
using Game;

public class Airtime : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject rock;
    private float starttime;
    private float endtime;
    private float time;
    //public ProjectileController objL;
    private bool firstCollision;
    private float startzpos;
    private float endzpos;
    private float zdistance;
    public GameObject menuController;

    private void Start()
    {
        startzpos = rock.transform.position.z;
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
        MenuController.GetMenuController.SetAirtimeText(time.ToString());

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
        endzpos = rock.transform.position.z;
        zdistance = endzpos - startzpos;
        menuController.GetComponent<MenuController>().SetHorisontalDistance(zdistance);
    }

    public void resettimer()
    {
        time = 0;
        starttime = 0;
        endtime = 0;
    }
}