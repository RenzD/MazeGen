using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private bool toggle = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) {
			
			toggle = !toggle;
			gameObject.layer = toggle ? 0 : 9;
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Vector3 temp = new Vector3(0f,2.0f,0f);
			gameObject.transform.position = temp;
		}

	}
}
