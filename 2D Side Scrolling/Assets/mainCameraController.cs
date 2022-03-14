using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCameraController : MonoBehaviour
{
    public float dampTime = 0.15f;
    public Vector3 velocity = Vector2.zero;
    public Transform target;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 point = cam.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f,0.5f,point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }
}
