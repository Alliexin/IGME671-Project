using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PatternEditorWindow : ExtendedEditorWindow
{
    Vector2 pos;
    static PatternDataBase p;
    public static void Open(PatternDataBase patternDataBase)
    {
        PatternEditorWindow window = GetWindow<PatternEditorWindow>("Pattern Editor");
        p = patternDataBase;
        window.serializedObject = new SerializedObject(p);
    }

    private void OnGUI()
    {
        currentProperty = serializedObject.FindProperty("patterns");

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("+"))
        {
            p.Add();
            Repaint();
        }

        if (GUILayout.Button("-"))
        {
            p.Remove();
            Repaint();
        }
        EditorGUILayout.EndHorizontal();

        DrawSidebar(currentProperty);

        EditorGUILayout.EndVertical();

        pos = EditorGUILayout.BeginScrollView(pos);
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
        if(selectedProperty != null)
        {
            DrawProperties(selectedProperty, true);
        }
        else
        {
            EditorGUILayout.LabelField("Select an item from the list");
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();

        EditorGUILayout.EndHorizontal();

        Apply();
    }

    protected void Apply()
    {
        serializedObject.ApplyModifiedProperties();
        serializedObject = new SerializedObject(p);
    }
}
