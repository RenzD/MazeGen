using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDayNight : MonoBehaviour
{
    Camera cam;
    Color skyBlue;

    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();
        skyBlue = cam.backgroundColor;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!cam.backgroundColor.Equals(Color.black))
            {
                cam.backgroundColor = Color.black;
            }
            else
            {
                cam.backgroundColor = skyBlue;
            }
        }
    }
}
