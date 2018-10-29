using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson; 

public class Player : MonoBehaviour {

	private bool toggle = true;
	public Quaternion originalRot;
	Vector3 initPos;
	Quaternion initRot;
	public FirstPersonController fpc;
	private MouseLook m_MouseLook; 
	private Quaternion m_CharacterTargetRot;
	private Quaternion m_CameraTargetRot;

	// Use this for initialization
	void Start () {
		initPos = gameObject.transform.position;
		initRot = gameObject.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) {
			
			toggle = !toggle;
			gameObject.layer = toggle ? 0 : 9;
		}

		if (Input.GetKeyDown(KeyCode.Home)) {
			gameObject.transform.position = initPos;
			fpc.m_MouseLook.m_CharacterTargetRot = Quaternion.Euler(0f, 0f, 0f); 
			fpc.m_MouseLook.m_CameraTargetRot = Quaternion.Euler(0f, 0f, 0f);
		}

	}
}
