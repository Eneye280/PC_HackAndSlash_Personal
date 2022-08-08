using UnityEngine;

public class Humanoid : MonoBehaviour
{
    [SerializeField] internal bool isMovement;
    [Range(0, 50)]
    [SerializeField] internal float speedMovement;

    public virtual void MovementAgent() { }

}
