using System;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    internal static event Action<bool> OnAnimationJump;

    private ManagerInput managerInput;

    private void Start()
    {
        managerInput = GetComponent<ManagerInput>();
    }

    private void Update()
    {
        OnAnimationJump?.Invoke(managerInput.isJump);
    }
}
