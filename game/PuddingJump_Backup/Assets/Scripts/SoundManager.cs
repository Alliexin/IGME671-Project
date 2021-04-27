using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FMODUnity;
using FMOD.Studio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager manager;

    public StudioEventEmitter menuBgm;
    public StudioEventEmitter gameBgm;
    public float hype;

    // Start is called before the first frame update
    void Start()
    {
        manager = this;
        EventSystem.current.OnPlayerDie += ClearHype;
    }

    // Update is called once per frame
    void Update()
    {
        gameBgm.SetParameter("Hype", hype / 100f);
        if (hype > 0f)
            hype -= 7.5f * Time.deltaTime;
    }

    public void PlayGameMusic()
    {
        if (menuBgm.IsPlaying())
        {
            menuBgm.Stop();
            gameBgm.Play();
        }
    }


    public void PlayMenuMusic()
    {
        if (gameBgm.IsPlaying())
        {
            gameBgm.Stop();
            menuBgm.Play();
        }
    }

    private void ClearHype()
    {
        hype = 0f;
    }
}
