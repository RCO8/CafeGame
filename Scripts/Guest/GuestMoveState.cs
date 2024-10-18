using UnityEngine;

public class GuestMoveState : GuestState
{
    //자리를 찾아가기
    //자리 객체들은 GameManger에서 탐색(?)
    Vector2 currentPos; //현재 위치
    Vector2 direction = Vector2.zero;

    float aimArea = 1f; //중간범위 (테이블 위치정면)

    public GuestMoveState(GuestStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        findTable = GameManager.instance.SeatManager.FindTable(stateMachine.Guest); //자리찾기
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.Guest.animation.SetStoppingAnimation();
        Movement(Vector2.zero);
    }

    public override void Update()
    {
        base.Update();

        //대각선이 아닌 일직선으로 이동
        //가로가 우선순위
        currentPos = stateMachine.Guest.transform.position; //위치 업데이트
        
        //자리의 방위를 찾아 이동 (자리 있으면 다른 자리로)
        direction = FindDirection(currentPos, findTable, aimArea);

        stateMachine.Guest.animation.SetMovingAnimation(direction);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        //자리로 이동
        Movement(direction);
    }
}