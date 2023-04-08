using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
	private Camera _cam;
	private Transform _cameraTransform;
	public Transform playerTransform;
	private Transform _playerReticleTransform;
	private BoxCollider2D _collider;
	public SpriteRenderer fadeSprite;
	public float fadeDuration = 1f;
	public Action OnFadeOutComplete;
	public Action OnFadeInComplete;

	[Header("Follow Variables")]
	public float f = 1f;
	public float zeta = 1f;
	public float r = 0f;
	
	private Vector3 _nextPosition = Vector3.zero;
	private Vector3 _currentVelocity = Vector3.zero;
	private Vector3 _playerVelocity = Vector3.zero;
	private Vector3 _playerPrevPosition = Vector3.zero;

	[Range(0,5)]
	public float cameraSpeed = 1f;
	[SerializeField] private float cameraSize = 10f;

	private Coroutine _coroutine;
	
	public float CameraSize
	{
		get => cameraSize;
		set
		{
			cameraSize = value;
			if (_coroutine != null) StopCoroutine(_coroutine);
			_coroutine = StartCoroutine(ChangeCameraSize(cameraSize));
		}
	}

	private IEnumerator ChangeCameraSize(float newSize)
	{
		while (Mathf.Abs(_cam.orthographicSize - newSize) > 0.01f)
		{
			_cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, newSize, 0.1f);
			UpdateCollider();
			yield return new WaitForEndOfFrame();
		}
		
		_cam.orthographicSize = newSize;
		UpdateCollider();
	}
	
	private void OnEnable()
	{
		DontDestroyOnLoad(gameObject);
		_cam = GetComponent<Camera>();
	    _cameraTransform = transform;
	    _cam.orthographicSize = cameraSize;
	    _collider = GetComponent<BoxCollider2D>();
	    Player player = playerTransform.GetComponent<Player>();
	    player.cameraTransform = _cameraTransform;
	    _playerReticleTransform = player.lookTransform;
	    UpdateCollider();
	    _playerPrevPosition = playerTransform.position+Vector3.back;
    }
	
	private Vector3 NextStep(float timeStep, Vector3 x, Vector3 xd)
	{
		float constant1 = zeta / (Mathf.PI * f);
		float constant2Stable = Mathf.Max(1 / (2*Mathf.PI * f * 2*Mathf.PI * f), 1.1f * (timeStep*timeStep/4+timeStep*constant1/2));
		_nextPosition = _cameraTransform.position + timeStep * _currentVelocity;
		_currentVelocity += timeStep * (x + r *zeta / (2*Mathf.PI * f) * xd - _nextPosition - constant1 * _currentVelocity) / constant2Stable;
		return _nextPosition;
	}

	public void FixedUpdate()
	{
		Vector3 position = playerTransform.position+_playerReticleTransform.localPosition/3f+Vector3.up*3; // Uncomment for the camera to track between the player and the reticle.
		_cameraTransform.position = NextStep(Time.fixedDeltaTime, position+Vector3.back, _playerVelocity);
		_playerVelocity = (position+Vector3.back - _playerPrevPosition) / Time.fixedDeltaTime;
		_playerPrevPosition = position+Vector3.back;
	}

    public void UpdateCollider()
    {
	    _collider.size = new Vector2(2*_cam.orthographicSize*_cam.aspect, 2*_cam.orthographicSize);
	    if(playerTransform != null) playerTransform.GetComponent<Player>().cameraSize = _collider.size;
    }
    
    public void FadeIn()
	{
	    fadeSprite.transform.localScale = new Vector3(2*_cam.orthographicSize*_cam.aspect, 2*_cam.orthographicSize,1);
	    StartCoroutine(FadeInCoroutine());
	}
    
    private IEnumerator FadeInCoroutine()
	{
		float counter = 0;
		while (counter < fadeDuration)
		{
			counter += Time.deltaTime;
			fadeSprite.color = new Color(0,0,0,1-counter/fadeDuration);
			yield return new WaitForEndOfFrame();
		}
		fadeSprite.color = new Color(0,0,0,0);
		OnFadeInComplete?.Invoke();
	}

    public void FadeOut()
    {
	    fadeSprite.transform.localScale = new Vector3(2*_cam.orthographicSize*_cam.aspect, 2*_cam.orthographicSize,1);
	    StartCoroutine(FadeOutCoroutine());
    }
    
    private IEnumerator FadeOutCoroutine()
    {
	    float counter = 0;
	    while (counter < fadeDuration)
	    {
		    counter += Time.deltaTime;
		    fadeSprite.color = new Color(0,0,0,counter/fadeDuration);
		    yield return new WaitForEndOfFrame();
	    }
	    fadeSprite.color = new Color(0,0,0,1);
	    OnFadeOutComplete?.Invoke();
	}
}
