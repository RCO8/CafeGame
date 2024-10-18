using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //플레이어 이동
    private Rigidbody2D rgdby2D;
    private Vector2 playerMovement;
    private float playerSpeed = 3f;

    //상호작용 트리거
    [SerializeField] private Transform aimTrigger;
    private Vector2 playerInteract; //트리거 방향
    public bool InOrder { get; private set; }

    private OrderTable interactTable;

    private void Awake()
    {
        rgdby2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        interactTable = GameManager.instance.Table;
    }

    private void Update()
    {
        InteractAround(playerInteract);
    }

    private void FixedUpdate()
    {
        if(MenuManager.instance.CanMove)
            Moving(playerMovement);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //플레이어 이동
        if (context.phase == InputActionPhase.Performed)
        {
            playerMovement = context.ReadValue<Vector2>();
            playerInteract = context.ReadValue<Vector2>();  //상호작용 회전 (z축)
        }
        else if (context.phase == InputActionPhase.Canceled)
            playerMovement = Vector2.zero;

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)  //상호작용
            Interacting(true);
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)   //캔슬
        {
            MenuManager.instance.ExitPanel();
            GameManager.instance.UpgradePanel.SetActive(false);
        }
    }

    private void Moving(Vector2 dir)    //이동
    {
        dir *= playerSpeed;
        rgdby2D.velocity = dir;
    }

    private void InteractAround(Vector2 dir) //상호작용 전환
    {
        aimTrigger.transform.localPosition = dir.normalized;
    }

    private void Interacting(bool conf)
    {
        if(InOrder) //테이블에 들어왔으면
        {
            //손님이 있으면
            MenuManager.instance.ShowMenu();
            interactTable.HideSpaceKey();
        }

        //자리 뒷정리

    }

    public void OrderInteract(bool conf)
    {
        InOrder = conf;
        //Space - 주문하기 를 띄우게
        if (InOrder)
            interactTable.ShowSpaceKey();
        else
            interactTable.HideSpaceKey();
    }

    public void CleanInteract(UseTable table)
    {
        table.CleanSeat();
    }
}
