using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public static EventSystem current;

    private void Awake()
    {
        current = this;
    }

    public event Action OnPlayerDie;
    public void PlayerDie()
    {
        if(OnPlayerDie != null)
        {
            OnPlayerDie();
        }
    }

    public event Action OnBounce;
    public void Bounce()
    {
        if (OnBounce != null)
        {
            OnBounce();
        }
    }

    public event Action OnPlayerEatFood;
    public void PlayerEatFood()
    {
        if(OnPlayerEatFood != null)
        {
            OnPlayerEatFood();
        }
    }

    public event Action OnPlayerGetsHigher;

    public void PlayerGetsHigher()
    {
        if(OnPlayerGetsHigher!= null)
        {
            OnPlayerGetsHigher();
        }
    }

    public event Action OnPlayerGainSkillPoint;

    public void PlayerGainSkillPoint()
    {
        if (OnPlayerGainSkillPoint != null)
        {
            OnPlayerGainSkillPoint();
        }
    }

    public event Action OnPlayerUseAbility;

    public void PlayerUseAbility()
    {
        if (OnPlayerUseAbility != null)
        {
            OnPlayerUseAbility();
        }
    }

    public event Action OnPlayerTap;
    public void PlayerTap()
    {
        if (OnPlayerTap != null)
        {
            OnPlayerTap();
        }
    }

}
