using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private bool isVisibleCursor;

    private void Awake()
    {
        Cursor.visible = isVisibleCursor;
    }
}
