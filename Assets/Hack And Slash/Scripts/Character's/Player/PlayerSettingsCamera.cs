using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSettingsCamera : MonoBehaviour
{
    private PlayerInput playerInput;
    private ManagerInput managerInput;

    [Header("General")]
    [SerializeField] private GameObject cinemachineCameraTarget;

    [Header("Checker")]
    [SerializeField] private bool lockCameraPosition;
    
    [Header("Parameter's")]
    [Range(0, 200)]
    [SerializeField]  private float topClamp;
    [Range(-200, 200)]
    [SerializeField]  private float bottomClamp;

    private float cameraAngleOverride;
    private const float threshold = 0.01f;
    private float cinemachineTargetYaw;
    private float cinemachineTargetPitch;
    private bool isCurrentDeviceMouse
    {
        get
        {
            return playerInput.currentControlScheme == "KeyboardMouse";
        }
    }

    private void Start()
    {
        cinemachineTargetYaw = cinemachineCameraTarget.transform.rotation.eulerAngles.y;

        playerInput = GetComponent<PlayerInput>();
        managerInput = GetComponent<ManagerInput>();
    }

    private void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        if(managerInput.lockAgent.sqrMagnitude >= threshold && !lockCameraPosition)
        {
            float deltaTimeMultiplier = isCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            cinemachineTargetYaw += managerInput.lockAgent.x * deltaTimeMultiplier;
            cinemachineTargetPitch += managerInput.lockAgent.y * deltaTimeMultiplier;
        }

        cinemachineTargetYaw = ClampAngle(cinemachineTargetYaw, float.MinValue, float.MaxValue);
        cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, bottomClamp, topClamp);

        cinemachineCameraTarget.transform.rotation = Quaternion.Euler(cinemachineTargetPitch + cameraAngleOverride, cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) 
            lfAngle += 360f;

        if (lfAngle > 360f) 
            lfAngle -= 360f;

        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
