using UnityEngine;

public class HumanoidMovement : MonoBehaviour
{
    [SerializeField] internal bool isMovement;
    [Range(0, 50)]
    [SerializeField] internal float speedMovement;
    [Range(-200, 200)]
    [SerializeField] internal float speedRotation;
    public virtual void MovementAgent() { }

}
