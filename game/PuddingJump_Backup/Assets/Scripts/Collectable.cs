using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    
    public float speed;
    protected float spd;

    public bool isCollected;

    // Start is called before the first frame update
    void Start()
    {
        isCollected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    abstract public void Collected();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!isCollected)
            {
                Collected();
            }
        }
        
    }

    protected void MoveTo(Vector3 target)
    {
        Vector2 dir = target - transform.position;
        transform.position += new Vector3(dir.x, dir.y, 0).normalized * spd * Time.deltaTime;
    }
}
