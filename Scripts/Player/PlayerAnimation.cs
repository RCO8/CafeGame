using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    private static readonly int IsMoving = Animator.StringToHash("Move");
    private static readonly int IsDirX = Animator.StringToHash("DirX");
    private static readonly int IsDirY = Animator.StringToHash("DirY");

    private bool _moving = false;
    private Vector2 _dir;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if(MenuManager.instance.CanMove)
            SetAnimation(_moving, _dir);
    }

    public void GetInputKey(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _moving = true;
            _dir = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
            _moving = false;
    }

    private void SetAnimation(bool mov, Vector2 dir)
    {
        animator.SetBool(IsMoving, mov);
        animator.SetFloat(IsDirX, dir.x);
        animator.SetFloat(IsDirY, dir.y);
    }
}
