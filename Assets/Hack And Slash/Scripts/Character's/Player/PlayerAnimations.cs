using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    internal Animator animatorPlayer;

    [Header("Parameters")]
    [SerializeField] private string parameterRun;

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
            animatorPlayer.SetBool(parameterRun, true);
        else
            animatorPlayer.SetBool(parameterRun, false);
    }

    private void OnDisable()
    {
        PlayerMovement.OnAddAnimationMovement -= AddAnimationMovement;
    }
}
