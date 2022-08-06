using UnityEngine;
using Cinemachine;
using System;

public class MovementPlayer : Humanoid
{
    [Range(-200,200)]
    [SerializeField] internal float speedRotation;

    internal Vector3 movement;
    internal Vector3 direction;

    [SerializeField] internal float desiredRotationAngle = 0;

    private Action<Vector3> OnMovementDirection { get; set; }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

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
        transform.Translate(movement.normalized * speed * Time.deltaTime);

        if (anim == null)
            return;

        anim.SetFloat("vertical", direction.y);
        anim.SetFloat("horizontal", direction.x);

    }
    private void GetMovementDirection()
    {
        var cameraForewardDIrection = Camera.main.transform.forward;
        Debug.DrawRay(Camera.main.transform.position, cameraForewardDIrection * 10, Color.red);

        var directionToMoveIn = Vector3.Scale(cameraForewardDIrection, (Vector3.right + Vector3.forward));
        Debug.DrawRay(Camera.main.transform.position, directionToMoveIn * 10, Color.blue);

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

    public override void Jump()
    {
        anim.SetTrigger("isJump");
    }

    private void OnDisable()
    {
        OnMovementDirection -= HandleMovementDirection;
    }
}
