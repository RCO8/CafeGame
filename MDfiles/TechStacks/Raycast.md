<h1>Raycast2D</h1>

<p>손님이 대기할 때 간격을 유지하는 연출로 구현</p>

적용하기 전<br>

<p>
  원래는 앞 손님이 주문하면 WaitOrder라고 대기하다 들어가는 상태를 구현했는데, <br>

    waitOrder = GameManager.instance.Table.IsInGuest
  <br>
  코드 가독성이 좋지 못해 Raycast로 변경하였다.
  
</p>

Raycast를 적용한 결과<br>

![image](https://github.com/user-attachments/assets/1d3c5fd6-4a92-46be-a9dc-1f219e66db55)

#코드 뷰
```cs
    private Transform nowPosition;

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
```
