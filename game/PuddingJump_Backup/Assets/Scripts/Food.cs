using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Collectable
{
    public Vector3 dir;

    public override void Collected()
    {
        if (!isCollected)
        {
            isCollected = true;
            EventSystem.current.PlayerEatFood();
            LeanTween.pause(gameObject);
            LeanTween.scale(gameObject, new Vector3(0.1f, 0.1f, 1), 0.5f).setEaseInBounce().setOnComplete(Deactivate);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spd = 0;
        ChangeSprite();
        LeanTween.scale(gameObject, new Vector3(0.42f, 0.42f, 1f), 1f).setLoopPingPong();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Deactivate()
    {
        ChangeSprite();
        isCollected = false;
        gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 1);
        gameObject.SetActive(false);
    }

    private void ChangeSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = ObjectManager.getInstance().food_fruit_sprites[Random.Range(0, 6)];
    }
}
