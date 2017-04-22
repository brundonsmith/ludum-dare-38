using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
    this.GetComponent<Rigidbody>().centerOfMass = this.GetComponent<Transform>().position;

	}

	// Update is called once per frame
	void Update () {
    this.GetComponent<Rigidbody>().AddTorque(new Vector3(100, 0, 0));

    if(Input.GetKeyDown(KeyCode.LeftArrow)) {
    }
    if(Input.GetKeyDown(KeyCode.RightArrow)) {
    }

	}
}
