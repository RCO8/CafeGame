using UnityEngine;

public class GuestAnimation : MonoBehaviour
{
    private Animator animator;

    private static readonly int IsMoving = Animator.StringToHash("Move");
    private static readonly int IsDirX = Animator.StringToHash("DirX");
    private static readonly int IsDirY = Animator.StringToHash("DirY");
    private static readonly int IsSitting = Animator.StringToHash("Sit");
    private static readonly int IsMirror = Animator.StringToHash("Mirror");

    private bool _moving = false;
    private bool _sitting = false;
    private bool _mirror = false;
    private Vector2 _dir;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        ApplyAnimator();
    }

    //이동 Anim
    public void SetMovingAnimation(Vector2 dir)
    {
        _moving = true;
        _sitting = false;
        _dir = dir;
    }

    //앉기 Anim
    public void SetSittingAnimation()
    {
        _sitting = true;
        _moving = false;
    }

    //좌우반전
    public void SetMirrorSit(bool conf = false)
    {
        _mirror = conf;
    }

    //정지
    public void SetStoppingAnimation()
    {
        _moving = false;
    }

    //설정한 변수들을 animator에 적용
    private void ApplyAnimator()
    {
        animator.SetBool(IsMoving, _moving);
        animator.SetBool(IsSitting, _sitting);
        animator.SetFloat(IsDirX, _dir.x);
        animator.SetFloat(IsDirY, _dir.y);
        animator.SetBool(IsMirror, _sitting);
    }
}