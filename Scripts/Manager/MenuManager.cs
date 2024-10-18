using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum MakeState { BLENDING, COOKING }

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public bool CanMove { get; private set; } = true;
    public bool IsOpen { get; private set; } = false;
    private bool isMaking = false;

    [SerializeField] private GameObject OrderMenu;
    [SerializeField] private GameObject Blending;
    [SerializeField] private TextMeshProUGUI BlendingText;
    [SerializeField] private Slider BlendingBar;
    [SerializeField] private OrderStock Stocks;

    public List<CoffeeSO> OrderDrinkData;
    public List<DesertSO> OrderDesertData;

    //여기에 메뉴 만드는 버튼을 추가하고 관리할 수 있도록
    [SerializeField] private Transform DrinkContents;
    [SerializeField] private Transform DesertContents;
    private List<OrderNode> DrinkButtons = new List<OrderNode>();
    private List<OrderNode> DesertButtons = new List<OrderNode>();

    public MakeState State { get; set; }    //상태 초기

    private void Awake()
    {
        if(instance == null)
            instance = this;

        //주문 콘텐츠 수집
        for (int i = 0; i < DrinkContents.childCount; i++)
            DrinkButtons.Add(DrinkContents.GetChild(i).GetComponent<OrderDrinkNode>());
        for (int i = 0; i < DesertContents.childCount; i++)
            DesertButtons.Add(DesertContents.GetChild(i).GetComponent<OrderDesertNode>());

        gameObject.SetActive(false);
    }

    //메뉴 기본 루프
    public void ShowMenu()
    {
        CanMove = false;
        OrderMenu.SetActive(true);
        Blending.SetActive(false);
        gameObject.SetActive(true);
        IsOpen = true;
    }
    public void MakingStart(OrderSO s)
    {
        OrderMenu.SetActive(false);
        Blending.SetActive(true);
        BlendingText.text = State.ToString();
        BlendingBar.value = 0;
        StartCoroutine(BlendStart(s));
    }
    IEnumerator BlendStart(OrderSO s)
    {
        float timeFlag = s.MakingTime/100;
        WaitForSeconds delayTime = new WaitForSeconds(timeFlag);

        isMaking = true;
        for (int i = 0; i < 100; i++)
        {
            BlendingBar.value += 0.01f;
            yield return delayTime;
        }

        //다되면 메뉴 닫고 완료된 음료를 손님께
        ShowMenu();
        //완성된 음료는 stock에 저장
        Stocks.AddOrder(s);
        isMaking = false;
    }
    public void ExitPanel()
    {
        if (!isMaking)
        {
            gameObject.SetActive(false);
            CanMove = true;
            IsOpen = false;
        }
    }

    //업그레이드 갱신
    public void ApplyMenu()
    {
        foreach(OrderNode order in DrinkButtons)
            order.CheckUnlockMenu();
        foreach(OrderNode order in DesertButtons)
            order.CheckUnlockMenu();
    }

    //랜덤주문
    public CoffeeSO PickupDrink()
    {
        int currentLevel = GameManager.instance.LevelUpManager.CurLevel;
        int randomIndex = Random.Range(0, OrderDrinkData.Count);

        while(OrderDrinkData[randomIndex].Level > currentLevel)
            randomIndex = Random.Range(0, OrderDrinkData.Count);

        return OrderDrinkData[randomIndex];
    }
    public DesertSO PickupDesert()
    {
        int currentLevel = GameManager.instance.LevelUpManager.CurLevel;
        int randomIndex = Random.Range(0,OrderDesertData.Count);

        while (OrderDesertData[randomIndex].Level > currentLevel)
            randomIndex = Random.Range(0, OrderDesertData.Count);

        return OrderDesertData[randomIndex];
    }
}
