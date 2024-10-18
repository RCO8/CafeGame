using System.Collections.Generic;
using UnityEngine;

public class AddonSeat : UpgradeSystem
{
    [SerializeField] private Transform TableGroup;
    [SerializeField] private GameObject UseTable;
    private List<UseTable> useTables;

    public int BeginSeat { get; private set; } = 3;
    const int MaxSeat = 10;

    private void Awake()
    {
        CurLevel = 1;

        PayLevels[0] = 20000;
        PayLevels[1] = 30000;
        PayLevels[2] = 50000;
        PayLevels[3] = 70000;
        PayLevels[4] = 90000;
        PayLevels[5] = 110000;
        PayLevels[6] = 140000;
        PayLevels[7] = 170000;
        PayLevels[8] = 200000;

        useTables = new List<UseTable>();
        SettingSeats();
        Apply();
    }

    public void Upgrade()
    {
        //PayLevels상수 참고
        if (CurLevel < MaxSeat)
        {
            if (GameManager.instance.UseMoney(PayLevels[CurLevel - 1]))
            {
                //자리 추가
                useTables[BeginSeat+CurLevel].gameObject.SetActive(true);
                CurLevel++;
                Apply();
            }
        }
    }

    protected override void Apply()
    {
        base.Apply();
    }

    private void SettingSeats() //초기에 자리여부 설정
    {
        int i;
        for (i = 0; i < TableGroup.childCount; i++)
        {
            GameObject table = Instantiate(UseTable, TableGroup.GetChild(i));
            useTables.Add(table.GetComponent<UseTable>());
            table.SetActive(false);
        }
        for (i = 0; i < BeginSeat; i++)
            useTables[i].gameObject.SetActive(true);
    }

    public Vector2 FindTable(Guest guest)
    {
        //여기서 가까운 자리 찾기
        SeatList findSeat = new SeatList();

        for (int i = 0; i < BeginSeat + (CurLevel-1); i++)
        {
            if (useTables[i].Clear)
            {
                findSeat.AddonSeat(guest.transform.position, useTables[i].transform.position);
            }
        }

        if (findSeat == null)
            return guest.transform.position;
        else
            return findSeat.NearDistance();
    }
}

class SeatList
{
    List<SeatData> seats = new List<SeatData>();

    public void AddonSeat(Vector2 startPos, Vector2 targetPos)
    {
        seats.Add(new SeatData(startPos, targetPos));
    }

    public Vector2 NearDistance()
    {
        float minimum = 999f;   //최단거리
        int seatIndex = 0;  //해당되는 자리 인덱스

        for (int i = 0; i < seats.Count; i++)
        {
            if (minimum > seats[i].seatDistance)
            {
                minimum = seats[i].seatDistance;
                seatIndex = i;
            }
        }

        return seats[seatIndex].seatPos;
    }
}

class SeatData
{
    public Vector2 seatPos;
    public float seatDistance;

    public SeatData(Vector2 startPos, Vector2 targetPos)
    {
        seatPos = targetPos;
        seatDistance = Vector2.Distance(startPos, targetPos);
    }
}