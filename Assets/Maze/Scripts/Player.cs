using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private bool toggle = true;
    public Quaternion originalRot;
    Vector3 initPos;
    Quaternion initRot;
    public FirstPersonController fpc;
    private MouseLook m_MouseLook;
    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;
    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        initPos = gameObject.transform.position;
        initRot = gameObject.transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton12))
        {
            toggle = !toggle;
            gameObject.layer = toggle ? 0 : 9;
        }

        if (Input.GetKeyDown(KeyCode.Home) || Input.GetKeyDown(KeyCode.JoystickButton13))
        {
            gameObject.transform.position = initPos;

            fpc.m_MouseLook.m_CharacterTargetRot = Quaternion.Euler(0f, 0f, 0f);
            fpc.m_MouseLook.m_CameraTargetRot = Quaternion.Euler(0f, 0f, 0f);
            rb.velocity = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            //SceneManager.LoadScene (0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Eaten");
            gameObject.transform.position = initPos;
            fpc.m_MouseLook.m_CharacterTargetRot = Quaternion.Euler(0f, 0f, 0f);
            fpc.m_MouseLook.m_CameraTargetRot = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
