using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdController : MonoBehaviour
{
    
    public static float _birdSpeed = 2.0f;

    public static birdController birdObject;

    // Start is called before the first frame update
    void Awake()
    {
        if (birdObject == null)
        {
            birdObject = this;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //To calculate distance between duck and target
        var distance = (duckTarget.obj.transform.position - this.transform.position);
        //Debug.Log(distance.magnitude);

        //To avoid glitch if the distance between duck and target is too less
        if (distance.magnitude < 1.5f)
        {
            duckTarget.obj.move();
        }


        transform.LookAt(duckTarget.obj.transform);
        transform.Translate(0, 0, _birdSpeed * Time.deltaTime);
    }
}