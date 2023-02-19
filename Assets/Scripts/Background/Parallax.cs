using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float length, startpos;
    public GameObject cam;
    public float pallaxEffect;
    Camera mainCamera;

    void Start()
    {
        // start position of camera (x)
        startpos = transform.position.x;
        // length of image (x)
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - pallaxEffect));
        float distance = (cam.transform.position.x * pallaxEffect);

        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);

        if (temp > startpos + length - 6)
        {
            startpos += length;
        }
        else if (temp < startpos - length + 6)
        {
            startpos -= length;
        }
    }
}
