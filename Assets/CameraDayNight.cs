using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDayNight : MonoBehaviour
{
    Camera cam;
    Color skyBlue;
    const string dnSh = "Custom/CameraDayNightShader";

    public float dnBlend;
    private Material material;
    // Use this for initialization
    void Start()
    {

        cam = GetComponent<Camera>();
        material = new Material(Shader.Find(dnSh));

        skyBlue = cam.backgroundColor;
    }


    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        Graphics.Blit(source, destination);


        material.SetFloat("_bwBlend", dnBlend);
        Graphics.Blit(source, destination, material);
    }

    // Update is called once per frame
    void Update()
    {


    }
}
