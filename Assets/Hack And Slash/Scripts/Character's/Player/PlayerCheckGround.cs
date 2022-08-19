using UnityEngine;

public class PlayerCheckGround : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private Transform positionRaycast;

    [Header("Check")]
    [SerializeField] private bool isGround;

    [Header("Layer")]
    [SerializeField] private LayerMask layerGround;

    [Header("Values")]
    [Range(0,1)]
    [SerializeField] private float groundedRadius = 0.28f;

    [Header("Colors")]
    [SerializeField] private Color colorIsGround;
    [SerializeField] private Color colorNotIsGround;

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        isGround = Physics.CheckSphere(positionRaycast.position, groundedRadius, (int)layerGround);
    }

    private void OnDrawGizmos()
    {
        if (isGround)
            Gizmos.color = colorIsGround;
        else
            Gizmos.color = colorNotIsGround;

        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), groundedRadius);
    }
}
