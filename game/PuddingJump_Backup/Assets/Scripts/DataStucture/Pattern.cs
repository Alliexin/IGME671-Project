using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pattern
{
    public float height;
    public string name;
    public List<Platform> platforms = new List<Platform>();
    public List<Enemy> enemies = new List<Enemy>();
}