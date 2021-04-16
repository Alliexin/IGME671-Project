using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squash : MonoBehaviour
{

    public float minY;
    public float maxX;
    public float speed;
    public float noise;

    private float xScale;
    private float yScale;

    private Vector2 startScale;

    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale;
        EventSystem.current.OnBounce += SquashSprite;

        xScale = yScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Recover();
    }

    private void FixedUpdate()
    {
        UpdateScale();
    }

    public void SquashSprite()
    {
        if(xScale == 1 && yScale == 1)
        {
            xScale = maxX;
            yScale = minY;
        }
    }

    public void SquashPudding()
    {
        if (xScale == 1 && yScale == 1)
        {
            LeanTween.scale(gameObject, new Vector3(maxX + UnityEngine.Random.Range(-noise,noise), minY + UnityEngine.Random.Range(-noise, noise), 1), speed);
        }
    }

    private void Recover()
    {
        if (true)
        {
            if (xScale > 1f)
            {
                xScale -= speed;
            }
            else
            {
                xScale = 1f;
            }

            if (yScale < 1f)
            {
                yScale += speed;
            }
            else
            {
                yScale = 1f;
            }
        }
    }


    private void UpdateScale()
    {
        transform.localScale = new Vector2(xScale, yScale);
    }
}
