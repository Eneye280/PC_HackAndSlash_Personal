using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    internal static event Action<float, float, float> OnAnimationBlend;

    [SerializeField] private Camera mainCamera;

    [Header("Parameter's")]
    [SerializeField][Range(0,50)] private float speedAgent;
    [SerializeField][Range(0,50)] private float speedSprintAgent;
    [SerializeField][Range(0,50)] private float speedChangeRate;
    [SerializeField][Range(0,10)] private float rotationSmoothTime;


    private ManagerInput managerInput;
    private CharacterController characterController;

    private float speed;
    private float targetRotation;
    private float rotationVelocity;
    private float verticalVelocity;

    private void Start()
    {
        managerInput = GetComponent<ManagerInput>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MovementAgent();
    }

    private void MovementAgent()
    {
        float targetSpeed = managerInput.sprint ? speedSprintAgent : speedAgent;

        if (managerInput.movementAgent == Vector2.zero) 
            targetSpeed = 0f;

        float currentHorizontalSpeed = new Vector3(characterController.velocity.x , 0f, characterController.velocity.z).magnitude;

        float speedOffSet = .1f;
        float inputMagnitude = managerInput.analogMovement ? managerInput.movementAgent.magnitude : 1f;

        if(currentHorizontalSpeed < targetSpeed - speedOffSet || currentHorizontalSpeed > targetSpeed + speedOffSet)
        {
            speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * speedChangeRate);
            speed = Mathf.Round(speed * 1000f) / 1000f;
        }
        else
        {
            speed = targetSpeed;
        }

        RotationAgent();
        DirectionAgent();

        OnAnimationBlend?.Invoke(targetSpeed, speedChangeRate, inputMagnitude);
    }

    private void RotationAgent()
    {
        Vector3 inputDirection = new Vector3(managerInput.movementAgent.x, 0f, managerInput.movementAgent.y).normalized;

        if (managerInput.movementAgent != Vector2.zero)
        {
            targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, rotation, 0f);
        }
    }

    private void DirectionAgent()
    {

        Vector3 targetDirection = Quaternion.Euler(0f, targetRotation, 0f) * Vector3.forward;

        characterController.Move(targetDirection.normalized * (speed * Time.deltaTime) + new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
    }
}
