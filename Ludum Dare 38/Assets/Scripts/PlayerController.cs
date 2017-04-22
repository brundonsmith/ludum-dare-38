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
    this.GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(1, this.transform.right) * this.GetComponent<Rigidbody>().rotation;

    if(Input.GetKey(KeyCode.LeftArrow)) {
      this.GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(-1f, this.transform.up) * this.GetComponent<Rigidbody>().rotation;
    } else if(Input.GetKey(KeyCode.RightArrow)) {
      this.GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(1f, this.transform.up) * this.GetComponent<Rigidbody>().rotation;
    } else {
      //torque.z = -0.1f * this.GetComponent<Rigidbody>().angularVelocity.z;
    }
	}
}
