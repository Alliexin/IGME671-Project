using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    GameObject scoreUI;
    GameObject deathUI;
    GameObject chargeUI;
    private float score = 0;
    int deathCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.OnPlayerDie += Die;
        EventSystem.current.OnPlayerEatFood += AddFoodScore;

        scoreUI = gameObject.transform.Find("Score").gameObject;
        deathUI = gameObject.transform.Find("Death").gameObject;
        chargeUI = gameObject.transform.Find("Charge").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        scoreUI.GetComponent<Text>().text = "Height: " + (Math.Round(CameraMovement.current.transform.position.y, 1) - 5 + "m");
        deathUI.GetComponent<Text>().text = "Death: " + deathCount;
        //chargeUI.GetComponent<Text>().text = "sp: " + PlayerMovement.getInstance().sp + " charges: " + PlayerMovement.getInstance().charges;
    }

    void AddFoodScore()
    {
        score += 10;
    }

    void Die()
    {
        deathCount++;
    }
}
