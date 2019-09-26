using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UFOData : MonoBehaviour {
	public float size;
	public Color color;
	public float speed;
}

[CustomEditor(typeof(UFOData))]
[CanEditMultipleObjects]
public class MyDiskEditor: Editor
{
    SerializedProperty size;                             
    SerializedProperty color;                              
    SerializedProperty speed;                              

    void OnEnable()
    {
        size = serializedObject.FindProperty("size");
        color = serializedObject.FindProperty("color");
        speed = serializedObject.FindProperty("speed");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(size);
        EditorGUILayout.PropertyField(color);
        EditorGUILayout.PropertyField(speed);
        serializedObject.ApplyModifiedProperties();
    }
    private void ProgressBar(float value, string label)
    {
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space();
    }
}
