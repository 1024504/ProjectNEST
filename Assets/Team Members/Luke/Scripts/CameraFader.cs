using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraFader : MonoBehaviour
{
	private Camera _cam;
	public Image fadeImage;
	public float fadeDuration = 1f;
	public Action OnFadeOutComplete;
	public Action OnFadeInComplete;

	private Coroutine _coroutine;

	private void OnEnable()
	{
		_cam = GetComponent<Camera>();
	}

	public void FadeIn()
	{
		StartCoroutine(FadeInCoroutine());
	}
    
    private IEnumerator FadeInCoroutine()
	{
		float counter = 0;
		while (counter < fadeDuration)
		{
			counter += Time.deltaTime;
			fadeImage.color = new Color(0,0,0,1-counter/fadeDuration);
			yield return new WaitForEndOfFrame();
		}
		fadeImage.color = new Color(0,0,0,0);
		OnFadeInComplete?.Invoke();
	}

    public void FadeOut()
    {
	    fadeImage.transform.localScale = new Vector3(2*_cam.orthographicSize*_cam.aspect, 2*_cam.orthographicSize,1);
	    StartCoroutine(FadeOutCoroutine());
    }
    
    private IEnumerator FadeOutCoroutine()
    {
	    float counter = 0;
	    while (counter < fadeDuration)
	    {
		    counter += Time.deltaTime;
		    fadeImage.color = new Color(0,0,0,counter/fadeDuration);
		    yield return new WaitForEndOfFrame();
	    }
	    fadeImage.color = new Color(0,0,0,1);
	    OnFadeOutComplete?.Invoke();
	}
}
