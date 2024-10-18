public class OrderDesertNode : OrderNode
{
    DesertSO desert;

    protected override void ApplyActiveMenu()
    {
        base.ApplyActiveMenu();
        desert = (DesertSO)MenuInfo;
        MakeStartButton.interactable = desert.Level <= LvUp.CurLevel;
    }

    public void CookStart()
    {
        MenuManager.instance.State = MakeState.COOKING;
        MenuManager.instance.MakingStart(MenuInfo);
    }
}
