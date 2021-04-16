using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float start;
    public float end;
    public float height;

    public GameObject dot_start;
    public GameObject dot_end;

    public GameObject dot_endpoint_prefab;
    public GameObject dot_prefab;
    public List<GameObject> dot_pool;
    public int dot_pool_capacity;

    public float dot_delta;
    public float speed;
    public int direciton;

    public float distance_min;
    private float distance_edge = 2;

    private void Awake()
    {
        Vector3 pos = new Vector3(0, -1, 0);
        Quaternion qua = Quaternion.identity;

        dot_start = Instantiate(dot_endpoint_prefab, pos, qua);
        dot_end = Instantiate(dot_endpoint_prefab, pos, qua);

        dot_pool_capacity = (int)(4f / dot_delta) + 2;

        for (int i = 0; i < dot_pool_capacity; i++)
        {
            dot_pool.Add(Instantiate(dot_prefab, pos, qua));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBlockPosition();
    }

    public void SetHeight(float height)
    {
        this.height = height;

        Randomize();

        UpdateDottedLine();

        SetBlockPosition();

        SetDirection(1);

        SetSpeed(Random.Range(1f, 2f));
    }

    void Randomize()
    {
        start = Random.Range(-distance_edge, distance_edge - distance_min - 0.2f);
        end = Random.Range(start + distance_min, distance_edge);
    }

    void UpdateDottedLine()
    {
        dot_start.transform.position = new Vector2(start, height);
        dot_end.transform.position = new Vector2(end, height);

        int i = 0;

        float temp = start + dot_delta;

        while (temp < end)
        {
            dot_pool[i].transform.position = new Vector2(temp, height);
            temp += dot_delta;
            i++;
        }
    }

    void SetBlockPosition()
    {
        transform.position = new Vector2(Random.Range(start + .5f, end - .5f), height);
    }

    void UpdateBlockPosition()
    {
        transform.position += new Vector3(direciton * speed, 0, 0) * Time.deltaTime;
        if(transform.position.x > end)
        {
            direciton = -1;
        }
        if(transform.position.x < start)
        {
            direciton = 1;
        }
    }

    void SetDirection(int num)
    {
        if(num > 0)
        {
            direciton = 1;
        }
        else
        {
            direciton = -1;
        }
    }

    void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
