using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SimpleEnemyAITest : MonoBehaviour
{
    public bool targetDestinationSet;
    public Vector3 targetDestination;
    public GameObject aiPrefab;
    public int patrolFrequency;
    
    public void Start()
    {
        SetDestination();
    }

    private void SetDestination()
    {
        if (targetDestinationSet == false)
        {
            targetDestination = transform.position + new Vector3(Random.Range(-10f, 10f),0f,0f);
            targetDestinationSet = true;
            GoAI();
        }
    }

    public void Update()
    {
        aiPrefab.transform.position = Vector3.MoveTowards(transform.position, targetDestination, 0.01f);
    }
    private void GoAI()
    {
        StartCoroutine(NextDestination());
    }

    private IEnumerator NextDestination()
    {
        yield return new WaitForSeconds(patrolFrequency);
        targetDestinationSet = false;
        SetDestination();
    }
}
