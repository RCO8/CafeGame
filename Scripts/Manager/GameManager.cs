using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("손님 상호작용")]
    public OrderTable Table;    //주문대
    public Entry Entry; //입구

    [Header("업그레이드")]
    public GameObject UpgradePanel;
    public LevelUp LevelUpManager;
    public AddonSeat SeatManager;

    [SerializeField] private TextMeshProUGUI MoneyUI;   //얼마있는지 표시

    private int money = 0;   //자산

    public int Level { get; private set; } = 1;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        //처음에 UI요소들 숨기고 시작
        UpgradePanel.SetActive(false);

        ApplyMoney();
    }

    //돈관리
    public void GetMoney(int pay)
    {
        money += pay;
        ApplyMoney();
    }

    public bool UseMoney(int pay)
    {
        if (money - pay < 0)
            return false;
        money -= pay;
        ApplyMoney();
        return true;
    }

    private void ApplyMoney() => MoneyUI.text = $"{money} 원";

    //채용

    //스피드
}
