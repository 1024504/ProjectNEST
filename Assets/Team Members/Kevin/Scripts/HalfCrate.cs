using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfCrate : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
