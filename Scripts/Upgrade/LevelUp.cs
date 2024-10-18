using TMPro;
using UnityEngine;

public class LevelUp : UpgradeSystem
{
    [SerializeField] private TextMeshProUGUI LevelUpPanel;

    const int MaxLevel = 10;

    private void Awake()
    {
        CurLevel = GameManager.instance.Level;

        PayLevels[0] = 15000;
        PayLevels[1] = 25000;
        PayLevels[2] = 35000;
        PayLevels[3] = 50000;
        PayLevels[4] = 65000;
        PayLevels[5] = 80000;
        PayLevels[6] = 100000;
        PayLevels[7] = 120000;
        PayLevels[8] = 150000;

        Apply();
    }

    public void Upgrade()
    {
        //PayLevels상수 참고
        if (GameManager.instance.UseMoney(PayLevels[CurLevel - 1]))
        {
            CurLevel++;
            Apply();
            MenuManager.instance.ApplyMenu();
        }
    }

    protected override void Apply()
    {
        LevelUpPanel.text = $"Lv. {CurLevel}";
        base.Apply();
    }
}
