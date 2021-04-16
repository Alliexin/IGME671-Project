using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement current;

    public bool isDisabled;

    public float height;

    
    public Vector3 pos;
    public float distance;
    public float minHeight;
    public Vector3 origin;

    // Start is called before the first frame update
    private void Awake()
    {
        current = this;
    }
    void Start()
    {
        height = minHeight;
        pos = new Vector3(transform.position.x, height, -10f);
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerMovement.getInstance().pos.y + distance > height)
        {
            height = PlayerMovement.getInstance().pos.y + distance;
            if(height < minHeight)
            {
                height = minHeight;
            }
            pos = new Vector3(transform.position.x, height, -10f);
        }

        transform.position = pos;
    }

    public void MoveToOrigin()
    {
        height = minHeight;
        pos = origin;
    }
}
