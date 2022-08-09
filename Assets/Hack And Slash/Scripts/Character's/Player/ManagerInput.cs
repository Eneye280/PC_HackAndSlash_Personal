using UnityEngine;
using UnityEngine.InputSystem;

public class ManagerInput : MonoBehaviour
{
    [Header("Character Input Values")]
    [SerializeField] internal Vector2 movementAgent;
    [SerializeField] internal Vector2 lockAgent;
    [SerializeField] internal bool sprint;

    [Header("Movement Settings")]
    [SerializeField] internal bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    public void OnMovement(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            LookInput(value.Get<Vector2>());
        }
    }

    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }

    public void MoveInput(Vector2 newMoveDirection) => movementAgent = newMoveDirection;
    public void SprintInput(bool isActive) => sprint = isActive;
    public void LookInput(Vector2 newLookDirection) => lockAgent = newLookDirection;
    private void SetCursorState(bool isActive)
    {
        Cursor.lockState = isActive ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }
}
