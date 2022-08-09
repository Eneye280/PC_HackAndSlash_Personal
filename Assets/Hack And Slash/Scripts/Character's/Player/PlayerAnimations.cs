using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animatorPlayer;
    private bool isAnimator;
    private float animationBlend;
    private int animIDSpeed;
    private int animIDMotionSpeed;

    private void OnEnable()
    {
        PlayerMovement.OnAnimationBlend += AnimationBlend;
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
    }

    private void AnimationBlend(float targetSpeed, float speedChangeRate, float inputMAgnitude)
    {
        animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * speedChangeRate);

        if (animationBlend < 0.01f)
            animationBlend = 0;

        IsAnimator(inputMAgnitude);
    }

    private void IsAnimator(float inputMagnitude)
    {
        if (isAnimator)
        {
            animatorPlayer.SetFloat(animIDSpeed, animationBlend);
            animatorPlayer.SetFloat(animIDMotionSpeed, inputMagnitude);
        }
    }

    private void OnDisable()
    {
        PlayerMovement.OnAnimationBlend -= AnimationBlend;
    }
}
