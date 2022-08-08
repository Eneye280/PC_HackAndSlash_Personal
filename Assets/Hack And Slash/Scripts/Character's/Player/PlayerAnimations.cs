using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    internal Animator animatorPlayer;

    [SerializeField] private bool isJump;

    [Header("Parameters")]
    [SerializeField] private string parameterRun;
    [SerializeField] private string parameterJump;

    private void Awake() => animatorPlayer = GetComponent<Animator>();

    private void OnEnable()
    {
        PlayerMovement.OnAddAnimationMovement += AddAnimationMovement;
        ManagerInput.OnJumpPlayer += AddAnimationJump;
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

    private void AddAnimationJump()
    {
        isJump = true;

        if(isJump)
            animatorPlayer.SetTrigger(parameterJump);
    }


    private void OnDisable()
    {
        PlayerMovement.OnAddAnimationMovement -= AddAnimationMovement;
        ManagerInput.OnJumpPlayer -= AddAnimationJump;
    }
}
