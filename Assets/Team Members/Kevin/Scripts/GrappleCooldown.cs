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
    private Player _player;
    
    public void OnEnable()
    {
	    if (_player != null) SetPlayer();
    }

    public void SetPlayer()
    {
	    _player = UIManager.Instance.player;
	    _player.OnGrappleHit += UpdateGrappleCooldown;
    }

    public void OnDisable()
    {
        if (_player != null) _player.OnGrappleHit -= UpdateGrappleCooldown;
    }

    public void Update()
    {
	    if (_player == null) return;
        if (_player._isGrappled)
        {
            grappleSlider.value = 5f;
        }
        else if (!_player._isGrappled && _player._terrainDetection.isGrounded)
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
