using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseTable : MonoBehaviour
{
    public Guest HasGuest { get; private set; } = null;   //자리에 손님이 있는지

    [SerializeField] private SpriteRenderer hasDrink;    //손님이 있으면 음료도 있지
    [SerializeField] private GameObject Clean;

    public bool Clear {  get; private set; }

    private void Awake()
    {
        Clear = true;   //방어코드
        hasDrink.sprite = null;

        //UI세팅
        Clean.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        LayerMask guest = LayerMask.NameToLayer("Guest");

        if (collision.gameObject.layer == guest)
        {
            if(!Clear)
                ExitGuest();
        }
        Clean.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Person"))
        {
            if (!HasGuest && !Clear)
                Clean.SetActive(true);
        }
    }

    public void EnterGuest(Guest guest)
    {
        HasGuest = guest;

        hasDrink.sprite = HasGuest.wantOrder.Image;
        Clear = false;
    }

    public void ExitGuest()
    {
        HasGuest = null;
    }

    public void CleanSeat()
    {
        //스페이스바를 누르면 청소
        if (!HasGuest)
        {
            hasDrink.sprite = null;
            Clear = true;
        }
    }
}
