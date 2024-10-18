using TMPro;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI StateInfo;
    [SerializeField] protected TextMeshProUGUI PaymentText;
    public int CurLevel { get; protected set; } = 1;
    protected int[] PayLevels = new int[9];

    protected virtual void Apply()
    {
        StateInfo.text = $"Lv. {CurLevel} → {CurLevel + 1}";
        PaymentText.text = $"{PayLevels[CurLevel - 1]} 원";
    }
}