using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.rotateAroundLocal(gameObject, new Vector3(0, 0, 1), 360, 10).setLoopClamp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
