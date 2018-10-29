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
    const string depGrSh = "Custom/DepthGrayScale";
    //public Shader shader = null;
    // Use this for initialization

    public float fogBlend;
    private float fog;
    private Material material;
    private bool fogActive;
    private Color skyBlue;
    private Color fogColor;
    // Creates a private material used to the effect
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.depthTextureMode = DepthTextureMode.Depth;
        material = new Material(Shader.Find(custFogSh));
        fogActive = false;
        fog = 1000f;
        fogColor = Color.white;
        skyBlue = cam.backgroundColor;
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        Graphics.Blit(source, destination);

        material.SetColor("_fogColor", fogColor);
        material.SetFloat("_bwBlend", fog);
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
		if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            if (!fogActive)
            {
                fogActive = true;
                fog = fogBlend;
            }
            else
            {
                fogActive = false;
                fog = 1000f;
            }
        }
		if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            if (!cam.backgroundColor.Equals(Color.black))
            {
                cam.backgroundColor = Color.black;
                fogColor = Color.black;
            }
            else
            {
                cam.backgroundColor = skyBlue;
                fogColor = Color.white;
            }
        }
    }
}
