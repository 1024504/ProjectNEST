using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(GrapplePlatform))]
public class GrapplePlatformEditor : Editor
{
    private GrapplePlatform _target;

    private void OnEnable()
    {
        _target = (GrapplePlatform) target;
    }

    private void OnSceneGUI()
    {
	    if (!_target.movablePlatform) return;

	    EditorGUI.BeginChangeCheck();

	    Vector3 newSecondPosition;
	    
	    if (Application.isPlaying) newSecondPosition = Handles.PositionHandle(_target.firstPositionWorld+_target.secondPositionLocal,Quaternion.identity);
	    else newSecondPosition = Handles.PositionHandle(_target.transform.position+_target.secondPositionLocal,Quaternion.identity);
        
	    if (!EditorGUI.EndChangeCheck()) return;
	    Undo.RecordObject(_target, "Change Second Position");
	    _target.secondPositionLocal = newSecondPosition-_target.transform.position;
    }
}
