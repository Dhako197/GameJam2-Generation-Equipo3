using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour
{
    public GameObject projectilePrefab;
    public int poolSize = 10;
    private List<GameObject> pool = new List<GameObject>();

    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject proj = Instantiate(projectilePrefab, transform);
            proj.SetActive(false);
            pool.Add(proj);
        }
    }

    public GameObject GetProjectile()
    {
        foreach (var proj in pool)
        {
            if (!proj.activeInHierarchy)
                return proj;
        }

        // Si todos están ocupados, crear uno nuevo (opcional)
        GameObject newProj = Instantiate(projectilePrefab, transform);
        newProj.SetActive(false);
        pool.Add(newProj);
        return newProj;
    }

    public void DisableAllProjectiles()
    {
        foreach (GameObject bullet in pool)
        {
            if (bullet.activeInHierarchy)
            {
                bullet.SetActive(false);
            }
        }
    }
}
