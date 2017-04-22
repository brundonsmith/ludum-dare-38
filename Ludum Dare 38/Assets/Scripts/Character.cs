using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

  public float moveAcceleration;
  public float moveResilience;
  public float turnSpeed;
  public float jumpHeight;

	// Use this for initialization
	void Start () {
    this.GetComponent<Rigidbody>().centerOfMass = this.GetComponent<Transform>().position;
	}

	// Update is called once per frame
	void Update () {
    this.GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(1, this.transform.right) * this.GetComponent<Rigidbody>().rotation;
	}

  public void TurnLeft() {
    this.GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(-1f, this.transform.up) * this.GetComponent<Rigidbody>().rotation;
  }

  public void TurnRight() {
    this.GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(1f, this.transform.up) * this.GetComponent<Rigidbody>().rotation;
  }
}
