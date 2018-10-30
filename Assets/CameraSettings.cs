using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
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

    private Color skyBlue;
    private Color fogColor;
    // Creates a private material used to the effect
    void Start()
    {
        cam = GetComponent<Camera>();
        skyBlue = cam.backgroundColor;
        fogColor = skyBlue;
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
        
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton11))
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
