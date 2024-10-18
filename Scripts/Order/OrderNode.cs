using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderNode : MonoBehaviour
{
    //충족 레벨 이상이면 제작 가능
    public OrderSO MenuInfo;
    protected LevelUp LvUp;

    [SerializeField] private Image MenuImage;
    [SerializeField] private TextMeshProUGUI MenuName;
    [SerializeField] private TextMeshProUGUI MenuPrice;
    [SerializeField] protected Button MakeStartButton;
    private float makingTime;

    private void Start()
    {
        MenuImage.sprite = MenuInfo.Image;
        MenuName.text = MenuInfo.Name;
        MenuPrice.text = MenuInfo.Price.ToString();
        makingTime = MenuInfo.MakingTime;

        LvUp = GameManager.instance.LevelUpManager;
        ApplyActiveMenu();
    }

    //Todo : 레벨업하면 잠겼던 메뉴를 해금
    public void CheckUnlockMenu()
    {
        ApplyActiveMenu();
    }

    protected virtual void ApplyActiveMenu()
    {

    }
}