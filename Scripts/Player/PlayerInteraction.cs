using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    private void Awake()
    {
        controller = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LayerMask sitting = LayerMask.NameToLayer("Sitting");
        if (collision.gameObject.layer == sitting)
        {
            UseTable cleanSeat = collision.gameObject.GetComponent<UseTable>();
            controller.CleanInteract(cleanSeat);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        LayerMask order = LayerMask.NameToLayer("Order");
        if (collision.gameObject.layer == order)
            controller?.OrderInteract(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        controller?.OrderInteract(false);
    }
}
