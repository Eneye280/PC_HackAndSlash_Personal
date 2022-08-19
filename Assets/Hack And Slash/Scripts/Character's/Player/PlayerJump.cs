using System;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    internal static event Action<bool> OnAnimationJump;

    private ManagerInput managerInput;
    private Rigidbody rigidbodyPlayer;

    private void Start()
    {
        managerInput = GetComponent<ManagerInput>();
        rigidbodyPlayer = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        OnAnimationJump?.Invoke(managerInput.isJump);
    }
}
