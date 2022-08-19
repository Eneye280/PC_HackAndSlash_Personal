using UnityEngine;
using UnityEngine.InputSystem;

public class ManagerInput : MonoBehaviour
{
    [Header("Character Input Values")]
    [SerializeField] internal Vector2 movementAgent;
    [SerializeField] internal Vector2 lockAgent;
    [SerializeField] internal bool isSprint;
    [SerializeField] internal bool isJump;
    [SerializeField] internal bool isGround;

    [Header("Movement Settings")]
    [SerializeField] internal bool isAnalogMovement;

    [Header("Mouse Cursor Settings")]
    public bool isCursorLocked = true;
    public bool isCursorInputForLook = true;

    public void OnMovement(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        if (isCursorInputForLook)
        {
            LookInput(value.Get<Vector2>());
        }
    }

    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }

    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }

    public void MoveInput(Vector2 newMoveDirection) => movementAgent = newMoveDirection;
    public void SprintInput(bool isActive) => isSprint = isActive;
    public void JumpInput(bool isActive) => isJump = isActive;
    public void LookInput(Vector2 newLookDirection) => lockAgent = newLookDirection;
    private void SetCursorState(bool isActive) => Cursor.lockState = isActive ? CursorLockMode.Locked : CursorLockMode.None;


    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(isCursorLocked);
    }
}
