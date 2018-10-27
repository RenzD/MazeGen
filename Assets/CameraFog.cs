using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFog : MonoBehaviour
{

    Camera cam;
    const string camFogSh = "Custom/CameraFogShader";
    const string custFogSh = "Custom/FogShader";
    const string renDepth = "Custom/RenderDepth";
    const string defFog = "Custom/DeferredFog";
    const string basic = "COMP7051 Shader Demo/PhongWithSpecular";
    const string whiteSh = "Custom/WhiteShader";
    const string bwSh = "Custom/BWShader";
    //public Shader shader = null;
    // Use this for initialization

    public float intensity;
    private Material material;

    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(Shader.Find(custFogSh));
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (intensity == 0)
        {
            Graphics.Blit(source, destination);
            return;
        }

        material.SetFloat("_bwBlend", intensity);
        Graphics.Blit(source, destination, material);
    }


    /*
    void Start()
    {
    cam = GetComponent<Camera>();
    Camera.main.SetReplacementShader(null, "Opaque");

    Camera.main.SetReplacementShader(Shader.Find(whiteSh), "Overlay");
    }
    */
    // Update is called once per frame
    void Update()
    {

    }
}
