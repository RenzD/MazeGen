using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{

    Camera cam;
    private DeferredFogEffect fog;
    private Color skyBlue;
    private Color defaultSkyColor;
    private Color fogColor;
    void Start()
    {
        cam = GetComponent<Camera>();
        fog = GetComponent<DeferredFogEffect>();
        defaultSkyColor = cam.backgroundColor;
        fogColor = Color.gray;
        RenderSettings.fogColor = fogColor;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton11))
        {
            if (!cam.backgroundColor.Equals(Color.black))
            {
                cam.backgroundColor = Color.black;
                fogColor = new Color(.075f, .075f, .075f, 1);
                RenderSettings.fogColor = fogColor;
            }
            else
            {
                cam.backgroundColor = defaultSkyColor;
                fogColor = Color.gray;
                RenderSettings.fogColor = fogColor;
            }
        }
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.JoystickButton10))
        {
            if (!fog.enabled)
            {
                fog.enabled = true;
            }
            else
            {
                fog.enabled = false;
            }
        }
    }
}
