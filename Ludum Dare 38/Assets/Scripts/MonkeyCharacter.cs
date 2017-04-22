using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyCharacter : Character {

	// Use this for initialization
	void Start () {
    speed = 0;
    moveAcceleration = .001F;
  }

	// Update is called once per frame
	void Update () {
    speed += moveAcceleration;
    this.GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(speed, this.transform.right) * this.GetComponent<Rigidbody>().rotation;
  }
}
