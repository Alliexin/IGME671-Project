using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddingMovement : PlayerMovement
{
    public float slamMultiplier;
    public float slamSpeed;

    public int charges;
    public int sp;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        EventSystem.current.OnPlayerEatFood += addCharge;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || swipeControls.SwipeDown)
        {
            StartCoroutine(Slam());
        }
    }
    public IEnumerator Slam()
    {
        if (sp > 0)
        {
            no_gravity = true;
            control_is_disabled = true;
            vel = Vector2.zero;

            sp--;

            EventSystem.current.PlayerUseAbility();

            yield return new WaitForSeconds(0.1f);

            base.jumpForce = baseJumpForce * slamMultiplier;
            base.jumpVel = baseJumpVel * slamMultiplier;

            vel = new Vector2(0, -slamSpeed);
            no_gravity = false;
        }
    }

    public void addCharge()
    {
        if (charges < 3 && sp <= 3)
        {
            charges++;

            if (sp == 3 && charges == 3)
            {
                charges = 2;
                return;
            }

            if (charges == 3 && sp < 3)
            {
                charges = 0;
                sp++;
                EventSystem.current.PlayerGainSkillPoint();
            }
        }
    }
}
