using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class AssetHandler
{
    [OnOpenAsset()]
    public static bool OpenEditor(int instanceId, int line)
    {
        PatternDataBase obj = EditorUtility.InstanceIDToObject(instanceId) as PatternDataBase;
        if(obj != null)
        {
            return true;
        }
        return false;
    }
}

[CustomEditor(typeof(PatternDataBase))]
public class PatternEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Open Editor"))
        {
            PatternEditorWindow.Open((PatternDataBase)target);
        }
    }
}
