using UnityEngine;

public class OrderStock : MonoBehaviour
{
    [SerializeField] private StockButton Img;
    public int MaxSize = 10;

    private OrderSO[] orders;
    private StockButton[] buttons;

    private void Start()
    {
        orders = new OrderSO[MaxSize];
        buttons = new StockButton[MaxSize];

        for (int i = 0; i < MaxSize; i++)
        {
            buttons[i] = Instantiate(Img, transform);
            buttons[i].gameObject.SetActive(false);
            buttons[i].Index = i;
        }
    }

    public void AddOrder(OrderSO order)
    {
        //있으면 집어넣기
        for (int i = 0; i < MaxSize; i++)
            if (orders[i] == null)
            {
                orders[i] = order;
                break;
            }
        SettingStock();
    }

    public void RemoveOrder(int idx)
    {
        //팔았으면 비워두기
        orders[idx] = null;
        SettingStock();
    }

    private void SettingStock() //재고 정리
    {
        for (int i = 0; i < orders.Length; i++)
            if(orders[i] != null)
                buttons[i].InsertOrder(orders[i]);
    }
}
