using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMainMenu : SwipeMenu
{
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
                        UpdateNavBar();
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
                            UpdateNavBar();
                        }
                    }

                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[position], 0.1f);
                }
            }

        }
    }

    public void MoveToShop()
    {
        position = 0;
        Debug.Log("Pressed");
    }

    public void MoveToPudding()
    {
        position = 1;
    }

    public void MoveToPlay()
    {
        position = 2;
    }

    public void MoveToUpgrade()
    {
        position = 3;
    }

    public void MoveToSettings()
    {
        position = 4;
    }

    public void UpdateNavBar()
    {
        switch (position)
        {
            case 0:
                NavBarTweening.current.ShopButtonPressed();
                break;
            case 1:
                NavBarTweening.current.PuddingButtonPressed();
                break;
            case 2:
                NavBarTweening.current.PlayButtonPressed();
                break;
            case 3:
                NavBarTweening.current.UpgradeButtonPressed();
                break;
            case 4:
                NavBarTweening.current.OptionButtonPressed();
                break;
            default:
                Debug.Log("Update NavBar Error");
                break;
        }
    }

}
