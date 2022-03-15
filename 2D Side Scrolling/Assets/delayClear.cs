using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delayClear : MonoBehaviour
{
    public float secondClear = 0.15f;
    float starTime;
    void Start()
    {
        starTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - starTime >= secondClear)
        {
            Destroy(this.gameObject);
        }
    }
}
