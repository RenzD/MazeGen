using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{

    public float dayAmbient = 1f;
    public float nightAmbient = .5f;
    // Use this for initialization
    void Start()
    {
        //incr = 0.005f;
    }

    // Update is called once per frame
    void Update()
    {

        Renderer rend = GetComponent<Renderer>();
        float ambientIntensity = rend.material.GetFloat("_AmbientLighIntensity");
		if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton11))
        {
            if (ambientIntensity == dayAmbient)
            {
                rend.material.SetFloat("_AmbientLighIntensity", nightAmbient);
            }
            else
            {
                rend.material.SetFloat("_AmbientLighIntensity", dayAmbient);
            }
        }
    }
}

