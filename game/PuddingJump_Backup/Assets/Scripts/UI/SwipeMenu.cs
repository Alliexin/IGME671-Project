using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    public GameObject parent;

    public GameObject scrollbar;
    protected float scroll_pos;

    protected float[] pos;
    protected float distance;

    /*
     Position:
     0: Shop
     1: Pudding selection
     2: Play
     3: Upgrade
     4: Settings
     */
    public int position;

    protected bool swipe_left;
    protected bool swipe_right;

    protected Vector2 touch_start_pos;
    protected Vector2 touch_end_pos;

    protected Vector3 mouse_start_pos;
    protected Vector3 mouse_end_pos;

    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        pos = new float[transform.childCount];
        distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        position = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //Swipe control with direction check
        if (isActive)
        {//Mouse control
            if (Input.GetMouseButton(0))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    mouse_start_pos = Input.mousePosition;
                }
                scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
            }
            else
            {
                if (Input.GetMouseButtonUp(0) && Input.mousePosition.y > 200f)
                {
                    Debug.Log("up");
                    mouse_end_pos = Input.mousePosition;
                    Vector2 direction = new Vector2(mouse_start_pos.x - mouse_end_pos.x, mouse_start_pos.y - mouse_end_pos.y);
                    if (direction.magnitude > 150)
                    {
                        if (mouse_start_pos.x > mouse_end_pos.x)
                        {
                            Debug.Log("Right");
                            if (position < 4)
                                position++;
                        }
                        else
                        {
                            Debug.Log("Left");
                            if (position > 0)
                                position--;
                        }
                    }
                }

                scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[position], 0.1f);
            }


            //Touch control
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    touch_start_pos = touch.position;
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
                }
                else
                {
                    if (touch.phase == TouchPhase.Ended && Input.mousePosition.y > 200f)
                    {
                        touch_end_pos = touch.position;
                        Vector2 direction = touch_end_pos - touch_start_pos;
                        if (direction.magnitude > 150)
                        {
                            if (touch_start_pos.x > touch_end_pos.x)
                            {
                                if (position < 4)
                                    position++;
                            }
                            else
                            {
                                if (position > 0)
                                    position--;
                            }
                        }
                    }

                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[position], 0.1f);
                }
            }

        }
    }
}
