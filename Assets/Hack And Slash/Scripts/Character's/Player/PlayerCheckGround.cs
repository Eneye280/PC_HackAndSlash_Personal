using UnityEngine;

public class PlayerCheckGround : MonoBehaviour
{
    private ManagerInput managerInput;

    [Header("Position")]
    [SerializeField] private Transform positionRaycast;

    [Header("Layer")]
    [SerializeField] private LayerMask layerGround;

    [Header("Values")]
    [Range(0,1)]
    [SerializeField] private float groundedRadius = 0.28f;

    [Header("Colors")]
    [SerializeField] private Color colorIsGround;
    [SerializeField] private Color colorNotIsGround;

    private void Awake()
    {
        managerInput = GetComponent<ManagerInput>();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        managerInput.isGround = Physics.CheckSphere(positionRaycast.position, groundedRadius, (int)layerGround);
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying && Application.isEditor)
        {
            if (managerInput.isGround)
                Gizmos.color = colorIsGround;
            else
                Gizmos.color = colorNotIsGround;

            Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), groundedRadius); 
        }
    }
}
