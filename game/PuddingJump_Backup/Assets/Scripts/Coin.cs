using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{
    public Vector3 dir;
    public float delay;
    private float collect_time;
    public override void Collected()
    {
        if (!isCollected)
        {
            collect_time = Time.time + delay;
            isCollected = true;
            LeanTween.rotateAroundLocal(gameObject, new Vector3(0, 0, 1), 720, 1).setEase(LeanTweenType.easeOutBack);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollected)
        {
            dir = (PlayerMovement.getInstance().transform.position - transform.position).normalized;
            spd += Time.deltaTime * speed;

            if (Vector2.Distance(transform.position, PlayerMovement.getInstance().pos) < 0.3f && collect_time < Time.time)
            {
                spd = 0;
                collect_time = Time.time + delay;
                gameObject.SetActive(false);
                isCollected = false;
                //PlayerMovement.getInstance().charges++;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isCollected)
        {
            MoveTo(PlayerMovement.getInstance().pos);
        }
    }
}
