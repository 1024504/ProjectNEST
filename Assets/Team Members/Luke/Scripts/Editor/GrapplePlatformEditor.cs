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

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (!_target.movablePlatform) return;

        EditorGUI.BeginChangeCheck();

        Vector3 newSecondPosition = Handles.PositionHandle(_target.transform.position+_target.secondPosition, Quaternion.identity);
        
        if (!EditorGUI.EndChangeCheck()) return;
        Undo.RecordObject(_target, "Change Second Position");
        _target.secondPosition = newSecondPosition;
    }
}
