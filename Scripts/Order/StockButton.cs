using UnityEngine;
using UnityEngine.UI;

public class StockButton : MonoBehaviour
{
    public OrderSO order { get; private set; }
    public int Index {get; set;}

    [SerializeField] private Image Img;
    [SerializeField] private OrderStock parent;

    private Guest checkGuest;

    private void Awake()
    {
        //부모 컴포넌트 연결
        parent = GetComponentInParent<OrderStock>();
    }

    private void Update()
    {
        //주문대에 손님 확인
        checkGuest = GameManager.instance.Table.IsInGuest;
    }

    public void InsertOrder(OrderSO o)
    {
        order = o;
        Img.sprite = order.Image;
        gameObject.SetActive(true);
    }

    public void SellOrder()
    {
        //if(주문대 앞에 손님이 있으면)
        //완성한 음료를 손님께
        if(checkGuest != null)
        {
            if(checkGuest.GetOrder(order))
            {
                GameManager.instance.GetMoney(order.Price);  //돈은 줘야지!!
                parent.RemoveOrder(Index);
                gameObject.SetActive(false);
            }
        }
    }
}
