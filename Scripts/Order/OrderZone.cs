using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderZone : MonoBehaviour
{
    private OrderTable tableParent;
    private LayerMask guest;

    private void Awake()
    {
        tableParent = GetComponentInParent<OrderTable>();
        guest = LayerMask.NameToLayer("Guest");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == guest)
        {
            Guest checkGuest = collision.GetComponent<Guest>();
            if (tableParent.IsInGuest == null)  //손님이 없으면 들어와
            {
                tableParent.EnterGuest(checkGuest);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == guest)
        {
            Guest checkGuest = collision.GetComponent<Guest>();
            if (!checkGuest.exitCafe)    //주문할것 아니면
                tableParent.ExitGuest();
        }
    }
}
