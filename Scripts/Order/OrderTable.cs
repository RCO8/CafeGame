using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderTable : MonoBehaviour
{
    [SerializeField] private GameObject SpaceKey;
    public Guest IsInGuest { get; private set; }

    private void Start()
    {
        HideSpaceKey();
    }

    public void EnterGuest(Guest guest)
    {
        //새로운 손님이 들어오도록
        IsInGuest = guest;
    }

    public void ExitGuest()
    {
        if (IsInGuest)
            IsInGuest = null;
    }

    public void ShowSpaceKey()  //인터페이스 UI
    {
        if(!MenuManager.instance.IsOpen)
            SpaceKey.SetActive(true);
    }

    public void HideSpaceKey()
    {
        SpaceKey.SetActive(false);
    }
}
