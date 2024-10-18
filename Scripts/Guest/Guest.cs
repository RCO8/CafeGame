using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : MonoBehaviour
{
    public OrderSO wantOrder;  //GameManger에서 랜덤한 주문을 임포트
    [SerializeField] private GameObject OrderObject;
    [SerializeField] private SpriteRenderer OrderImage;

    //로직 속성
    public bool arriveOrder { get; private set; } = false;  //주문하기
    private float stayTime;     //카페있을 시간
    private OrderSO hasOrder;   //받은 주문
    private bool desertOrder;   //디저트 주문
    public bool exitCafe { get; private set; } = false; //카페 나간다

    private GuestStateMachine stateMachine;

    public GuestAnimation animation;

    private void Awake()
    {
        //FSM 등록
        stateMachine = new GuestStateMachine(this);
        //컴포넌트 등록
        animation = GetComponent<GuestAnimation>();
    }
    private void Start()
    {

        //orderType = Random.Range(매장, 포장);

        wantOrder = MenuManager.instance.PickupDrink();
        SettingBegin();
    }
    private void OnEnable()
    {   //활성시 기본 값 초기
        transform.localPosition = Vector2.zero;
        SettingBegin();

        //Todo : 오브젝트가 재활성하면 처음 들어가는 상태로 v
        //Todo : 주문대에 들어가면 다른 손님이 주문받는 버그 해결
    }

    private void Update()
    {
        stateMachine.Update();
    }
    private void FixedUpdate()
    {
        //위로 이동
        stateMachine.PhysicsUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Order테이블에 있으면 주문
        LayerMask isOrder = LayerMask.NameToLayer("Order");
        LayerMask isSitting = LayerMask.NameToLayer("Sitting");
        LayerMask isExit = LayerMask.NameToLayer("Entry");

        if (exitCafe)
        {
            //여기서 앞에 주문대랑 닿지 않게

            if (collision.gameObject.layer.Equals(isExit))
            {
                //Debug.Log("안녕히 계세요~~");
                gameObject.SetActive(false);
                GameManager.instance.Entry.ExitGuest();
            }
        }
        else
        {
            if (collision.gameObject.layer.Equals(isOrder))  //주문대
            {
                ShowOrder();
            }
            else if (collision.gameObject.layer.Equals(isSitting)) //자리
            {

                UseTable checkTable = collision.GetComponent<UseTable>();
                if (checkTable.Clear)
                {
                    //현재 위치를 현재 자리위치로 (비어있는 기준)
                    transform.position = checkTable.transform.position;
                    stateMachine.ChangeState(stateMachine.StayState);
                    checkTable.EnterGuest(this);
                }
                else
                {   //딴자리 찾기
                    stateMachine.ChangeState(stateMachine.MoveState);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //주문한걸 다른 위치에 표시되지 않게
        LayerMask isOrder = LayerMask.NameToLayer("Order");
        if(collision.gameObject.layer.Equals(isOrder))
            OrderObject.SetActive(false);
    }

    private void SettingBegin() //초기 데이터 등록
    {
        exitCafe = false;
        arriveOrder = false;

        //음료를 랜덤으로 하나 뽑기
        wantOrder = MenuManager.instance.PickupDrink();
        OrderImage.sprite = wantOrder.Image;
        //추가로 디저트 주문
        desertOrder = Random.Range(0f, 1f) < 0.3f;  //30%확률의 디저트 주문
        if (desertOrder)
            Debug.Log("디저트 주문 (미구현)");

        stayTime = Random.Range(5f, 18f);
        stateMachine.ChangeState(stateMachine.EnterState);
    }
    private void ShowOrder() //주문 표시
    {
        OrderObject.SetActive(true);
        arriveOrder = true;
    }
    public bool GetOrder(OrderSO order) //주문받기
    {
        if (wantOrder == order)
        {
            //Debug.Log("맞아");
            OrderObject.SetActive(false);
            hasOrder = order;
            //퇴장 (포장 or 매장)
            //if(매장)
            stateMachine.ChangeState(stateMachine.MoveState);
            return true;
        }
        return false;
    }
    public void CheckTime(float t)  //시간 확인
    {
        if (stayTime < t)
        {
            exitCafe = true;
            stateMachine.ChangeState(stateMachine.ExitState);
        }
    }
}
