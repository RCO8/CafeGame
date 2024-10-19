<h1>State Machine</h1>

<p>손님이 어떤 동작을 하기 위해 상태머신을 사용</p>

<p>
    순서 : Enter -> Move -> Stay -> Exit<br>
    참고로 손님 오브젝트가 재생성되면 EnterState로
<p>

<h3>EnterState</h3>

![{52E83185-A598-4A8E-BF8B-DF3409D25516}](https://github.com/user-attachments/assets/c1389df3-6ca2-40c5-89ba-0d779626cc77)

주문대에 들어가면 정지후 주문<br>

![{F32371B7-B12A-4B2E-9B37-54D17E9A7730}](https://github.com/user-attachments/assets/1b450438-73ef-40c9-bf49-50e05fbf57ab)

<h3>MoveState</h3>

가장 가까운 빈자리를 탐색<br>

![{79F7855D-8B61-4645-851F-C92E773E902B}](https://github.com/user-attachments/assets/c7ede446-91b1-4916-9971-6ce7594d0912)

<h3>StayState</h3>

![{55B864E0-E2A2-4D07-A55E-6E9911E8D225}](https://github.com/user-attachments/assets/88f70105-799f-460e-8b75-aa0f3ffb1550)

<h3>ExitState</h3>

![{C08B3F3D-841E-46B7-83EE-2E9D7402EC66}](https://github.com/user-attachments/assets/bba16bc2-1006-478f-b65d-3b2cea802f00)

손님이 매장에서 나간다<br>

<hr/>

<p>
    상태머신은 Guest만 적용시켰기 때문에 GuestStateMachine을 다음과 같이 관리하였다.<br>
    <a href="https://github.com/RCO8/CafeGame/blob/main/Scripts/Guest/GuestStateMachine.cs">스크립트 뷰</a><br>
    <a href="https://github.com/RCO8/CafeGame/blob/main/Scripts/Guest/GuestState.cs">기본 상태머신 동작</a>
</p>
