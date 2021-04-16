using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    public enum EnemyType
    {
        bouncy,
        floaty,
        floaty_h,
        floaty_v
    }

    public EnemyType type;
    public Vector2 pos;
}