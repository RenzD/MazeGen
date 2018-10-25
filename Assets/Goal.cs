using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
	public GameObject p1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			Vector3 temp = new Vector3(0f,2.0f,0f);
			p1.transform.position = temp;
		}
	}
}
