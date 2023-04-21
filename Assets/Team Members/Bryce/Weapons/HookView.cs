using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class HookView : MonoBehaviour
{
	//Hook Reference
    private Hook _hook;
    public GameObject _HookView;
    
    //Audio References
    public FMODUnity.EventReference grappleShoot;
    public FMODUnity.EventReference grappleHit;
    
    //Sprite References
    public Sprite hookOpen;
    public Sprite hookClosed;
    private SpriteRenderer currentSprite;

    //Grapple Hook States
    private void OnEnable()
    {
	    currentSprite = _HookView.GetComponent<SpriteRenderer>();
        _hook = GetComponentInParent<Hook>();
        _hook.OnHit += Hit;
        Shoot();
    }

    void Shoot()
    {
	    currentSprite.sprite = hookOpen;
    }

    void Hit(Transform target)
    {
	    currentSprite.sprite = hookClosed;
    }
}
