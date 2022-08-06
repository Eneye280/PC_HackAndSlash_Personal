#if UNITY_EDITOR
using UnityEngine;

public class HierarchyColor : MonoBehaviour
{
    public static readonly Color DEFAULT_BACKGROUND_COLOR = new Color(0.76f, 0.76f, 0.76f, 1f);

    public static readonly Color DEFAULT_BACKGROUND_COLOR_INACTIVE = new Color(0.306f, 0.396f, 0.612f, 1f);

    public static readonly Color DEFAULT_TEXT_COLOR = Color.black;

    [Header("Active State")]
    public Color textColor = Color.white;
    public FontStyle textStyle = FontStyle.Bold;
    public Color backgroundColor = Color.black;

    [Header("Inactive State")]
    public bool customInactiveColor = true;
    public Color textColorInactive = DEFAULT_TEXT_COLOR;
    public FontStyle textStyleInactive = FontStyle.Bold;
    public Color backgroundColorInactive = DEFAULT_BACKGROUND_COLOR_INACTIVE;

    public HierarchyColor() { }

    public HierarchyColor(Color inBackgroundColor) => backgroundColor = inBackgroundColor;

    public HierarchyColor(Color inBackgroundColor, Color inTextColor, FontStyle inFontStyle = FontStyle.Normal)
    {
        backgroundColor = inBackgroundColor;
        textColor = inTextColor;
        textStyle = inFontStyle;
    }
}

#endif