using UnityEngine;

public class Humanoid : MonoBehaviour
{
    [Range(0, 50)]
    [SerializeField] internal float speedMovement;

    public virtual void Movement() { }
    public virtual void Crouching() { }
    public virtual void Jump() { }

}
