using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnselectingButton : UnityEngine.UI.Button
{
	public override void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter(eventData);
		EventSystem.current.SetSelectedGameObject(gameObject);
	}
}
