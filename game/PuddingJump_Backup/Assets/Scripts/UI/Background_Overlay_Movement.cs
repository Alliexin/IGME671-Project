using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background_Overlay_Movement : MonoBehaviour
{
    public GameObject[] images;
    public float speed;
    private float endpoint;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        endpoint = images[0].transform.position.x;
        distance = images[2].transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < images.Length; i++)
        {
            //Debug.Log(images[0].transform.position.x);

            if (images[i].transform.position.x < endpoint)
            {
                
                if(i == 0)
                {
                    images[i].transform.position = images[2].transform.position + new Vector3(distance, 0, 0);
                }
                else
                {
                    images[i].transform.position = images[i-1].transform.position + new Vector3(distance, 0, 0);
                }
            }
            images[i].transform.position -= Vector3.left * speed * Time.deltaTime;
        }
    }
}
