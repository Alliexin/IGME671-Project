using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Platform
{
    public enum PlatformType
    {
        normal,
        moving,
        sequential,
        dissolve,
        switch_a,
        switch_b
    }

    public PlatformType type;
    public Vector2 pos;

    public int coin;
    public int food;
}
