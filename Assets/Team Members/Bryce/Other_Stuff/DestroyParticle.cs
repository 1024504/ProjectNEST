using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
  public void awake()
    {
        Destroy(gameObject, 2f);
    }
}
