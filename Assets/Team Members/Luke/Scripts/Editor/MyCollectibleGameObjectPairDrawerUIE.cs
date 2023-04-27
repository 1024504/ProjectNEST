using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MyCollectibleGameObjectPair))]
public class MyCollectibleGameObjectPairDrawerUIE : PropertyDrawer
{
	// Draw the property inside the given rect
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		// Using BeginProperty / EndProperty on the parent property means that
		// prefab override logic works on the entire property.
		EditorGUI.BeginProperty(position, label, property);

		// Draw label
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		// Don't make child fields be indented
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		// Calculate rects
		var collectibleRect = new Rect(position.x, position.y, position.width/3, position.height);
		var gameObjectRect = new Rect(position.x + position.width/3, position.y, position.width*2/3, position.height);

		// Draw fields - pass GUIContent.none to each so they are drawn without labels
		EditorGUI.PropertyField(collectibleRect, property.FindPropertyRelative("collectible"), GUIContent.none);
		EditorGUI.PropertyField(gameObjectRect, property.FindPropertyRelative("gameObject"), GUIContent.none);

		// Set indent back to what it was
		EditorGUI.indentLevel = indent;

		EditorGUI.EndProperty();
	}
}

