using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectContainer
{
    public GameObject prefab;
    public List<GameObject> container;
    public int capacity;
    public int oldest;

    public ObjectContainer(GameObject _prefab, int _capacity)
    {
        prefab = _prefab;
        capacity = _capacity;
        container = new List<GameObject>();
        oldest = 0;

        for (int i = 0; i < capacity; i++)
        {
            
            container.Add(MonoBehaviour.Instantiate(prefab, new Vector3(0, -5, 0), Quaternion.identity));
        }
    }

    public ObjectContainer(GameObject _prefab, int _capacity, int depth)
    {
        prefab = _prefab;
        capacity = _capacity;
        container = new List<GameObject>();
        oldest = 0;

        for (int i = 0; i < capacity; i++)
        {

            container.Add(MonoBehaviour.Instantiate(prefab, new Vector3(0, -5, depth), Quaternion.identity));
        }
    }

    public void Cycle()
    {
        if (oldest == capacity - 1)
        {
            oldest = 0;
        }
        else
        {
            oldest++;
        }
    }

    public GameObject this[int index]
    {
        get
        {
            return container[index];
        }

        set
        {
            container[index] = value;
        }
    }

    public void Activate()
    {
        container[oldest].SetActive(true);
    }

    public GameObject GetOldest()
    {
        return container[oldest];
    }

}
