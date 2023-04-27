using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{
    public GameObject mothAlveriumPrefab;
    public float spawnRate;

    public void SpawnMoth()
    {
        GameObject go = Instantiate(mothAlveriumPrefab, transform.position - (Vector3.up * 5), Quaternion.identity);
        go.transform.localScale = Vector3.one / 1.5f;
        go.GetComponent<HealthBase>().HealthLevel = go.GetComponent<HealthBase>().maxHealth / 2f;
        StartCoroutine(NextSpawn());
    }

    private IEnumerator NextSpawn()
    {
        yield return new WaitForSeconds(spawnRate);
        SpawnMoth();
    }

    private void OnDisable()
    {
        StopCoroutine(NextSpawn());
    }
}
