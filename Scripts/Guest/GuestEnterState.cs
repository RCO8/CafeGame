using UnityEngine;

public class GuestEnterState : GuestState
{
    private bool waiting = false;
    private Vector2 direction = Vector2.zero;
    private RaycastHit2D frontGuest;
    private int checkGuest = LayerMask.GetMask("Guest");
    private Transform nowPosition;

    public GuestEnterState(GuestStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        //주문대로
        base.Enter();
        waiting = false;
    }

    public override void Exit()
    {
        base.Exit();
        Movement(Vector2.zero);
    }

    public override void Update()
    {
        base.Update();
        nowPosition = stateMachine.Guest.transform;

        //그전에 앞에 손님이 갈때 까지 기다리기
        RaycastHit2D hit = Physics2D.Raycast(nowPosition.position + (nowPosition.up / 10), nowPosition.up, 1f, checkGuest);
        waiting = GameManager.instance.Table.IsInGuest != null;

        //다음주문까지 기다리기
        if (stateMachine.Guest.arriveOrder || hit.collider)
        {
            direction = Vector2.zero;
            stateMachine.Guest.animation.SetStoppingAnimation();
        }
        else
        {
            direction = Vector2.up;
            stateMachine.Guest.animation.SetMovingAnimation(direction);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Movement(direction);
    }
}
