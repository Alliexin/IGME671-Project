using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FMODUnity;
using FMOD.Studio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager manager;
    public StudioEventEmitter bgm;
    public float hype;

    // Start is called before the first frame update
    void Start()
    {
        manager = this;
    }

    // Update is called once per frame
    void Update()
    {
        bgm.SetParameter("Hype", hype / 100f);
        if (hype > 0f)
            hype -= 7.5f * Time.deltaTime;
    }
}
