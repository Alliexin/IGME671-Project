using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class NavBarButton
{
    public RectTransform button;
    public Vector2 origin;
    public bool dirty;
    public float time;
    public bool selected;

    public NavBarButton(RectTransform _button, Vector2 _origin, float _time)
    {
        button = _button;
        origin = _origin;
        dirty = false;
        time = _time;
        selected = false;
    }

    public void Move(Vector2 des)
    {
        LeanTween.move(button, des, time).setEaseInOutCirc();
        dirty = true;
    }

    public void Reset()
    {
        if (dirty)
        {
            LeanTween.move(button, origin, time);
            dirty = false;
        }
    }
}

public class NavBarTweening : MonoBehaviour
{
    public static NavBarTweening current;

    NavBarButton shop;
    NavBarButton pudding;
    NavBarButton play;
    NavBarButton upgrade;
    NavBarButton option;

    public RectTransform Indicator;
    public RectTransform ShopButton;
    public RectTransform PuddingButton;
    public RectTransform PlayButton;
    public RectTransform UpgradeButton;
    public RectTransform SettingsButton;

    public Vector2 shop_pos;
    public Vector2 pudding_pos;
    public Vector2 play_pos;
    public Vector2 upgrade_pos;
    public Vector2 setting_pos;

    public float tween_time;

    void Start()
    {
        current = this;

        shop = new NavBarButton(ShopButton, shop_pos, tween_time);
        pudding = new NavBarButton(PuddingButton, pudding_pos, tween_time);
        play = new NavBarButton(PlayButton, play_pos, tween_time);
        play.selected = true;
        upgrade = new NavBarButton(UpgradeButton, upgrade_pos, tween_time);
        option = new NavBarButton(SettingsButton, setting_pos, tween_time);
    }

    public void ShopButtonPressed()
    {
        if (!shop.selected)
        {
            UnselectAll();

            LeanTween.move(Indicator, new Vector2(-276, 0), tween_time).setEaseInOutCirc();

            shop.Move(new Vector2(-276, 0));
            shop.selected = true;

            pudding.Move(new Vector2(-69, 0));

            play.Move(new Vector2(69, 0));

            upgrade.Reset();
            option.Reset();
        }
    }

    public void PuddingButtonPressed()
    {
        if (!pudding.selected)
        {
            UnselectAll();

            LeanTween.move(Indicator, new Vector2(-138, 0), tween_time).setEaseInOutCirc();

            shop.Reset();

            pudding.Move(new Vector2(-138, 0));
            pudding.selected = true;

            play.Move(new Vector2(69, 0));

            upgrade.Reset();
            option.Reset();
        }
    }

    public void PlayButtonPressed()
    {
        if (!play.selected)
        {
            UnselectAll();

            LeanTween.move(Indicator, new Vector2(0, 0), tween_time).setEaseInOutCirc();

            shop.Reset();
            pudding.Reset();

            play.Reset();
            play.selected = true;

            upgrade.Reset();
            option.Reset();
        }
    }

    public void UpgradeButtonPressed()
    {
        if (!upgrade.selected)
        {
            UnselectAll();

            LeanTween.move(Indicator, new Vector2(138, 0), tween_time).setEaseInOutCirc();

            shop.Reset();
            pudding.Reset();

            play.Move(new Vector2(-69, 0));

            upgrade.Move(new Vector2(138, 0));
            upgrade.selected = true;

            option.Reset();
        }
    }

    public void OptionButtonPressed()
    {
        if (!option.selected)
        {
            UnselectAll();

            LeanTween.move(Indicator, new Vector2(276, 0), tween_time).setEaseInOutCirc();

            shop.Reset();
            pudding.Reset();

            play.Move(new Vector2(-69, 0));

            upgrade.Move(new Vector2(69, 0));

            option.Move(new Vector2(276, 0));
            option.selected = true;
        }
    }

    public void UnselectAll()
    {
        shop.selected = false;
        pudding.selected = false;
        play.selected = false;
        upgrade.selected = false;
        option.selected = false;
    }
}
