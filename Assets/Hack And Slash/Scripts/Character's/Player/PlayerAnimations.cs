using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animatorPlayer;

    private bool isAnimator;
    private float animationBlend;

    private int animIDSpeed;
    private int animIDMotionSpeed;
    private int animIDSprint;
    private int animIDJump;

    private void OnEnable()
    {
        PlayerMovement.OnAnimationBlend += AnimationBlendMovement;
        PlayerSprint.OnAnimationSprint += AnimationSprint;
        PlayerJump.OnAnimationJump += AnimationJump;
    }

    private void Start()
    {
        animatorPlayer = GetComponent<Animator>();
        isAnimator = TryGetComponent(out animatorPlayer);

        AssignAnimationID();
    }

    private void Update()
    {
        isAnimator = TryGetComponent(out animatorPlayer);
    }

    private void AssignAnimationID()
    {
        animIDSpeed = Animator.StringToHash("Speed");
        animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        animIDSprint = Animator.StringToHash("isSprint");
        animIDJump = Animator.StringToHash("isJump");
    }

    private void AnimationBlendMovement(float targetSpeed, float speedChangeRate, float inputMAgnitude)
    {
        animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * speedChangeRate);

        if (animationBlend < 0.01f)
            animationBlend = 0;

        AnimationMovementAgent(inputMAgnitude);
    }
    private void AnimationMovementAgent(float inputMagnitude)
    {
        if (isAnimator)
        {
            animatorPlayer.SetFloat(animIDSpeed, animationBlend);
            animatorPlayer.SetFloat(animIDMotionSpeed, inputMagnitude);
        }
    }

    private void AnimationSprint(bool isActive)
    {
        if(isAnimator)
        {
            animatorPlayer.SetBool(animIDSprint, isActive);
        }
    }
    private void AnimationJump(bool isActive)
    {
        if (isAnimator)
        {
            animatorPlayer.SetBool(animIDJump, isActive);
        }
    }

    private void OnDisable()
    {
        PlayerMovement.OnAnimationBlend -= AnimationBlendMovement;
        PlayerSprint.OnAnimationSprint -= AnimationSprint;
        PlayerJump.OnAnimationJump -= AnimationJump;
    }
}
