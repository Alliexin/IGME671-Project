using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerMovement : MonoBehaviour
{
    //Singleton
    private static PlayerMovement instance;

    [Header("Module")]
    public Swipe swipeControls;

    [Header("Physics")]
    public Vector2 pos;
    public Vector2 vel;
    public Vector2 acc;

    private float xVel;

    [Header("Stats")]
    //Stats
    public float mass;
    public float maxSpeed;
    public float baseJumpForce;
    public float baseJumpVel;
    protected float jumpForce;
    protected float jumpVel;

    public float gravity;




    public bool no_gravity;
    public bool control_is_disabled;


    public static PlayerMovement getInstance()
    {
        return instance;
    }

    // Start is called before the first frame update
    protected void Start()
    {
        instance = this;

        no_gravity = false;
        control_is_disabled = false;

        jumpForce = baseJumpForce;

        EventSystem.current.OnPlayerDie += Die;
        
        pos = transform.position;
        vel = Vector2.zero;
        acc = Vector2.zero;

        CameraMovement.current.height = pos.y + CameraMovement.current.distance;
        CameraMovement.current.pos = new Vector3(CameraMovement.current.pos.x, CameraMovement.current.height, -10);

        addForce(new Vector2(0, 800));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        //add force of gravity
        if (!no_gravity)
        {
            addForce(new Vector2(0, -gravity));
        }


        //get x-axis input
        if (!control_is_disabled)
        {
            //xVel = Input.acceleration.x;
            xVel = Input.GetAxis("Horizontal") * 0.5f;
        }

        //compute acc and clamp y magnitude
        vel += acc * Time.deltaTime;

        if (vel.y > maxSpeed)
        {
            vel = new Vector2(vel.x, maxSpeed);
        }

        //add horizontal velocity
        vel += new Vector2(xVel, 0);

        //Limit x-axis velocity
        if(Math.Abs(vel.x) > maxSpeed)
        {
            if(vel.x > 0)
            {
                vel = new Vector2(maxSpeed, vel.y);
            }

            if (vel.x < 0)
            {
                vel = new Vector2(-maxSpeed, vel.y);
            }
        }

        //Add velocity to position
        pos += vel * Time.deltaTime;

        //warp player if needed
        warp();

        //set position
        transform.position = pos;

        //reset acc
        acc = Vector2.zero;
    }

    public void addForce(Vector2 force)
    {
        acc += force / mass;
    }

    protected void Jump()
    {
        //Force
        //addForce(new Vector2(0, jumpForce));

        //Set vel
        vel = new Vector2(xVel, jumpVel);

        if(control_is_disabled)
            control_is_disabled = false;

        //Reset stats
        jumpForce = baseJumpForce;
        jumpVel = baseJumpVel;
    }

    void warp()
    {
        if (pos.x > 2.5f)
        {
            pos = new Vector2(-2.45f, pos.y);
        }

        if (pos.x < -2.5f)
        {
            pos = new Vector2(2.45f, pos.y);
        }
    }

    void Die()
    {
        vel = Vector2.zero;
        pos = new Vector3(0, CameraMovement.current.transform.position.y - 2, 0);
        transform.position = pos;
        control_is_disabled = false;

        //addForce(new Vector2(0, 300));

        Jump();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            if (vel.y < 0)
            {
                vel = Vector2.zero;
                gameObject.GetComponentInChildren<Squash>().SquashPudding();
                Jump();
                EventSystem.current.Bounce();
            }
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            if (vel.y < 0f)
            {
                vel = Vector2.zero;
                gameObject.GetComponentInChildren<Squash>().SquashPudding();
                Jump();
                EventSystem.current.Bounce();
            }
        }
    }


}
