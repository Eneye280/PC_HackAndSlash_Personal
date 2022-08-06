using UnityEngine;

public class ManagerInput : MonoBehaviour
{
    private InputPlayer controls;
    private PlayerMovement movementPlayer;

    private void Awake()
    {
        controls = new InputPlayer();

        movementPlayer = GetComponent<PlayerMovement>();

        controls.Player.Movement.performed += ctx => movementPlayer.direction = ctx.ReadValue<Vector2>();
        controls.Player.Jump.performed += ctx => movementPlayer.Jump();
    }

    private void OnDisable() => controls.Disable();
    private void OnEnable() => controls.Enable();
}
