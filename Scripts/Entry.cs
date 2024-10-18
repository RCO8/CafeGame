using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : MonoBehaviour
{
    //출입구로 손님 생성 역할

    //스프라이트는 랜덤(?) 이유는 사람마다 다르니까

    //오브젝트 풀
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;

    //쿨타임
    private float coolTime = 2f;
    private float currentTime = 0f;
    private int seatNum;    //자리에 맞게 인원수 입장
    private int _guestNum;  //현재 매장의 인원수

    private void Start()
    {
        //오브젝트 풀
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach(var pool in Pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.name += $"_{i}";
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            PoolDictionary.Add(pool.tag, objectPool);
        }

        seatNum = GameManager.instance.SeatManager.BeginSeat;
        _guestNum = 0;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > coolTime)
        {
            SpawnGuest("one");
            currentTime = 0f;
        }
    }

    public void SpawnGuest(string tag)
    {
        if (!PoolDictionary.ContainsKey(tag))
            return;
        if (seatNum > _guestNum)
        {
            GameObject obj = PoolDictionary[tag].Dequeue();
            PoolDictionary[tag].Enqueue(obj);
            obj.SetActive(true);
            _guestNum++;
        }
    }

    public void ExitGuest() => _guestNum--;
}
