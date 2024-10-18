using UnityEngine;

public class GuestState : IState
{
    protected GuestStateMachine stateMachine;

    protected static Vector2 findTable;

    private int orderDesk = LayerMask.GetMask("Order");

    public GuestState(GuestStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Update()
    {
    }

    protected void Movement(Vector2 dir)
    {
        stateMachine.Guest.transform.Translate(dir * Time.deltaTime);
    }

    protected Vector2 FindDirection(Vector2 curPos, Vector2 tarPos, float dist = 0f)
    {   //대상 지점까지 방향전환
        //앞에 장애물이 있으면 다른 방향으로
        //if(table.Clear) -> 

        RaycastHit2D hit;
        float horizonDist = 1f;
        float verticalDist = 1f;
        if (curPos.x < tarPos.x - dist) // && 장애물이 앞에 없다
        {
            //앞에 장애물이 있다면
            hit = Physics2D.Raycast(curPos + (Vector2.right),
                Vector2.right, horizonDist, orderDesk);
            if(!hit.collider)
                return Vector2.right;
        }
        else if (curPos.x > tarPos.x + dist)
        {
            hit = Physics2D.Raycast(curPos + (Vector2.left),
                Vector2.left, horizonDist, orderDesk);
            if (!hit.collider)
                return Vector2.left;
        }
        
        if (curPos.y < tarPos.y)
        {
            hit = Physics2D.Raycast(curPos + (Vector2.up),
                Vector2.up, verticalDist, orderDesk);
            if (!hit.collider)
                return Vector2.up;
        }
        else if (curPos.y > tarPos.y)
        {
            hit = Physics2D.Raycast(curPos + (Vector2.down),
                Vector2.down, verticalDist, orderDesk);
            if (!hit.collider)
                return Vector2.down;
        }
        return Vector2.zero;
    }


}
