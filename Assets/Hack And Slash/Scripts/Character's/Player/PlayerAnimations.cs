using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    internal Animator animatorPlayer;

    [Header("Parameters")]
    [SerializeField] private string parameterRun;
    [SerializeField] private string parameterJump;

    private void Awake() => animatorPlayer = GetComponent<Animator>();

    private void OnEnable()
    {
        PlayerMovement.OnAddAnimationMovement += AddAnimationMovement;
        ManagerInput.OnJumpPlayer += JumpPlayer;
    }

    private void AddAnimationMovement(Vector3 refVectorMovement)
    {
        if (animatorPlayer == null)
            return;

        if (refVectorMovement.z != 0)
            animatorPlayer.SetBool(parameterRun, true);
        else
            animatorPlayer.SetBool(parameterRun, false);
    }

    private void JumpPlayer() => animatorPlayer.SetTrigger(parameterJump);


    private void OnDisable()
    {
        PlayerMovement.OnAddAnimationMovement -= AddAnimationMovement;
        ManagerInput.OnJumpPlayer -= JumpPlayer;
    }
}
