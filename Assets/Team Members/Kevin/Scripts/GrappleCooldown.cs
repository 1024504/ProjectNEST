using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class GrappleCooldown : MonoBehaviour
{
    public Slider grappleSlider;
    public float grappleCooldown;
    public Player player;
    
    public void OnEnable()
    {
        player.OnGrappleHit += UpdateGrappleCooldown;
    }

    public void OnDisable()
    {
        player.OnGrappleHit -= UpdateGrappleCooldown;
    }

    private void UpdateGrappleCooldown()
    {
        Debug.Log("hithithit");
        for (float i = grappleCooldown; i < grappleCooldown; i -= Time.deltaTime)
        {
            grappleSlider.value -= Time.deltaTime;
        }
    }
}
