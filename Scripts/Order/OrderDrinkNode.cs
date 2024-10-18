public class OrderDrinkNode : OrderNode
{
    CoffeeSO coffee;

    protected override void ApplyActiveMenu()
    {
        base.ApplyActiveMenu();
        coffee = (CoffeeSO)MenuInfo;
        MakeStartButton.interactable = coffee.Level <= LvUp.CurLevel;
    }

    public void BlendStart()
    {
        //if(Level > nowLevel)
        MenuManager.instance.State = MakeState.BLENDING;
        MenuManager.instance.MakingStart(MenuInfo);
    }
}
