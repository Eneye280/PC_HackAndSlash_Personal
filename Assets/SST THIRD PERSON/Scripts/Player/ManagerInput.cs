using UnityEngine;

public class ManagerInput : MonoBehaviour
{
    private InputPlayer controls;
    private MovementPlayer movementPlayer;

    private void Awake()
    {
        controls = new InputPlayer();

        movementPlayer = GetComponent<MovementPlayer>();

        controls.Player.Movement.performed += ctx => movementPlayer.direction = ctx.ReadValue<Vector2>();
        controls.Player.Jump.performed += ctx => movementPlayer.Jump();
    }

    private void OnDisable() => controls.Disable();
    private void OnEnable() => controls.Enable();
}
