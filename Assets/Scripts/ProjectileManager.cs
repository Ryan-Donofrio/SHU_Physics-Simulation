using UnityEngine;
using System.Collections.Generic;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int maxNumberOfProjectiles = 5;

    private List<ProjectileController> projectiles = new List<ProjectileController>();
    private int lastFired = 0;

    private void Start()
    {
        for(int i = 0; i < maxNumberOfProjectiles; i++)
        {
            var newObj = Instantiate(projectilePrefab);
            projectiles.Add(newObj.GetComponent<ProjectileController>());
            projectiles[i].ResetPos(true);
        }
    }

    public void LaunchNewProjectile(bool holdFire = false)
    {
        for(int i = 0; i < maxNumberOfProjectiles; i++)
        {
            if (!projectiles[i].gameObject.activeSelf)
            {
                projectiles[i].gameObject.SetActive(true);
                projectiles[i].Fire();
                CycleThroughLastFired();
                return;
            }
            else if (i == maxNumberOfProjectiles - 1 && projectiles[i].gameObject.activeSelf)
            {
                if (!holdFire)
                {
                    projectiles[lastFired].Fire();
                    CycleThroughLastFired();
                }
                else
                {
                    projectiles[lastFired].ResetPos();
                }
            }
        }
    }

    public void ResetAllProjectiles()
    {
        foreach(ProjectileController pc in projectiles)
        {
            pc.ResetPos(true);
            CycleThroughLastFired(true);
        }
    }

    private void CycleThroughLastFired(bool reset = false)
    {
        if (reset)
        {
            lastFired = 0;
            return;
        }

        lastFired++;

        if (lastFired == maxNumberOfProjectiles)
            lastFired = 0;
    }
}
