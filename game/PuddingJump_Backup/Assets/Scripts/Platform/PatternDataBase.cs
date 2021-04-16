using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PatternDataBase : MonoBehaviour
{
    public static PatternDataBase current;

    [SerializeField]
    public List<Pattern> patterns = new List<Pattern>(5);

    private void Awake()
    {
        current = this;
    }

    public void Add()
    {
        patterns.Add(new Pattern());
    }

    public void Remove()
    {
        patterns.RemoveAt(patterns.Count - 1);
    }
}
