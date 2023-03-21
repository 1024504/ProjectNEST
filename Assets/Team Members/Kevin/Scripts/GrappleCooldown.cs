using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public void Update()
    {
        if (player._isGrappled)
        {
            grappleSlider.value = 5f;
        }
        else if (!player._isGrappled && player._terrainDetection.isGrounded)
        {
            grappleSlider.value = 0f;
        }
    }

    private void UpdateGrappleCooldown()
    {
        /*if (player._isGrappled)
        {
            grappleSlider.value = grappleCooldown;
            for (float i = grappleCooldown; i < grappleCooldown; i -= Time.deltaTime)
            {
                grappleSlider.value -= Time.deltaTime;
            }
        }
        else if(player._isGrappled == false)
        {
            grappleSlider.value = 0f;
        }*/

        /*if (player._isGrappled)
        {
            grappleSlider.value = 5f;
        }

        if (player._isGrappled == false)
        {
            grappleSlider.value = 0f;
        }*/
    }
}
