using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;
using FMOD.Studio;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    EventInstance shootSound;
    [FMODUnity.EventRef]
    public string path;

    // Start is called before the first frame update
    void Start()
    {
        shootSound = RuntimeManager.CreateInstance(path);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            GameObject g = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            g.GetComponent<Bullets>().dir = (CameraMovement.current.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)) - transform.position).normalized;
            g.GetComponent<Bullets>().dir.z = 0;
            shootSound.start();
        }
    }
}
