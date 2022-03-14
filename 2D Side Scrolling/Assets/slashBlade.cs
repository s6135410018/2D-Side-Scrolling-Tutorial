using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashBlade : MonoBehaviour
{
    public float speed = 6f;
    public float secondsDestroy = 0.15f;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (Time.time - startTime >= secondsDestroy)
        {
            Destroy(this.gameObject);
        }
    }
    
}
