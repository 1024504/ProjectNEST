using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
	private Player _target;
	
	private void OnEnable()
	{
		_target = (Player) target;
	}

	public override void OnInspectorGUI()
	{
		if (GUILayout.Button("Kill Player"))
		{
			if (Application.isPlaying)
			{
				HealthBase healthBase = _target.GetComponent<HealthBase>();
				healthBase.HealthLevel -= healthBase.HealthLevel;
			}
		}
		
		if (GUILayout.Button("God Mode"))
		{
			if (Application.isPlaying)
			{
				HealthBase healthBase = _target.GetComponent<HealthBase>();
				healthBase.maxHealth = 1000000;
				healthBase.HealthLevel += 1000000;
			}
		}
		
		base.OnInspectorGUI();
	}
}
