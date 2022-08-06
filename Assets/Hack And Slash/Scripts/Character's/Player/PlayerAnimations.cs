using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    internal Animator animatorPlayer;

    private void Awake() => animatorPlayer = GetComponent<Animator>();

    private void OnEnable()
    {
        PlayerMovement.OnAddAnimationMovement += AddAnimationMovement;
    }

    private void AddAnimationMovement(Vector3 refVectorMovement)
    {
        if (animatorPlayer == null)
            return;

        if (refVectorMovement.z != 0)
            animatorPlayer.SetBool("isRun", true);
        else
            animatorPlayer.SetBool("isRun", false);
    }

    private void OnDisable()
    {
        PlayerMovement.OnAddAnimationMovement -= AddAnimationMovement;
    }
}
