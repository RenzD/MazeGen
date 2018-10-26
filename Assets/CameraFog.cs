using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFog : MonoBehaviour
{

    Camera cam;

    public Shader shader;
    // Use this for initialization
    void Awake()
    {
        cam = GetComponent<Camera>();
        cam.RenderWithShader(shader, "FogCamera");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
