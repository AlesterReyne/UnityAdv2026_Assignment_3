using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FireHazardScriptableObject))]
public class OurCoolInspector : Editor
{
    [SerializeField] private string helpBoxMessage = "Some help box";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.HelpBox(helpBoxMessage, MessageType.Info);
    }
}