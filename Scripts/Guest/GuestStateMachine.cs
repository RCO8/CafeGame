public class GuestStateMachine : StateMachine
{
    public Guest Guest { get; }

    //States
    public GuestEnterState EnterState { get; }
    public GuestMoveState MoveState { get; }
    public GuestStayState StayState { get; }
    public GuestExitState ExitState { get; }

    public GuestStateMachine(Guest guest)
    {
        Guest = guest;

        EnterState = new GuestEnterState(this);
        MoveState = new GuestMoveState(this);
        StayState = new GuestStayState(this);
        ExitState = new GuestExitState(this);
    }
}
