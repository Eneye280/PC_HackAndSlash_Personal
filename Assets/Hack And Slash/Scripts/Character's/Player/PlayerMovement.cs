using UnityEngine;
using System;

public class PlayerMovement : Humanoid
{
    private Action<Vector3> OnMovementDirection { get; set; }
    internal static event Action<Vector3> OnAddAnimationMovement;

    [Range(-200,200)]
    [SerializeField] internal float speedRotation;

    [Space]
    [Range(0, 200)]
    [SerializeField] internal float desiredRotationAngle = 0;

    internal Vector3 movement;
    internal Vector3 direction;


    private void OnEnable()
    {
        OnMovementDirection += HandleMovementDirection;
    }

    private void FixedUpdate()
    {
        Movement();

        GetMovementDirection();

        if (movement.magnitude > 0)
        {
            RotateAgent();
        }
    }

    public override void Movement()
    {
        float h = direction.x;
        float v = direction.y;

        movement = new Vector3();
        movement.Set(h, 0, v);
        transform.Translate(movement.normalized * speedMovement * Time.deltaTime);

        OnAddAnimationMovement?.Invoke(movement);
    }


    private void GetMovementDirection()
    {
        var cameraForewardDIrection = Camera.main.transform.forward;
        var directionToMoveIn = Vector3.Scale(cameraForewardDIrection, (Vector3.right + Vector3.forward));

        OnMovementDirection?.Invoke(directionToMoveIn.normalized);
    }

    public void HandleMovementDirection(Vector3 direction)
    {
        desiredRotationAngle = Vector3.Angle(transform.forward, direction);
        var crossProduct = Vector3.Cross(transform.forward, direction).y;

        if (crossProduct < 0)
        {
            desiredRotationAngle *= -1;
        }
    }

    private void RotateAgent()
    {
        if (desiredRotationAngle > 10 || desiredRotationAngle < -10)
        {
            transform.Rotate(Vector3.up * desiredRotationAngle * speedRotation * Time.deltaTime);
        }
    }

    private void OnDisable()
    {
        OnMovementDirection -= HandleMovementDirection;
    }
}
