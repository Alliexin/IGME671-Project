using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using FMODUnity;
using FMOD.Studio;

public class ButtonSound : MonoBehaviour, IPointerDownHandler
{

    [FMODUnity.EventRef]
    public string path;
    EventInstance useSound;

    // Start is called before the first frame update
    void Start()
    {
        useSound = RuntimeManager.CreateInstance(path);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        useSound.start();
    }
    
}
