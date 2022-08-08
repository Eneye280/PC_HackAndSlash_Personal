using UnityEngine;
using System;

public class PlayerMovement : HumanoidMovement
{
    private Action<Vector3> OnMovementDirection { get; set; }
    internal static event Action<Vector3> OnAddAnimationMovement;

    internal float desiredRotationAngle;
    internal Vector3 movement;
    internal Vector3 direction;

    private void OnEnable()
    {
        OnMovementDirection += HandleMovementDirection;
    }

    private void FixedUpdate()
    {
        MovementAgent();
        GetMovementDirectionAgent();
        RotateAgent();   
    }

    public override void MovementAgent()
    {
        float movementHorizontal = direction.x;
        float movementVertical = direction.y;

        movement.Set(movementHorizontal, 0, movementVertical);
        transform.Translate(movement.normalized * AddSpeedAgent());

        OnAddAnimationMovement?.Invoke(movement);

        if (movement.z != 0) 
            CheckMovementAgent(true);
        else 
            CheckMovementAgent(false);
    }

    private bool CheckMovementAgent(bool isActive) => isMovement = isActive;
    private float AddSpeedAgent()
    {
        float resultSpeedAgent = Time.deltaTime * speedMovement;
        return resultSpeedAgent;
    }

    private void GetMovementDirectionAgent()
    {
        Vector3 cameraForewardDIrection = Camera.main.transform.forward;
        Vector3 directionToMoveIn = Vector3.Scale(cameraForewardDIrection, (Vector3.right + Vector3.forward));

        OnMovementDirection?.Invoke(directionToMoveIn.normalized);
    }

    private void HandleMovementDirection(Vector3 direction)
    {
        desiredRotationAngle = Vector3.Angle(transform.forward, direction);
        float crossProduct = Vector3.Cross(transform.forward, direction).y;

        if (crossProduct < 0)
            desiredRotationAngle *= -1;
    }

    private void RotateAgent()
    {
        if (movement.magnitude > 0)
        {
            if (desiredRotationAngle > 10 || desiredRotationAngle < -10)
                transform.Rotate(Vector3.up * desiredRotationAngle * speedRotation * Time.deltaTime); 
        }
    }

    private void OnDisable()
    {
        OnMovementDirection -= HandleMovementDirection;
    }
}
