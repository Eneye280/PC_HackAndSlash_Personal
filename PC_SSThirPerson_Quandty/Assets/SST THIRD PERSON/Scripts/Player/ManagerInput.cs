using UnityEngine;

public class ManagerInput : MonoBehaviour
{
    private InputPlayer controls;

    [SerializeField] internal MovementPlayer movementPlayer;

    private void Awake()
    {
        controls = new InputPlayer();

        movementPlayer = GetComponent<MovementPlayer>();

        controls.Player.Movement.performed += ctx => movementPlayer.direction = ctx.ReadValue<Vector2>();
    }

    private void OnDisable() => controls.Disable();
    private void OnEnable() => controls.Enable();
}
