using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Medi;

public class StarSpawner : MonoBehaviour
{
    public GameObject starPrefab;
    public int numStars = 500;
    public float distance = 1000f;
    public float distVariance = 100f;
    public float radius = 10f;

    private List<GameObject> spawned = new List<GameObject>();

    private void OnEnable()
    {
        for (int i = 0; i < numStars; ++i)
        {
            Vector3 dir = Random.onUnitSphere;
            dir = dir.WithY(Mathf.Abs(dir.y)).normalized;  // only upward;
            Vector3 pos = transform.position + (dir * Random.Range(distance - distVariance, distance + distVariance));

            GameObject star = Instantiate(starPrefab);
            star.transform.parent = transform;
            star.transform.position = pos;
            star.transform.localScale = Vector3.one * radius;
            spawned.Add(star);
        }
    }

    private void OnDisable()
    {
        if (!App.Cycle.IsStopped)
        {
            foreach (GameObject star in spawned)
            {
                Destroy(star);
            }
        }
    }
}
