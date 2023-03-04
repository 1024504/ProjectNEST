using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject alveriumPrefab;

    public int spawnAmount;

    public List<Transform> spawnPosition;

    [SerializeField] private float timeCount;
    public float spawnTimer;

    public int spawnLimit;

    public int alveriumCount;
    
    public void Update()
    {
        timeCount += Time.deltaTime;
        if (timeCount >= spawnTimer)
        {
            SpawnAvlerium();
        }
    }
    private void SpawnAvlerium()
    {
        if (alveriumCount >= spawnLimit) return;
        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(alveriumPrefab, spawnPosition[i].position, Quaternion.identity);
            alveriumCount++;
        }

        timeCount = 0f;
    }
}
