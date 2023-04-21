using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateProximityDetector : MonoBehaviour
{
    private void OnCollisionEnter2D( Collision2D col )
    {
        Player otherPlayer = col.gameObject.GetComponent<Player>();
        if ( otherPlayer!=null && this.GetComponentInParent<CrateScript>().isLoaded == 1 )
        {
            this.GetComponentInParent<CrateScript>().Destroyed();
        }
    }
}
