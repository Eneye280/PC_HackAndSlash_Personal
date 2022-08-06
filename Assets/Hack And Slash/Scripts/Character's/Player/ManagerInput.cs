using System;
using UnityEngine;

public class ManagerInput : MonoBehaviour
{
    internal static event Action OnJumpPlayer;

    private InputPlayer controls;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        controls = new InputPlayer();

        playerMovement = GetComponent<PlayerMovement>();

        Inputs();
    }

    private void Inputs()
    {
        controls.Player.Movement.performed += ctx => playerMovement.direction = ctx.ReadValue<Vector2>();
        controls.Player.Jump.performed += ctx => OnJumpPlayer?.Invoke();
    }

    private void OnDisable() => controls.Disable();
    private void OnEnable() => controls.Enable();
}
