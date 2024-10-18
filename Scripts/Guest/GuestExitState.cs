using UnityEngine;

public class GuestExitState : GuestState
{
    //출구로 나가기
    Vector2 currentPos; //현재 위치
    Vector2 findExit;
    Vector2 direction = Vector2.zero;

    public GuestExitState(GuestStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Exit");
        findExit = GameManager.instance.Entry.transform.position;
        //Debug.Log(findExit);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        currentPos = stateMachine.Guest.transform.position;
        direction = FindDirection(currentPos, findExit, 0.5f);

        stateMachine.Guest.animation.SetMovingAnimation(direction);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        //출입문을 향해
        Movement(direction);
    }
}