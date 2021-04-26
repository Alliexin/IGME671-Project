using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public Vector3 dir;
    private float speed = 10f;
    private float life = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Move
        transform.position += dir * speed * Time.deltaTime;

        //Check removal
        if (life < 0f)
            gameObject.SetActive(false);

        //Update life
        life -= Time.deltaTime;
    }
}
