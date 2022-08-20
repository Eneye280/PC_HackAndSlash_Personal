using System;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    internal static event Action<bool> OnAnimationJump;

    private ManagerInput managerInput;
    private Rigidbody rigidbodyPlayer;

    [SerializeField] private float forceImpulseJump;

    private void Start()
    {
        managerInput = GetComponent<ManagerInput>();
        rigidbodyPlayer = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        OnAnimationJump?.Invoke(managerInput.isJump);
        Jump();
    }

    private void Jump()
    {
        if (managerInput.isJump)
        {
            rigidbodyPlayer.AddForce(Vector3.up * forceImpulseJump, ForceMode.Impulse);
            Debug.Log("is Jump");
        }
    }
}
