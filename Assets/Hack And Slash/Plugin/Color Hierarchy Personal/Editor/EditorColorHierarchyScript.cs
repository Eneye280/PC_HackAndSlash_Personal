#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HierarchyColor))]
public class EditorColorHierarchyScript : Editor
{
    private string[] tabs = { "Active", "Inactive" };
    private int tabsSelected = 0;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
        tabsSelected = GUILayout.Toolbar(tabsSelected, tabs);
        EditorGUILayout.EndVertical();

        if(tabsSelected >= 0)
        {
            serializedObject.Update();
            switch (tabs[tabsSelected])
            {
                case "Active":

                    GUILayout.BeginHorizontal("Box");
                    EditorGUILayout.HelpBox("1. SCRIPT: BLUE / 2. ENVIROMENT: PURPLE / 3. CM: GREEN / 4. SETTINGS: ORANGE / 5. UI: YELLOW", MessageType.Info);
                    GUILayout.EndHorizontal();

                    EditorGUILayout.PropertyField(serializedObject.FindProperty("textColor"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("textStyle"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("backgroundColor"));
                    break;
                case "Inactive":
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("customInactiveColor"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("textColorInactive"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("textStyleInactive"));
                    break;
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
} 
#endif
