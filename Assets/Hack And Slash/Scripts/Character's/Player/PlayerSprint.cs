using System;
using UnityEngine;

public class PlayerSprint : MonoBehaviour
{
    internal static event Action<bool> OnAnimationSprint;

    private ManagerInput managerInput;

    private void Start()
    {
        managerInput = GetComponent<ManagerInput>();
    }

    private void Update()
    {
        OnAnimationSprint?.Invoke(managerInput.isSprint);
    }
}
