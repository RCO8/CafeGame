using UnityEngine;

public class GuestStayState : GuestState
{
    //손님이 자리에 앉는 연출 구현
    //일정시간동안 있다가 퇴장
    float time = 0f;
    Vector2 sit = new Vector2(0.7f, 0.1f);

    public GuestStayState(GuestStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        time = 0f;
        //table위치가 좌냐 우냐에 따라서 sit
        if (stateMachine.Guest.transform.position.x < findTable.x)   //왼쪽
        {
            sit.x *= -1;
            stateMachine.Guest.animation.SetMirrorSit(false);
        }

        stateMachine.Guest.transform.position = findTable + sit;
        //애니메이션 적용
        stateMachine.Guest.animation.SetSittingAnimation();
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.Guest.animation.SetMirrorSit();
    }

    public override void Update()
    {
        base.Update();
        //stayTime이 넘어가면 ExitState
        time += Time.deltaTime;
        stateMachine.Guest.CheckTime(time);
    }
}
