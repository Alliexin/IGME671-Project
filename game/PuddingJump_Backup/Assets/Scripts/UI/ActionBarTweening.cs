using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Networking;

public class ActionBarTweening : MonoBehaviour
{
    public GameObject high;
    public GameObject mid;
    public GameObject low;
    public GameObject first_L;
    public GameObject first_R;
    public GameObject second_L;
    public GameObject second_R;

    public CanvasGroup canvas;

    public float grow_time;
    public float shrink_time;

    public int charges;
    public int sp;

    public float display_timer;
    public float display_duration;
    public float emerge_duration;
    public float fade_duration;
    public int fadeTweenId;

    void Start()
    {
        //charges = PlayerMovement.getInstance().charges;
        //sp = PlayerMovement.getInstance().sp;

       
        EventSystem.current.OnPlayerEatFood += Charge;
        EventSystem.current.OnPlayerEatFood += AddSkillPoint;
        EventSystem.current.OnPlayerEatFood += DisplayUI;

        EventSystem.current.OnPlayerUseAbility += SpendSkillPoint;
        EventSystem.current.OnPlayerUseAbility += UpdateChargeShrink;
        EventSystem.current.OnPlayerUseAbility += UpdateSP;
        EventSystem.current.OnPlayerUseAbility += DisplayUI;

        EventSystem.current.OnPlayerTap += DisplayUI;

        Init();

    }

    // Update is called once per frame
    void Update()
    {
        if(display_timer > 0f)
        {
            display_timer -= Time.deltaTime;
            if(canvas.alpha < 1)
            {
                LeanTween.alphaCanvas(canvas, 1, emerge_duration);
            }
        }
        else
        {
            display_timer = 0;

            LeanTween.alphaCanvas(canvas, 0, fade_duration);

        }
    }

    void Init()
    {
        if(charges < 1 && sp < 1)
        {
            Empty();
            LeanTween.alphaCanvas(canvas, 0, fade_duration);
            return;
        }
    }

    void Grow(GameObject g)
    {
        g.SetActive(true);
        LeanTween.scale(g, new Vector3(1, 1, 1), grow_time).setEaseOutExpo();
    }

    void Shrink(GameObject g)
    {
        LeanTween.scale(g, new Vector3(0f, 0f, 1), shrink_time);
    }

    void Squash(GameObject g)
    {
        LeanTween.scale(g, new Vector3(1f, 0f, 1), shrink_time);
    }

    void Stretch(GameObject g)
    {
        g.SetActive(true);
        LeanTween.scale(g, new Vector3(1, 1, 1), grow_time).setEaseOutCirc();

    }

    void AddSkillPoint()
    {
        
        if (sp == 3 || charges < 3)
        {
            return;
        }

        Debug.Log("SP");

        Shrink(first_L);
        Shrink(first_R);
        Shrink(second_L);
        Shrink(second_R);
        charges = 0;
        sp++;

        if (sp == 1)
        {
            Stretch(low);
        }
        if (sp == 2)
        {
            Stretch(mid);
        }
        if (sp == 3)
        {
            Stretch(high);
        }
    }

    void UpdateSP()
    {
        if (sp == 2)
        {
            Squash(high);
        }
        if (sp == 1)
        {
            Squash(high);
            Squash(mid);
        }
        if (sp == 0)
        {
            Squash(high);
            Squash(mid);
            Squash(low);
        }
    }

    void Charge()
    {
        if(charges < 3 || (sp == 3 && charges < 2))
            charges++;

        UpdateChargeGrow();
    }

    void UpdateChargeGrow()
    {
        if (charges == 1)
        {
            Grow(first_L);
            Grow(first_R);
        }
        if (charges == 2)
        {
            Grow(first_L);
            Grow(first_R);
            Grow(second_L);
            Grow(second_R);
        }
    }

    void UpdateChargeShrink()
    {
        if (charges == 1)
        {
            Grow(first_L);
            Grow(first_R);
        }
        if (charges == 2)
        {
            Grow(first_L);
            Grow(first_R);
            Grow(second_L);
            Grow(second_R);
        }
    }

    void SpendSkillPoint()
    {
        sp--;

        if (sp == 2)
        {
            Squash(high);
        }
        if(sp == 1)
        {
            Squash(mid);
        }
        if (sp == 0)
        {
            Squash(low);
        }
    }

    void Empty()
    {
        Shrink(first_L);
        Shrink(first_R);
        Shrink(second_L);
        Shrink(second_R);
        Squash(low);
        Squash(mid);
        Squash(high);
    }

    void DisplayUI()
    {
        display_timer = display_duration;
    }

    private void OnDestroy()
    {
    }
}
