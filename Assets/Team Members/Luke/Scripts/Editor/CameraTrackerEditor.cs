using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(CameraTracker))]
public class CameraTrackerEditor : Editor
{
    private CameraTracker _agent;

	private void OnEnable()
	{
		_agent = (CameraTracker) target;
	}

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Resize Collider"))
        {
            _agent.UpdateCollider();
        }
    }
}
