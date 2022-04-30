#if UNITY_EDITOR
using System.Linq;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class EditorColorHierarchy
{
    public static readonly Color DEFAULT_COLOR_HIERARCHY_SELECTED = new Color(0.243f, 0.4901f, 0.9058f, 1f);

    static EditorColorHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI -= HierarchyHighlight_OnGUI;
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyHighlight_OnGUI;
    }

    private static void HierarchyHighlight_OnGUI(int inSelectionID, Rect inSelectionRect)
    {
        GameObject GO_Label = EditorUtility.InstanceIDToObject(inSelectionID) as GameObject;

        if (GO_Label != null)
        {
            HierarchyColor Label = GO_Label.GetComponent<HierarchyColor>();

            if (Label != null && Event.current.type == EventType.Repaint)
            {
#region Determine Styling

                bool ObjectIsSelected = Selection.instanceIDs.Contains(inSelectionID);

                Color _backgroundColor = Label.backgroundColor;
                Color _textColor = Label.textColor;
                FontStyle _textStyle = Label.textStyle;

                if (!Label.isActiveAndEnabled)
                {
                    if (Label.customInactiveColor)
                    {
                        _backgroundColor = Label.backgroundColorInactive;
                        _textColor = Label.textColorInactive;
                        _textStyle = Label.textStyleInactive;
                    }
                    else
                    {
                        if (_backgroundColor != HierarchyColor.DEFAULT_BACKGROUND_COLOR)
                            _backgroundColor.a = _backgroundColor.a * 0.5f;

                        _textColor.a = _textColor.a * 0.5f;
                    }
                }

#endregion

                Rect offset = new Rect(inSelectionRect.position + new Vector2(2f, 0f), inSelectionRect.size);

#region Draw Background
                if (_backgroundColor.a > 0f)
                {
                    Rect backgroundOffset = new Rect(inSelectionRect.position, inSelectionRect.size);

                    if (Label.backgroundColor.a < 1f || ObjectIsSelected)
                    {
                        EditorGUI.DrawRect(backgroundOffset, HierarchyColor.DEFAULT_BACKGROUND_COLOR);
                    }

                    if (ObjectIsSelected)
                        EditorGUI.DrawRect(backgroundOffset, Color.Lerp(GUI.skin.settings.selectionColor, _backgroundColor, 0.3f));
                    else
                        EditorGUI.DrawRect(backgroundOffset, _backgroundColor);
                }

#endregion

                EditorGUI.LabelField(offset, GO_Label.name, new GUIStyle()
                {
                    normal = new GUIStyleState() { textColor = _textColor },
                    fontStyle = _textStyle
                });

                EditorApplication.RepaintHierarchyWindow();
            }
        }
    }
} 
#endif