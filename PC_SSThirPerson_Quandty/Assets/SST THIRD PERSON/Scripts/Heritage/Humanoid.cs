using UnityEngine;

public class Humanoid : MonoBehaviour
{
    [SerializeField] internal Animator anim;
    [SerializeField] internal float speed;
    public virtual void Movement() { }
    public virtual void Crouching() { }
    public virtual void Jump() { }

}
