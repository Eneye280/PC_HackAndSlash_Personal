using UnityEngine;
using Cinemachine;
using System;

public class MovementPlayer : Humanoid
{
    [SerializeField] internal float speedRotation;
    [SerializeField] internal Vector3 movement;
    [SerializeField] internal Vector3 direction;

    private float desiredRotationAngle = 0;

    private Action<Vector3> OnMovementDirection { get; set; }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        OnMovementDirection += HandleMovementDirection;
    }

    private void Update()
    {
        GetMovementDirection();

        if (movement.magnitude > 0)
        {
            RotateAgent();
        }
    }

    private void FixedUpdate()
    {
        Movement();
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

    private void OnDisable()
    {
        OnMovementDirection -= HandleMovementDirection;
    }
}
