using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duckTarget : MonoBehaviour
{
    public static duckTarget obj;
    public float targetDifficulty = 2.25f;

    void Start()
    {
        if(obj == null)
        {
            obj = this;
        }

        //Initial position
        transform.position = new Vector3(3.7f, 1.0f, -1.2f);
    }

    public void move()
    {
        float xValue = Random.Range(-5.0f, 5.0f);
        float yValue = Random.Range(2.2f, targetDifficulty);
        float zValue = Random.Range(-5.0f, 5.0f);

        transform.localPosition = new Vector3(xValue, yValue, zValue);
    }

    public void OnTriggerEnter(Collider box)
    {
        if(box.tag == "duck")
        {
            move();
        }
    }
}
