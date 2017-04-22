using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
    if (this.GetComponent<Character>().controlStatus == ControlStatus.Normal) {

      if (Input.GetKey(KeyCode.LeftArrow)) {
        this.GetComponent<Character>().TurnLeft();
      } else if (Input.GetKey(KeyCode.RightArrow)) {
        this.GetComponent<Character>().TurnRight();
      } else if (Input.GetKeyDown(KeyCode.Space)) {
        this.GetComponent<Character>().Jump();
      }

    }
	}
}
