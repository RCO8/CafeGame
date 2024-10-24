using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Coffee
{
    Americano,
    Cafelatte,
    Cafuccino,
    Vanillalatte,
    Cafemocca,
    CaremelMaciatto,
    Dolcelatte
}

public enum Desert
{
    Donut,
    PieceCake,
    Sandwitch,
    Hotdog,
    OneCake
}

public class OrderSO : ScriptableObject
{
    public string Name;
    public int Price;
    public float MakingTime;
    public Sprite Image;
}
