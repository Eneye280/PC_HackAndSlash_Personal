using UnityEngine;

public class Humanoid : MonoBehaviour
{
    [SerializeField] internal Animator anim;
    [Range(-10,10)]
    [SerializeField] internal float speed;
    public virtual void Movement() { }
    public virtual void Crouching() { }
    public virtual void Jump() { }

}
